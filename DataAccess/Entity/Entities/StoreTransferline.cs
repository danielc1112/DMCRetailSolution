using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entity.Entities
{
    class StoreTransferline : BaseEntity
    {
        [Key]
        public int StoreTransferlineID { get; set; }
        [Required]
        public int StoreTransferID { get; set; }
        [Required]
        public int ProductID { get; set; }
        public int Quantity { get; set; }

        public virtual Product Product { get; set; }
    }
}
