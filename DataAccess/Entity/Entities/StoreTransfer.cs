using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entity.Entities
{
    class StoreTransfer : BaseEntity
    {
        [Key]
        public int StoreTransferID { get; set; }
        public string Description { get; set; }
        public int FromStoreID { get; set; }
        public int ToStoreID { get; set; }
        public string Status { get; set; }
        public DateTime ReceivedTime { get; set; }
    }
}
