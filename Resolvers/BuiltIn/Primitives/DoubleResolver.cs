using System.Xml.Linq;

namespace SerializeMachine.Resolvers.Primitives
{
    public sealed class DoubleResolver : Core.IResolver
    {
        public override void Serialize(XElement serialized, object resolveObject)
        {
            serialized.Value = resolveObject.ToString();
        }
        public override void Deserialzie(XElement serialized,ref object instance)
        {
            instance = System.Double.Parse(serialized.Value);
        }
        public DoubleResolver() : base(Utility.TypeOf<double>.Type) { }
        protected internal override object ManagedObjectOf(XElement serializedObject)
        {
            return 0.0;
        }
    }
}
