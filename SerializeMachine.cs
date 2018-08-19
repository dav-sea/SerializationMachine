using System;
using System.Collections.Generic;
using SerializeMachine.Core;
using System.Xml.Linq;


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
            var typeDictionary = Serializator.TypeDictionary;
            var storage = Serializator.ResolverStorage;
            typeDictionary.AddConvention(typeof(int), "INT");
            storage.AddResolver(new Resolvers.Primitives.IntegerResolver(), "INT");
            
        }

        public XElement Serialize(object root)
        {
            var package = new XElement("SMPackage");

            package.Add(new XAttribute("Root",Serializator.Heap.GetOriginalHeap().GuidOf(root).ToString()));
            package.Add(TypeDictionary.CreateSerializedTypeDictionary(Serializator.TypeDictionary.ToDictionary()));
            package.Add(SerializedHeap.CreateSerializedHeap(Serializator.Heap.ToDictionary()));

            return package;
        }
        internal XElement SerializeRoot(object root)
        {
            Serializator.FlashHeap();
            return Serializator.GetSerialized(root);
        }
        //public object Deserialize(XElement serializedRoot)
        //{
          //  Serializator.FlashHeap();

        //}
    }


}
