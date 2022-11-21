namespace Shared.DataTransferObjects
{
    public record ContactDto
    {
        public string? Lastname { get; init; }
        public string? Firstname { get; init; }
    }
}