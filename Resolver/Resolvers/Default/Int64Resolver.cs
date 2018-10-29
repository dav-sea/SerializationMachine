using System.Xml.Linq;

namespace SerializationMachine.Resolver.Resolvers
{
    public sealed class Int64Resolver : IResolver
    {
        public override void Serialize(XElement serialized, object obj)
        {
            serialized.Value = obj.ToString();
        }
        public override void Deserialzie(XElement serialized,ref object instance)
        {
            instance = System.Int64.Parse(serialized.Value);
        }
        public Int64Resolver() : base(TypeOf<long>.Type) { }
        protected internal override object GetTemplateInstance(XElement serializedObject)
        {
            return (long)0;
        }
    }
}
