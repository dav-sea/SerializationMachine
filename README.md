# SerializationMachine
A library for serializing objects.
Support:
-serialization of unfamiliar types
-serialization of arrays
-Saving referential integrity (regular references, circular references, cross-references)
-Override of descriptors / resolvers
-Types Dictionary

# Examples

### Array serialization

```csharp
int[] data = { 1,5,61,2,-512 };
var machine = new SerializeMachine();

var serialized =  machine.Serialize(data);// serialized is System.Xml.Linq.XElement 
//you can save serialized in xml-file to then restore the state of the object
var deserialized = machine.Deserialize(serialized) as int;

foreach (var e in data) Console.Write(e.ToString() + ", "); //write 1, 5, 61, 2, -512 
Console.WriteLine();
foreach (var e in deserialized) Console.Write(e.ToString() + ", ");//write 1, 5, 61, 2, -512 
Console.WriteLine("\n" + (deserialized == data));//write False 
```

Here is what is in the **serialized**:

```xml
<SMPackage Root="98be5d59-563b-4328-899c-9e1a26bc973c">
  <DICTIONARY>
    <_0>System.Int32[], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089</_0>
    <_1>System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089</_1>
  </DICTIONARY>
  <HEAP>
    <_0 SIZE="5" GUID="98be5d59-563b-4328-899c-9e1a26bc973c">
      <_1>1</_1>
      <_1>5</_1>
      <_1>61</_1>
      <_1>2</_1>
      <_1>-512</_1>
    </_0>
  </HEAP>
</SMPackage>
```

**serialized** consists of the nodes <DICTIONARY> and <HEAP>.

The <DICTIONARY > stores all data to work with the type.
The name of each node located in <DICTIONARY> is a type symbol (**convention**), and as a value of such
the node uses a string representation of the type obtained from *AssemblyQualifiedName*.

in <HEAP> stored the serialized version of the object, each node located in the <HEAP> must have a GUID attribute 
with a unique 128 bit value in the form of *xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxx* each digit is a hexadecimal value
