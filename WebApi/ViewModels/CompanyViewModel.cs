using Shared.DataTransferObjects;

namespace WebApi.ViewModels
{
    public class CompanyViewModel
    {
        public string? Name { get; init; }
        public AddressViewModel? Address { get; init; }
        public IEnumerable<ContactViewModel>? Contacts { get; init; }

    }
}
