using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Runtime.Serialization;
using System.Reflection;

using SerializeMachine.Utility;

namespace SerializeMachine.Resolvers
{
    public sealed class SerializableResolver : Core.IResolver
    {
        private readonly Core.Serializator Serializator;
        private readonly ConstructorInfo Constructor;
        private static readonly Type[] ConstructorSignature = new Type[]{typeof(SerializationInfo),typeof(StreamingContext)};
        private readonly IFactory InstanceFactory;

        public override void Serialize(XElement serialized, object resolveObject)
        {
            var serializableObject = resolveObject as ISerializable;

            var serializationInfo = new SerializationInfo(ResolveType, new FormatterConverter());
            serializableObject.GetObjectData(serializationInfo, new StreamingContext());

            Serializator.ResolveTo(serializationInfo, serialized);
        }

        public override void Deserialzie(XElement serializedObject, ref object instance)
        {
            var serializationInfo = new SerializationInfo(ResolveType, new FormatterConverter());
            Serializator.DeresolveTo(serializationInfo, serializedObject);

            Constructor.Invoke(instance, new object[] { serializationInfo, new StreamingContext() });
        }

        protected internal override object ManagedObjectOf(System.Xml.Linq.XElement serializedObject)
        {
            return InstanceFactory.Instantiate();   
        }

        private XElement SerializeEntry(SerializationEntry entry)
        {
            var serialized = Serializator.AutoResolve(entry.Value);
            serialized.SetAttributeValue("NAME", entry.Name);
            return serialized;
        }

        public SerializableResolver(Type resolveType,Core.Serializator serializator)
            : base(resolveType)
        {
            this.Serializator = serializator;
            InstanceFactory = FactoryUtility.CreateUninitializedFactory(resolveType);
            Constructor = resolveType.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, ConstructorSignature, null);
            
            var resolverStorage = Serializator.ResolverBank.Storage;
            var serializationInfoConvention = Serializator.TypeManager.ConventionOf(typeof(SerializationInfo));
            if (!resolverStorage.ContainsResolverFor(serializationInfoConvention))
                resolverStorage.AddResolver(new SerializationInfoResolver(serializator), serializationInfoConvention);
        }
    }
}
