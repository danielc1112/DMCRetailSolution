using System;
using System.Collections.Generic;
using System.ServiceModel;
using DataAccess.Entity.Entities;

namespace PosWebService
{
    [ServiceContract]
    public interface IPosWebService
    {
        [OperationContract]
        List<Product> GetProducts();
        [OperationContract]
        List<Customer> GetCustomers();
        [OperationContract]
        List<Employee> GetEmployees();
        [OperationContract]
        List<Supplier> GetSuppliers();

        [OperationContract]
        void UploadSale(Sale sale);
        [OperationContract]
        void UploadGrn(Grn grn);
        [OperationContract]
        void UploadStocktake(Stocktake stocktake);
    }
}
