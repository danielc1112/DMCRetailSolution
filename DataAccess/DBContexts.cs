using System.Data.Entity;
using System.Configuration;
using System.Data.SqlClient;
using DataAccess.Entity.Entities;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace DataAccess
{
    public class RetailDbContext : DbContext
    {        
        //Entities
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Register> Registers { get; set; }
        public DbSet<Printer> Printers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<TenderType> TenderTypes { get; set; }

        //Juntion tables
        public DbSet<StoreProduct> StoreProducts { get; set; }

        //Historical Documents
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Saleline> Salelines { get; set; }
        public DbSet<Tenderline> Tenderlines { get; set; }
        public DbSet<Grn> Grns { get; set; }
        public DbSet<Grnline> Grnlines { get; set; }
        public DbSet<StoreProductTran> StoreProductTrans { get; set; }
        public DbSet<Stocktake> Stocktakes { get; set; }
        public DbSet<Stocktakeline> Stocktakelines { get; set; }
        public DbSet<PO> Pos { get; set; }
        public DbSet<POline> Polines { get; set; }

        public RetailDbContext() : base("Data Source=6800K;Initial Catalog= HODB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
            //(new SqlConnectionStringBuilder(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString)).InitialCatalog)
        {
            Database.SetInitializer(new MyDBInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            // Fluent API
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            //modelBuilder.Entity<Saleline>()
            //    .HasKey(s => s.SalelineID);
            //.HasRequired<Sale>(s => s.Sale)
            //.WithMany(g => g.Salelines)
            //.HasForeignKey<int>(s => s.SaleID);

            //Index
            //modelBuilder
            //    .Entity<Saleline>()
            //    .Property(t => t.SaleID)
            //    .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute()));

            //cascade delete of sale
            //modelBuilder.Entity<Sale>()
            //    .HasKey(s => s.SaleID)
            //    .HasMany<Saleline>(g => g.Salelines)
            //    .WithRequired(s => s.Sale)
            //    .WillCascadeOnDelete();

            //modelBuilder.Entity<Product>()
            //    .HasKey(s => s.ProductID);

            base.OnModelCreating(modelBuilder);
        }
    }
}