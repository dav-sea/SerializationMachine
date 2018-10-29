using System;
using System.Xml.Linq;

namespace SerializationMachine.Utility
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
        public static XElement CreateNode(string convention)
        {
            return new XElement(convention);
        }

        
        public static Guid GuidOfAttribute(XElement node)
        {
            if (node == null) return Serializator.GUID_NULL;
            return new Guid(GuidOfAttributeInternal(node));    
        }
        internal static string GuidOfAttributeInternal(XElement node)
        {
            var attribute = node.Attribute(Serializator.XML_ATTRIBUTENAME_GUID);
            return attribute == null ? Serializator.GUID_NULL_TOSTRING : attribute.Value;  
        }
        public static Guid GuidOfValue(XElement node)
        {
            if (node == null) return Serializator.GUID_NULL;
            return new Guid(node.Value);
        }
        internal static string GuidOfValueInternal(XElement node)
        {
            return node.Value;
        }

        internal static bool GUIDAttributeConatins(XElement node)
        {
            return node.Attribute(Serializator.XML_ATTRIBUTENAME_GUID) != null;
        }


        internal static bool IsNullOf(XElement node)
        {
            return node.Name.LocalName == Serializator.XML_ELEMENTNAME_NULL;
        }
        internal static XElement CreateReferenceNode(string convention, string guid)
        {
            return new XElement(convention, guid);
        }
        internal static XElement CreateNullNode()
        {
            return new XElement(Serializator.XML_ELEMENTNAME_NULL);
        }
    }
}
