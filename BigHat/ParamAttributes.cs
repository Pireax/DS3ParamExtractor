using System;

namespace BigHat
{
    [AttributeUsage(AttributeTargets.Struct | AttributeTargets.Class)]
    public sealed class ParamAttribute : Attribute
    {
        public readonly int Size;

        public ParamAttribute(int size)
        {
            Size = size;
        }
    }

    [AttributeUsage(AttributeTargets.Field)]
    public sealed class PBitfieldAttribute : Attribute
    {
        public readonly int Bitoffset;
        public readonly int Index;

        public PBitfieldAttribute(int bitoffset, int index)
        {
            Bitoffset = bitoffset;
            Index = index;
        }
    }

    public sealed class PFieldOffsetAttribute : Attribute
    {
        public readonly int Offset;

        public PFieldOffsetAttribute(int offset)
        {
            Offset = offset;
        }
    }

    public sealed class PArrayLengthAttribute : Attribute
    {
        public readonly int Length;

        public PArrayLengthAttribute(int length)
        {
            Length = length;
        }
    }

    public sealed class HiddenAttribute : Attribute
    {
    }
}