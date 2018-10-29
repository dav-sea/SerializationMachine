using System.Xml.Linq;

namespace SerializationMachine.Resolver.Resolvers
{
    public sealed class CharResolver : IResolver
    {
        public override void Serialize(XElement serialized, object resolveObject)
        {
            serialized.Value = resolveObject.ToString();
        }
        public override void Deserialzie(XElement serialized,ref object instance)
        {
            instance = System.Char.Parse(serialized.Value);
        }
        public CharResolver() : base(TypeOf<char>.Type) { }
        protected internal override object GetTemplateInstance(XElement serializedObject)
        {
            return '\0';
        }
    }
}
