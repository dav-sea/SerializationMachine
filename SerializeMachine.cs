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
            Serializator.TypeManager.Dictionary.AddConvention(typeof(int), "INT");
            storage.AddResolver(new Resolvers.Primitives.IntegerResolver(), "INT");
            
        }

        public XElement Serialize(object root)
        {
            Serializator.FlashHeap();

            var rootGuid = Guid.NewGuid();
            Serializator.HeapManager.Original.AddObject(root, rootGuid);
            var serialized = Serializator.Resolve(root);
            Serializator.HeapManager.Serialized.Push(rootGuid, serialized);
            
            var package = new XElement("SMPackage");

            package.Add(new XAttribute("Root",Serializator.HeapManager.Original.GuidOf(root).ToString()));
            PackageUtility.PackToInternal(package, Serializator.TypeManager.Dictionary, Serializator.HeapManager.Serialized);

            return package;
        }
        public object Deserialize(XElement package)
        {
            return null;
        }
        
        //public object Deserialize(XElement serializedRoot)
        //{
          //  Serializator.FlashHeap();

        //}
    }


}
