using System;
using System.Collections.Generic;
using System.IO;

namespace DarkSouls3ExtractionTool
{
    public class ProtoBufWriter : IDisposable
    {
        private readonly StreamWriter _sr;
        private readonly StructureHelper _structHelper = new StructureHelper();
        private readonly Dictionary<Type, string> _protoTypeNames = new Dictionary<Type, string>
        {
            {typeof(Double), "double"},
            {typeof(Single), "float"},
            {typeof(SByte), "sint32"},
            {typeof(Int16), "sint32"},
            {typeof(Int32), "sint32"},
            {typeof(Int64), "sint64"},
            {typeof(Byte), "uint32"},
            {typeof(UInt16), "uint32"},
            {typeof(UInt32), "uint32"},
            {typeof(UInt64), "uint64"},
            {typeof(Boolean), "bool"},
            {typeof(String), "string"},
            {typeof(Byte[]), "bytes"},
            {typeof(Enum), "enum"}
        };

        public ProtoBufWriter(string path)
        {
            _sr = File.CreateText(path);
            _sr.WriteLine("syntax = \"proto3\";");
            _sr.WriteLine("package ds3extraction");
            _sr.WriteLine();

            _structHelper.TypesToSkip.AddRange(new[] { typeof(Unknown), typeof(Padding) });
        }

        public void WriteMessage<T>(string messageName) where T : struct
        {
            var types = _structHelper.GetFieldTypes<T>();
            var names = _structHelper.GetFieldNames<T>();

            if (types.Count != names.Count)
                throw new Exception();

            _sr.WriteLine($"message {messageName} {{");
            for (var i = 0; i < types.Count; i++)
            {
                var type = _protoTypeNames[types[i]];
                _sr.WriteLine($"  {type} {names[i]};");
            }

            _sr.WriteLine("}");
            _sr.WriteLine();
        }

        public void Dispose()
        {
            _sr.Close();
        }
    }
}
