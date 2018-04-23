using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entity.Entities
{
    public class BankAccount : KeyedEntity
    {
        [Required]
        public string BankName { get; set; }
        [Required]
        public string Bsb { get; set; }
        [Required]
        public string AccountNo { get; set; }
        [Required]
        public string AccountName { get; set; }
    }
}
