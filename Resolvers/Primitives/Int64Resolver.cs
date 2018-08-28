using System.Xml.Linq;

namespace SerializeMachine.Resolvers.Primitives
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
        public Int64Resolver() : base(Utility.TypeOf<long>.Type, null) { }
    }
}
