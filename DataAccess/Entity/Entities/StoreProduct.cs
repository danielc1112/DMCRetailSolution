using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entity.Entities
{
    public class StoreProduct : BaseEntity
    {
        [Key]
        public int StoreProductID { get; set; }
        [Required]
        public int ProductID { get; set; }
        [Required]
        public int StoreID { get; set; }
        public int QtyOnHand { get; set; }

        public virtual Store Store { get; set; }
    }
}
