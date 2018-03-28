using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entity.Entities
{
    public class TenderType : BaseEntity
    {
        [Key]
        public int TenderTypeID { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public int DisplaySequence { get; set; }
        public bool ChangeGiven { get; set; }

    }
}
