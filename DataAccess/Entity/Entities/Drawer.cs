using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entity.Entities
{
    class Drawer : BaseEntity
    {
        [Key]
        public int DrawerID { get; set; }
        [Required]
        public int StoreID { get; set; }
        public int RegisterID { get; set; }
        [Required]
        public DateTime IssuedTime { get; set; }
        public DateTime ReconciledTime { get; set; }
        public string Status { get; set; }
        [DataType(DataType.Currency)]
        public float Float { get; set; }
        [DataType(DataType.Currency)]
        public float FinishingValue { get; set; }
        [DataType(DataType.Currency)]
        public float Discrepency { get; set; }
        public bool CanReconcile { get; set; }
        public bool Reconciled { get; set; }
    }
}
