using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entity.Entities
{
    class Division : BaseEntity
    {
        [Key]
        public int DivisionID { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
    }
}
