using System;
using System.Collections.Generic;
using System.Web.Mvc;
using DataAccess.Entity.Entities;
using PosWebApp.Models;
using DataAccess;

namespace PosWebApp.Controllers
{
    public class SellController : Controller
    {
        public SaleModel sm = new SaleModel();

        public ActionResult Index()
        {
            sm.LoadProducts();
            sm.LoadCustomers();
            sm.LoadTenderTypes();
            return View(sm);
        }

        [HttpPost]
        public JsonResult AddSaleline(string productID, string quantity)
        {
            //This will return a SalelineView in Json format
            JsonResult result = Json(sm.AddSaleline(productID, quantity));
            return result;
        }

        [HttpPost]
        public JsonResult AddTenderline(string tendertypeID, string amount, string saleTotal, string paidTotal)
        {
            //This will return List<TenderlineView> in Json format and updates the model
            JsonResult result = Json(sm.AddTenderline(tendertypeID, amount, saleTotal, paidTotal));
            return result;
        }

        [HttpPost]
        public ActionResult FinaliseSale(Sale sale)
        {
            sm.FinaliseSale(sale);
            return View("Index", sm);
        }

        [HttpPost]
        public JsonResult CanTenderTypeGiveChange(string tendertypeID)
        {
            int tendertypeIDi = Convert.ToInt32(tendertypeID);
            return sm.CanTenderTypeGiveChange(tendertypeIDi) ? Json(new { success = true }, JsonRequestBehavior.AllowGet)
                                                             : Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult LoginUser(FormCollection formCollection)
        {
            UserLoginResult ulr = sm.LoginUser(formCollection["usrname"], formCollection["psw"]);
            if (ulr.ulResult == ULResult.Valid)
            {
                Session["UserID"] = ulr.UserID;
                Session["EmployeeID"] = ulr.EmployeeID;
            }
            JsonResult result = Json(ulr);
            return result;
        }
    }
}