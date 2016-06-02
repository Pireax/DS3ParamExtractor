namespace BigHat.ParamEntries
{
    [Param(800)]
    public class SpEffectParam : ParamEntry
    {
        [PFieldOffset(0x0)]
        public int IconId;
        [PFieldOffset(0x4)]
        public float ConditionHp;
        [PFieldOffset(0x8)]
        public float EffectEndurance;
        [PFieldOffset(0xC)]
        public int MotionInterval;
        [PFieldOffset(0x10)]
        public float MaxHpRate;
        [PFieldOffset(0x14)]
        public float MaxMpRate;
        [PFieldOffset(0x18)]
        public float MaxStaminaRate;
        [PFieldOffset(0x1C)]
        public float SlashDamageCutRate;
        [PFieldOffset(0x20)]
        public float BlowDamageCutRate;
        [PFieldOffset(0x24)]
        public float ThrustDamageCutRate;
        [PFieldOffset(0x28)]
        public float NeutralDamageCutRate;
        [PFieldOffset(0x2C)]
        public float MagicDamageCutRate;
        [PFieldOffset(0x30)]
        public float FireDamageCutRate;
        [PFieldOffset(0x34)]
        public float ThunderDamageCutRate;
        [PFieldOffset(0x38)]
        public float PhysicsAttackRate;
        [PFieldOffset(0x3C)]
        public float MagicAttackRate;
        [PFieldOffset(0x40)]
        public float FireAttackRate;
        [PFieldOffset(0x44)]
        public float ThunderAttackRate;
        [PFieldOffset(0x48)]
        public float PhysicsAttackPowerRate;
        [PFieldOffset(0x4C)]
        public float MagicAttackPowerRate;
        [PFieldOffset(0x50)]
        public float FireAttackPowerRate;
        [PFieldOffset(0x54)]
        public float ThunderAttackPowerRate;
        [PFieldOffset(0x58)]
        public int PhysicsAttackPower;
        [PFieldOffset(0x5C)]
        public int MagicAttackPower;
        [PFieldOffset(0x60)]
        public int FireAttackPower;
        [PFieldOffset(0x64)]
        public int ThunderAttackPower;
        [PFieldOffset(0x68)]
        public float PhysicsDifferenceRate;
        [PFieldOffset(0x6C)]
        public float MagicDifferenceRate;
        [PFieldOffset(0x70)]
        public float FireDifferenceRate;
        [PFieldOffset(0x74)]
        public float ThunderDifferenceRate;
        [PFieldOffset(0x78)]
        public int PhysicsDifference;
        [PFieldOffset(0x7C)]
        public int MagicDifference;
        [PFieldOffset(0x80)]
        public int FireDifference;
        [PFieldOffset(0x84)]
        public int ThunderDifference;
        [PFieldOffset(0x88)]
        public float NoGuardDamageRate;
        [PFieldOffset(0x8C)]
        public float VitalSpotChangeRate;
        [PFieldOffset(0x90)]
        public float NormalSpotChangeRate;
        [PFieldOffset(0x94)]
        public float MaxHpChangeRate;
        [PFieldOffset(0x98)]
        public int BehaviorId;
        [PFieldOffset(0x9C)]
        public float ChangeHpRate;
        [PFieldOffset(0xA0)]
        public int ChangeHpPoint;
        [PFieldOffset(0xA4)]
        public float ChangeMpRate;
        [PFieldOffset(0xA8)]
        public int ChangeMpPoint;
        [PFieldOffset(0xAC)]
        public int MpRecoverChangeSpeed;
        [PFieldOffset(0xB0)]
        public float ChangeStaminaRate;
        [PFieldOffset(0xB4)]
        public int ChangeStaminaPoint;
        [PFieldOffset(0xB8)]
        public int StaminaRecoverChangeSpeed;
        [PFieldOffset(0xBC)]
        public float MagicEffectTimeChange;
        [PFieldOffset(0xC0)]
        public int InsideDurability;
        [PFieldOffset(0xC4)]
        public int MaxDurability;
        [PFieldOffset(0xCC)]
        public int PoisonAttackPower;
        [PFieldOffset(0xD0)]
        public int RegistIllness;
        [PFieldOffset(0xD4)]
        public int BloodAttackPower;
        [PFieldOffset(0xD8)]
        public int RegistCurse;
        [PFieldOffset(0xDC)]
        public float FallDamageRate;
        [PFieldOffset(0xE0)]
        public float SoulRate;
        [PFieldOffset(0xE4)]
        public float EquipWeightChangeRate;
        [PFieldOffset(0xE8)]
        public float AllItemWeightChangeRate;
        [PFieldOffset(0xEC)]
        public int Soul;
        [PFieldOffset(0xF0)]
        public int AnimidOffset;
        [PFieldOffset(0xF4)]
        public float HaveSoulRate;
        [PFieldOffset(0xF8)]
        public float TargetPriority;
        [PFieldOffset(0xFC)]
        public int SightSearchEnemyCut;
        [PFieldOffset(0x100)]
        public float HearingSearchEnemyCut;
        [PFieldOffset(0x104)]
        public float GrabityRate;
        [PFieldOffset(0x108)]
        public float RegistPoisonChangeRate;
        [PFieldOffset(0x10C)]
        public float RegistToxicChangeRate;
        [PFieldOffset(0x110)]
        public float RegistBloodChangeRate;
        [PFieldOffset(0x114)]
        public float RegistCurseChangeRate;
        [PFieldOffset(0x118)]
        public float SoulStealRate;
        [PFieldOffset(0x11C)]
        public float LifeReductionRate;
        [PFieldOffset(0x120)]
        public float HpRecoverRate;
        [PFieldOffset(0x124)]
        public int ReplaceSpEffectId;
        [PFieldOffset(0x128)]
        public int CycleOccurrenceSpEffectId;
        [PFieldOffset(0x12C)]
        public int AtkOccurrenceSpEffectId;
        [PFieldOffset(0x130)]
        public float GuardDefFlickPowerRate;
        [PFieldOffset(0x134)]
        public float GuardStaminaCutRate;
        [PFieldOffset(0x138)]
        public short RayCastPassedTime;
        [PFieldOffset(0x13A)]
        public short ChangeSuperArmorPoint;
        [PFieldOffset(0x13C)]
        public short BowDistRate;
        [PFieldOffset(0x13E)]
        public ushort SpCategory;
        [PFieldOffset(0x140)]
        public byte CategoryPriority;
        [PFieldOffset(0x141)]
        public sbyte SaveCategory;
        [PFieldOffset(0x142)]
        public byte ChangeMagicSlot;
        [PFieldOffset(0x143)]
        public byte ChangeMiracleSlot;
        [PFieldOffset(0x144)]
        public sbyte HeroPointDamage;
        [PFieldOffset(0x145)]
        public byte DefFlickPower;
        [PFieldOffset(0x146)]
        public byte FlickDamageCutRate;
        [PFieldOffset(0x147)]
        public byte BloodDamageRate;
        [PFieldOffset(0x148)]
        public sbyte DmgLv_None;
        [PFieldOffset(0x149)]
        public sbyte DmgLv_S;
        [PFieldOffset(0x14A)]
        public sbyte DmgLv_M;
        [PFieldOffset(0x14B)]
        public sbyte DmgLv_L;
        [PFieldOffset(0x14C)]
        public sbyte DmgLv_BlowM;
        [PFieldOffset(0x14D)]
        public sbyte DmgLv_Push;
        [PFieldOffset(0x14E)]
        public sbyte DmgLv_Strike;
        [PFieldOffset(0x14F)]
        public sbyte DmgLv_BlowS;
        [PFieldOffset(0x150)]
        public sbyte DmgLv_Min;
        [PFieldOffset(0x151)]
        public sbyte DmgLv_Uppercut;
        [PFieldOffset(0x152)]
        public sbyte DmgLv_BlowLL;
        [PFieldOffset(0x153)]
        public sbyte DmgLv_Breath;
        [PFieldOffset(0x154)]
        public byte AtkAttribute;
        [PFieldOffset(0x155)]
        public byte SpAttribute;
        [PFieldOffset(0x156)]
        public byte StateInfo;
        [PFieldOffset(0x157)]
        public byte WepParamChange;
        [PFieldOffset(0x158)]
        public byte MoveType;
        [PFieldOffset(0x159)]
        public byte LifeReductionType;
        [PFieldOffset(0x15A)]
        public byte ThrowCondition;
        [PFieldOffset(0x15B)]
        public sbyte AddBehaviorJudgeId_condition;
        [PFieldOffset(0x15C)]
        public byte AddBehaviorJudgeId_add;

        // Birdulon's https://docs.google.com/spreadsheets/d/1Wn2G4Z7yCbXdBjIdCUSusfvOWg75-kFbswKTPm4PcEQ/pubhtml
        [PBitfield(0, 0x15D)]
        public bool EffectTargetSelf;
        [PBitfield(1, 0x15D)]
        public bool EffectTargetFriend;
        [PBitfield(2, 0x15D)]
        public bool EffectTargetEnemy;
        [PBitfield(3, 0x15D)]
        public bool EffectTargetPlayer;
        [PBitfield(4, 0x15D)]
        public bool EffectTargetAI;
        [PBitfield(5, 0x15D)]
        public bool EffectTargetLive;
        [PBitfield(6, 0x15D)]
        public bool EffectTargetGhost;
        [PBitfield(7, 0x15D)]
        public bool EffectTargetWhiteGhost;
        [PBitfield(0, 0x15E)]
        public bool EffectTargetBlackGhost;
        [PBitfield(1, 0x15E)]
        public bool EffectTargetAttacker;
        [PBitfield(2, 0x15E)]
        public bool DispIconNonActive;
        [PBitfield(3, 0x15E)]
        public bool UseSpEffectEffect;
        [PBitfield(4, 0x15E)]
        public bool AdjustMagicAbility;
        [PBitfield(5, 0x15E)]
        public bool AdjustFaithAbility;
        [PBitfield(6, 0x15E)]
        public bool GameClearBonus;
        [PBitfield(7, 0x15E)]
        public bool MagParamChange;
        [PBitfield(0, 0x15F)]
        public bool MiracleParamChange;
        [PBitfield(1, 0x15F)]
        public bool ClearSoul;
        [PBitfield(2, 0x15F)]
        public bool RequestSOS;
        [PBitfield(3, 0x15F)]
        public bool RequestBlackSOS;
        [PBitfield(4, 0x15F)]
        public bool RequestForceJoinBlackSOS;
        [PBitfield(5, 0x15F)]
        public bool RequestKickSession;
        [PBitfield(6, 0x15F)]
        public bool RequestLeaveSession;
        [PBitfield(7, 0x15F)]
        public bool RequestNpcInveda;
        [PBitfield(0, 0x160)]
        public bool NoDead;
        [PBitfield(1, 0x160)]
        public bool CurrHPIndependeMax;
        [PBitfield(2, 0x160)]
        public bool CorrosionIgnore;
        [PBitfield(3, 0x160)]
        public bool SightSearchIgnore;
        [PBitfield(4, 0x160)]
        public bool HearingSearchCutIgnore;
        [PBitfield(5, 0x160)]
        public bool AntiMagicIgnore;
        [PBitfield(6, 0x160)]
        public bool FakeTargetIgnore;
        [PBitfield(7, 0x160)]
        public bool FakeTargetIgnoreUndead;
        [PBitfield(0, 0x161)]
        public bool FakeTargetIgnoreAnimal;
        [PBitfield(1, 0x161)]
        public bool GrabityIgnore;
        [PBitfield(2, 0x161)]
        public bool DisablePoison;
        [PBitfield(3, 0x161)]
        public bool DisableDisease;
        [PBitfield(4, 0x161)]
        public bool DisableBlood;
        [PBitfield(5, 0x161)]
        public bool DisableCurse;
        [PBitfield(6, 0x161)]
        public bool EnableCharm;
        [PBitfield(7, 0x161)]
        public bool EnableLifeTime;
        [PBitfield(0, 0x162)]
        public bool HasTarget;
        [PBitfield(1, 0x162)]
        public bool IsFireDamageCancel;
        [PBitfield(2, 0x162)]
        public bool IsExtendSpEffectLife;
        [PBitfield(3, 0x162)]
        public bool RequestLeaveColiseumSession;
        [PBitfield(0, 0x163)]
        public bool VowType0;
        [PBitfield(1, 0x163)]
        public bool VowType1;
        [PBitfield(2, 0x163)]
        public bool VowType2;
        [PBitfield(3, 0x163)]
        public bool VowType3;
        [PBitfield(4, 0x163)]
        public bool VowType4;
        [PBitfield(5, 0x163)]
        public bool VowType5;
        [PBitfield(6, 0x163)]
        public bool VowType6;
        [PBitfield(7, 0x163)]
        public bool VowType7;
        [PBitfield(0, 0x164)]
        public bool VowType8;
        [PBitfield(1, 0x164)]
        public bool VowType9;
        [PBitfield(2, 0x164)]
        public bool VowType10;
        [PBitfield(3, 0x164)]
        public bool VowType11;
        [PBitfield(4, 0x164)]
        public bool VowType12;
        [PBitfield(5, 0x164)]
        public bool VowType13;
        [PBitfield(6, 0x164)]
        public bool VowType14;
        [PBitfield(7, 0x164)]
        public bool VowType15;
        //

        [PFieldOffset(0x1AC)]
        public int FrostAttackPower;

        [PFieldOffset(0x1D8)]
        public float DarkDifferenceRate;
    }
}
