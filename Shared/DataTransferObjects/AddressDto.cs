namespace Shared.DataTransferObjects
{
    public record class AddressDto
    {
        public string? Zipcode { get; init ; }
        public string? Street { get; init; }
        public string? BoxNumber { get; init; }
        public string? City { get; init; }
    }
}