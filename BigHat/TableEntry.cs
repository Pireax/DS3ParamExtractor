using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;

namespace BigHat
{
    public abstract class TableEntry
    {
        private static int GetArrayLength(FieldInfo field)
        {
            var attr = (PArrayLengthAttribute) Attribute.GetCustomAttribute(field, typeof(PArrayLengthAttribute));
            return attr.Length;
        }

        public IList<string> GetFieldNames(bool ignoreHidden = true)
        {
            return GenerateListForeachField<string>((list, field) =>
            {
                if (field.FieldType.IsArray)
                {
                    var length = GetArrayLength(field);
                    for (var i = 0; i < length; i++)
                        list.Add(field.Name + i);
                }
                else list.Add(field.Name);
            }, ignoreHidden);
        }

        public IList<int> GetFieldOffsets(bool ignoreHidden = true)
        {
            var currentOffset = 0;
            var ignoredFields = typeof(ParamEntry).GetFields();
            return GenerateListForeachField<int>((list, field) =>
            {
                if (!ignoredFields.Contains(field))
                {
                    var offsetAttr =
                   (PFieldOffsetAttribute[])field.GetCustomAttributes(typeof(PFieldOffsetAttribute), false);
                    if (offsetAttr.Length > 0)
                        currentOffset = offsetAttr[0].Offset;
                    if (field.FieldType.IsArray)
                    {
                        var length = GetArrayLength(field);
                        var sizeOfArrayItem = Marshal.SizeOf(field.FieldType.GetElementType());
                        for (var i = 0; i < length; i++)
                        {
                            currentOffset += sizeOfArrayItem;
                            list.Add(currentOffset);
                        }
                    }
                    else
                    {
                        list.Add(currentOffset);
                        currentOffset += Marshal.SizeOf(field.FieldType);
                    }
                }
                else list.Add(-1);
               
            }, ignoreHidden);
        }

        public abstract IReadOnlyList<FieldInfo> GetFields();

        public IList<Type> GetFieldTypes(bool ignoreHidden = true)
        {
            return GenerateListForeachField<Type>((list, field) =>
            {
                if (field.FieldType.IsArray)
                {
                    var length = GetArrayLength(field);
                    for (var i = 0; i < length; i++)
                        list.Add(field.FieldType.GetElementType());
                }
                else list.Add(field.FieldType);
            }, ignoreHidden);
        }

        public IList<object> GetFieldValues(bool ignoreHidden = true)
        {
            return GenerateListForeachField<object>((list, field) =>
            {
                var val = field.GetValue(this);
                if (val.GetType().IsArray)
                {
                    var array = (Array) val;
                    foreach (var value in array)
                        list.Add(value);
                }
                else list.Add(val);
            }, ignoreHidden);
        }

        private IList<T> GenerateListForeachField<T>(Action<List<T>, FieldInfo> action, bool ignoreHidden = true)
        {
            var fields = GetFields();
            var returnVal = new List<T>(fields.Count);
            foreach (var field in fields)
            {
                if (!ignoreHidden)
                {
                    var hide = (HiddenAttribute[]) field.GetCustomAttributes(typeof(HiddenAttribute), false);
                    if (hide.Length > 0)
                        continue;
                }
                action(returnVal, field);
            }
            return returnVal;
        }
    }
}