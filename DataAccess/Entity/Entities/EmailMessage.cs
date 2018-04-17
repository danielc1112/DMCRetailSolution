using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entity.Entities
{
    class EmailMessage : BaseEntity
    {
        [Key]
        public int EmailMessageID { get; set; }
        public int CreatedStoreID { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public int ReplyToEmailMessageID { get; set; }
        public int ForwardedEmailMessageID { get; set; }
    }
}
