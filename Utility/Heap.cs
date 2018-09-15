using System;
using System.Collections;
using System.Collections.Generic;

namespace SerializeMachine.Utility
{
    public sealed class Heap<TValue> : IHeap<TValue>
    {
        private readonly SortedList<Guid, TValue> mList;

        public int Count { get { return mList.Count; }}
        public bool IsReadOnly { get { return false; }}

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

        public void ReplaceValue(Guid guid, TValue value)
        {
            var index = mList.IndexOfKey(guid);
            if (index < 0) mList.Add(guid, value);
            mList[guid] = value;
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

        public bool ContainsGuid(Guid guid)
        {
            return mList.ContainsKey(guid);
        }
        public bool ContainsValue(TValue value)
        {
            return mList.ContainsValue(value);
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

        public void CopyTo(KeyValuePair<Guid, TValue>[] array, int arrayIndex)
        {
            for (; arrayIndex < array.Length; arrayIndex++)
                mList.Add(array[arrayIndex].Key, array[arrayIndex].Value);
        }
        public void Clear()
        {
            mList.Clear();
        }
        
        bool ICollection<KeyValuePair<Guid, TValue>>.Contains(KeyValuePair<Guid, TValue> item)
        {
            var index = mList.IndexOfKey(item.Key);
            if (index < 0) return false;
            return mList.Keys[index] == item.Key && item.Value != null && item.Value.Equals(mList.Values[index]);
        }
        
        void ICollection<KeyValuePair<Guid, TValue>>.Add(KeyValuePair<Guid, TValue> item)
        {
            mList.Add(item.Key, item.Value);
        }
        bool ICollection<KeyValuePair<Guid, TValue>>.Remove(KeyValuePair<Guid, TValue> item)
        {
            return mList.Remove(item.Key);
        }

        IEnumerator<KeyValuePair<Guid, TValue>> IEnumerable<KeyValuePair<Guid, TValue>>.GetEnumerator()
        {
            return mList.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return mList.GetEnumerator();
        }

        public Heap(int startHeapSize)
        {
            if (startHeapSize < 0) throw new ArgumentException("startHeapSize cannot be negative", "startHeapSize");
            mList = new SortedList<Guid, TValue>(startHeapSize);
        }
    }
}
