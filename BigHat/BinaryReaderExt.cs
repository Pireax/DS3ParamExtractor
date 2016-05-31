using System;
using System.Collections.Generic;
using System.IO;

namespace BigHat
{
    public class BinaryReaderExt : BinaryReader
    {
        private readonly Dictionary<Type, Func<BinaryReaderExt, object>> _readDictionary = new Dictionary
            <Type, Func<BinaryReaderExt, object>>
        {
            {typeof(byte), x => x.ReadByte()},
            {typeof(sbyte), x => x.ReadSByte()},
            {typeof(char), x => x.ReadChar()},
            {typeof(short), x => x.ReadInt16()},
            {typeof(ushort), x => x.ReadUInt16()},
            {typeof(int), x => x.ReadInt32()},
            {typeof(uint), x => x.ReadUInt32()},
            {typeof(long), x => x.ReadInt64()},
            {typeof(ulong), x => x.ReadUInt64()},
            {typeof(bool), x => x.ReadBoolean()},
            {typeof(float), x => x.ReadSingle()},
            {typeof(double), x => x.ReadDouble()},
            {typeof(decimal), x => x.ReadDecimal()}
        };

        public BinaryReaderExt(Stream input) : base(input)
        {
        }

        public object ReadValueType(Type type)
        {
            return _readDictionary[type](this);
        }


        public void Seek(long offset, SeekOrigin origin)
        {
            BaseStream.Seek(offset, origin);
        }
    }
}