using System.Xml.Linq;

namespace SerializeMachine.Resolvers.Primitives
{
    /// <summary>
    /// Стандартный сериализатор типа Int16 (short)
    /// </summary>
    public sealed class Int16Resolver : Core.IResolver
    {
        public void Serialize(XElement serialized, object resolveObject)
        {
            serialized.Value = resolveObject.ToString();
        }
        public void Deserialzie(XElement serialized,ref object instance)
        {
            instance = System.Int16.Parse(serialized.Value);
        }
    }
}
