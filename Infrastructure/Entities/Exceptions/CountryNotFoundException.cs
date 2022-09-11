namespace Infrastructure.Entities.Exceptions
{
    internal class CountryNotFoundException : NotFoundException
    {
        public CountryNotFoundException(int personId) : base($"The person with id: {personId} doesn't exist in the database.")
        {

        }
    }
}
