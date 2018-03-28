using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entity.Entities
{
    public class Saleline : BaseEntity
    {
        [Key]
        public int SalelineID { get; set; }
        [Required]
        [Index]
        public int SaleID { get; set; } //Automatically a foreign key
        [Required]
        [Index]
        public int ProductID { get; set; } //Automatically a foreign key
        [DataType(DataType.Currency)]
        public float Price { get; set; }
        [DataType(DataType.Currency)]
        public float EffPrice { get; set; }
        public int Quantity { get; set; }
        [DataType(DataType.Currency)]
        public float LineAmount { get; set; }
        [DataType(DataType.Currency)]
        public float GstAmount { get; set; }
        public float GstRate { get; set; }
        public string Status { get; set; }

        public virtual Product Product { get; set; }
    }
}
