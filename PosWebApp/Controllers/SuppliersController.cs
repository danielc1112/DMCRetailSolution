using System.Collections.Generic;
using System.Web.Mvc;
using PosWebApp.Models;
using DataAccess.Entity.Entities;

namespace PosWebApp.Controllers
{
    public class SuppliersController : Controller
    {
        public SuppliersModel sm = new SuppliersModel();

        public ActionResult Index()
        {
            sm.LoadSuppliers();
            return View(sm);
        }

        [HttpPost]
        public JsonResult SearchSuppliers(string suppName)
        {
            List<Supplier> Suppliers = sm.LoadSuppliers(suppName);
            return Json(Suppliers);
        }

        [HttpPost]
        public JsonResult UpdateSuppliers(string suppName)
        {
            sm.UpdateSuppliers();

            List<Supplier> Suppliers = sm.LoadSuppliers(suppName);
            return Json(Suppliers);
        }
    }
}