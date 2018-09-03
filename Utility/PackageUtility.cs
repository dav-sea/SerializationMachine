using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace SerializeMachine.Utility
{
    public static class PackageUtility
    {
        internal static void PackToInternal(XElement to, TypeDictionary dictionary, Heap<XElement> heap)
        {
            to.Add(TypeDictionary.CreateSerializedTypeDictionary(dictionary.ToDictionary()));
            to.Add(SerializedHeap.CreateSerializedHeap(heap.ToDictionary()));
        }
        internal static XElement GetTypeDictionary(XElement package)
        {
            return package.Element(TypeDictionary.XML_ELEMENTNAME_TYPEDICTIONARY);
        }
        internal static XElement GetSerializedHeap(XElement package)
        {
            return package.Element(SerializedHeap.XML_ELEMENTNAME_SERIALIZEDHEAP);
        }

        internal static XElement PackSerializedHeap(IHeap<XElement> heap)
        {

        }
    }
}
