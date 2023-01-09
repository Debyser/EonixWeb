namespace WebApi.Models
{
    public class CompanyView
    {
        public string Name { get; init; }
        public AddressView Address { get; init; }
        public IEnumerable<ContactView> Contacts { get; init; }

    }
}
