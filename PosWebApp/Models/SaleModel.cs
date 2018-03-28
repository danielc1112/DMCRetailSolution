using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Entity.Entities;
using DataAccess;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Globalization;

namespace PosWebApp.Models
{
    //The product description is shown in the view's grid here, but isn't part of the saleline table
    public class SalelineView : Saleline
    {
        public string Description { get; set; }
    }
    //The tendertype description is shown in the view's grid here, but isn't part of the Tenderline table
    public class TenderlineView : Tenderline
    {
        public string Description { get; set; }
    }

    public class SaleModel
    {
        private RetailDbContext db = new RetailDbContext();

        public List<SelectListItem> prodSelectList = new List<SelectListItem>();        
        public string SelectedProduct;
        public List<SelectListItem> customerSelectList = new List<SelectListItem>();
        public string SelectedCustomer;
        public List<SelectListItem> tenderTypeSelectList = new List<SelectListItem>();
        public string SelectedTenderType;
        public string Quantity = "1";
        public string TenderValue = "0";
        //Sale enquiry
        public List<Sale> sales = new List<Sale>();
        public List<SalelineView> saleSalelines = new List<SalelineView>();
        public List<TenderlineView> saleTenderlines = new List<TenderlineView>();

        public void LoadProducts()
        {
            foreach (Product prod in db.Products.ToList())
            {
                prodSelectList.Add(new SelectListItem()
                {
                    Value = prod.ProductID.ToString(),
                    Text = prod.Description
                });
            }
        }

        public void LoadCustomers()
        {
            foreach (Customer customer in db.Customers.ToList())
            {
                customerSelectList.Add(new SelectListItem()
                {
                    Value = customer.CustomerID.ToString(),
                    Text = customer.DisplayName
                });
            }
        }

        public void LoadTenderTypes()
        {
            foreach (TenderType tenderType in db.TenderTypes.ToList())
            {
                tenderTypeSelectList.Add(new SelectListItem()
                {
                    Value = tenderType.TenderTypeID.ToString(),
                    Text = tenderType.Description
                });
            }
        }

        public void LoadSales(int? SaleID)
        {
            if(SaleID == null)
               sales.AddRange(db.Sales.ToList());
            else
               sales.AddRange(db.Sales.Where(x => x.SaleID == SaleID).ToList());
        }

        public SalelineView AddSaleline(string ProductId, string Quantity)
        {
            SalelineView sl = new SalelineView();
            sl.ProductID = Convert.ToInt32(ProductId);
            sl.Quantity = Convert.ToInt32(Quantity);
            Product prod = db.Products.ToList().Single<Product>(x => x.ProductID == sl.ProductID);
            sl.Description = prod.Description;
            sl.Price = prod.Price;
            sl.EffPrice = prod.Price;
            sl.LineAmount = sl.Quantity * sl.Price;
            sl.Status = "A";
            sl.GstRate = 10;
            sl.GstAmount = (sl.GstRate / 100) * sl.LineAmount;
            return sl;
        }

        public bool CanTenderTypeGiveChange(int tenderTypeID)
        {
            if (tenderTypeID == 0) return true;
            TenderType tenderType = db.TenderTypes.ToList().Single<TenderType>(x => x.TenderTypeID == tenderTypeID);
            return tenderType.ChangeGiven;
        }

        //called by an ajax function
        public TenderlineView AddTenderline(string TenderTypeId, string TenderValue, string saleTotal, string paidTotal)
        {
            float saleTotali = float.Parse(saleTotal, CultureInfo.InvariantCulture.NumberFormat);
            float paidTotali = float.Parse(paidTotal, CultureInfo.InvariantCulture.NumberFormat);

            TenderlineView tl = new TenderlineView();
            tl.TenderTypeID = Convert.ToInt32(TenderTypeId);
            tl.Status = "A";

            TenderType tenderType = db.TenderTypes.ToList().Single<TenderType>(x => x.TenderTypeID == tl.TenderTypeID);
            tl.Description = tenderType.Description;
            tl.TenderValue = float.Parse(TenderValue, CultureInfo.InvariantCulture.NumberFormat);

            if (paidTotali + tl.TenderValue > saleTotali)
            {
                if (tenderType.ChangeGiven) //e.g. cash
                {
                    //If the paid total goes over the sale total, then calculate change given
                    tl.ChangeGiven = paidTotali + tl.TenderValue - saleTotali;
                }
            }

            return tl;
        }

        public void FinaliseSale(Sale s)
        {
            s.TransactionTime = DateTime.Now;
            DBRepository.SaveSale(s);

            //Replicate to HODB using WCF service in a TPL task
            UploadSale(s);
        }

        private void UploadSale(Sale s)
        {
            Task taskA = Task.Factory.StartNew(() =>
            {
                PosWebService.PosWebServiceClient client = new PosWebService.PosWebServiceClient();
                client.UploadSale(s);
            });
        }

        public UserLoginResult LoginUser(string userCode, string password)
        {
            UserLoginResult ulr = new UserLoginResult();
            ulr.LoginUser(userCode, password);
            return ulr;
        }
    }
}