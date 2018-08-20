using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace SerializeMachine.Core
{
    public sealed class Serializator
    {
        public const string XATTRIBUTE_GUID = "GUID";

        public SerializedHeap Heap;
        public ResolverStorage ResolverStorage;
        public TypeDictionary TypeDictionary;

        public void FlashHeap()
        {
            Heap.ClearSerialized();
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
        public XElement GetFieldSerialized(object obj)
        {
            var convention = TypeDictionary.ConventionOf(obj.GetType());
            var result = TypeDictionary.GetHead(convention);
            Guid guid;
            if (Heap.GetOriginalHeap().TryGetGuid(obj, out guid))
            {
                result.Value = guid.ToString();
            }
            else
            {
                ResolveTo(obj, convention, result);
            }
            return result;
        }

        public Guid GuidOf(object obj,bool createGuid, bool forceCreateGUID)
        {
            Guid guid;
            if (!Heap.GetOriginalHeap().TryGetGuid(obj, out guid))
                if (createGuid && Utility.SerializationUtility.Targeting.IsSaveReferenceInternal(obj.GetType()) || forceCreateGUID)
                {
                    guid = Guid.NewGuid();
                    Heap.GetOriginalHeap().AddObject(obj, guid);
                }
            return guid;
        }

        public Serializator()
        {
            this.TypeDictionary = new TypeDictionary(50);
            this.ResolverStorage = new global::SerializeMachine.ResolverStorage(this.TypeDictionary);
            Heap = new SerializedHeap(new Heap(50),this.TypeDictionary,this.ResolverStorage);
        }

        private static void AttachGUID(XElement to, Guid guid)
        {
            to.SetAttributeValue(XATTRIBUTE_GUID, guid.ToString());
        }
    }
}
