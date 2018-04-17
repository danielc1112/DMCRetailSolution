using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entity.Entities
{
    class GL : BaseEntity
    {
        [Key]
        public int GLID { get; set; }
        public string Description { get; set; }
        public string DRCR { get; set; }
        public int GLReportID { get; set; }
        public string RecordType { get; set; }

        public int TaxID { get; set; }
        public int GstBasID { get; set; }
    }
}
