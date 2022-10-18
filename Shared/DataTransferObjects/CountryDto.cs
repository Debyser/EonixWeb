
namespace Shared.DataTransferObjects
{
    // public record CountryDto(string countryAlpha3Code, string name);
    public record CountryDto
    {
        public int Id { get; init; }
        public string? CountryAlpha3Code { get; set; }
        public string? Name { get; set; }
    }
}
