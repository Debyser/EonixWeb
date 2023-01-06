namespace Infrastructure.Entities.Exceptions
{
    internal class ContactNotFoundException : NotFoundException
    {
        public ContactNotFoundException(long countryId) : base($"The country with id: {countryId} doesn't exist in the database.")
        {

        }
    }
}