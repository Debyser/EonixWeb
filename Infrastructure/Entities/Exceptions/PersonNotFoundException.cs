namespace Infrastructure.Entities.Exceptions
{
    public sealed class PersonNotFoundException : NotFoundException
    {
        public PersonNotFoundException(Guid personId) : base($"The person with id: {personId} doesn't exist in the database.")
        {

        }
    }
}