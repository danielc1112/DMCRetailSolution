using System.Collections.Generic;
using DataAccess;
using DataAccess.Entity.Entities;

namespace PosWebService
{
    public class PosWebService : IPosWebService
    {
        public List<Product> GetProducts()
        {
            return DBRepository.GetProducts();
        }

        public List<Customer> GetCustomers()
        {
            return DBRepository.GetCustomers();
        }

        public List<Employee> GetEmployees()
        {
            return DBRepository.GetEmployees();
        }

        public List<Supplier> GetSuppliers()
        {
            return DBRepository.GetSuppliers();
        }


        public void UploadSale(Sale sale)
        {
            DBRepository.SaveSale(sale);
        }

        public void UploadGrn(Grn grn)
        {
            DBRepository.SaveGrn(grn);
        }

        public void UploadStocktake(Stocktake stocktake)
        {
            DBRepository.SaveStocktake(stocktake);
        }
    }
}
