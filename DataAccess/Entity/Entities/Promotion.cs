using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entity.Entities
{
    class Promotion : BaseEntity
    {
        [Key]
        public int PromotionID { get; set; }
        [Required]
        public string Description { get; set; }
        public string Status { get; set; }
        [Required]
        [Index]
        public DateTime FromDate { get; set; }
        [Required]
        [Index]
        public DateTime ToDate { get; set; }
        public int PrecedenceLevel { get; set; }
        public string PromoItemLevel { get; set; } //i.e. 'I' item, 'D' department, etc
        public string BenefitType { get; set; } //i.e. 'A' amount off total, 'C' percentage off, etc
        public float TotalPercentageOffRate { get; set; }
        [DataType(DataType.Currency)]
        public float TotalAmountOff { get; set; }
        public bool TenderBased { get; set; }
        public string TenderTypeID { get; set; }
    }
}
