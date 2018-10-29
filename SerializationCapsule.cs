using System;
using System.Collections.Generic;

namespace SerializationMachine
{
    public class SerializationCapsule
    {
        Serializator Serializator;

        private readonly SortedList<string, Guid> KeysList;

        void UpdateSerializeObject(object obj)
        {
            UpdateSerializeObject(obj, Serializator.GetHeapManager().GetCreateGuid(obj));
        }
        void UpdateSerializeObject(object obj, Guid guid)
        {
            var serialized = Serializator.Resolve(obj);
            Serializator.GetHeapManager().Serialized.ReplaceValue(guid, serialized);
        }
    }
}
