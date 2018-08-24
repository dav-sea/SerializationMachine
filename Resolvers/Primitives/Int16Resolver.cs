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
        public object Deserialzie(XElement serialized)
        {
            return System.Int16.Parse(serialized.Value);
        }
    }
}
