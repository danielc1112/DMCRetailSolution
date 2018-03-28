using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entity.Entities
{
    public class User : BaseEntity
    {
        [Key]
        public int UserID { get; set; }
        [Required]
        public string UserCode { get; set; }
        [Required]
        public string Password { get; set; }
        public bool Active { get; set; }
    }
}
