using System.Collections.Generic;
using System.Web.Mvc;
using DataAccess.Entity.Entities;
using PosWebApp.Models;

namespace PosWebApp.Controllers
{
    public class ReceiveController : Controller
    {
        public ReceiveModel rm = new ReceiveModel();

        public ActionResult Index()
        {
            rm.LoadProducts();
            rm.LoadSuppliers();
            return View(rm);
        }

        [HttpPost]
        public ActionResult AddGrnline(string productID, string quantity)
        {
            JsonResult result = Json(rm.AddGrnline(productID, quantity));
            return result;
        }

        [HttpPost]
        public ActionResult FinaliseGrn(Grn grn)
        {
            rm.FinaliseGrn(grn);
            return View("Index", rm);
        }
    }
}