using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entity.Entities
{
    class Reason : BaseEntity
    {
        [Key]
        public int ReasonID { get; set; }
        public string Description { get; set; }
        public string Type { get; set; } //discount, refund, adjustment, price change, etc
        public bool Active { get; set; }
    }
}
