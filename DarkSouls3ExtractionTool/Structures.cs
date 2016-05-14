using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace DarkSouls3ExtractionTool
{
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 80)]
    public struct CalcCorrectGraph
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
        public float[] StageMaxVal;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
        public float[] StageMaxGrowVal;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
        public float[] AdjPt_maxGrowVal;
        public float Init_inclination_soul;
        public float Adjustment_value;
        public float Boundry_inclination_soul;
        public float Boundry_value;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 128)]
    public struct ReinforceParamWeapon
    {
        public float PhysicsAtkRate;
        public float MagicAtkRate;
        public float FireAtkRate;
        public float ThunderAtkRate;
        public float StaminaAtkRate;
        public float SaWeaponAtkRate;
        public float SaDurabilityRate;
        public float CorrectStrengthRate;
        public float CorrectAgilityRate;
        public float CorrectMagicRate;
        public float CorrectFaithRate;
        public float PhysicsGuardCutRate;
        public float MagicGuardCutRate;
        public float FireGuardCutRate;
        public float ThunderGuardCutRate;
        public float PoisonGuardResistRate;
        public float DiseaseGuardResistRate;
        public float BloodGuardResistRate;
        public float CurseGuardResistRate;
        public float StaminaGuardDefRate;
        public byte SpEffectId1;
        public byte SpEffectId2;
        public byte SpEffectId3;
        public byte ResidentSpEffectId1;
        public byte ResidentSpEffectId2;
        public byte ResidentSpEffectId3;
        public byte MaterialSetId1;
        public byte MaterialSetId2;
        public float DarkAtkRate;
        public float DarkGuardResistRate;
        public float CorrectLuckRate;
        public float Unknown1;
        public float Unknown2;
        public float Unknown3;
    }

    [StructLayout(LayoutKind.Explicit, Size = 608)]
    public struct EquipParamWeapon
    {
        [FieldOffset(0x0)]
        public uint BehaviorVariationId;
        [FieldOffset(0x4)]
        public uint SortId;
        [FieldOffset(0xC)]
        public float Weight;
        [FieldOffset(0x20)]
        public float CorrectStrength;
        [FieldOffset(0x24)]
        public float CorrectAgility;
        [FieldOffset(0x28)]
        public float CorrectMagic;
        [FieldOffset(0x2C)]
        public float CorrectFaith;
        [FieldOffset(0x30)]
        public float PhysGuardCutRate;
        [FieldOffset(0x34)]
        public float MagGuardCutRate;
        [FieldOffset(0x38)]
        public float FireGuardCutRate;
        [FieldOffset(0x3C)]
        public float ThunGuardCutRate;
        [FieldOffset(0x40)]
        public int SpEffectBehaviorId1;
        [FieldOffset(0x44)]
        public int SpEffectBehaviorId2;
        [FieldOffset(0x48)]
        public int SpEffectBehaviorId3;
        [FieldOffset(0x4C)]
        public int ResidentSpEffectId1;
        [FieldOffset(0x50)]
        public int ResidentSpEffectId2;
        [FieldOffset(0x54)]
        public int ResidentSpEffectId3;
        [FieldOffset(0xBE)]
        public short MaxDurability;
        [FieldOffset(0xC4)]
        public short AttackBasePhysics;
        [FieldOffset(0xC6)]
        public short AttackBaseMagic;
        [FieldOffset(0xC8)]
        public short AttackBaseFire;
        [FieldOffset(0xCA)]
        public short AttackBaseThunder;
        [FieldOffset(0xCC)]
        public short AttackBaseStamina;
        [FieldOffset(0xD4)]
        public short Stability;
        [FieldOffset(0xD6)]
        public short ReinforceTypeId;
        [FieldOffset(0xE8)]
        public byte PhysicsStatFunc;
        [FieldOffset(0xEE)]
        public byte ProperStrength;
        [FieldOffset(0xEF)]
        public byte ProperAgility;
        [FieldOffset(0xF0)]
        public byte ProperMagic;
        [FieldOffset(0xF1)]
        public byte ProperFaith;
        [FieldOffset(0x179)]
        public byte MagicStatFunc;
        [FieldOffset(0x17A)]
        public byte FireStatFunc;
        [FieldOffset(0x17B)]
        public byte ThunderStatFunc;
        [FieldOffset(0x188)]
        public short AttackBaseDark;
        [FieldOffset(0x18A)]
        public byte DarkStatFunc;
        [FieldOffset(0x18B)]
        public byte PoisonStatFunc;
        [FieldOffset(0x190)]
        public byte BleedStatFunc;
        [FieldOffset(0x191)]
        public byte ProperLuck;
        [FieldOffset(0x198)]
        public float CorrectLuck;
        [FieldOffset(0x1A2)]
        public short WeaponClass;
        [FieldOffset(0x228)]
        public int AecpId;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 128)]
    public struct AttackElementCorrectParam
    {
        public int Bitmask;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
        public short[] v;
    }

    [StructLayout(LayoutKind.Explicit, Size = 800)]
    public struct SpEffectParam
    {
        [FieldOffset(0xCC)]
        public short PoisonBase;
        [FieldOffset(0xD0)]
        public int StrangeValue;
        [FieldOffset(0xD4)]
        public short BleedBase;
        [FieldOffset(0x160)]
        public byte StrangeEnum;
        [FieldOffset(0x1AC)]
        public short FrostBase;
    }

    [StructLayout(LayoutKind.Explicit, Size = 0)]
    public struct Magic
    {
        [FieldOffset(0x8)]
        public short SortId;
    }

    [StructLayout(LayoutKind.Explicit, Size = 0)]
    public struct EquipParamProtector
    {
        [FieldOffset(0x0)]
        public int SortId;
        [FieldOffset(0x1C)]
        public int SellValue;
        [FieldOffset(0x20)]
        public float Weight;
        [FieldOffset(0xC0)]
        public short PoisonResist;
        [FieldOffset(0xC2)]
        public short ToxicResist;
        [FieldOffset(0xC4)]
        public short BleedResist;
        [FieldOffset(0xC6)]
        public byte CurseResist;
        [FieldOffset(0xE0)]
        public float PhysicalDef;
        [FieldOffset(0xE4)]
        public float SlashDef;
        [FieldOffset(0xE8)]
        public float StrikeDef;
        [FieldOffset(0xEC)]
        public float ThrustDef;
        [FieldOffset(0xF0)]
        public float MagicDef;
        [FieldOffset(0xF4)]
        public float FireDef;
        [FieldOffset(0xF8)]
        public float LightningDef;
        [FieldOffset(0xAA)]
        public short Durability;
        [FieldOffset(0xAC)]
        public short DurabilityMax;
        [FieldOffset(0x110)]
        public float Poise;
        [FieldOffset(0x118)]
        public float DarkDef;
        [FieldOffset(0x12C)]
        public short FrostResist;
    }
}
