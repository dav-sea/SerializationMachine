using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace SerializeMachine.Core
{
    public sealed class Serializator
    {
        public SerializedHeap Heap;
        public ResolverStorage ResolverStorage;
        public TypeDictionary TypeDictionary;

        public void FlashHeap()
        {
            Heap.ClearSerialized();
        }

        public XElement GetSerialized(object obj)
        {
            var guid = Heap.GetOriginalHeap().GuidOf(obj);
            if (guid != TypeDictionary.GUID_NULL)
            {
                var serialized = Heap.GetSerialized(guid);
                if(serialized == null)
                    return Resolve(obj);
                return serialized;
            }
            return Resolve(obj);
        }
        public XElement Resolve(object obj)
        {
            var conventionType = TypeDictionary.ConventionOf(obj.GetType());
            var serialized = TypeDictionary.GetHead(conventionType);
            ResolveTo(obj, conventionType, serialized);
            return serialized;
        }
        public void ResolveTo(object obj, string conventionType , XElement serialized)
        {
            var resolver = ResolverStorage.GetResolver(conventionType);
            resolver.Serialize(serialized, obj);
        }
    
        public Serializator()
        {
            this.TypeDictionary = new TypeDictionary(50);
            this.ResolverStorage = new global::SerializeMachine.ResolverStorage(this.TypeDictionary);
            Heap = new SerializedHeap(new Heap(50),this.TypeDictionary,this.ResolverStorage);
        }
    }
}
