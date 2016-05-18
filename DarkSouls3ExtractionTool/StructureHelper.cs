using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Reflection;

namespace DarkSouls3ExtractionTool
{
    class StructureHelper
    {
        public List<Type> TypesToSkip = new List<Type>();

        public static T BytesToStruct<T>(byte[] value, int startIndex) where T : struct
        {
            int size = Marshal.SizeOf(typeof(T));
            byte[] bytes = new byte[size];
            Array.ConstrainedCopy(value, startIndex, bytes, 0, size);

            var handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            T structure = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            handle.Free();

            return structure;
        }

        public bool ShouldSkip(Type fieldType)
        {
            foreach (var type in TypesToSkip)
                if (fieldType == type ||
                (fieldType.IsArray & fieldType.GetElementType() == type))
                    return true;
            return false;
        }

        private List<T> GenerateListForeachField<T>(IReadOnlyCollection<FieldInfo> fields, Action<List<T>, FieldInfo> action)
        {
            var returnVal = new List<T>(fields.Count);
            foreach (var field in fields)
            {
                if (ShouldSkip(field.FieldType))
                    continue;
                action(returnVal, field);
            }
            return returnVal;
        }

        private List<T2> GenerateListForeachField<T1, T2>(T1 structure, Action<List<T2>, FieldInfo> action) where T1 : struct
        {
            var fields = structure.GetType().GetFields();
            return GenerateListForeachField<T2>(fields, action);
        }

        private List<T2> GenerateListForeachField<T1, T2>(Action<List<T2>, FieldInfo> action) where T1 : struct
        {
            var fields = typeof(T1).GetFields();
            return GenerateListForeachField<T2>(fields, action);
        }

        private int GetArrayLength(FieldInfo field)
        {
            var attr = (MarshalAsAttribute)Attribute.GetCustomAttribute(field, typeof(MarshalAsAttribute));
            return attr.SizeConst;
        }

        public List<object> GetFieldValues<T>(T structure) where T : struct
        {
            return GenerateListForeachField<T, object>(structure, (list, field) =>
            {
                var val = field.GetValue(structure);
                if (val.GetType().IsArray)
                {
                    var array = (Array)val;
                    foreach (var value in array)
                        list.Add(value);
                }
                else list.Add(val);
            });
        }

        public List<string> GetFieldNames<T>() where T : struct
        {
            return GenerateListForeachField<T, string>((list, field) =>
            {
                if (field.FieldType.IsArray)
                {
                    var length = GetArrayLength(field);
                    for (var i = 0; i < length; i++)
                        list.Add(field.Name + i);
                }
                else list.Add(field.Name);
            });
        }

        public List<int> GetFieldOffsets<T>() where T : struct
        {
            return GenerateListForeachField<T, int>((list, field) =>
            {
                if (field.FieldType.IsArray)
                {
                    var baseOffset = Marshal.OffsetOf(typeof(T), field.Name).ToInt32();
                    var length = GetArrayLength(field);
                    var sizeOfArrayItem = Marshal.SizeOf(field.FieldType.GetElementType());
                    for (var i = 0; i < length; i++)
                        list.Add(baseOffset + i * sizeOfArrayItem);
                }
                else list.Add(Marshal.OffsetOf(typeof(T), field.Name).ToInt32());
            });
        }

        public List<Type> GetFieldTypes<T>() where T : struct
        {
            return GenerateListForeachField<T, Type>((list, field) =>
            {
                if (field.FieldType.IsArray)
                    list.Add(field.FieldType.GetElementType());
                else list.Add(field.FieldType);
            });
        }
    }
}
