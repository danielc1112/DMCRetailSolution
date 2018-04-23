using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entity.Entities
{
    public class StoreProduct : KeyedEntity
    {
        [Required]
        public int ProductID { get; set; }
        [Required]
        public int StoreID { get; set; }
        public int QtyOnHand { get; set; }

        public virtual Store Store { get; set; }
    }
}
