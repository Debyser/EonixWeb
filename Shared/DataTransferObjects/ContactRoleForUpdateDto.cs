namespace Shared.DataTransferObjects
{
    public record class ContactRoleForUpdateDto
    {
        public int Id { get; set; }
        public int CompanyId { get; init; }
        public int ContactId { get; init; }
        public string? Name { get; init; }
    }
}
