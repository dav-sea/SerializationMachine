using System.Xml.Linq;

namespace SerializeMachine.Resolvers.Primitives
{
    public sealed class ObjectResolver : Core.IResolver
    {
        public override void Serialize(XElement serialized, object resolveObject)
        {
            
        }
        public override void Deserialzie(XElement serialized,ref object instance)
        {
            
        }
        public ObjectResolver() : base(Utility.TypeOf<object>.Type, ObjectFactoryFunc) { }
        
        private static object ObjectFactoryFunc()
        {
            return new object();
        }
    }
}
