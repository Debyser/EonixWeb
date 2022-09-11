
using ApplicationCore.Entities;

namespace WebApi.Models
{
    public partial class Company : IEntityBase
    {
        public Company()
        {
            ContactRoles = new HashSet<ContactRole>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Company2address { get; set; }

        public virtual Address Company2addressNavigation { get; set; }
        public virtual ICollection<ContactRole> ContactRoles { get; set; }
    }
}
