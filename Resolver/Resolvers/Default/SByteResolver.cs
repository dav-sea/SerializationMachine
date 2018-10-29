using System.Xml.Linq;

namespace SerializationMachine.Resolver.Resolvers
{
    public sealed class SByteResolver : IResolver
    {
        public override void Serialize(XElement serialized, object resolveObject)
        {
            serialized.Value = resolveObject.ToString();
        }
        public override void Deserialzie(XElement serialized,ref object instance)
        {
            instance = System.SByte.Parse(serialized.Value);
        }
        public SByteResolver() : base(TypeOf<sbyte>.Type) { }


        protected internal override object GetTemplateInstance(XElement serializedObject)
        {
            return (sbyte)0;
        }
    }
}
