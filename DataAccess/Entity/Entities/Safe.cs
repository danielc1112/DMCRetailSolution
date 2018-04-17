using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entity.Entities
{
    class Safe : BaseEntity
    {
        [Key]
        public int SafeID { get; set; }
        [Required]
        public int StoreID { get; set; }
        [DataType(DataType.Currency)]
        public float Amount { get; set; }
    }
}
