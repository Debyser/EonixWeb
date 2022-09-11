
using ApplicationCore.Entities;

namespace WebApi.Models
{
    public partial class ContactRole : IEntityBase
    {
        public int Id { get; set; }
        public int ContactRole2company { get; set; }
        public int ContactRole2contact { get; set; }
        public string Name { get; set; }

        public virtual Company ContactRole2companyNavigation { get; set; }
        public virtual Contact ContactRole2contactNavigation { get; set; }
    }
}
