namespace Infrastructure.Entities.Exceptions
{
    public class CompanyNotFoundException : NotFoundException
    {
        public CompanyNotFoundException(long countryId) : base($"The country with id: {countryId} doesn't exist in the database.")
        {

        }
    }
}
