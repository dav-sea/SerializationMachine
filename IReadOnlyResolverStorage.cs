using SerializationMachine.Core;

namespace SerializationMachine
{
    public interface IReadOnlyResolverStorage
    {
        IResolver GetResolver(string convention);
        TypeDictionary GetTypeDictionary();
    }
}
