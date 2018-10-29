using System.Xml.Linq;

namespace SerializationMachine.Utility
{
    public static class PackageUtility
    {
        public const string XML_ELEMENTNAME_HEAP = "HEAP";

        internal static void PackToInternal(XElement to, TypeDictionary dictionary, Heap<XElement> heap)
        {
            to.Add(TypeDictionary.ToXML(dictionary));
            to.Add(PackSerializedHeapInternal(heap));
        }
        internal static XElement GetTypeDictionaryInternal(XElement package)
        {
            return package.Element(TypeDictionary.XML_ELEMENTNAME_TYPEDICTIONARY);
        }
        internal static XElement GetSerializedHeapInternal(XElement package)
        {
            return package.Element(XML_ELEMENTNAME_HEAP);
        }
        internal static XElement PackSerializedHeapInternal(IHeap<XElement> heap)
        {
            var pack = new XElement(XML_ELEMENTNAME_HEAP);

            var heapEnumerator = heap.GetEnumerator();

            while(heapEnumerator.MoveNext())
            {
                heapEnumerator.Current.Value.SetAttributeValue(Serializator.XML_ATTRIBUTENAME_GUID, heapEnumerator.Current.Key.ToString());
                pack.Add(heapEnumerator.Current.Value);
            }

            return pack;
        }
    
        internal static void UnpackSerializedHeap(XElement heapNode,IHeap<XElement> heap)
        {
            var elementsEnumerator = heapNode.Elements().GetEnumerator();

            while(elementsEnumerator.MoveNext())
            {
                heap.Add(XMLUtility.GuidOfAttribute(elementsEnumerator.Current), elementsEnumerator.Current);
            }
        }
    }
}
