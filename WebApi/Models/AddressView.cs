using WebApi.Models;

namespace WebApi
{
    public class AddressView
    {
        public string Zipcode { get; init; }
        public string Street { get; init; }
        public string BoxNumber { get; init; }
        public string City { get; init; }
        public CountryView Country { get; init; }
    }
}