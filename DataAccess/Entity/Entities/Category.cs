using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entity.Entities
{
    class Category : BaseEntity
    {
        [Key]
        public int CategoryID { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
    }
}
