# SerializationMachine
A library for serializing objects.
Support:
-serialization of unfamiliar types
-serialization of arrays
-Saving referential integrity (regular references, circular references, cross-references)
-Override of descriptors / resolvers
-Types Dictionary

# Examples

### Int array serialization
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

>The ```<DICTIONARY >``` stores all data to work with the type.
The name of each node located in ```<DICTIONARY>``` is a type symbol (**convention**), and as a value of such
the node uses a string representation of the type obtained from *AssemblyQualifiedName*.

>In <HEAP> stored the serialized version of the object, each node located in the <HEAP> must have a GUID attribute 
with a unique 128 bit value in the form of *xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxx* each digit is a hexadecimal value.

>For this example, the ```<_0 SIZE="5" GUID="98be5d59-563b-4328-899c-9e1a26bc973c" >``` node header contains another attribute
```SIZE```. This attribute is exhibited and read by ArrayResolver


### Reference elements in arrays serialization

```csharp
object[] data = { null, "Hi!", 3.45f, '$', typeof(object) };
data[0] = data;//circular reference
var machine = new SerializeMachine.SerializeMachine();

var serialized = machine.Serialize(data);// serialized is System.Xml.Linq.XElement 
//you can save serialized in xml-file
var deserialized = machine.Deserialize(serialized) as object[];

foreach (var e in data) Console.Write(e.ToString() + ", ");//write System.Object[], Hi!, 3,45, $, System.Object
Console.WriteLine();
foreach (var e in deserialized) Console.Write(e.ToString() + ", ");//write System.Object[], Hi!, 3,45, $, System.Object
Console.WriteLine("\n" + (deserialized == data));//write False
```

Here is what is in the **serialized**:

```xml
<SMPackage Root="d1cf5e51-3733-41f4-8b2d-83f4936d9143">
  <DICTIONARY>
    <_0>System.Object[], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089</_0>
    <_1>System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089</_1>
    <_2>System.Single, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089</_2>
    <_3>System.Char, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089</_3>
    <_4>System.RuntimeType, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089</_4>
  </DICTIONARY>
  <HEAP>
    <_1 GUID="9b1314de-42d7-4484-96d6-014de06f4b13">Hi!</_1>
    <_0 SIZE="5" GUID="d1cf5e51-3733-41f4-8b2d-83f4936d9143">
      <_0>d1cf5e51-3733-41f4-8b2d-83f4936d9143</_0>
      <_1>9b1314de-42d7-4484-96d6-014de06f4b13</_1>
      <_2>3,45</_2>
      <_3>$</_3>
      <_4>efc0faa7-cefa-4167-89cc-f5e92acd7c8b</_4>
    </_0>
    <_4 GUID="efc0faa7-cefa-4167-89cc-f5e92acd7c8b">System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089</_4>
  </HEAP>
</SMPackage>
```

>In the <HEAP> node, the first element (```<_1 GUID= "9b1314de-42d7-4484-96d6-014de06f4b13">Hi!< / _1>```) is a string. 
Because a string is a reference object in .NET,each string is assigned a unique GUID.
To refer to this string later, you must specify the GUID of the string as the value of the nodes, as demonstrated in
the second element of the array (```<_1>9b1314de-42d7-4484-96d6-014de06f4b13</_1>```). When deserializing, the serializer will understand that the reference is specified.

>The first element of the array is the same array, which is a circular reference, As you can see, the serializer stops easily there

>Note the last element of the heap (```<_4 GUID="efc0faa7-cefa-4167-89cc-f5e92acd7c8b" >System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089</_4>```), there we put the type System.Object if the value is a type that exists in a type dictionary, such as System.Char, then the node value will look like this:```<_4 GUID= "efc0faa7-cefa-4167-89cc-f5e92acd7c8b">@_3</_4>```. @ - this is a special label for TypeResolver, after this label TypeResolver expects to read the **convention** specified in (```<DICTIONARY>```).

>System.Char and System.Single-are significant types, but in the process of casting these types to System.Object, these objects are Packed, and then you can work with these objects as with reference, but so far the serializer does not pay attention to this and does not allocate a reference to the packed objects 
