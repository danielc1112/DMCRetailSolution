using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entity.Entities
{
    class Layby : BaseEntity
    {
        [Key]
        public int LaybyID { get; set; }
        [Required]
        public int SaleID { get; set; }
        [Required]
        public int StoreID { get; set; }
        public string Status { get; set; }
        [DataType(DataType.Currency)]
        public float OriginalAmount { get; set; }
        [DataType(DataType.Currency)]
        public float Deposit { get; set; }
        [DataType(DataType.Currency)]
        public float AmountOwing { get; set; }
        [DataType(DataType.Currency)]
        public float EstablishmentFee { get; set; }
        [DataType(DataType.Currency)]
        public float CancellationFee { get; set; }
        public DateTime PlannedFinalisationDate { get; set; }
        public DateTime ActualFinalisationDate { get; set; }
    }
}
