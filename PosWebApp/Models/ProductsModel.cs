using DataAccess;
using DataAccess.Entity.Entities;
using System.Collections.Generic;
using System.Linq;

namespace PosWebApp.Models
{
    public class ProductsModel
    {
        private RetailDbContext db = new RetailDbContext();

        public class ProductView : Product
        {
            public int QtyOnHand { get; set; }
        }

        public List<Product> LoadProducts(string prodName = "")
        {
            List<Product> prods;
            if (prodName == "")
            {
                prods = (from p in db.Products
                        select p)
                        .OrderBy(p => p.Description).ToList();
            }
            else
            {
                prods = (from p in db.Products
                        where p.Description.Contains(prodName)
                        select p)
                        .OrderBy(p => p.Description).ToList();
            }

            foreach(Product prod in prods)
            {
                StoreProduct sp = (from s in db.StoreProducts
                                   where (s.StoreID == 1) && (s.ProductID == prod.ProductID)
                                   select s).SingleOrDefault();

                prod.StoreProduct = sp;
            }

            return prods;
        }

        public void UpdateProducts()
        {
            PosWebService.PosWebServiceClient client = new PosWebService.PosWebServiceClient();
            Product[] products = client.GetProducts();
            db.Database.ExecuteSqlCommand("delete from Products");
            db.Products.AddRange(products);
            db.SaveChanges();
        }
    }
}