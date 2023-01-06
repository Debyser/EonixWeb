namespace Infrastructure.Entities.Exceptions
{
    internal class ContactRoleNotFoundException : NotFoundException
    {
        public ContactRoleNotFoundException(long contactRoleId) : base($"The contact role with id: {contactRoleId} doesn't exist in the database.")
        {
        }
    }
}