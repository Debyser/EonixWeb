namespace WebApi.Models
{
    internal class CompanyView
    {
        public string? Name { get; set; }
        public AddressView? Address { get; set; }
        public IEnumerable<ContactView>? Contacts { get; set; }

    }
}
