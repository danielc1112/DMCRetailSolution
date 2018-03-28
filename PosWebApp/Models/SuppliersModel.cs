using DataAccess;
using DataAccess.Entity.Entities;
using System.Collections.Generic;
using System.Linq;

namespace PosWebApp.Models
{
    public class SuppliersModel
    {
        private RetailDbContext db = new RetailDbContext();

        public List<Supplier> LoadSuppliers(string suppName = "")
        {
            if (suppName == "")
            {
                return (from p in db.Suppliers
                        select p)
                        .OrderBy(p => p.Description).ToList();
            }
            else
            {
                return (from p in db.Suppliers
                        where (p.Description.Contains(suppName))
                        select p)
                        .OrderBy(p => p.Description).ToList();
            }
        }

        public void UpdateSuppliers()
        {
            PosWebService.PosWebServiceClient client = new PosWebService.PosWebServiceClient();
            Supplier[] Suppliers = client.GetSuppliers();
            db.Database.ExecuteSqlCommand("delete from Suppliers");
            db.Suppliers.AddRange(Suppliers);
            db.SaveChanges();
        }
    }
}