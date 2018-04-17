using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entity.Entities
{
    class UOM : BaseEntity
    {
        [Key]
        public int UOMID { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public string Type { get; set; } //i.e. mass, volume, unit measurements, etc
        public bool IsMaster { get; set; }
        public float MasterConversionRate { get; set; } //conversion to master of same type
    }
}
