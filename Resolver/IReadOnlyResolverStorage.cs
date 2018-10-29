namespace SerializationMachine.Resolver
{
    public interface IReadOnlyResolverStorage
    {
        IResolver GetResolver(string convention);
        TypeDictionary GetTypeDictionary();
    }
}
