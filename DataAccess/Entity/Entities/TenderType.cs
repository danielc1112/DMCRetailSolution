using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entity.Entities
{
    public class TenderType : KeyedEntity
    {
        public string Description { get; set; }
        public bool Active { get; set; }
        public int DisplaySequence { get; set; }
        public bool ChangeGiven { get; set; }

    }
}
