using System;
using System.Collections.Generic;

namespace SerializeMachine.Utility
{
    public sealed class Heap<TValue> //: IHeap<TValue>
    {
        private readonly SortedList<Guid, TValue> mList;

        public int Count
        {
            get { return mList.Count; }
        }
        
        public bool TryGetGuid(TValue value, out Guid guid)
        {
            var index = mList.IndexOfValue(value);
            if (index < 0)
            {
                guid = Guid.Empty;
                return false;
            }
            guid = mList.Keys[index];
            return true;
        }
        public bool TryGetValue(Guid guid, out TValue value)
        {
            return mList.TryGetValue(guid, out value);
        }

        public Guid GuidOf(TValue value)
        {
            var index = mList.IndexOfValue(value);
            if (index < 0) return Guid.Empty;
            return mList.Keys[index];
        }
        public TValue ValueOf(Guid guid)
        {
            var index = mList.IndexOfKey(guid);
            if (index < 0) return default(TValue);
            return mList.Values[index];
        }

        public void Add(Guid guid, TValue value)
        {
            mList.Add(guid, value);
        }
        public bool Remove(Guid guid)
        {
            return mList.Remove(guid);
        }
        public int GetHeapSize()
        {
            return mList.Capacity;
        }

        public void SetHeapSize(int size)
        {
            if (size < Count) return;
            mList.Capacity = size;
        }
        public IDictionary<Guid, TValue> ToDictionary()
        {
            var dictionary = new Dictionary<Guid, TValue>(Count);
            var mListEnumerator = mList.GetEnumerator();
            while (mListEnumerator.MoveNext())
                dictionary.Add(mListEnumerator.Current.Key, mListEnumerator.Current.Value);
            return dictionary;
        }
        public Heap(int startHeapSize)
        {
            if (startHeapSize < 0) throw new ArgumentException("startHeapSize cannot be negative", "startHeapSize");
            mList = new SortedList<Guid, TValue>(startHeapSize);
        }

        public bool ContainsGuid(Guid guid)
        {
            return mList.ContainsKey(guid);
        }
        public bool ContainsValue(TValue value)
        {
            return mList.ContainsValue(value);
        }

        public void ClearHeap()
        {
            mList.Clear();
        }
    }
}
