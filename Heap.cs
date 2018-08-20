using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SerializeMachine
{
    public sealed class Heap
    {
        private readonly SortedList<Guid, object> HeapList;//TODO OPTIMIZE FOR TYPE
        //private readonly IGUIDFactory GUIDFactory;

        public Heap(int heapCapacity = 50)
        {
            //this.GUIDFactory = guidFactory;
            HeapList = new SortedList<Guid, object>(heapCapacity);
        }

        public Guid GuidOf(object obj)
        {
            var index = HeapList.IndexOfValue(obj);
            return index < 0 ? Guid.Empty : HeapList.Keys[index];
        }
        public bool TryGetGuid(object obj, out Guid guid)
        {
            var index = HeapList.IndexOfValue(obj);
            if (index < 0)
            {
                guid = TypeDictionary.GUID_NULL;
                return false;
            }
            guid = HeapList.Keys[index];
            return true;
        }
        public bool Contains(object obj)
        {
            return HeapList.ContainsValue(obj);
        }
        public bool Contains(Guid guid)
        {
            return HeapList.ContainsKey(guid);
        }
        public void AddObject(object obj, Guid guid)
        {
            HeapList.Add(guid, obj);
        }
        public void DeleteObject(Guid guid)
        {
            HeapList.Remove(guid);
        }
    }
}
