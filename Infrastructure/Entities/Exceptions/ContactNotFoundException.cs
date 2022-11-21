namespace Infrastructure.Entities.Exceptions
{
    internal class ContactNotFoundException : NotFoundException
    {
        public ContactNotFoundException(int countryId) : base($"The country with id: {countryId} doesn't exist in the database.")
        {

        }
    }
}