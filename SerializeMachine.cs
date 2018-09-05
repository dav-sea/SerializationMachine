using System;
using System.Collections.Generic;
using SerializeMachine.Core;
using System.Xml.Linq;
using SerializeMachine.Utility;

namespace SerializeMachine
{

    /* XML SAMPLE
     * 
     * <?xml>
     * <SerializeMachine Root="ffffffffffffffff">
     *      <TypeDictionary>
     *          <INT>System.Int32</INT>
     *          <PNT>System.Drawing.Point</PNT>
     *          <T0>User.RectangleReference</T0>
     *      </TypeDictionary>
     *      <Heap>
     *          <T0 GUID="ffffffffffffffff">
     *              <PNT Field="Position">
     *                  <INT>100</PNT>
     *                  <INT>200</PNT>
     *              </PNT>
     *              <T0 Field="Parent">fffffffffffffffe</T0>
     *          <T0>
     *          <T0 GUID="fffffffffffffffe">
     *              <PNT Field="Position">
     *                  <INT>-5</PNT>
     *                  <INT>0</PNT>
     *              </PNT>
     *              <T0 Field="Parent">0000000000000000</T0>
     *          <T0>
     *      </Heap>
     * </SerializeMachine>
     */

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
        
        //public object Deserialize(XElement serializedRoot)
        //{
          //  Serializator.FlashHeap();

        //}
    }


}
