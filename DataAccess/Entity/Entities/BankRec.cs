using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entity.Entities
{
    class BankRec : BaseEntity
    {
        [Key]
        public int BankRecID { get; set; }
        [Required]
        [Index]
        public DateTime TransactionTime { get; set; }
        public int GLID { get; set; }
        [DataType(DataType.Currency)]
        public float OpeningBalance { get; set; }
        [DataType(DataType.Currency)]
        public float BankBalance { get; set; }
        public string Description { get; set; }
        [DataType(DataType.Currency)]
        public float TotalPayments { get; set; }
        [DataType(DataType.Currency)]
        public float TotalReceipts { get; set; }
        [DataType(DataType.Currency)]
        public float TotalCreds { get; set; }
        [DataType(DataType.Currency)]
        public float TotalGenJNL { get; set; }

        public bool UnDone { get; set; }
    }
}
