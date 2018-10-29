using System.Xml.Linq;

namespace SerializationMachine.Resolver.Resolvers
{
    public sealed class DoubleResolver : IResolver
    {
        public override void Serialize(XElement serialized, object resolveObject)
        {
            serialized.Value = resolveObject.ToString();
        }
        public override void Deserialzie(XElement serialized,ref object instance)
        {
            instance = System.Double.Parse(serialized.Value);
        }
        public DoubleResolver() : base(TypeOf<double>.Type) { }
        protected internal override object GetTemplateInstance(XElement serializedObject)
        {
            return 0.0;
        }
    }
}
