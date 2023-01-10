
using ApplicationCore.Entities;

namespace ApplicationCore.Entities
{
    // Contact Role = détail du db design
    public partial class ContactRole : EntityBase
    {
        public string Name { get; set; }
        public bool? Active { get; set; }

        // navigation variables
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
        public int ContactId { get; set; }
        public virtual Contact Contact { get; set; }
    }
}
