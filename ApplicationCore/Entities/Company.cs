
using ApplicationCore.Entities;
using System.Diagnostics;

namespace WebApi.Models
{
    [DebuggerDisplay("Name = {Name}")]

    public partial class Company : IEntityBase
    {
        public Company()
        {
            ContactRoles = new HashSet<ContactRole>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Company2address { get; set; }

        public virtual Address Address { get; set; }
        public virtual ICollection<ContactRole> ContactRoles { get; set; }
    }
}
