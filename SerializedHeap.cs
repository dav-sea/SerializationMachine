using System;
using System.Collections.Generic;
using System.Xml.Linq;
using SerializeMachine.Utility;

namespace SerializeMachine
{
    
    //public sealed class SerializedHeap
    //{
    //    public const string XML_ELEMENTNAME_SERIALIZEDHEAP = "HEAP";
    //    //private const string XML_ELEMENTNAME_ELEMENT = "E";

    //    private readonly Heap<object> ManagedHeap;

    //    private readonly SortedList<Guid, XElement> HeapList;

    //    public void Push(Guid guid, XElement serialized)
    //    {
    //        var index = HeapList.IndexOfKey(guid);
    //        if (index < 0)
    //            HeapList.Add(guid, serialized);
    //        else HeapList.Values[index] = serialized;
    //    }
    //    public void SafeAdd(Guid guid, XElement serialized)
    //    {
    //        if (HeapList.IndexOfKey(guid) < 0)
    //            HeapList.Add(guid, serialized);
    //    }

    //    public bool TryGetSerialized(Guid guid, out XElement serialized)
    //    {
    //        return HeapList.TryGetValue(guid,out serialized);
    //    }

    //    public XElement GetSerialized(Guid guid)
    //    {
    //        XElement result;
    //        HeapList.TryGetValue(guid,out result);
    //        return result;
    //    }

    //    public SerializedHeap(Heap<object> heap)
    //    {
    //        this.ManagedHeap = heap;
    //        this.HeapList = new SortedList<Guid, XElement>();//TODO typeDictionary replace to heap???? capcacity too
    //    }

    //    public Heap<object> GetManagedHeap()
    //    {
    //        return ManagedHeap;
    //    }

    //    public void ClearSerialized()
    //    {
    //        HeapList.Clear();//TODO?
    //    }

    //    public void LoadHeapSerialized(XElement serializedHeap,bool overloadSerialized)
    //    {
    //        if (serializedHeap == null || serializedHeap.Name != XML_ELEMENTNAME_SERIALIZEDHEAP) return;

    //        if (overloadSerialized)
    //            foreach (var element in serializedHeap.Elements())
    //                Push(new Guid(XMLUtility.GuidOfAttributeInternal(element)), element);
    //        else
    //            foreach (var element in serializedHeap.Elements())
    //                SafeAdd(new Guid(XMLUtility.GuidOfAttributeInternal(element)), element);
    //    }

    //    public IDictionary<Guid, XElement> ToDictionary()
    //    {
    //        return HeapList;
    //    }

    //    public static XElement CreateSerializedHeap(SerializedHeap heap)
    //    {
    //        if (heap == null) throw new ArgumentNullException("heap");
    //        var target = heap.ToDictionary();
    //        if (target == null) throw new InvalidOperationException("heap method ToDictionary() return null");
    //        return CreateSerializedHeap(target);
    //    }
    //    internal static XElement CreateSerializedHeap(IDictionary<Guid, XElement> heap)
    //    {
    //        var serialized = new XElement(XML_ELEMENTNAME_SERIALIZEDHEAP);
    //        XElement element;

    //        foreach (var pair in heap)
    //        {
    //            XMLUtility.AttachGUIDInternal(pair.Value, pair.Key.ToString());
    //            serialized.Add(pair.Value);
    //        }

    //        return serialized;
    //    }
    //}
}
