using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entity.Entities
{
    class Laybyline : BaseEntity
    {
        [Key]
        public int LaybylineID { get; set; }
        [Required]
        public int LaybyID { get; set; }
        [Required]
        public int ProductID { get; set; }
        public int Quantity { get; set; }
    }
}
