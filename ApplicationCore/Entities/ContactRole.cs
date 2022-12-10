
using ApplicationCore.Entities;

namespace WebApi.Models
{
    // Contact Role = détail du db design
    public partial class ContactRole : IEntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // navigation variables
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
        public int ContactId { get; set; }
        public virtual Contact Contact { get; set; }
    }
}
