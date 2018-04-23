using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entity.Entities
{
    public class Product : KeyedEntity
    {
        [Required]
        public string Description { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public float Price { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public float Cost { get; set; }
        public int SupplierID { get; set; }

        public virtual Supplier Supplier { get; set; }
        public virtual StoreProduct StoreProduct { get; set; }
    }
}
