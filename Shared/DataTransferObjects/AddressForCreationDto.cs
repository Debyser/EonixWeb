
namespace Shared.DataTransferObjects
{
    public record class AddressForCreationDto
    {
        public string? Zipcode { get; init; }
        public string? Street { get; init; }
        public string? BoxNumber { get; init; }
        public string? City { get; init; }
        public CountryDto? Country { get; set; }
    }
}
