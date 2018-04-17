using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entity.Entities
{
    class Setting : BaseEntity
    {
        [Key]
        public int SettingID { get; set; }
        [Required]
        public string Name { get; set; }
        public string Value { get; set; }
        public bool Active { get; set; }
        public string Level { get; set; }
        public string LevelCode { get; set; }
    }
}
