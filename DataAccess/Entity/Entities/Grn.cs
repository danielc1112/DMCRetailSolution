using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entity.Entities
{
    public class Grn : BaseEntity
    {
        [Key]
        public int GrnID { get; set; }
        [Required]
        public int StoreID { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public float NetAmount { get; set; }
        [Required]
        [Index]
        public DateTime TransactionTime { get; set; }
        public int EmployeeID { get; set; }
        public int SupplierID { get; set; }

        public virtual ICollection<Grnline> Grnlines { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
