namespace Infrastructure.Entities.Exceptions
{
    internal class AddressNotFoundException : NotFoundException
    {
        public AddressNotFoundException(long countryId) : base($"The address with id: {countryId} doesn't exist in the database.")
        {

        }
    }
}