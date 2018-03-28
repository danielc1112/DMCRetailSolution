using DataAccess;
using DataAccess.Entity.Entities;
using System.Collections.Generic;
using System.Linq;

namespace PosWebApp.Models
{
    public class CustomersModel
    {
        private RetailDbContext db = new RetailDbContext();

        public List<Customer> LoadCustomers(string custName = "")
        {
            if (custName == "")
            {
                return (from p in db.Customers
                        select p)
                        .OrderBy(p => p.LastName).ToList();
            }
            else
            {
                return (from p in db.Customers
                        where (p.FirstName.Contains(custName)) || (p.LastName.Contains(custName))
                        select p)
                        .OrderBy(p => p.LastName).ToList();
            }
        }

        public void UpdateCustomers()
        {
            PosWebService.PosWebServiceClient client = new PosWebService.PosWebServiceClient();
            Customer[] Customers = client.GetCustomers();
            db.Database.ExecuteSqlCommand("delete from Customers");
            db.Customers.AddRange(Customers);
            db.SaveChanges();
        }
    }
}