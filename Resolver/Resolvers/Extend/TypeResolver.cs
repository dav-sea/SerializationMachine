using System;
using System.Xml.Linq;
using SerializationMachine.Utility;

namespace SerializationMachine.Resolver.Resolvers
{
    public class TypeResolver : IResolver
    {
        private readonly Serializator Serializator;

        public TypeResolver(Serializator serializator)
            :base(TypeOf<Type>.Type)
        {
            this.Serializator = serializator; 
        }

        public override void Deserialzie(XElement serializedObject, ref object instance){ }

        public override void Serialize(XElement serialized, object resolveObject)
        {
            string convention;
            var value = resolveObject as Type;

            if(Serializator.GetTypeManager().UsingDictionary.TryGetConvention(value,out convention))
                serialized.Value = "@" + convention;
            else
                serialized.Value = value.AssemblyQualifiedName;

        }

        protected internal override object GetTemplateInstance(XElement serializedObject)
        {
            if (serializedObject.Value[0] == '@')
                return Serializator.GetTypeManager().UsingDictionary.TypeOf(serializedObject.Value.Remove(0, 1));
            else
                return Type.GetType(serializedObject.Value);
        }
    }
}
