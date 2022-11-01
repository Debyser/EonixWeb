namespace WebApi.Models
{
    public enum ScopeLifeTime
    {
        Transient = 1,
        Singleton = 2,
        Thread = 3,
        Request = 4,
        Scoped = 5,
        NamedCallParent = 7,
        Custom = 9
    }
}