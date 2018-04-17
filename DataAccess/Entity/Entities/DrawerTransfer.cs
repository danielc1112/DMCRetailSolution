using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entity.Entities
{
    class DrawerTransfer : BaseEntity
    {
        [Key]
        public int DrawerTransferID { get; set; }
        public string TransferType { get; set; }
        public int FromRegisterID { get; set; }
        public int ToRegisterID { get; set; }
        [DataType(DataType.Currency)]
        public float AmountOut { get; set; }
        public int TenderTypeID { get; set; }
        public DateTime OutTime { get; set; }
        public DateTime ReceivedTime { get; set; }
        public bool ConfirmReceived { get; set; }
        [DataType(DataType.Currency)]
        public float DiscrepancyAmount { get; set; }
    }
}
