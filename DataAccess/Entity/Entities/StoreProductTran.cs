using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entity.Entities
{
    public class StoreProductTran : BaseEntity
    {
        [Key, Column(Order = 0)]
        public int ProductID { get; set; }
        [Key, Column(Order = 1)]
        public string DocumentType { get; set; }
        [Key, Column(Order = 2)]
        public int DocumentID { get; set; }
        public DateTime EffectiveTime { get; set; }
        public int Quantity { get; set; }
    }
}
