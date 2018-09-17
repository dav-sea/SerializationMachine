using System;
using System.Collections.Generic;
using System.Xml.Linq;


using SerializeMachine.Utility;
using SerializeMachine.Core;

namespace SerializeMachine
{
    public sealed class HeapManager
    {
        private readonly Heap<object> ManagedHeap;
        private readonly Heap<XElement> SerializedHeap;

        public Heap<object> Managed { get { return ManagedHeap; } }
        public Heap<XElement> Serialized { get { return SerializedHeap; } }

        public bool GetCreateGuid(object obj, out Guid guid)
        {
            if (!ManagedHeap.TryGetGuid(obj, out guid))
            {
                guid = Guid.NewGuid();
                ManagedHeap.Add(guid , obj);
                return true;
            }
            return false;
        }
        public Guid GetCreateGuid(object obj)
        {
            Guid guid;
            GetCreateGuid(obj,out guid);
            return guid;
        }
        public object GetObject(Guid guid)
        {
            return ManagedHeap.ValueOf(guid);
        }

        public HeapManager()
        {
            ManagedHeap = new Heap<object>(50);
            SerializedHeap = new Heap<XElement>(50);
        }
        public void FlashHeaps()
        {
            SerializedHeap.Clear();
            ManagedHeap.Clear();
        }
    }
}
