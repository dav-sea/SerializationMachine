﻿using System.Xml.Linq;

namespace SerializationMachine.Resolvers
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

        protected internal override object GetTemplateInstance(XElement serializedObject)
        {
            return new object();
        }
    }
}