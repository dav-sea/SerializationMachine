using System;
using System.Xml.Linq;
using SerializationMachine.Utility;

namespace SerializationMachine
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

            //Serializator.AutoResolve(root);
            Serializator.HeapResolve(root);
            
            var package = new XElement("SMPackage");

            package.Add(new XAttribute("Root",Serializator.GetHeapManager().Managed.GuidOf(root).ToString()));
            PackageUtility.PackToInternal(package, Serializator.GetTypeManager().UsingDictionary, Serializator.GetHeapManager().Serialized);

            return package;
        }
        public object Deserialize(XElement package)
        {
            Serializator.FlashHeaps();

            TypeDictionary.LoadTypes(Serializator.GetTypeManager().UsingDictionary, PackageUtility.GetTypeDictionaryInternal(package));
            PackageUtility.UnpackSerializedHeap(PackageUtility.GetSerializedHeapInternal(package), Serializator.GetHeapManager().Serialized);

            var rootGuid = new Guid(package.Attribute("Root").Value);
            return Serializator.HeapDeresolve(Serializator.GetHeapManager().Serialized.ValueOf(rootGuid));
        }
        public void ReserveConvention(Type type,string convention)
        {
            Serializator.GetTypeManager().ReserveConvention(type, convention);
        }
    }


}
