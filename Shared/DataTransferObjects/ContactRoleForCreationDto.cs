namespace Shared.DataTransferObjects
{
    public record class ContactRoleForCreationDto
    {
        public int ContactRole2company { get; init; }
        public int ContactRole2contact { get; init; }
        public string? Name { get; init; }
    }
}