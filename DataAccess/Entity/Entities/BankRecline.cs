using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entity.Entities
{
    class BankRecline : BaseEntity
    {
        [Key]
        public int BankReclineID { get; set; }
        [Required]
        [Index]
        public int BankRecID { get; set; }
        public DateTime TransactionTime { get; set; }
        public string DocSource { get; set; }
        public int DocNumber { get; set; }
        public string Description { get; set; }
        [DataType(DataType.Currency)]
        public float PaymentAmount { get; set; }
        [DataType(DataType.Currency)]
        public float ReceiptAmount { get; set; }
    }
}
