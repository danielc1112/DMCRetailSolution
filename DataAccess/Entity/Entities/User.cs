using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entity.Entities
{
    public class User : KeyedEntity
    {
        [Required]
        public string UserCode { get; set; }
        [Required]
        public string Password { get; set; }
        public bool Active { get; set; }
    }
}
