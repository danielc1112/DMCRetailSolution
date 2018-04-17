using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entity.Entities
{
    class Department : BaseEntity
    {
        [Key]
        public int DepartmentID { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
    }
}
