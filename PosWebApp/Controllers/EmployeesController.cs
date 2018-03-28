using System.Collections.Generic;
using System.Web.Mvc;
using PosWebApp.Models;
using DataAccess.Entity.Entities;

namespace PosWebApp.Controllers
{
    public class EmployeesController : Controller
    {
        public EmployeesModel em = new EmployeesModel();

        public ActionResult Index()
        {
            em.LoadEmployees();
            return View(em);
        }

        [HttpPost]
        public JsonResult SearchEmployees(string empName)
        {
            List<Employee> Employees = em.LoadEmployees(empName);
            return Json(Employees);
        }

        [HttpPost]
        public JsonResult UpdateEmployees(string empName)
        {
            em.UpdateEmployees();

            List<Employee> Employees = em.LoadEmployees(empName);
            return Json(Employees);
        }
    }
}