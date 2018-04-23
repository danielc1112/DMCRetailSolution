﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entity.Entities
{
    public class POline : KeyedEntity
    {
        [Required]
        [Index]
        public int POID { get; set; } //Automatically a foreign key
        [Required]
        [Index]
        public int ProductID { get; set; } //Automatically a foreign key
        [DataType(DataType.Currency)]
        public float Cost { get; set; }
        public int Quantity { get; set; }

        [DataType(DataType.Currency)]
        public float LineAmount => Cost * Quantity;

        public virtual Product Product { get; set; }
    }
}
