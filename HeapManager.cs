using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SerializeMachine.Core;

namespace SerializeMachine
{
    public sealed class HeapManager
    {
        private readonly Heap OriginalHeap;
        private readonly SerializedHeap SerializedHeap;

        public Heap Original { get { return OriginalHeap; } }
        public SerializedHeap Serialized {  get { return SerializedHeap;} }

        public bool GetCreateGuid(object obj, out Guid guid)
        {
            if (!OriginalHeap.TryGetGuid(obj, out guid))
            {
                guid = Guid.NewGuid();
                return true;
            }
            return false;
        }
        public Guid GetCreateGuid(object obj)
        {
            Guid guid;
            if (!OriginalHeap.TryGetGuid(obj, out guid))
                guid = Guid.NewGuid();
            return guid;
        }
        public object GetObject(Guid guid)
        {
            return OriginalHeap.ObjectOf(guid);
        }

        public HeapManager(Serializator serializator)
        {
            OriginalHeap = new Heap(50);
            SerializedHeap = new SerializedHeap(OriginalHeap);
        }
        public void FlashHeaps()
        {
            SerializedHeap.ClearSerialized();
            OriginalHeap.ClearHeap();
        }
    }
}
