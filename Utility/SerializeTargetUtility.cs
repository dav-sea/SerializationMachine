using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace SerializeMachine.Utility
{
    public static class SerializeTargetUtility
    {
        internal static bool IsSerializableInternal(Type type)
        {
            return (type.Attributes & TypeAttributes.Serializable) != 0;
        }
        public static bool IsSerializable(Type type)
        {
            return type != null && IsSerializableInternal(type);
        }
        internal static bool IsNonSerializedInternal(FieldInfo field)
        {
            return (field.Attributes & FieldAttributes.NotSerialized) != 0;
        }
        public static bool IsNonSerialized(FieldInfo field)
        {
            return field != null && IsNonSerializedInternal(field);
        }
    }
}
