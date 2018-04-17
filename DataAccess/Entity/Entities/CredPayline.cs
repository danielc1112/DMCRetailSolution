using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entity.Entities
{
    class CredPayline : BaseEntity
    {
        [Key]
        public int CredPaylineID { get; set; }
        [Required]
        [Index]
        public int CredPayID { get; set; } //Automatically a foreign key
        [Required]
        public int SupplierID { get; set; }
        public string Description { get; set; }
        [Required]
        public int InvoiceNbr { get; set; }
        public DateTime InvoiceDate { get; set; }
        [DataType(DataType.Currency)]
        public float NetAmount { get; set; }
        [DataType(DataType.Currency)]
        public float GstAmount { get; set; }

        public virtual Supplier Supplier { get; set; }
    }
}
