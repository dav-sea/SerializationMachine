using System;
using SerializeMachine.Core;

namespace SerializeMachine.Resolvers
{
    public abstract class Resolver : IResolver
    {
        protected readonly Type ResolveType;
        protected readonly Serializator Serializator;

        protected Resolver(Serializator serializator, Type resolveType)
        {
            this.Serializator = serializator;
            this.ResolveType = resolveType;
        }

        public abstract void Serialize(System.Xml.Linq.XElement serialized, object resolveObject);
        public abstract void Deserialzie(System.Xml.Linq.XElement serializedObject,ref object instance);

        public override bool Equals(object obj)
        {
            if (obj == this) return true;

            Resolver resolver = obj as Resolver;
            if (resolver == null) return false;

            return resolver.ResolveType == ResolveType && resolver.Serializator == Serializator; 
        }
        public override int GetHashCode()
        {
            return ResolveType.GetHashCode() ^ Serializator.GetHashCode();
        }
        public override string ToString()
        {
            return string.Format("Resolver: {ResolveType = {0}}",ResolveType.FullName);
        }
    }
}
