namespace Infrastructure.Entities.Exceptions
{
    public class CompanyNotFoundException : NotFoundException
    {
        public CompanyNotFoundException(int countryId) : base($"The country with id: {countryId} doesn't exist in the database.")
        {

        }
    }
}
