
namespace Shared.DataTransferObjects
{
    //This object is still immutable and init-only properties protect the state of the object from mutation once initialization is finished
    // with init in Postman , in the header key : Accept , Value : text/xml with init in the property , the object is immutable so we can see
    // Id LastName FirstName instead of strange names
    public record class CountryDto
    {
        public string? CountryAlpha3Code { get; init; }
        public string? Name { get; init; }
    }
}
