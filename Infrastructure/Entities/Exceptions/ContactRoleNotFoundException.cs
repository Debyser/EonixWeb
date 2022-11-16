namespace Infrastructure.Entities.Exceptions
{
    internal class ContactRoleNotFoundException : NotFoundException
    {
        public ContactRoleNotFoundException(int contactRoleId) : base($"The contact role with id: {contactRoleId} doesn't exist in the database.")
        {
        }
    }
}