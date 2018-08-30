using System.Xml.Linq;

namespace SerializeMachine.Resolvers.Primitives
{
    public sealed class SingleResolver : Core.IResolver
    {
        public override void Serialize(XElement serialized, object resolveObject)
        {
            serialized.Value = resolveObject.ToString();
        }
        public override void Deserialzie(XElement serialized,ref object instance)
        {
            instance = System.Single.Parse(serialized.Value);
        }
        public SingleResolver() : base(Utility.TypeOf<float>.Type) { }
        protected internal override object ManagedObjectOf(XElement serializedObject)
        {
            return 0.0f;
        }
    }
}
