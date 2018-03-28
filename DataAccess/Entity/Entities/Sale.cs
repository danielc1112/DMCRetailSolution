using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entity.Entities
{
    public class Sale : BaseEntity
    {
        [Key]
        public int SaleID { get; set; }
        [Required]
        public int StoreID { get; set; }
        [Required]
        public int RegisterID { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public float NetAmount { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public float GstAmount { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public float SaleAmount { get; set; }
        [Required]
        [Index]
        public DateTime TransactionTime { get; set; }
        public int EmployeeID { get; set; }
        public int? CustomerID { get; set; }

        public virtual ICollection<Saleline> Salelines { get; set; }
        public virtual ICollection<Tenderline> Tenderlines { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Store Store { get; set; }
        public virtual Register Register { get; set; }
    }
}
