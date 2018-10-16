using System;
using System.Collections.Generic;
using System.Reflection;//TODO CHECK System.Reflection.Cache

namespace SerializationMachine.Utility
{
   public static class SerializationUtility
   {
        public static bool IsStruct(Type type)
        {
           return type != null && !type.IsClass;
        }
        internal static bool IsStructInternal(Type type)
        {
           return !type.IsClass;
        }
        internal static object InstantiateUninitializedObject(Type typeObject)
        {
           return System.Runtime.Serialization.FormatterServices.GetUninitializedObject(typeObject);
        }


        public static class Reflection
        {
            public static FieldInfo[] GetFieldInfo(Type type, BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
            {
                return type == null ? new FieldInfo[0] : GetFieldInfoInternal(type,flags);   
            }
            internal static FieldInfo[] GetFieldInfoInternal(Type type,BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
            {
                return type.GetFields(flags);
            }
            internal static ConstructorInfo GetDefaultConstructor(Type type)
            {
                return type.GetConstructor(BindingFlags.NonPublic | BindingFlags.Public  | BindingFlags.Instance, null, new Type[0], null);
            }
        }
        public static class Targeting
        {
            public static bool IsSerializationTarget(Type type)
            {
                return Utility.AtributtesUtility.IsSerializable(type);
            }
            public static bool IsSerializationTarget(FieldInfo type)
            {
                return !Utility.AtributtesUtility.IsNonSerialized(type) && Utility.AtributtesUtility.IsSerializable(type.DeclaringType);
            }
            public static bool IsSaveReference(Type type)
            {
                return type != null && IsSaveReferenceInternal(type);
            }
            internal static bool IsSaveReferenceInternal(Type type)
            {
                return type.IsClass;
            }
            internal static bool IsSerializableBaseFieldsInternal(Type type)
            {
                return IsSerializationTarget(type.BaseType);
            }

            public static FieldInfo[] SublistSerializableFields(FieldInfo[] fields)
            {
                if (fields == null || fields.Length == 0) return new FieldInfo[0];
                var list = new List<FieldInfo>(10);
                SublistSerializableFieldsInternal(list, fields);
                return list.ToArray();
            }
            public static FieldInfo[] GetSerializableFields(Type type)
            {
                return GetSerializableFieldsInternal(type, IsSerializableBaseFieldsInternal(type)).ToArray();
            }

            internal static List<FieldInfo> GetSerializableFieldsInternal(Type targetType,bool includeBaseFields)
            {
                var list = new List<FieldInfo>(10);
                FindSerialiableFieldsInternal(list ,targetType,includeBaseFields);
                return list;
            }
            internal static void FindSerialiableFieldsInternal(List<FieldInfo> fields,Type targetType,bool includeBaseFields)
            {
                if (TypeOf<Object>.Equals(targetType)) 
                    return;

                SublistSerializableFieldsInternal(fields, targetType);

                if (includeBaseFields)
                    FindSerialiableFieldsInternal(fields, targetType.BaseType, true);
            }
            internal static void SublistSerializableFieldsInternal(List<FieldInfo> targetList, FieldInfo[] typeFields)
            {
                for (int i = 0; i < typeFields.Length; i++)
                    if (Targeting.IsSerializationTarget(typeFields[i]))
                        targetList.Add(typeFields[i]);
            }
            internal static void SublistSerializableFieldsInternal(List<FieldInfo>targetList,Type type)
            {
                SublistSerializableFieldsInternal(targetList, Reflection.GetFieldInfoInternal(type));
            }

            [Obsolete("Use SublistSerializableFields")]
            public static FieldInfo[] GetSerializableFieldsInternal(Type type)
            {
                return SublistSerializableFields(Reflection.GetFieldInfoInternal(type));
            }
            [Obsolete("Use SublistSerializableFieldsInternal")]
            internal static List<FieldInfo> GetSerializableFieldsInternal(FieldInfo[] fields)
            {
                var resilt = new List<FieldInfo>(fields.Length);
                for (int i = 0; i < fields.Length; i++)
                   if (Targeting.IsSerializationTarget(fields[i]))
                       resilt.Add(fields[i]);
                return resilt;
            }
        }
    }
}
