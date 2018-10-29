using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Runtime.Serialization;
using System.Reflection;

using SerializationMachine.Utility;

namespace SerializationMachine.Resolver.Resolvers
{
    public sealed class SerializableResolver : IResolver
    {
        private const string XML_ATTRIBUTE_NAME = "NAME";
        private readonly Serializator Serializator;
        private readonly ConstructorInfo Constructor;
        private static readonly Type[] ConstructorSignature = new Type[]{typeof(SerializationInfo),typeof(StreamingContext)};
        private readonly ITemplateInstanceFactory InstanceFactory;

        public override void Serialize(XElement serialized, object instance)
        {
            var serializableObject = instance as ISerializable;

            var serializationInfo = new SerializationInfo(ResolveType, new FormatterConverter());
            serializableObject.GetObjectData(serializationInfo, new StreamingContext());

            Serializator.ResolveTo(serializationInfo, serialized);
        }

        public override void Deserialzie(XElement serialized, ref object instance)
        {
            var serializationInfo = new SerializationInfo(ResolveType, new FormatterConverter());
            Serializator.DeresolveTo(serializationInfo, serialized);

            Constructor.Invoke(instance, new object[] { serializationInfo, new StreamingContext() });
        }

        protected internal override object GetTemplateInstance(XElement serialized)
        {
            return InstanceFactory.Instantiate(serialized);   
        }

        private XElement SerializeEntry(SerializationEntry entry)
        {
            var serialized = Serializator.AutoResolve(entry.Value);
            serialized.SetAttributeValue(XML_ATTRIBUTE_NAME, entry.Name);
            return serialized;
        }

        public SerializableResolver(Serializator serializator, Type resolveType)
            : base(resolveType)
        {
            if (serializator == null) throw new ArgumentNullException($"{nameof(SerializableResolver)}. Initializing was failed: serializator cant be null");
            if (resolveType == null) throw new ArgumentNullException($"{nameof(SerializableResolver)}. Initializing was failed: resolveType cant be null");

            Serializator = serializator;
            InstanceFactory = new UninitializedInstanceFactory(resolveType);

            Constructor = resolveType.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, ConstructorSignature, null);
            if (Constructor == null) throw new NotImplementedException($"{nameof(SerializableResolver)}. Initializing was failed: resolveType not implemented constructor with <SerializationInfo>, <StreamingContext> parameters");

            var resolverStorage = Serializator.GetResolverManager().Storage;
            var serializationInfoConvention = Serializator.GetTypeManager().ConventionOf(typeof(SerializationInfo));
            resolverStorage.SetResolver(new SerializationInfoResolver(serializator), serializationInfoConvention);
        }
    }
}
