namespace Shared.DataTransferObjects
{
    public record class CompanyDto
    {
        public string? Name { get; init; }
        public AddressDto? Address { get; set; }
    }
}