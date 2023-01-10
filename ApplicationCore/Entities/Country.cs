
using System.Diagnostics;

namespace ApplicationCore.Entities
{
    [DebuggerDisplay("Name = {Name}; Iso3Code = {Iso3Code}")]
    public partial class Country : EntityBase
    {
        public string Iso2Code { get; set; }
        public string Iso3Code { get; set; }
        public string Name { get; set; }
        public bool? Active { get; set; }

    }
}