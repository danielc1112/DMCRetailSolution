using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entity.Entities
{
    public class Tenderline : KeyedEntity
    {
        [Required]
        public int TenderTypeID { get; set; } //Automatically a foreign key
        [Required]
        [Index]
        public int SaleID { get; set; } //Automatically a foreign key
        public string Status { get; set; }
        [DataType(DataType.Currency)]
        public float TenderValue { get; set; }
        [DataType(DataType.Currency)]
        public float ChangeGiven { get; set; }
        [DataType(DataType.Currency)]
        public float CashOut { get; set; }

        public virtual TenderType TenderType { get; set; }
    }
}
