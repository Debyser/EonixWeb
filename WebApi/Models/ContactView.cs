namespace WebApi.Models
{
    public class ContactView
    {
        public string Lastname { get; init; }
        public string PhoneNumber { get; init; }
        public string Firstname { get; init; }
        public string RoleName { get; init; }
        public AddressView Address { get; init; }
    }
}