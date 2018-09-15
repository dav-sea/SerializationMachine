using System;
using System.Collections.Generic;
using SerializeMachine.Core;
using System.Xml.Linq;
using SerializeMachine.Utility;

namespace SerializeMachine
{
    public sealed class SerializeMachine
    {
        Serializator Serializator;

        public SerializeMachine()
        {
            Serializator = new Serializator();

            var storage = Serializator.ResolverBank.Storage;
            
        }

        public XElement Serialize(object root)
        {
            Serializator.FlashHeaps();

            Serializator.AutoResolve(root);
            
            var package = new XElement("SMPackage");

            package.Add(new XAttribute("Root",Serializator.HeapManager.Managed.GuidOf(root).ToString()));
            PackageUtility.PackToInternal(package, Serializator.TypeManager.Dictionary, Serializator.HeapManager.Serialized);

            return package;
        }
        public object Deserialize(XElement package)
        {
            Serializator.FlashHeaps();

            Serializator.TypeManager.Dictionary.Clear();
            Serializator.TypeManager.Dictionary.OverloadTypes(PackageUtility.GetTypeDictionaryInternal(package));
            //Serializator.HeapManager.Serialized.LoadHeapSerialized(PackageUtility.GetSerializedHeap(package),true);
            PackageUtility.UnpackSerializedHeap(PackageUtility.GetSerializedHeapInternal(package), Serializator.HeapManager.Serialized);

            var rootGuid = new Guid(package.Attribute("Root").Value);

           

            return Serializator.AutoDeresolve(Serializator.HeapManager.Serialized.ValueOf(rootGuid));
        }
    }


}
