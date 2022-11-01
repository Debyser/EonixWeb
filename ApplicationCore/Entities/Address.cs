
using ApplicationCore.Entities;

namespace WebApi.Models
{
    public partial class Address : IEntityBase
    {
        public Address()
        {
            Companies = new HashSet<Company>();
        }

        public int Id { get; set; }
        public string Zipcode { get; set; }
        public string Street { get; set; }
        public string BoxNumber { get; set; }
        public string City { get; set; }
        public int Address2country { get; set; }

        public virtual Country Country { get; set; }
        public virtual ICollection<Company> Companies { get; set; }
    }
}
