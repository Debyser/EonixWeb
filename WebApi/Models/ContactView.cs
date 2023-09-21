namespace WebApi.Models
{
    public class ContactView
    {
        public long Id { get; set; }
        public string Lastname { get; set; }
        public string PhoneNumber { get; set; }
        public string Firstname { get; set; }

        public IEnumerable<RoleView> Roles { get; set; }  // Include roles associated with the contact
        public AddressView Address { get; set; }
    }
}