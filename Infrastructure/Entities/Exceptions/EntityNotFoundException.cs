namespace Infrastructure.Entities.Exceptions
{
    public class EntityNotFoundException : NotFoundException
    {
        public EntityNotFoundException(Type type, long id) : base($"The {type.Name} with id {id} doesn't exist in the database.")
        {
        }
    }
}
