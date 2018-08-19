using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Linq;

using SerializeMachine.Core;
using SerializeMachine.Utility;

namespace SerializeMachine.Resolvers
{
    public sealed class RuntimeResolver : Core.IResolver
    {
        private readonly Type ResolveType;
        private readonly Serializator Serializator;
        private readonly FieldInfo[] Fields;

        public void Serialize(XElement serialized, object obj)
        {
            
        }

        public object Deserialzie(XElement serialized)
        {
            return null;
        }

        public RuntimeResolver(Type resolveType,Serializator serializator)
        {
            if (resolveType != null)
            {
                this.ResolveType = resolveType;
                this.Serializator = serializator;
                Fields = SerializationUtility.GetSerializableFieldsInternal(resolveType).ToArray();
            }
        }
    }
}
