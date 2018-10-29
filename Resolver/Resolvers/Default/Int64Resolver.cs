using System.Xml.Linq;

namespace SerializationMachine.Resolvers.Primitives
{
    public sealed class Int64Resolver : Core.IResolver
    {
        public override void Serialize(XElement serialized, object obj)
        {
            serialized.Value = obj.ToString();
        }
        public override void Deserialzie(XElement serialized,ref object instance)
        {
            instance = System.Int64.Parse(serialized.Value);
        }
        public Int64Resolver() : base(Utility.TypeOf<long>.Type) { }
        protected internal override object GetTemplateInstance(XElement serializedObject)
        {
            return (long)0;
        }
    }
}
