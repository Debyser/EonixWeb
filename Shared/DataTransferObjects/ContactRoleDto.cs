namespace Shared.DataTransferObjects
{
    public record class ContactRoleDto
    {
        public int Id { get; init; }
        public CompanyDto Company { get; init; }
        public ContactDto Contact { get; init; }
        public string? Name { get; init; }
    }
}