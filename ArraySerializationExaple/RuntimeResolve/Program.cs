using System;
using SerializationMachine;

namespace RuntimeResolve
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            var machine = new SerializeMachine();
            var data = new MyClass();
            data.SetFields(int.MaxValue, long.MinValue, DateTime.Now);

            var serialzied = machine.Serialize(data);
            var deserialized = machine.Deserialize(serialzied) as MyClass;

            Console.WriteLine("Editable - " + data.Editable.ToString());
            Console.WriteLine("Somelong - " + deserialized.GetSomelong().ToString());
            Console.WriteLine("ISerializableObject - " + deserialized.GetISerializableObject());
        }

        [Serializable]
        public class MyClass
        {
            internal int Editable;
            protected long Somelong;
            private DateTime ISerializableObject;
            private NustredStruct nustrd;

            public long GetSomelong() 
            {
                return Somelong;
            }

            public DateTime GetISerializableObject()
            {
                return ISerializableObject;
            }
            public void SetFields(int editable, long somelong, DateTime iSerializableObject)
            {
                Editable = editable;
                Somelong = somelong;
                ISerializableObject = iSerializableObject;

            }
            public MyClass(int editable, long somelong, DateTime iSerializableObject)

            public struct NustredStruct
            {
                public int IntField;
                public ushort UInt16Field;
            }
        }
    }
}
