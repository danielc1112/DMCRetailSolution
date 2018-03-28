using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entity.Entities
{
    public class Customer : BaseEntity
    {
        [Key]
        public int CustomerID { get; set; }
        public string CustomerType { get; set; }
        public string Status { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public int AddressID { get; set; }
        [NotMapped]
        public string DisplayName { get { return FirstName + " " + LastName; } }

        public virtual Address Address { get; set; }
    }
}
