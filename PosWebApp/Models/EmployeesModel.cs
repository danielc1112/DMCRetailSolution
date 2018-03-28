using DataAccess;
using DataAccess.Entity.Entities;
using System.Collections.Generic;
using System.Linq;

namespace PosWebApp.Models
{
    public class EmployeesModel
    {
        private RetailDbContext db = new RetailDbContext();

        public List<Employee> LoadEmployees(string empName = "")
        {
            if (empName == "")
            {
                return (from p in db.Employees
                        select p)
                        .OrderBy(p => p.LastName).ToList();
            }
            else
            {
                return (from p in db.Employees
                        where (p.FirstName.Contains(empName)) || (p.LastName.Contains(empName))
                        select p)
                        .OrderBy(p => p.LastName).ToList();
            }
        }

        public void UpdateEmployees()
        {
            PosWebService.PosWebServiceClient client = new PosWebService.PosWebServiceClient();
            Employee[] Employees = client.GetEmployees();
            db.Database.ExecuteSqlCommand("delete from Employees");
            db.Employees.AddRange(Employees);
            db.SaveChanges();
        }
    }
}