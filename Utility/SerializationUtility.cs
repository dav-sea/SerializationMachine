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

           public static FieldInfo[] GetSerializableFields(Type type)
           {
               return GetSerializableFields(Reflection.GetFieldInfo(type));
           }
           public static FieldInfo[] GetSerializableFields(FieldInfo[] fields)
           {
               if (fields == null || fields.Length == 0) return new FieldInfo[0];
               return GetSerializableFieldsInternal(fields).ToArray();
           }
           internal static List<FieldInfo> GetSerializableFieldsInternal(Type type)
           {
               return GetSerializableFieldsInternal(Reflection.GetFieldInfoInternal(type));
           }
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
