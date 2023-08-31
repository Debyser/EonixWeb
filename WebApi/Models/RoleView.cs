namespace WebApi.Models
{
    public class RoleView
    {
        public long Id { get; init; }
        public string Name { get; init; }
        public bool? Active { get; init; }
        public CompanyView? Company { get; init; }
        public ContactView? Contact { get; init; }

    }
}