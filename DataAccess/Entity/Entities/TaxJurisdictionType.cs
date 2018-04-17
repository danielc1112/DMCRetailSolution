using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entity.Entities
{
    class TaxJurisdictionType : BaseEntity
    {
        [Key]
        public int TaxJurisdictionTypeID { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
    }
}
