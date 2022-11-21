namespace Infrastructure.Entities.Exceptions
{
    internal class AddressNotFoundException : NotFoundException
    {
        public AddressNotFoundException(int countryId) : base($"The address with id: {countryId} doesn't exist in the database.")
        {

        }
    }
}