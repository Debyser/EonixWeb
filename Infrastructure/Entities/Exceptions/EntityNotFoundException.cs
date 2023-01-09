namespace Infrastructure.Entities.Exceptions
{
    public class EntityNotFoundException : NotFoundException
    {
        public EntityNotFoundException(string entityName, long id) : base($"The {entityName} with id {id} doesn't exist in the database.")
        {
        }
    }
}
