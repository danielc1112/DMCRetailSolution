using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entity.Entities
{
    class LaybyPayment : BaseEntity
    {
        [Key]
        public int LaybyPaymentID { get; set; }
        [Required]
        public int LaybyID { get; set; }
        public string Type { get; set; } //deposit, payment, refund, remote, cancellation fee
        public string Status { get; set; }
        [DataType(DataType.Currency)]
        public float Amount { get; set; }
        public int PaymentStoreID { get; set; }
        public DateTime EffectiveTime { get; set; }
    }
}
