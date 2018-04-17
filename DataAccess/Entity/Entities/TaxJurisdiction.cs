using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entity.Entities
{
    class TaxJurisdiction : BaseEntity
    {
        [Key]
        public int TaxJurisdictionID { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public int TaxJurisdictionTypeID { get; set; } //i.e. county, state
        public int ParentTaxJurisdictionID { get; set; }
    }
}
