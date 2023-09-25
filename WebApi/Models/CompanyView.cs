namespace WebApi.Models
{
    public class CompanyView
    {
        public long Id { get; init; }
        public string? Name { get; init; }
        public AddressView? Address { get; init; }
    }
}