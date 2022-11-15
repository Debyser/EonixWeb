
using ApplicationCore.Entities;
using System.Diagnostics;

namespace WebApi.Models
{
    [DebuggerDisplay("Name = {Name}; Iso3Code = {Iso3Code}")]
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
