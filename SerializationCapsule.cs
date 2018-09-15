using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SerializeMachine.Core;

namespace SerializeMachine
{
    public class SerializationCapsule
    {
        Serializator Serializator;

        private readonly SortedList<string, Guid> KeysList;

        void UpdateSerializeObject(object obj)
        {
            UpdateSerializeObject(obj, Serializator.HeapManager.GetCreateGuid(obj));
        }
        void UpdateSerializeObject(object obj, Guid guid)
        {
            var serialized = Serializator.Resolve(obj);
            Serializator.HeapManager.Serialized.ReplaceValue(guid, serialized);
        }
    }
}
