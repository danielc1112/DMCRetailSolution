using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entity.Entities
{
    class GLJournal : BaseEntity
    {
        [Key]
        public int GLJournalID { get; set; }
        [Required]
        [Index]
        public DateTime TransactionTime { get; set; }
        public string DocSource { get; set; }
        public int DocNumber { get; set; }
        public int GLID { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public float Amount { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public float NetAmount { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public float GstAmount { get; set; }
        public string Description { get; set; }
        public int StoreID { get; set; }
        public int SupplierID { get; set; }
        public int CustomerID { get; set; }
        public bool Banked { get; set; }
        public int BankRecID { get; set; }
        public DateTime BankRecDate { get; set; }
    }
}
