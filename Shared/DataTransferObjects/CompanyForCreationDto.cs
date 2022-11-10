namespace Shared.DataTransferObjects
{
    public record class CompanyForCreationDto
    {
        public string? Name { get; init; }
        public AddressDto? Address { get; set; }
    }
}