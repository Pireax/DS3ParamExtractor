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

    /* Incomplete */

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
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
        public Unknown[] Unknown;
    }

    [StructLayout(LayoutKind.Explicit, Size = 608)]
    public struct EquipParamWeapon
    {
        [FieldOffset(0x0)]
        public uint BehaviorVariationId;
        [FieldOffset(0x4)]
        public uint SortId;
        [FieldOffset(0x8)]
        public int WanderingEquipId;
        [FieldOffset(0xC)]
        public float Weight;
        [FieldOffset(0x10)]
        public int WeaponWeightRate;
        [FieldOffset(0x14)]
        public int FixPrice;
        [FieldOffset(0x18)]
        public int BasicPrice;
        [FieldOffset(0x1C)]
        public int SellValue;
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
        [FieldOffset(0x58)]
        public int MaterialSetId;
        [FieldOffset(0x5C)]
        public int OriginEquipWep;
        [FieldOffset(0x60)]
        public int OriginEquipWep1;
        [FieldOffset(0x64)]
        public int OriginEquipWep2;
        [FieldOffset(0x68)]
        public int OriginEquipWep3;
        [FieldOffset(0x6C)]
        public int OriginEquipWep4;
        [FieldOffset(0x70)]
        public int OriginEquipWep5;
        [FieldOffset(0x74)]
        public int OriginEquipWep6;
        [FieldOffset(0x78)]
        public int OriginEquipWep7;
        [FieldOffset(0x7C)]
        public int OriginEquipWep8;
        [FieldOffset(0x80)]
        public int OriginEquipWep9;
        [FieldOffset(0x84)]
        public int OriginEquipWep10;
        [FieldOffset(0x88)]
        public int OriginEquipWep11;
        [FieldOffset(0x8C)]
        public int OriginEquipWep12;
        [FieldOffset(0x90)]
        public int OriginEquipWep13;
        [FieldOffset(0x94)]
        public int OriginEquipWep14;
        [FieldOffset(0x98)]
        public int OriginEquipWep15;
        [FieldOffset(0x9C)]
        public float AntiDemonDamageRate;
        [FieldOffset(0xA0)]
        public float AntSaintDamageRate;
        [FieldOffset(0xA4)]
        public float AntWeakA_DamageRate;
        [FieldOffset(0xA8)]
        public float AntWeakB_DamageRate;
        [FieldOffset(0xAC)]
        public int VagrantItemLotId;
        [FieldOffset(0xB0)]
        public int VagrantBonusEneDropItemLotId;
        [FieldOffset(0xB4)]
        public int VagrantItemEneDropItemLotId;
        [FieldOffset(0xB8)]
        public ushort EquipModelId;
        [FieldOffset(0xBA)]
        public ushort IconId;
        [FieldOffset(0xBC)]
        public short Durability;
        [FieldOffset(0xBE)]
        public short MaxDurability;
        [FieldOffset(0xC0)]
        public short ParryDamageLife;
        [FieldOffset(0xC2)]
        public ushort AttackThrowEscape;
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
        [FieldOffset(0xCE)]
        public short AttackBasePoise;
        [FieldOffset(0xD0)]
        public short SaDurability;
        [FieldOffset(0xD2)]
        public short GuardAngle;
        [FieldOffset(0xD4)]
        public short Stability;
        [FieldOffset(0xD6)]
        public short ReinforceTypeId;
        [FieldOffset(0xD8)]
        public short TrophySGradeId;
        [FieldOffset(0xDA)]
        public short ThrophySeqId;
        [FieldOffset(0xDC)]
        public short ThrowAtkRate;
        [FieldOffset(0xDE)]
        public short BowDistRate;
        [FieldOffset(0xE0)]
        public byte EquipModelCategory;
        [FieldOffset(0xE1)]
        public byte EquipModelGender;
        [FieldOffset(0xE2)]
        public byte WeaponCategory;
        [FieldOffset(0xE3)]
        public byte MotionCategory;
        [FieldOffset(0xE4)]
        public byte GuardMotionCategory;
        [FieldOffset(0xE5)]
        public byte AtkMaterial;
        [FieldOffset(0xE6)]
        public byte DefMaterial;
        [FieldOffset(0xE7)]
        public byte DefSfxMaterial;
        [FieldOffset(0xE8)]
        public byte PhysicsStatFunc;
        [FieldOffset(0xE9)]
        public byte SpAttribute;
        [FieldOffset(0xEE)]
        public byte ProperStrength;
        [FieldOffset(0xEF)]
        public byte ProperAgility;
        [FieldOffset(0xF0)]
        public byte ProperMagic;
        [FieldOffset(0xF1)]
        public byte ProperFaith;
        [FieldOffset(0xF2)]
        public byte OverStrength;
        [FieldOffset(0xF3)]
        public byte AttackBaseParry;
        [FieldOffset(0xF4)]
        public byte DefenseBaseParry;
        [FieldOffset(0xF5)]
        public byte GuardBaseRepel;
        [FieldOffset(0xF6)]
        public byte AttackBaseRepel;
        [FieldOffset(0xF7)]
        public byte GuardCutCancelRate;
        [FieldOffset(0xF8)]
        public byte GuardLevel;
        [FieldOffset(0xF9)]
        public byte SlashGuardCutRate;
        [FieldOffset(0xFA)]
        public byte BlowGuardCutRate;
        [FieldOffset(0xFB)]
        public byte ThrustGuardCutRate;
        [FieldOffset(0xFC)]
        public byte PoisonGuardResist;
        [FieldOffset(0xFD)]
        public byte DiseaseGuardResist;
        [FieldOffset(0xFE)]
        public byte BloodGuardResist;
        [FieldOffset(0xFF)]
        public byte CurseGuardResist;
        [FieldOffset(0x100)]
        public byte IsDurabilityDivergence;
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
        [FieldOffset(0x194)]
        public byte WeaponArt;
        [FieldOffset(0x198)]
        public float CorrectLuck;
        [FieldOffset(0x1A2)]
        public short WeaponClass;
        [FieldOffset(0x228)]
        public int AecpId;
    }

    [StructLayout(LayoutKind.Explicit, Size = 400)]
    public struct EquipParamProtector
    {
        [FieldOffset(0x0)]
        public int SortId;
        [FieldOffset(0x4)]
        public int WanderingEquipId;
        [FieldOffset(0x8)]
        public int VagrantItemLotId;
        [FieldOffset(0x0C)]
        public int VagrantBonusEneDropItemLotId;
        [FieldOffset(0x10)]
        public int VagrantItemEneDropItemLotId;
        [FieldOffset(0x14)]
        public int FixPrice;
        [FieldOffset(0x18)]
        public int BasicPrice;
        [FieldOffset(0x1C)]
        public int SellValue;
        [FieldOffset(0x20)]
        public float Weight;
        [FieldOffset(0x24)]
        public int ResidentSpEffectId1;
        [FieldOffset(0x28)]
        public int ResidentSpEffectId2;
        [FieldOffset(0x2C)]
        public int ResidentSpEffectId3;
        [FieldOffset(0x30)]
        public int MaterialSetId;
        [FieldOffset(0x34)]
        public float PartsDamageRate;
        [FieldOffset(0xC0)]
        public short ResistPoison;
        [FieldOffset(0xC2)]
        public short ResistToxic;
        [FieldOffset(0xC4)]
        public short ResistBlood;
        [FieldOffset(0xC6)]
        public byte ResistCurse;
        [FieldOffset(0xE0)]
        public float DefensePhysics;
        [FieldOffset(0xE4)]
        public float DefenseSlash;
        [FieldOffset(0xE8)]
        public float DefenseStrike;
        [FieldOffset(0xEC)]
        public float DefenseThrust;
        [FieldOffset(0xF0)]
        public float DefenseMagic;
        [FieldOffset(0xF4)]
        public float DefenseFire;
        [FieldOffset(0xF8)]
        public float DefenseThunder;
        [FieldOffset(0xA0)]
        public short EquipModelId;
        [FieldOffset(0xA8)]
        public short KnockbackBounceRate;
        [FieldOffset(0xAA)]
        public short Durability;
        [FieldOffset(0xAC)]
        public short DurabilityMax;
        [FieldOffset(0xAE)]
        public byte SaDurability;
        [FieldOffset(0x110)]
        public float Poise;
        [FieldOffset(0x118)]
        public float DefenseDark;
        [FieldOffset(0x12C)]
        public short ResistFrost;
    }

    [StructLayout(LayoutKind.Sequential, Size = 64)]
    public struct ReinforceParamProtector
    {
        public float PhysicsDefRate;
        public float MagicDefRate;
        public float FireDefRate;
        public float ThunderDefRate;
        public float SlashDefRate;
        public float StrikeDefRate;
        public float ThrustDefRate;
        public float ResistPoisonRate;
        public float ResistDiseaseRate;
        public float ResistBloodRate;
        public float ResistCurseRate;
        public byte ResidentSpEffectId1;
        public byte ResidentSpEffectId2;
        public byte ResidentSpEffectId3;
        public byte MaterialSetId;
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
        [FieldOffset(0x0)]
        public int IconId;
        [FieldOffset(0x4)]
        public float ConditionHp;
        [FieldOffset(0x8)]
        public float EffectEndurance;
        [FieldOffset(0xC)]
        public int MotionInterval;
        [FieldOffset(0x10)]
        public float MaxHpRate;
        [FieldOffset(0x14)]
        public float MaxMpRate;
        [FieldOffset(0x18)]
        public float MaxStaminaRate;
        [FieldOffset(0x1C)]
        public float SlashDamageCutRate;
        [FieldOffset(0x20)]
        public float BlowDamageCutRate;
        [FieldOffset(0x24)]
        public float ThrustDamageCutRate;
        [FieldOffset(0x28)]
        public float NeutralDamageCutRate;
        [FieldOffset(0x2C)]
        public float MagicDamageCutRate;
        [FieldOffset(0x30)]
        public float FireDamageCutRate;
        [FieldOffset(0x34)]
        public float ThunderDamageCutRate;
        [FieldOffset(0x38)]
        public float PhysicsAttackRate;
        [FieldOffset(0x3C)]
        public float MagicAttackRate;
        [FieldOffset(0x40)]
        public float FireAttackRate;
        [FieldOffset(0x44)]
        public float ThunderAttackRate;
        [FieldOffset(0x48)]
        public float PhysicsAttackPowerRate;
        [FieldOffset(0x4C)]
        public float MagicAttackPowerRate;
        [FieldOffset(0x50)]
        public float FireAttackPowerRate;
        [FieldOffset(0x54)]
        public float ThunderAttackPowerRate;
        [FieldOffset(0x58)]
        public int PhysicsAttackPower;
        [FieldOffset(0x5C)]
        public int MagicAttackPower;
        [FieldOffset(0x60)]
        public int FireAttackPower;
        [FieldOffset(0x64)]
        public int ThunderAttackPower;
        [FieldOffset(0x68)]
        public float PhysicsDifferenceRate;
        [FieldOffset(0x6C)]
        public float MagicDifferenceRate;
        [FieldOffset(0x70)]
        public float FireDifferenceRate;
        [FieldOffset(0x74)]
        public float ThunderDifferenceRate;
        [FieldOffset(0x78)]
        public int PhysicsDifference;
        [FieldOffset(0x7C)]
        public int MagicDifference;
        [FieldOffset(0x80)]
        public int FireDifference;
        [FieldOffset(0x84)]
        public int ThunderDifference;
        [FieldOffset(0x88)]
        public float NoGuardDamageRate;
        [FieldOffset(0x8C)]
        public float VitalSpotChangeRate;
        [FieldOffset(0x90)]
        public float NormalSpotChangeRate;
        [FieldOffset(0x94)]
        public float MaxHpChangeRate;
        [FieldOffset(0x98)]
        public int BehaviorId;
        [FieldOffset(0x9C)]
        public float ChangeHpRate;
        [FieldOffset(0xA0)]
        public int ChangeHpPoint;
        [FieldOffset(0xA4)]
        public float ChangeMpRate;
        [FieldOffset(0xA8)]
        public int ChangeMpPoint;
        [FieldOffset(0xAC)]
        public int MpRecoverChangeSpeed;
        [FieldOffset(0xB0)]
        public float ChangeStaminaRate;
        [FieldOffset(0xB4)]
        public int ChangeStaminaPoint;
        [FieldOffset(0xB8)]
        public int StaminaRecoverChangeSpeed;
        [FieldOffset(0xBC)]
        public float MagicEffectTimeChange;
        [FieldOffset(0xC0)]
        public int InsideDurability;
        [FieldOffset(0xC4)]
        public int MaxDurability;
        [FieldOffset(0xCC)]
        public int PoisonAttackPower;
        [FieldOffset(0xD0)]
        public int RegistIllness;
        [FieldOffset(0xD4)]
        public int BloodAttackPower;
        [FieldOffset(0xD8)]
        public int RegistCurse;
        [FieldOffset(0xDC)]
        public float FallDamageRate;
        [FieldOffset(0xE0)]
        public float SoulRate;
        [FieldOffset(0xE4)]
        public float EquipWeightChangeRate;
        [FieldOffset(0xE8)]
        public float AllItemWeightChangeRate;
        [FieldOffset(0xEC)]
        public int Soul;
        [FieldOffset(0xF0)]
        public int AnimidOffset;
        [FieldOffset(0xF4)]
        public float HaveSoulRate;
        [FieldOffset(0xF8)]
        public float TargetPriority;
        [FieldOffset(0xFC)]
        public int SightSearchEnemyCut;
        [FieldOffset(0x100)]
        public float HearingSearchEnemyCut;
        [FieldOffset(0x104)]
        public float GrabityRate;
        [FieldOffset(0x108)]
        public float RegistPoisonChangeRate;
        [FieldOffset(0x10C)]
        public float RegistToxicChangeRate;
        [FieldOffset(0x110)]
        public float RegistBloodChangeRate;
        [FieldOffset(0x114)]
        public float RegistCurseChangeRate;
        [FieldOffset(0x118)]
        public float SoulStealRate;
        [FieldOffset(0x11C)]
        public float LifeReductionRate;
        [FieldOffset(0x120)]
        public float HpRecoverRate;
        [FieldOffset(0x124)]
        public int ReplaceSpEffectId;
        [FieldOffset(0x128)]
        public int CycleOccurrenceSpEffectId;
        [FieldOffset(0x12C)]
        public int AtkOccurrenceSpEffectId;
        [FieldOffset(0x130)]
        public float GuardDefFlickPowerRate;
        [FieldOffset(0x134)]
        public float GuardStaminaCutRate;
        [FieldOffset(0x138)]
        public short RayCastPassedTime;
        [FieldOffset(0x13A)]
        public short ChangeSuperArmorPoint;
        [FieldOffset(0x13C)]
        public short BowDistRate;
        [FieldOffset(0x13E)]
        public ushort SpCategory;
        [FieldOffset(0x140)]
        public byte CategoryPriority;
        [FieldOffset(0x141)]
        public sbyte SaveCategory;
        [FieldOffset(0x142)]
        public byte ChangeMagicSlot;
        [FieldOffset(0x143)]
        public byte ChangeMiracleSlot;
        [FieldOffset(0x144)]
        public sbyte HeroPointDamage;
        [FieldOffset(0x145)]
        public byte DefFlickPower;
        [FieldOffset(0x146)]
        public byte FlickDamageCutRate;
        [FieldOffset(0x147)]
        public byte BloodDamageRate;
        [FieldOffset(0x148)]
        public sbyte DmgLv_None;
        [FieldOffset(0x149)]
        public sbyte DmgLv_S;
        [FieldOffset(0x14A)]
        public sbyte DmgLv_M;
        [FieldOffset(0x14B)]
        public sbyte DmgLv_L;
        [FieldOffset(0x14C)]
        public sbyte DmgLv_BlowM;
        [FieldOffset(0x14D)]
        public sbyte DmgLv_Push;
        [FieldOffset(0x14E)]
        public sbyte DmgLv_Strike;
        [FieldOffset(0x14F)]
        public sbyte DmgLv_BlowS;
        [FieldOffset(0x150)]
        public sbyte DmgLv_Min;
        [FieldOffset(0x151)]
        public sbyte DmgLv_Uppercut;
        [FieldOffset(0x152)]
        public sbyte DmgLv_BlowLL;
        [FieldOffset(0x153)]
        public sbyte DmgLv_Breath;
        [FieldOffset(0x154)]
        public byte AtkAttribute;
        [FieldOffset(0x155)]
        public byte SpAttribute;
        [FieldOffset(0x156)]
        public byte StateInfo;
        [FieldOffset(0x157)]
        public byte WepParamChange;
        [FieldOffset(0x158)]
        public byte MoveType;
        [FieldOffset(0x159)]
        public byte LifeReductionType;
        [FieldOffset(0x15A)]
        public byte ThrowCondition;
        [FieldOffset(0x15B)]
        public sbyte AddBehaviorJudgeId_condition;
        [FieldOffset(0x15C)]
        public byte AddBehaviorJudgeId_add;

        [FieldOffset(0x160)]
        public byte StrangeEnum;
        [FieldOffset(0x1AC)]
        public int FrostAttackPower;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 112)]
    public struct Magic
    {
        public int YesNoDialogMessageId;
        public int LimitCancelSpEffectId;
        public short SortId;
        public short RefId;
        public short Mp; //Focus point cost
        public short Stamina;
        public short Icon;
        public short BehaviorId;
        public short MtrlItemId;
        public short ReplaceMagicId;
        public short MaxQuantity;
        public byte Humanity;
        public byte OverDexterity;
        public byte SfxVariationid;
        public byte SlotLength;
        public byte RequirementIntellect;
        public byte RequirementFaith;
        public byte AnalogDexterityMin;
        public byte AnalogDexterityMax;
        public byte EzStateBehaviorType;
        public byte RefCategory;
        public byte SpEffectCategory;
        public byte RefType;
        public byte OpmeMenuType;
        public byte HasSpEffectType;
        public byte ReplaceCategory;
        public byte UseLimitCategory;
        //public bool VowType0;
        //public bool VowType1;
        //public bool VowType2;
        //public bool VowType3;
        //public bool VowType4;
        //public bool VowType5;
        //public bool VowType6;
        //public bool VowType7;
        //public bool Enable_multi;
        //public bool Enable_multi_only;
        //public bool IsEnchant;
        //public bool IsShieldEnchant;
        //public bool Enable_live;
        //public bool Enable_gray;
        //public bool Enable_white;
        //public bool Enable_black;
        //public bool DisableOffline;
        //public bool CastResonanceMagic;
        //public bool VowType8;
        //public bool VowType9;
        //public bool VowType10;
        //public bool VowType11;
        //public bool VowType12;
        //public bool VowType13;
        //public bool VowType14;
        //public bool VowType15;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 96)]
    public struct EquipParamAccesory
    {
        public int RefId;
        public int SfxVariationId;
        public float Weight;
        public int BehaviorId;
        public int BasicPrice;
        public int SellValue;
        public int SortId;
        public int QwcId;
        public short EquipModelId;
        public short IconId;
        public short ShopLv;
        public short TrophySGradeId;
        public short ThrophySeqId;
        public byte EquipModelCategory;
        public byte EquipModelGender;
        public byte AccessoryCategory;
        public byte RefCategory;
        public byte SpEffectCategory; 
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 580)]
    public struct NpcParam
    {
        public int BehaviorVariationId;
        public int AiThinkId;
        public int NameId;
        public float TurnVelocity;
        public float HitHeight;
        public float HitRadius;
        public uint Weight;
        public float HitYOffset;
        public uint Hp;
        public uint Mp;
        public uint GetSoul;
        public int ItemLotId_1;
        public int ItemLotId_2;
        public int ItemLotId_3;
        public int ItemLotId_4;
        public int ItemLotId_5;
        public int ItemLotId_6;
        public int HumanityLotId;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public int[] SpEffectID;
        public int GameClearSpEffectID;
        public float PhysGuardCutRate;
        public float MagGuardCutRate;
        public float FireGuardCutRate;
        public float ThunGuardCutRate;
        public int AnimIdOffset;
        public int MoveAnimId;
        public int SpMoveAnimId1;
        public int SpMoveAnimId2;
        public float NetworkWarpDist;
        public int DbgBehaviorR1;
        public int DbgBehaviorL1;
        public int DbgBehaviorR2;
        public int DbgBehaviorL2;
        public int DbgBehaviorRL;
        public int DbgBehaviorRR;
        public int DbgBehaviorRD;
        public int DbgBehaviorRU;
        public int DbgBehaviorLL;
        public int DbgBehaviorLR;
        public int DbgBehaviorLD;
        public int DbgBehaviorLU;
        public int AnimIdOffset2;
        public float PartsDamageRate1;
        public float PartsDamageRate2;
        public float PartsDamageRate3;
        public float PartsDamageRate4;
        public float PartsDamageRate5;
        public float PartsDamageRate6;
        public float PartsDamageRate7;
        public float PartsDamageRate8;
        public float WeakPartsDamageRate;
        public float SuperArmorRecoverCorrection;
        public float SuperArmorBrakeKnockbackDist;
        public ushort Stamina;
        public ushort StaminaRecoverBaseVel;
        public short Def_phys;
        public short Def_slash;
        public short Def_blow;
        public short Def_thrust;
        public short Def_mag;
        public short Def_fire;
        public short Def_thun;
        public ushort DefFlickPower;
        public ushort ResistPoison;
        public ushort ResistDisease;
        public ushort ResistBleed;
        public ushort ResistCurse;
        public short GhostModelId;
        public short NormalChangeResourceId;
        public short GuardAngle;
        public short SlashGuardCutRate;
        public short BlowGuardCutRate;
        public short ThrustGuardCutRate;
        public short SuperArmorDurability;
        public short NormalChangeTexChrId;
        public ushort DropType;
        public byte KnockbackRate;
        public byte KnockbackParamId;
        public byte FallDamageDump;
        public byte StaminaGuardDef;
        public byte PcAttrB;
        public byte PcAttrW;
        public byte PcAttrL;
        public byte PcAttrR;
        public byte AreaAttrB;
        public byte AreaAttrW;
        public byte AreaAttrL;
        public byte AreaAttrR;
        public byte MpRecoverBaseVel;
        public byte FlickDamageCutRate;
        public sbyte DefaultLodParamId;
        public byte DrawType;
        public byte NpcType;
        public byte TeamType;
        public byte MoveType;
        public byte LockDist;
        public byte Material;
        public byte MaterialSfx;
        public byte Material_weak;
        public byte MaterialSfx_weak;
        public byte PartsDamageType;
        public byte MaxUndurationAng;
        public sbyte GuardLevel;
        public byte BurnSfxType;
        public sbyte PoisonGuardResist;
        public sbyte DiseaseGuardResist;
        public sbyte BloodGuardResist;
        public sbyte CurseGuardResist;
        public byte ParryAttack;
        public byte ParryDefense;
        public byte SfxSize;
        public byte PushOutCamRegionRadius;
        public byte HitStopType;
        public byte LadderEndChkOffsetTop;
        public byte LadderEndChkOffsetLow;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
        public byte[] BitFields;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public Unknown[] Unknown1;
        public short Def_dark;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
        public Unknown[] Unknown2;
        public float Phys;
        public float Thrust;
        public float Strike;
        public float Slash;
        public float Magic;
        public float Fire;
        public float Lightning;
        public float Dark;
    }

    public struct Unknown
    {
        public byte Raw;

        public override string ToString()
        {
            return Raw.ToString();
        }
    }

    public struct Padding
    {
        public byte Raw;

        public override string ToString()
        {
            return Raw.ToString();
        }
    }
}
