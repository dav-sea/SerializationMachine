using System.Xml.Linq;

namespace SerializeMachine.Resolvers.Primitives
{
    public sealed class BooleanResolver : Core.IResolver
    {
        public void Serialize(XElement serialized, object resolveObject)
        {
            serialized.Value = resolveObject.ToString();
        }
        public void Deserialzie(XElement serialized,ref object instance)
        {
            instance = System.Boolean.Parse(serialized.Value);
        }
    }
}
