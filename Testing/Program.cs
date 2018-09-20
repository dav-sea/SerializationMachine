using System;
using SerializationMachine;

namespace Testing
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            var machine = new SerializeMachine();
            var data = new testmain();
            var serialized = machine.Serialize(data);

            var deserialized = machine.Deserialize(serialized);
            Console.WriteLine(deserialized);

        }
    }

    [Serializable]
    class test 
    {
        int asd = 21;
    }
    [Serializable]
    class testmain : test
    {
        int dsa = 12;
    }
}
