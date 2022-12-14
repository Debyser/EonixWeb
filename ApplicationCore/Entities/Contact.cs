using ApplicationCore.Entities;
using System.Diagnostics;

namespace ApplicationCore.Entities
{
    [DebuggerDisplay("Lastname = {Lastname}; Firstname = {Firstname}")]
    public partial class Contact : IEntityBase
    {
        public Contact()
        {
            ContactRoles = new HashSet<ContactRole>();
        }

        public int Id { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public int Contact2address { get; set; }
        public bool? Active { get; set; }

        public DateTime? CreationTime { get; set; }

        public virtual Address Address { get; set; }

        public virtual ICollection<ContactRole> ContactRoles { get; set; }
    }
}