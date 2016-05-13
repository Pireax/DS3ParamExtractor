using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace DarkSouls3ExtractionTool
{
    [StructLayout(LayoutKind.Explicit, Size = 608)]
    public struct EquipParamWeapon
    {
        [FieldOffset(0x0)]
        public uint sortId;
        [FieldOffset(0xC)]
        public float weight;
        [FieldOffset(0x20)]
        public float strBaseCoef;
        [FieldOffset(0x24)]
        public float dexBaseCoef;
        [FieldOffset(0x28)]
        public float intBaseCoef;
        [FieldOffset(0x2C)]
        public float fthBaseCoef;
        [FieldOffset(0x30)]
        public float physicalAbsorption;
        [FieldOffset(0x34)]
        public float magicAbsorption;
        [FieldOffset(0x38)]
        public float fireAbsorption;
        [FieldOffset(0x3C)]
        public float lightningAbsorption;
        [FieldOffset(0x40)]
        public int effectOnHit1;
        [FieldOffset(0x44)]
        public int effectOnHit2;
        [FieldOffset(0x48)]
        public int effectOnHit3;
        [FieldOffset(0x4C)]
        public int effectOnSelf1;
        [FieldOffset(0x50)]
        public int effectOnSelf2;
        [FieldOffset(0x54)]
        public int effectOnSelf3;
        [FieldOffset(0xBE)]
        public short maxDurability;
        [FieldOffset(0xC4)]
        public short physicalBaseDamage;
        [FieldOffset(0xC6)]
        public short magicBaseDamage;
        [FieldOffset(0xC8)]
        public short fireBaseDamage;
        [FieldOffset(0xCA)]
        public short lightningBaseDamage;
        [FieldOffset(0xD4)]
        public short stability;
        [FieldOffset(0xD6)]
        public short reinforcementType;
        [FieldOffset(0xE8)]
        public byte physicalStatFunc;
        [FieldOffset(0xEE)]
        public byte strReq;
        [FieldOffset(0xEF)]
        public byte dexReq;
        [FieldOffset(0xF0)]
        public byte intReq;
        [FieldOffset(0xF1)]
        public byte fthReq;
        [FieldOffset(0x179)]
        public byte magicStatFunc;
        [FieldOffset(0x17A)]
        public byte fireStatFunc;
        [FieldOffset(0x17B)]
        public byte lightningStatFunc;
        [FieldOffset(0x188)]
        public short darkBaseDamage;
        [FieldOffset(0x18A)]
        public byte darkStatFunc;
        [FieldOffset(0x18B)]
        public byte poisonStatFunc;
        [FieldOffset(0x190)]
        public byte bleedStatFunc;
        [FieldOffset(0x191)]
        public byte luckReq;
        [FieldOffset(0x198)]
        public float lckBaseCoef;
        [FieldOffset(0x1A2)]
        public short weaponClass;
        [FieldOffset(0x228)]
        public int aecp;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ReinforceParamWeapon
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        public float[] v1;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] v2;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public float[] v3;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CalcCorrectGraph
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        public float[] values;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 128)]
    public struct AttackElementCorrectParam
    {
        public int bitmask;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
        public short[] values;
    }

    [StructLayout(LayoutKind.Explicit, Size = 800)]
    public struct SpEffectParam
    {
        [FieldOffset(0xCC)]
        public short poisonBase;
        [FieldOffset(0xD0)]
        public int strangeValue;
        [FieldOffset(0xD4)]
        public short bleedBase;
        [FieldOffset(0x160)]
        public byte strangeEnum;
        [FieldOffset(0x1AC)]
        public short frostBase;
    }

    [StructLayout(LayoutKind.Explicit, Size = 0)]
    public struct Magic
    {
        [FieldOffset(0x8)]
        public short sortId;
    }

    [StructLayout(LayoutKind.Explicit, Size = 0)]
    public struct EquipParamProtector
    {
        [FieldOffset(0x0)]
        public int sortId;
        [FieldOffset(0x1C)]
        public int sellValue;
        [FieldOffset(0x20)]
        public float weight;
        [FieldOffset(0xC0)]
        public short poisonResist;
        [FieldOffset(0xC2)]
        public short toxicResist;
        [FieldOffset(0xC4)]
        public short bleedResist;
        [FieldOffset(0xC6)]
        public byte curseResist;
        [FieldOffset(0xE0)]
        public float physicalDef;
        [FieldOffset(0xE4)]
        public float slashDef;
        [FieldOffset(0xE8)]
        public float strikeDef;
        [FieldOffset(0xEC)]
        public float thrustDef;
        [FieldOffset(0xF0)]
        public float magicDef;
        [FieldOffset(0xF4)]
        public float fireDef;
        [FieldOffset(0xF8)]
        public float lightningDef;
        [FieldOffset(0xAA)]
        public short durability;
        [FieldOffset(0xAC)]
        public short durabilityMax;
        [FieldOffset(0x110)]
        public float poise;
        [FieldOffset(0x118)]
        public float darkDef;
        [FieldOffset(0x12C)]
        public short frostResist;
    }
}
