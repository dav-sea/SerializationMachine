using SerializeMachine.Core;

namespace SerializeMachine
{
    public interface IReadOnlyResolverStorage
    {
        IResolver GetResolver(string convention);
        TypeDictionary GetTypeDictionary();
    }
}
