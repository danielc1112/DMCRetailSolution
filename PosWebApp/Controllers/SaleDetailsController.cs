using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PosWebApp.Models;

namespace PosWebApp.Controllers
{
    public class SaleDetailsController : Controller
    {
        public SaleModel sm = new SaleModel();

        public ActionResult Index(int? id)
        {
            sm.LoadSales(id);
            return View(sm);
        }
    }
}