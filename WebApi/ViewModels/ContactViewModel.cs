namespace WebApi.ViewModels
{
    public class ContactViewModel
    {
        public string? Lastname { get; init; }
        public string? Firstname { get; init; }
        public string? RoleName { get; init; }
        public AddressViewModel? Address { get; set; }
    }
}