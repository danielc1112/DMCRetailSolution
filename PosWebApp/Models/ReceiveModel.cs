using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Entity.Entities;
using DataAccess;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace PosWebApp.Models
{
    //The product description is shown in the view's grid here, but isn't part of the database
    public class GrnlineView : Grnline
    {
        public string Description { get; set; }
    }

    public class ReceiveModel
    {
        private RetailDbContext db = new RetailDbContext();

        public List<SelectListItem> prodSelectList = new List<SelectListItem>();
        public string SelectedProduct;
        public string Quantity = "1";
        public List<SelectListItem> supplierSelectList = new List<SelectListItem>();
        public string SelectedSupplier;
        public List<Grn> grns = new List<Grn>();
        public List<GrnlineView> grnGrnlines = new List<GrnlineView>();

        public void LoadProducts()
        {
            foreach (Product prod in db.Products.ToList())
            {
                prodSelectList.Add(new SelectListItem(){ Value = prod.ProductID.ToString(), Text = prod.Description});
            }
        }

        public void LoadSuppliers()
        {
            foreach (Supplier supplier in db.Suppliers.ToList())
            {
                supplierSelectList.Add(new SelectListItem()
                {
                    Value = supplier.SupplierID.ToString(),
                    Text = supplier.Description
                });
            }
        }

        public void LoadGrns(int? GrnID)
        {
            if (GrnID == null)
                grns.AddRange(db.Grns.ToList());
            else
                grns.AddRange(db.Grns.Where(x => x.GrnID == GrnID).ToList());
        }

        public GrnlineView AddGrnline(string ProductId, string Quantity)
        {
            GrnlineView gl = new GrnlineView();
            gl.ProductID = Convert.ToInt32(ProductId);
            gl.Quantity = Convert.ToInt32(Quantity);
            Product prod = db.Products.ToList().Single<Product>(x => x.ProductID == gl.ProductID);
            gl.Description = prod.Description;
            gl.Cost = prod.Cost;
            gl.LineAmount = gl.Quantity * gl.Cost;
            return gl;
        }

        public void FinaliseGrn(Grn g)
        {
            g.TransactionTime = DateTime.Now;            
            DBRepository.SaveGrn(g);

            //Replicate to HODB using WCF service in a TPL task
            UploadGrn(g);
        }

        private void UploadGrn(Grn g)
        {
            Task taskA = Task.Factory.StartNew(() =>
            {
                PosWebService.PosWebServiceClient client = new PosWebService.PosWebServiceClient();
                client.UploadGrn(g);
            });
        }
        
    }
}