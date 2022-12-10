namespace Shared.DataTransferObjects
{
    public record class ContactForUpdateDto
    {
        public string? Lastname { get; init; }
        public string? Firstname { get; init; }
        public AddressDto? Address { get; init; }
    }
}