using System;
using SerializationMachine;

namespace Testing
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            var machine = new SerializeMachine();
            int? data = null;
            var serialized = machine.Serialize(data);

            var deserialized = machine.Deserialize(serialized);
            Console.WriteLine(deserialized == null);

        }
    }
}
