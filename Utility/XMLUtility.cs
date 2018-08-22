using System;
using System.Collections.Generic;
using System.Xml.Linq;

using SerializeMachine.Core;
namespace SerializeMachine.Utility
{
    public static class XMLUtility
    {
        public static void AttachGUID(XElement node,Guid guid)
        {
            if(node != null)
                node.SetAttributeValue(Serializator.XML_ATTRIBUTENAME_GUID,guid.ToString());
        }
        internal static void AttachGUIDInternal(XElement node, string guid)
        {
            node.SetAttributeValue(Serializator.XML_ATTRIBUTENAME_GUID, guid);
        }
        public static Guid GuidOf(XElement node)
        {
            if (node == null) return Serializator.GUID_NULL;
            return new Guid(GuidOfInternal(node));    
        }
        internal static string GuidOfInternal(XElement node)
        {
            var attribute = node.Attribute(Serializator.XML_ATTRIBUTENAME_GUID);
            return attribute == null ? string.Empty : attribute.Value;
                
        }
    }
}
