using System.Xml.Linq;

namespace SerializationMachine.Resolver.Resolvers
{
    public sealed class ObjectResolver : IResolver
    {
        public override void Serialize(XElement serialized, object resolveObject)
        {
            
        }
        public override void Deserialzie(XElement serialized,ref object instance)
        {
            
        }
        public ObjectResolver() : base(TypeOf<object>.Type) { }

        protected internal override object GetTemplateInstance(XElement serializedObject)
        {
            return new object();
        }
    }
}
