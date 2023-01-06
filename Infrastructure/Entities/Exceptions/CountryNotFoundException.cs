namespace Infrastructure.Entities.Exceptions
{
    internal class CountryNotFoundException : NotFoundException
    {
        public CountryNotFoundException(long countryId) : base($"The country with id: {countryId} doesn't exist in the database.")
        {

        }
    }
}
