using System;
using System.Runtime.Serialization;
using System.Xml.Linq;
using SerializationMachine.Core;
using SerializationMachine.Utility;

namespace SerializeMachine.Resolvers
{
    public sealed class MultirankArrayResolver : IResolver
    {
        private const string XML_NAME_DIMENTION_CONTAINER = "_";
        private const string XML_ATTRIBUTE_DIMENSION = "DIMENSION";

        public readonly int Rank;
        public readonly Type ElementType;
        public readonly Serializator Serializator;

        public MultirankArrayResolver(Type resolveType, Serializator serializator) : base(resolveType)
        {
            if (!resolveType.IsArray) throw new ArgumentException($"MultirankArrayResolver. Initializing was failed: {nameof(resolveType)} is not array");
            ElementType = resolveType.GetElementType();
            Rank = resolveType.GetArrayRank();
            Serializator = serializator ?? throw new ArgumentNullException($"MultirankArrayResolver. Initializing was failed: ");
        }

        public override void Deserialzie(XElement serialized, ref object instance)
        {
            var resolveArray = instance as Array;
            var indexator = new int[Rank];

            DimensionDeresolve(serialized.Element(XML_NAME_DIMENTION_CONTAINER), resolveArray, indexator, indexator.Length - 1);
        }

        public override void Serialize(XElement serialized, object instance)
        {
            var resolveArray = instance as Array;
            var indexator = new int[Rank];

            DimensionResolve(serialized, resolveArray, indexator, indexator.Length - 1);

            SetDimensions(serialized, resolveArray);
        }

        protected internal override object GetTemplateInstance(XElement serializedObject)
        {
            var dimensions = GetDimensions(serializedObject, Rank);

            return Array.CreateInstance(ElementType, dimensions);
        }

        int[] GetDimensions(XElement serializedObject, int rank)
        {
            var dimensions = new int[rank];
            for (int i = 0; i < dimensions.Length; i++)
            {
                var dimensionAttribute = serializedObject.Attribute(XML_ATTRIBUTE_DIMENSION + i.ToString());
                if (dimensionAttribute == null)
                    throw new SerializationException($"MultirankArrayResolver: Dimension length read was failed: attribute <{XML_ATTRIBUTE_DIMENSION}> not contains");

                int dimensionLength;
                if (!int.TryParse(dimensionAttribute.Value, out dimensionLength))
                    throw new SerializationException($"MultirankArrayResolver. Dimension length read was failed: value <{XML_ATTRIBUTE_DIMENSION}> is not 32-bit integer number");

                dimensions[i] = dimensionLength;
            }
            return dimensions;
        }
        void SetDimensions(XElement serializedObject, Array array)
        {
            var rank = array.Rank;
            for (int i = 0; i < rank; i++)
                serializedObject.SetAttributeValue(XML_ATTRIBUTE_DIMENSION + i.ToString(), array.GetLength(i).ToString());//MB USE INT RESOLVER?..
        }

        void DimensionResolve(XElement node, Array array, int[] indexator, int Dimension)
        {
            var newNode = new XElement(XML_NAME_DIMENTION_CONTAINER);

            if (Dimension == 0)
            {
                FirstDimensionResolve(newNode, array, indexator);
            }
            else
            {
                for (; indexator[Dimension] < array.GetLength(Dimension); indexator[Dimension]++)
                {
                    DimensionResolve(newNode, array, indexator, Dimension - 1);
                    CutIndexator(indexator, Dimension);
                }
            }

            node.Add(newNode);
        }
        void FirstDimensionResolve(XElement node, Array array, int[] indexator)
        {
            var length = array.GetLength(0);
            for (; indexator[0] < length; indexator[0]++)
                node.Add(Serializator.AutoResolve(array.GetValue(indexator)));
        }
        void DimensionDeresolve(XElement node, Array array, int[] indexator, int Dimension)
        {
            if (Dimension == 0)
            {
                FirstDimensionDeresolve(node, array, indexator);
            }
            else
            {
                var nodeEnumerator = node.Elements().GetEnumerator();
                while (nodeEnumerator.MoveNext() && indexator[Dimension] < array.GetLength(Dimension))
                {
                    DimensionDeresolve(nodeEnumerator.Current, array, indexator, Dimension - 1);
                    CutIndexator(indexator, Dimension);
                    indexator[Dimension]++;
                }
            }
            
        }
        void FirstDimensionDeresolve(XElement node, Array array, int[] indexator)
        {
            var length = array.GetLength(0);
            var nodeEnumerator = node.Elements().GetEnumerator();
            while (nodeEnumerator.MoveNext() && indexator[0] < length)
            {
                array.SetValue(Serializator.AutoDeresolve(nodeEnumerator.Current), indexator);
                indexator[0]++;
            }
        }

        void CutIndexator(int[] indexator, int cutDimension)
        {
            for (int i = 0; i < cutDimension; i++)
                indexator[i] = 0;
        }
    }
}
