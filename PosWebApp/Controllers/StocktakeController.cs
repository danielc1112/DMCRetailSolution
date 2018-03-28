using System.Collections.Generic;
using System.Web.Mvc;
using DataAccess.Entity.Entities;
using PosWebApp.Models;

namespace PosWebApp.Controllers
{
    public class StocktakeController : Controller
    {
        public StocktakeModel stm = new StocktakeModel();

        public ActionResult Index()
        {
            stm.LoadProducts();
            return View(stm);
        }

        [HttpPost]
        public ActionResult AddStocktakeline(string productID, string quantity)
        {
            JsonResult result = Json(stm.AddStocktakeline(productID, quantity));
            return result;
        }

        [HttpPost]
        public ActionResult FinaliseStocktake(Stocktake st)
        {
            stm.FinaliseStocktake(st);
            return View("Index", stm);
        }
    }
}