
using ApplicationCore.Entities;

namespace WebApi.Models
{
    public partial class Country : IEntityBase
    {
        public Country()
        {
            Addresses = new HashSet<Address>();
        }

        public int Id { get; set; }
        public string Iso3Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
    }
}
