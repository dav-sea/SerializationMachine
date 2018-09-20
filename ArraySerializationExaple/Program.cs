using System;
using SerializationMachine;


namespace ArraySerializationExaple
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            var machine = new SerializeMachine();
            var data = new int[]{ 1, 2000, -312, 52, 12 };
            var serialized = machine.Serialize(data);
            var deserialized = machine.Deserialize(serialized) as int[];

            foreach (var e in deserialized) Console.WriteLine(e.ToString());
        }
    }
}
