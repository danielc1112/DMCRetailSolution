using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entity.Entities
{
    class EmailMessageline : BaseEntity
    {
        [Key]
        public int EmailMessagelineID { get; set; }
        [Required]
        public int EmailMessageID { get; set; }
        public int Sequence { get; set; }
        public string Text { get; set; }
    }
}
