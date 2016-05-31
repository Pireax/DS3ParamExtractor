using System;
using System.Collections.Generic;
using System.IO;

namespace BigHat
{
    public class ProtoBufWriter : IDisposable
    {
        private readonly Dictionary<Type, string> _protoTypeNames = new Dictionary<Type, string>
        {
            {typeof(double), "double"},
            {typeof(float), "float"},
            {typeof(sbyte), "sint32"},
            {typeof(short), "sint32"},
            {typeof(int), "sint32"},
            {typeof(long), "sint64"},
            {typeof(byte), "uint32"},
            {typeof(ushort), "uint32"},
            {typeof(uint), "uint32"},
            {typeof(ulong), "uint64"},
            {typeof(bool), "bool"},
            {typeof(string), "string"},
            {typeof(byte[]), "bytes"},
            {typeof(Enum), "enum"}
        };

        private readonly StreamWriter _sr;

        public ProtoBufWriter(string path, string packageName)
        {
            _sr = File.CreateText(path);
            _sr.WriteLine("syntax = \"proto3\";");
            _sr.WriteLine($"package {packageName};");
            _sr.WriteLine();
        }

        public void Dispose()
        {
            _sr.Close();
        }

        public void WriteMessage<T>() where T : TableEntry, new()
        {
            var inst = new T();
            var types = inst.GetFieldTypes();
            var names = inst.GetFieldNames();

            if (types.Count != names.Count)
                throw new Exception();

            _sr.WriteLine($"message {typeof(T).Name} {{");

            for (var i = 0; i < types.Count; i++)
            {
                var type = _protoTypeNames[types[i]];
                _sr.WriteLine($"  {type} {names[i]} = {i + 2};");
            }

            _sr.WriteLine("}");
            _sr.WriteLine();
        }
    }
}