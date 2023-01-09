
using System.Diagnostics;

namespace ApplicationCore.Entities
{
    [DebuggerDisplay("Zipcode = {Zipcode}, Street = {Street}")]
    public partial class Address : IEntityBase
    {
        public Address()
        {
            Companies = new HashSet<Company>();
            Contacts = new HashSet<Contact>();
        }

        public int Id { get; set; }
        public string Zipcode { get; set; }
        public string Street { get; set; }
        public string BoxNumber { get; set; }
        public string City { get; set; }
        public int Address2country { get; set; }
        public bool? Active { get; set; }

        public virtual Country Country { get; set; }
        public virtual ICollection<Company> Companies { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }

    }
}
