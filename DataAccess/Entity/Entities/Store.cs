using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entity.Entities
{
    public class Store : KeyedEntity
    {
        [Required]
        public string Description { get; set; }
        public bool Active { get; set; }
        public int AddressID { get; set; }

        public virtual Address Address { get; set; }
    }
}
