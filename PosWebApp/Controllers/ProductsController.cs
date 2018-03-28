using System.Collections.Generic;
using System.Web.Mvc;
using PosWebApp.Models;
using DataAccess.Entity.Entities;

namespace PosWebApp.Controllers
{
    public class ProductsController : Controller
    {
        public ProductsModel pm = new ProductsModel();

        public ActionResult Index()
        {
            pm.LoadProducts();
            return View(pm);
        }

        [HttpPost]
        public JsonResult SearchProducts(string prodName)
        {
            List<Product> prods = pm.LoadProducts(prodName);
            return Json(prods);
        }

        [HttpPost]
        public JsonResult UpdateProducts(string prodName)
        {
            pm.UpdateProducts();

            List<Product> prods = pm.LoadProducts(prodName);
            return Json(prods);
        }
    }
}