using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SerializeMachine.Utility
{
    public interface IHeap<TValue> : IDictionary<Guid,TValue>
    {
        int Count { get; }

        bool TryGetGuid(TValue value, out Guid guid);
        bool TryGetValue(Guid guid, out TValue value);

        void Add(Guid guid, TValue value);
        bool Remove(Guid guid);

        bool ContainsGuid(Guid guid);
        bool ContainValue(TValue value);

        int GetHeapSize();
        void SetHeapSize(int size);
    }
}
