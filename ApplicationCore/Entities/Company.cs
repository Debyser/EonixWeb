using System.Diagnostics;

namespace ApplicationCore.Entities
{
    [DebuggerDisplay("Name = {Name}")]

    public partial class Company : EntityBase
    {
        public string Name { get; set; }
        public bool? Active { get; set; }

        public long AddressId { get; set; }

        public Address Address { get; set; }
        public IEnumerable<ContactRole> ContactRoles { get; set; }
    }
}