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
        private readonly Heap Heap;
        private readonly TypeDictionary TypeDictionary;
        private readonly ResolverStorage ResolverStorage;
        private readonly SerializedHeap SerializedHeap;

        public SerializeMachine()
        {
            TypeDictionary = new TypeDictionary(20);
            ResolverStorage = new ResolverStorage(TypeDictionary);
            Heap = new Heap(20);
        }
    }


}
