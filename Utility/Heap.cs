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

        public void CopyTo(KeyValuePair<Guid,TValue>[] array, int arrayIndex)
        {
            for (; arrayIndex < array.Length; arrayIndex++)
                mList.Add(array[arrayIndex].Key, array[arrayIndex].Value);
        }



        public void ReplaceValue(Guid guid, TValue value)
        {
            var index = mList.IndexOfKey(guid);
            if (index < 0) mList.Add(guid, value);
            mList.Values[index] = value;
        }

        public void Add(Guid guid, TValue value)
        {
            mList.Add(guid, value);
        }
        void Add(KeyValuePair<Guid,TValue> pair)
        {
            mList.Add(pair.Key, pair.Value);
        }
        public bool Remove(Guid guid)
        {
            return mList.Remove(guid);
        }
        bool Remove(KeyValuePair<Guid,TValue> pair)
        {
            return mList.Remove(pair.Key);
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
        //public IDictionary<Guid, TValue> ToDictionary()
        //{
        //    var dictionary = new Dictionary<Guid, TValue>(Count);
        //    var mListEnumerator = mList.GetEnumerator();
        //    while (mListEnumerator.MoveNext())
        //        dictionary.Add(mListEnumerator.Current.Key, mListEnumerator.Current.Value);
        //    return dictionary;
        //}

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

        public void Clear()
        {
            mList.Clear();
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

        void IHeap<TValue>.Add(Guid guid, TValue value)
        {
            mList.Add(guid, value);
        }

        bool IHeap<TValue>.Remove(Guid guid)
        {
            return mList.Remove(guid);
        }

        int IHeap<TValue>.GetHeapSize()
        {
            return mList.Capacity;
        }

        void IHeap<TValue>.SetHeapSize(int size)
        {
            if (size < mList.Capacity) return;
            mList.Capacity = size;
        }

        void ICollection<KeyValuePair<Guid, TValue>>.Add(KeyValuePair<Guid, TValue> item)
        {
            mList.Add(item.Key, item.Value);                    
        }

        void ICollection<KeyValuePair<Guid, TValue>>.Clear()
        {
            mList.Clear();
        }

        bool ICollection<KeyValuePair<Guid, TValue>>.Contains(KeyValuePair<Guid, TValue> item)
        {
            var index = mList.IndexOfKey(item.Key);
            if (index < 0) return false;
            return mList.Keys[index] == item.Key && item.Value != null && item.Value.Equals(mList.Values[index]);
        }

        void ICollection<KeyValuePair<Guid, TValue>>.CopyTo(KeyValuePair<Guid, TValue>[] array, int arrayIndex)
        {
            for (; arrayIndex < array.Length; arrayIndex++)
                mList.Add(array[arrayIndex].Key, array[arrayIndex].Value);
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
    }
}
