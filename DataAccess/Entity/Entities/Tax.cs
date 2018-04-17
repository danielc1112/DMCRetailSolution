using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entity.Entities
{
    class Tax : BaseEntity
    {
        [Key]
        public int TaxID { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public int GLID { get; set; }
    }
}
