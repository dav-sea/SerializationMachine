using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SerializationMachine
{
    public interface IHeap<TValue> : ICollection<KeyValuePair<Guid,TValue>>
    {
        bool TryGetGuid(TValue value, out Guid guid);
        bool TryGetValue(Guid guid, out TValue value);

        void Add(Guid guid, TValue value);
        bool Remove(Guid guid);

        int GetHeapSize();
        void SetHeapSize(int size);
    }
}
