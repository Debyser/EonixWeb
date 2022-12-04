namespace Shared.DataTransferObjects
{
    public record class CompanyForCreationDto
    {
        public string? Name { get; init; }
        public AddressDto? Address { get; init; }
        public IEnumerable<ContactForCreationDto>? Contacts { get; init; }
    }
}