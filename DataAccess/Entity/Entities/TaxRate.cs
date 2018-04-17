using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entity.Entities
{
    class TaxRate : BaseEntity
    {
        [Key]
        public int TaxRateID { get; set; }
        public int TaxID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public float Rate { get; set; }
        public bool Active { get; set; }
    }
}
