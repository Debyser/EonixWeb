namespace Shared.DataTransferObjects
{
    public record class ContactForCreationDto
    {
        public string? Lastname { get; init; }
        public string? Firstname { get; init; }
        public AddressDto? Address { get; set; }
    }
}
