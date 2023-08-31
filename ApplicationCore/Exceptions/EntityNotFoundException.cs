namespace ApplicationCore.Exceptions
{
    public class EntityNotFoundException : NotFoundException
    {
        public EntityNotFoundException(Type type, long id) : base($"The {type.Name} with id {id} doesn't exist in the database.")
        {
        }
        public EntityNotFoundException(Type type, string name) : base($"The {type.Name} with {name} doesn't exist in the database.")
        {
        }
    }
}
