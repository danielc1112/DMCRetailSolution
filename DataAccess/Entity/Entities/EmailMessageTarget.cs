using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entity.Entities
{
    class EmailMessageTarget : BaseEntity
    {
        [Key]
        public int EmailMessageTargetID { get; set; }
        [Required]
        public int EmailMessageID { get; set; }
        public string TargetType { get; set; } //'L' location, 'E' employee
        public int EmployeeID { get; set; }
        public int StoreID { get; set; }
    }
}
