using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PosWebApp.Models;

namespace PosWebApp.Controllers
{
    public class GrnDetailsController : Controller
    {
        public ReceiveModel rm = new ReceiveModel();

        public ActionResult Index(int? id)
        {
            rm.LoadGrns(id);
            return View(rm);
        }
    }
}