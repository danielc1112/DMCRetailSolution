using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Entity.Entities;
using DataAccess;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace PosWebApp.Models
{
    //The product description is shown in the view's grid here, but isn't part of the stocktakeline
    public class StocktakelineView : Stocktakeline
    {
        public string Description { get; set; }
    }

    public class StocktakeModel
    {
        private RetailDbContext db = new RetailDbContext();

        public List<SelectListItem> prodSelectList = new List<SelectListItem>();
        public string SelectedProduct;
        public string Quantity = "0";
        public List<Stocktake> stocktakes = new List<Stocktake>();
        public List<StocktakelineView> stocktakeStocktakelines = new List<StocktakelineView>();

        public void LoadProducts()
        {
            foreach (Product prod in db.Products.ToList())
            {
                prodSelectList.Add(new SelectListItem() { Value = prod.ProductID.ToString(), Text = prod.Description });
            }
        }

        public void LoadStocktakes(int? StocktakeID)
        {
            if (StocktakeID == null)
                stocktakes.AddRange(db.Stocktakes.ToList());
            else
                stocktakes.AddRange(db.Stocktakes.Where(x => x.StocktakeID == StocktakeID).ToList());
        }

        public StocktakelineView AddStocktakeline(string ProductId, string Quantity)
        {
            StocktakelineView stl = new StocktakelineView();
            stl.ProductID = Convert.ToInt32(ProductId);
            stl.CountedQty = Convert.ToInt32(Quantity);
            Product prod = db.Products.ToList().Single<Product>(x => x.ProductID == stl.ProductID);
            stl.Description = prod.Description;
            return stl;
        }

        public void FinaliseStocktake(Stocktake st)
        {
            st.TransactionTime = DateTime.Now;
            DBRepository.SaveStocktake(st);

            //Replicate to HODB using WCF service in a TPL task
            UploadStocktake(st);
        }

        private void UploadStocktake(Stocktake st)
        {
            Task taskA = Task.Factory.StartNew(() =>
            {
                PosWebService.PosWebServiceClient client = new PosWebService.PosWebServiceClient();
                client.UploadStocktake(st);
            });
        }

    }
}