using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entity.Entities
{
    public class Register : BaseEntity
    {
        [Key]
        public int RegisterID { get; set; }
        [Required]
        public int StoreID { get; set; }
        public string RegisterStatus { get; set; }
        public string PrinterID { get; set; }
        public string EFTType { get; set; }
        public bool EftEnabled { get; set; }
    }
}
