using System.Diagnostics;

namespace ApplicationCore.Entities
{
    [DebuggerDisplay("Lastname = {Lastname}; Firstname = {Firstname}")]
    public partial class Contact : EntityBase
    {
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string PhoneNumber { get; set; }
        public long AddressId { get; set; }
        public bool? Active { get; set; }

        public DateTime? CreationTime { get; set; }

        public Address Address { get; set; }

        public List<ContactRole> ContactRoles { get; set; }
    }
}