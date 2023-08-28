namespace WebApi.Models
{
    public class ContactView
    {
        public string Lastname { get; init; }
        public string PhoneNumber { get; init; }
        public string Firstname { get; init; }
        public List<RoleView> Roles { get; init; }  // Include roles associated with the contact
        public AddressView Address { get; init; }

    }
}