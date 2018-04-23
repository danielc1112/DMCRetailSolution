using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entity.Entities
{
    public class Stocktakeline : KeyedEntity
    {
        [Required]
        [Index]
        public int StocktakeID { get; set; } //Automatically a foreign key
        [Required]
        public int ProductID { get; set; }
        public int CountedQty { get; set; }
    }
}
