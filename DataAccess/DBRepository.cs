using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entity.Entities;

namespace DataAccess
{
    //Database code we intend to share between HO and POS
    public class DBRepository
    {
        public static List<Sale> GetSales()
        {
            RetailDbContext retailDbContext = new RetailDbContext();
            return retailDbContext.Sales.Include("Salelines").ToList();
        }

        public static List<Product> GetProducts()
        {
            RetailDbContext retailDbContext = new RetailDbContext();
            return retailDbContext.Products.ToList();
        }

        public static List<Customer> GetCustomers()
        {
            RetailDbContext retailDbContext = new RetailDbContext();
            return retailDbContext.Customers.ToList();
        }

        public static List<Employee> GetEmployees()
        {
            RetailDbContext retailDbContext = new RetailDbContext();
            return retailDbContext.Employees.ToList();
        }

        public static List<Supplier> GetSuppliers()
        {
            RetailDbContext retailDbContext = new RetailDbContext();
            return retailDbContext.Suppliers.ToList();
        }

        public static void SaveSale(Sale sale)
        {
            using (RetailDbContext retailDbContext = new RetailDbContext())
            {
                retailDbContext.Sales.Add(sale);
                retailDbContext.SaveChanges(); //This will give us a unique SaleID

                //QtyOnHand change
                foreach (Saleline sl in sale.Salelines)
                {
                    StoreProduct storeProduct = retailDbContext.StoreProducts.ToList().SingleOrDefault<StoreProduct>(x => (x.ProductID == sl.ProductID) && (x.StoreID == sale.StoreID));
                    if (storeProduct == null)
                    {
                        storeProduct = new StoreProduct();
                        storeProduct.ProductID = sl.ProductID;
                        storeProduct.StoreID = sale.StoreID;
                        storeProduct.QtyOnHand -= sl.Quantity;
                        retailDbContext.StoreProducts.Add(storeProduct);
                    }
                    else
                        storeProduct.QtyOnHand -= sl.Quantity;

                    StoreProductTran storeProductTran = new StoreProductTran();
                    storeProductTran.ProductID = storeProduct.ProductID;
                    storeProductTran.DocumentType = "S";
                    storeProductTran.DocumentID = sale.Id;
                    storeProductTran.Quantity = sl.Quantity;
                    storeProductTran.EffectiveTime = sale.TransactionTime;
                    retailDbContext.StoreProductTrans.Add(storeProductTran);
                }
                retailDbContext.SaveChanges();
            }
        }

        public static void SaveGrn(Grn grn)
        {
            using (RetailDbContext retailDbContext = new RetailDbContext())
            {
                retailDbContext.Grns.Add(grn);
                retailDbContext.SaveChanges(); //This will give us a unique GrnID

                //QtyOnHand change
                foreach (Grnline gl in grn.Grnlines)
                {
                    StoreProduct storeProduct = retailDbContext.StoreProducts.ToList().SingleOrDefault<StoreProduct>(x => (x.ProductID == gl.ProductID) && (x.StoreID == grn.StoreID));
                    if (storeProduct == null)
                    {
                        storeProduct = new StoreProduct();
                        storeProduct.ProductID = gl.ProductID;
                        storeProduct.StoreID = grn.StoreID;
                        storeProduct.QtyOnHand += gl.Quantity;
                        retailDbContext.StoreProducts.Add(storeProduct);
                    }
                    else
                        storeProduct.QtyOnHand += gl.Quantity;

                    StoreProductTran storeProductTran = new StoreProductTran();
                    storeProductTran.ProductID = storeProduct.ProductID;
                    storeProductTran.DocumentType = "G";
                    storeProductTran.DocumentID = grn.Id;
                    storeProductTran.Quantity = gl.Quantity;
                    storeProductTran.EffectiveTime = grn.TransactionTime;
                    retailDbContext.StoreProductTrans.Add(storeProductTran);
                }
                retailDbContext.SaveChanges();
            }
        }

        public static void SaveStocktake(Stocktake st)
        {
            using (RetailDbContext retailDbContext = new RetailDbContext())
            {
                retailDbContext.Stocktakes.Add(st);
                retailDbContext.SaveChanges(); //This will give us a unique StocktakeID

                //QtyOnHand change
                foreach (Stocktakeline stl in st.Stocktakelines)
                {
                    int QtyBefore = 0;
                    StoreProduct storeProduct = retailDbContext.StoreProducts.ToList().SingleOrDefault<StoreProduct>(x => (x.ProductID == stl.ProductID) && (x.StoreID == st.StoreID));
                    if (storeProduct == null)
                    {
                        storeProduct = new StoreProduct();
                        storeProduct.ProductID = stl.ProductID;
                        storeProduct.StoreID = st.StoreID;
                        storeProduct.QtyOnHand = stl.CountedQty;
                        retailDbContext.StoreProducts.Add(storeProduct);
                    }
                    else {
                        QtyBefore = storeProduct.QtyOnHand;
                        storeProduct.QtyOnHand = stl.CountedQty;                       
                    }

                    //QtyOnHand change history
                    StoreProductTran storeProductTran = new StoreProductTran();
                    storeProductTran.ProductID = storeProduct.ProductID;
                    storeProductTran.DocumentType = "A";
                    storeProductTran.DocumentID = st.Id;
                    storeProductTran.Quantity = stl.CountedQty - QtyBefore;
                    storeProductTran.EffectiveTime = st.TransactionTime;
                    retailDbContext.StoreProductTrans.Add(storeProductTran);
                }
                retailDbContext.SaveChanges();
            }
        }

        public static void SaveProduct(Product product)
        {
            using (RetailDbContext retailDbContext = new RetailDbContext())
            {
                retailDbContext.Products.Add(product);
                retailDbContext.SaveChanges();
            }
        }

    }
}
