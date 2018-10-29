using System.Xml.Linq;

namespace SerializationMachine.Resolver.Resolvers
{
    public sealed class StringResolver : IResolver
    {
        public override void Serialize(XElement serialized, object resolveObject)
        {
            serialized.Value = resolveObject.ToString();
        }
        public override void Deserialzie(XElement serialized,ref object instance)
        {
            instance = serialized.Value;
        }
        public StringResolver() : base(TypeOf<string>.Type) { }
        protected internal override object GetTemplateInstance(XElement serializedObject)
        {
            return string.Empty;
        }
    }
}
