using System.Xml.Linq;

namespace SerializeMachine.Resolvers.Primitives
{
    public sealed class StringResolver : Core.IResolver
    {
        public override void Serialize(XElement serialized, object resolveObject)
        {
            serialized.Value = resolveObject.ToString();
        }
        public override void Deserialzie(XElement serialized,ref object instance)
        {
            instance = serialized.Value;
        }
        public StringResolver() : base(Utility.TypeOf<string>.Type) { }
        protected internal override object ManagedObjectOf(XElement serializedObject)
        {
            return string.Empty;
        }
    }
}
