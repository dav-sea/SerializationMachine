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

            var storage = Serializator.GetResolverManager().Storage;
            
        }

        public XElement Serialize(object root)
        {
            Serializator.FlashHeaps();

            Serializator.AutoResolve(root);
            
            var package = new XElement("SMPackage");

            package.Add(new XAttribute("Root",Serializator.GetHeapManager().Managed.GuidOf(root).ToString()));
            PackageUtility.PackToInternal(package, Serializator.GetTypeManager().Dictionary, Serializator.GetHeapManager().Serialized);

            return package;
        }
        public object Deserialize(XElement package)
        {
            Serializator.FlashHeaps();

            //Serializator.GetTypeManager().Dictionary.Clear();
            //Serializator.GetTypeManager().Dictionary.OverloadTypes(PackageUtility.GetTypeDictionaryInternal(package));
            TypeDictionary.LoadTypes(Serializator.GetTypeManager().Dictionary, PackageUtility.GetTypeDictionaryInternal(package));
            PackageUtility.UnpackSerializedHeap(PackageUtility.GetSerializedHeapInternal(package), Serializator.GetHeapManager().Serialized);

            var rootGuid = new Guid(package.Attribute("Root").Value);
            return Serializator.AutoDeresolve(Serializator.GetHeapManager().Serialized.ValueOf(rootGuid));
        }
        public void ReserveConvention(Type type,string convention)
        {
            //Serializator.GetTypeManager().InvalidTypeDictionary.OverloadConvention(type, convention);
            Serializator.GetTypeManager().ReserveConvention(type, convention);
        }
    }


}
