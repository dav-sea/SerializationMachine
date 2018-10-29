namespace SerializationMachine.Resolver
{
    public interface IResolverStorage : IReadOnlyResolverStorage
    {
        void SetResolver(IResolver resolver, string convention);
    }
}
