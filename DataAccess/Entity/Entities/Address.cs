using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entity.Entities
{
    public class Address : KeyedEntity
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Suburb { get; set; }
        public string Postcode { get; set; }
        public string State { get; set; }
        public string Iso3CountryCode { get; set; }
    }
}
