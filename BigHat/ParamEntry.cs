using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace BigHat
{
    public abstract class ParamEntry : TableEntry
    {
        public uint Id;
        public int Index;
        public int Offset;

        public static T FromBytes<T>(byte[] value, int startIndex) where T : ParamEntry, new()
        {
            object paramEntry = new T();
            var paramSizeAttr =
                (ParamAttribute[]) paramEntry.GetType().GetCustomAttributes(typeof(ParamAttribute), false);
            if (paramSizeAttr.Length < 1)
                throw new ArgumentException($"Structure does not have the Param attribute");

            using (var sr = new MemoryStream(value, startIndex, paramSizeAttr[0].Size))
            using (var reader = new BinaryReaderExt(sr))
            {
                var fields = paramEntry.GetType().GetFields(BindingFlags.Public | 
                    BindingFlags.Instance | 
                    BindingFlags.DeclaredOnly);
                foreach (var field in fields)
                {
                    var bitfieldAttr =
                        (PBitfieldAttribute[]) field.GetCustomAttributes(typeof(PBitfieldAttribute), false);
                    var fieldOffsetAttr =
                        (PFieldOffsetAttribute[]) field.GetCustomAttributes(typeof(PFieldOffsetAttribute), false);
                    var marshalAsAttr =
                        (PArrayLengthAttribute[]) field.GetCustomAttributes(typeof(PArrayLengthAttribute), false);

                    if (fieldOffsetAttr.Length > 0)
                        reader.Seek(fieldOffsetAttr[0].Offset, SeekOrigin.Begin);
                    if (bitfieldAttr.Length > 0)
                    {
                        var offset = bitfieldAttr[0].Bitoffset;
                        var index = bitfieldAttr[0].Index;
                        reader.Seek(index, SeekOrigin.Begin);
                        field.SetValue(paramEntry, ((reader.ReadByte() & 1 << offset) >> offset == 1));
                    }
                    if (marshalAsAttr.Length > 0)
                    {
                        var arrayType = field.FieldType.GetElementType();
                        var array = Array.CreateInstance(arrayType, marshalAsAttr[0].Length);
                        for (var i = 0; i < array.Length; i++)
                            array.SetValue(reader.ReadValueType(arrayType), i);
                        field.SetValue(paramEntry, array);
                    }
                    else field.SetValue(paramEntry, reader.ReadValueType(field.FieldType));
                }
            }
            return (T) paramEntry;
        }

        public static int SizeOf<T>() where T : ParamEntry
        {
            var paramSizeAttr = (ParamAttribute[]) typeof(T).GetCustomAttributes(typeof(ParamAttribute), false);
            if (paramSizeAttr.Length < 1)
                throw new ArgumentException($"Structure does not have the Param attribute");
            return paramSizeAttr[0].Size;
        }

        public override IReadOnlyList<FieldInfo> GetFields()
        {
            var baseFields = typeof(ParamEntry).GetFields();
            var derivedFields = GetType().GetFields(BindingFlags.Public |
                    BindingFlags.Instance |
                    BindingFlags.DeclaredOnly);
            var fields = new FieldInfo[baseFields.Length + derivedFields.Length];
            baseFields.CopyTo(fields, 0);
            derivedFields.CopyTo(fields, baseFields.Length);
            return fields;
        }
    }
}