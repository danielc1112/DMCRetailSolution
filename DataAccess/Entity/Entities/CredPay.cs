using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entity.Entities
{
    class CredPay : BaseEntity
    {
        [Key]
        public int CredPayID { get; set; }
        [Required]
        public int BankAccountID { get; set; }
        public string Description { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public float NetAmount { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public float GstAmount { get; set; }
        [Required]
        [Index]
        public DateTime TransactionTime { get; set; }
        [Required]
        public int InvoiceCount { get; set; }
        public bool Banked { get; set; }
        public int BankRecID { get; set; }
        public DateTime BankRecDate { get; set; }

        public virtual ICollection<CredPayline> CredPaylines { get; set; }
        
    }
}
