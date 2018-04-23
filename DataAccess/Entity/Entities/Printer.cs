using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entity.Entities
{
    public class Printer : KeyedEntity
    {
        [Required]
        public string Description { get; set; }
        public bool Active { get; set; }
        public int ReceiptPrintWidth { get; set; }
        public bool SupportsGraphics { get; set; }
        public bool SupportsBarcodes { get; set; }
    }
}
