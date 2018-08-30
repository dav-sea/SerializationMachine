using System.Xml.Linq;

namespace SerializeMachine.Resolvers
{
    public sealed class ObjectResolver : Core.IResolver
    {
        public override void Serialize(XElement serialized, object resolveObject)
        {
            
        }
        public override void Deserialzie(XElement serialized,ref object instance)
        {
            
        }
        public ObjectResolver() : base(Utility.TypeOf<object>.Type) { }

        protected internal override object ManagedObjectOf(XElement serializedObject)
        {
            return new object();
        }
    }
}
