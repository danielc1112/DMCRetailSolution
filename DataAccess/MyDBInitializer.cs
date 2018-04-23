using System;
using System.Collections.Generic;
using DataAccess.Entity.Entities;
using System.Data.Entity;

namespace DataAccess
{
    public class MyDBInitializer : CreateDatabaseIfNotExists<RetailDbContext>
    {
        protected override void Seed(RetailDbContext context)
        {
            context.Addresses.AddRange(new [] {
                new Address() { Id = 1, AddressLine1 = "1 Smith St", AddressLine2 = "", Suburb = "Northcote", Postcode = "3070", State = "VIC", Iso3CountryCode = "AUS" },
                new Address() { Id = 2, AddressLine1 = "2 Smith St", AddressLine2 = "", Suburb = "Northcote", Postcode = "3070", State = "VIC", Iso3CountryCode = "AUS" },
                new Address() { Id = 3, AddressLine1 = "3 Smith St", AddressLine2 = "", Suburb = "Northcote", Postcode = "3070", State = "VIC", Iso3CountryCode = "AUS" },
                new Address() { Id = 4, AddressLine1 = "4 Smith St", AddressLine2 = "", Suburb = "Northcote", Postcode = "3070", State = "VIC", Iso3CountryCode = "AUS" },
                new Address() { Id = 5, AddressLine1 = "5 Smith St", AddressLine2 = "", Suburb = "Northcote", Postcode = "3070", State = "VIC", Iso3CountryCode = "AUS" },
                new Address() { Id = 6, AddressLine1 = "6 Smith St", AddressLine2 = "", Suburb = "Northcote", Postcode = "3070", State = "VIC", Iso3CountryCode = "AUS" },
                new Address() { Id = 7, AddressLine1 = "7 Smith St", AddressLine2 = "", Suburb = "Northcote", Postcode = "3070", State = "VIC", Iso3CountryCode = "AUS" },
                new Address() { Id = 8, AddressLine1 = "8 Smith St", AddressLine2 = "", Suburb = "Northcote", Postcode = "3070", State = "VIC", Iso3CountryCode = "AUS" }
            });

            context.Suppliers.AddRange(new [] {
                new Supplier() { Id = 1, Description = "Supplier 1", AddressID = 3 },
                new Supplier() { Id = 2, Description = "Supplier 2", AddressID = 4 }
            });

            //3 products
            IList<Product> defaultProducts = new List<Product>();

            defaultProducts.Add(new Product() { Id = 1, Description = "red sandles", Price = 12.5f, Cost = 6.25f, SupplierID =1 });
            defaultProducts.Add(new Product() { Id = 2, Description = "blue shirt", Price = 8f, Cost = 4f, SupplierID = 1 });
            defaultProducts.Add(new Product() { Id = 3, Description = "blue sandles", Price = 30f, Cost = 15f, SupplierID = 1 });

            foreach (Product std in defaultProducts)
                context.Products.Add(std);            

            //Bank Accounts
            IList<BankAccount> defaultBankAccounts = new List<BankAccount>();

            defaultBankAccounts.Add(new BankAccount() { Id = 1, BankName = "Test Account 1", Bsb = "123456", AccountNo = "123456789", AccountName = "Main account" });
            defaultBankAccounts.Add(new BankAccount() { Id = 1, BankName = "Test Account 1", Bsb = "654321", AccountNo = "987654321", AccountName = "Paypal account" });

            foreach (BankAccount std in defaultBankAccounts)
                context.BankAccounts.Add(std);            

            //Stores
            IList<Store> defaultStores = new List<Store>();

            defaultStores.Add(new Store() { Id = 1, Description = "Store 1", Active = true, AddressID = 1 });
            defaultStores.Add(new Store() { Id = 2, Description = "Store 2", Active = true, AddressID = 2 });

            foreach (Store std in defaultStores)
                context.Stores.Add(std);

            //Registers
            IList<Register> defaultRegisters = new List<Register>();

            defaultRegisters.Add(new Register() { Id = 1, StoreID = 1, RegisterStatus = "A", PrinterID = "1", EFTType = "1", EftEnabled = true });
            defaultRegisters.Add(new Register() { Id = 2, StoreID = 1, RegisterStatus = "A", PrinterID = "1", EFTType = "2", EftEnabled = true });
            defaultRegisters.Add(new Register() { Id = 3, StoreID = 2, RegisterStatus = "A", PrinterID = "1", EFTType = "1", EftEnabled = true });
            defaultRegisters.Add(new Register() { Id = 4, StoreID = 2, RegisterStatus = "A", PrinterID = "1", EFTType = "2", EftEnabled = true });

            foreach (Register std in defaultRegisters)
                context.Registers.Add(std);            

            //Customers
            IList<Customer> defaultCustomers = new List<Customer>();

            defaultCustomers.Add(new Customer() { Id = 1, CustomerType = "P", Status = "A", Title = "Mr", FirstName = "John", LastName = "Smith", Phone = "123456789", Mobile = "0123456789", Email = "jsmith@gmail.com", AddressID = 5 });
            defaultCustomers.Add(new Customer() { Id = 2, CustomerType = "P", Status = "A", Title = "Ms", FirstName = "Samantha", LastName = "Jones", Phone = "987654321", Mobile = "0987654321", Email = "sjones@gmail.com", AddressID = 6 });

            foreach (Customer std in defaultCustomers)
                context.Customers.Add(std);

            //Users
            IList<User> defaultUsers = new List<User>();

            defaultUsers.Add(new User() { Id = 1, UserCode = "user1", Password = "password1", Active = true });
            defaultUsers.Add(new User() { Id = 2, UserCode = "user2", Password = "password2", Active = true });

            foreach (User std in defaultUsers)
                context.Users.Add(std);

            //Employees
            IList<Employee> defaultEmployees = new List<Employee>();

            defaultEmployees.Add(new Employee() { Id = 1, Status = "A", TaxFileNumber = "123456789", UserID = 1, EmployeeTitle = "Manager", FirstName = "Roger", LastName = "Kennedy", Phone = "123456789", Mobile = "0123456789", Email = "rkennedy@gmail.com", AddressID = 7 });
            defaultEmployees.Add(new Employee() { Id = 2, Status = "A", TaxFileNumber = "987654321", UserID = 2, EmployeeTitle = "Casual", FirstName = "Jason", LastName = "Cobain", Phone = "987654321", Mobile = "0987654321", Email = "jcobain@gmail.com", AddressID = 8 });

            foreach (Employee std in defaultEmployees)
                context.Employees.Add(std);

            //Printers
            IList<Printer> defaultPrinters = new List<Printer>();

            defaultPrinters.Add(new Printer() { Id = 1, Description = "Epson", Active = true, ReceiptPrintWidth = 80, SupportsGraphics = true, SupportsBarcodes = false });
            defaultPrinters.Add(new Printer() { Id = 2, Description = "Star", Active = true, ReceiptPrintWidth = 80, SupportsGraphics = true, SupportsBarcodes = false });

            foreach (Printer std in defaultPrinters)
                context.Printers.Add(std);

            //TenderTypes
            IList<TenderType> defaultTenderTypes = new List<TenderType>();

            defaultTenderTypes.Add(new TenderType() { Id = 1, Description = "Cash", Active = true, DisplaySequence = 1, ChangeGiven = true });
            defaultTenderTypes.Add(new TenderType() { Id = 2, Description = "EFT", Active = true, DisplaySequence = 2, ChangeGiven = false });

            foreach (TenderType std in defaultTenderTypes)
                context.TenderTypes.Add(std);

            //2 sales
            IList<Sale> defaultSales = new List<Sale>();

            defaultSales.Add(new Sale() { StoreID = 1, RegisterID = 1, NetAmount = 28.5f, SaleAmount = 28.5f, TransactionTime = DateTime.Now, CustomerID = 1, EmployeeID = 1 });
            defaultSales.Add(new Sale() { StoreID = 1, RegisterID = 2, NetAmount = 20.5f, SaleAmount = 20.5f, TransactionTime = DateTime.Now, CustomerID = 2, EmployeeID = 2 });

            foreach (Sale std in defaultSales)
                context.Sales.Add(std);

            //2 salelines for each sale
            IList<Saleline> defaultSalelines = new List<Saleline>();

            defaultSalelines.Add(new Saleline() { SaleID = 1, ProductID = 1, Price = 12.5f, EffPrice = 12.5f, Quantity = 1, LineAmount = 12.5f });
            defaultSalelines.Add(new Saleline() { SaleID = 1, ProductID = 2, Price = 8f, EffPrice = 8f, Quantity = 2, LineAmount = 16f });

            defaultSalelines.Add(new Saleline() { SaleID = 2, ProductID = 2, Price = 8f, Quantity = 1, LineAmount = 8f });
            defaultSalelines.Add(new Saleline() { SaleID = 2, ProductID = 3, Price = 30f, Quantity = 3, LineAmount = 90f });

            foreach (Saleline std in defaultSalelines)
                context.Salelines.Add(std);

            //1 tenderline for each sale
            IList<Tenderline> defaultTenderlines = new List<Tenderline>();

            defaultTenderlines.Add(new Tenderline() { SaleID = 1, TenderTypeID = 1, TenderValue = 28.5f });
            defaultTenderlines.Add(new Tenderline() { SaleID = 2, TenderTypeID = 1, TenderValue = 20.5f });

            foreach (Tenderline std in defaultTenderlines)
                context.Tenderlines.Add(std);

            //2 grns
            //IList<Grn> defaultGrns = new List<Grn>();

            //defaultGrns.Add(new Grn() { StoreID = 1, NetAmount = 14.25f, TransactionTime = DateTime.Now });
            //defaultGrns.Add(new Grn() { StoreID = 1, NetAmount = 10.25f, TransactionTime = DateTime.Now });

            //foreach (Grn std in defaultGrns)
            //    context.Grns.Add(std);

            //2 grnlines for each grn
            //IList<Grnline> defaultGrnlines = new List<Grnline>();

            //defaultGrnlines.Add(new Grnline() { GrnID = 1, ProductID = 1, Cost = 6.25f, Quantity = 1, LineAmount = 6.25f });
            //defaultGrnlines.Add(new Grnline() { GrnID = 1, ProductID = 2, Cost = 4f, Quantity = 2, LineAmount = 8f });

            //defaultGrnlines.Add(new Grnline() { GrnID = 2, ProductID = 2, Cost = 4f, Quantity = 1, LineAmount = 4f });
            //defaultGrnlines.Add(new Grnline() { GrnID = 2, ProductID = 3, Cost = 15f, Quantity = 3, LineAmount = 45f });

            //foreach (Grnline std in defaultGrnlines)
            //    context.Grnlines.Add(std);

            base.Seed(context);
        }
    }
}
