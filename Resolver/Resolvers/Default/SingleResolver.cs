using System.Xml.Linq;


namespace SerializationMachine.Resolver.Resolvers
{
    public sealed class SingleResolver : IResolver
    {
        public override void Serialize(XElement serialized, object resolveObject)
        {
            serialized.Value = resolveObject.ToString();
        }
        public override void Deserialzie(XElement serialized,ref object instance)
        {
            instance = System.Single.Parse(serialized.Value);
        }
        public SingleResolver() : base(TypeOf<float>.Type) { }
        protected internal override object GetTemplateInstance(XElement serializedObject)
        {
            return 0.0f;
        }
    }
}
