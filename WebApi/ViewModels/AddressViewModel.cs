namespace WebApi.ViewModels
{
    public class AddressViewModel
    {
        public string? Zipcode { get; init; }
        public string? Street { get; init; }
        public string? BoxNumber { get; init; }
        public string? City { get; init; }
        public CountryViewModel? Country { get; set; }
    }
}
