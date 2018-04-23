using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entity.Entities
{
    public class PO : KeyedEntity
    {
        [Required]
        public int SupplierID { get; set; }
        [Required]
        public int StoreID { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public float NetAmount { get; set; }
        [Required]
        [Index]
        public DateTime TransactionTime { get; set; } = DateTime.UtcNow;
        public int EmployeeID { get; set; } = 1;

        public virtual ICollection<POline> POlines { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
