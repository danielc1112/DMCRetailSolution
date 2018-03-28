using System.Collections.Generic;
using System.Web.Mvc;
using PosWebApp.Models;
using DataAccess.Entity.Entities;

namespace PosWebApp.Controllers
{
    public class CustomersController : Controller
    {
        public CustomersModel cm = new CustomersModel();

        public ActionResult Index()
        {
            cm.LoadCustomers();
            return View(cm);
        }

        [HttpPost]
        public JsonResult SearchCustomers(string custName)
        {
            List<Customer> customers = cm.LoadCustomers(custName);
            return Json(customers);
        }

        [HttpPost]
        public JsonResult UpdateCustomers(string custName)
        {
            cm.UpdateCustomers();

            List<Customer> customers = cm.LoadCustomers(custName);
            return Json(customers);
        }
    }
}