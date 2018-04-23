using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entity.Entities
{
    public class Supplier : KeyedEntity
    {
        [Required]
        public string Description { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public int AddressID { get; set; }

        public virtual Address Address { get; set; }
    }
}
