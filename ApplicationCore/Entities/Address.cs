
using System.Diagnostics;

namespace ApplicationCore.Entities
{
    [DebuggerDisplay("Zipcode = {Zipcode}, Street = {Street}")]
    public partial class Address : EntityBase
    {
        public string Zipcode { get; set; }
        public string Street { get; set; }
        public string BoxNumber { get; set; }
        public string City { get; set; }
        public long CountryId { get; set; }
        public bool Active { get; set; }

        public Country Country { get; set; }
    }
}
