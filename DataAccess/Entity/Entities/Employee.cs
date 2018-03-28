using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entity.Entities
{
    public class Employee : BaseEntity
    {
        [Key]
        public int EmployeeID { get; set; }
        [Required]
        public string Status { get; set; }
        public string TaxFileNumber { get; set; }
        public int UserID { get; set; }
        public string EmployeeTitle { get; set; }
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
