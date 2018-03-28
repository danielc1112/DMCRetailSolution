using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DataAccess.Entity.Entities
{
    public class Stocktake : BaseEntity
    {
        [Key]
        public int StocktakeID { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime TransactionTime { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public string StocktakeType { get; set; }
        [Required]
        public int StoreID { get; set; }
        public int EmployeeID { get; set; }

        public virtual ICollection<Stocktakeline> Stocktakelines { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
