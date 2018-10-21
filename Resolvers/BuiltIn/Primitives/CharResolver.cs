using System.Xml.Linq;

namespace SerializationMachine.Resolvers.Primitives
{
    public sealed class CharResolver : Core.IResolver
    {
        public override void Serialize(XElement serialized, object resolveObject)
        {
            serialized.Value = resolveObject.ToString();
        }
        public override void Deserialzie(XElement serialized,ref object instance)
        {
            instance = System.Char.Parse(serialized.Value);
        }
        public CharResolver() : base(Utility.TypeOf<char>.Type) { }
        protected internal override object GetTemplateInstance(XElement serializedObject)
        {
            return '\0';
        }
    }
}
