namespace BigHat.ParamEntries
{
    [Param(608)]
    public class EquipParamWeapon : ParamEntry
    {
        [PFieldOffset(0x0)]
        public uint BehaviorVariationId;
        [PFieldOffset(0x4)]
        public uint SortId;
        [PFieldOffset(0x8)]
        public int WanderingEquipId;
        [PFieldOffset(0xC)]
        public float Weight;
        [PFieldOffset(0x10)]
        public int WeaponWeightRate;
        [PFieldOffset(0x14)]
        public int FixPrice;
        [PFieldOffset(0x18)]
        public int BasicPrice;
        [PFieldOffset(0x1C)]
        public int SellValue;
        [PFieldOffset(0x20)]
        public float CorrectStrength;
        [PFieldOffset(0x24)]
        public float CorrectAgility;
        [PFieldOffset(0x28)]
        public float CorrectMagic;
        [PFieldOffset(0x2C)]
        public float CorrectFaith;
        [PFieldOffset(0x30)]
        public float PhysGuardCutRate;
        [PFieldOffset(0x34)]
        public float MagGuardCutRate;
        [PFieldOffset(0x38)]
        public float FireGuardCutRate;
        [PFieldOffset(0x3C)]
        public float ThunGuardCutRate;
        [PFieldOffset(0x40)]
        public int SpEffectBehaviorId1;
        [PFieldOffset(0x44)]
        public int SpEffectBehaviorId2;
        [PFieldOffset(0x48)]
        public int SpEffectBehaviorId3;
        [PFieldOffset(0x4C)]
        public int ResidentSpEffectId1;
        [PFieldOffset(0x50)]
        public int ResidentSpEffectId2;
        [PFieldOffset(0x54)]
        public int ResidentSpEffectId3;
        [PFieldOffset(0x58)]
        public int MaterialSetId;
        [PFieldOffset(0x5C)]
        public int OriginEquipWep;
        [PFieldOffset(0x60)]
        public int OriginEquipWep1;
        [PFieldOffset(0x64)]
        public int OriginEquipWep2;
        [PFieldOffset(0x68)]
        public int OriginEquipWep3;
        [PFieldOffset(0x6C)]
        public int OriginEquipWep4;
        [PFieldOffset(0x70)]
        public int OriginEquipWep5;
        [PFieldOffset(0x74)]
        public int OriginEquipWep6;
        [PFieldOffset(0x78)]
        public int OriginEquipWep7;
        [PFieldOffset(0x7C)]
        public int OriginEquipWep8;
        [PFieldOffset(0x80)]
        public int OriginEquipWep9;
        [PFieldOffset(0x84)]
        public int OriginEquipWep10;
        [PFieldOffset(0x88)]
        public int OriginEquipWep11;
        [PFieldOffset(0x8C)]
        public int OriginEquipWep12;
        [PFieldOffset(0x90)]
        public int OriginEquipWep13;
        [PFieldOffset(0x94)]
        public int OriginEquipWep14;
        [PFieldOffset(0x98)]
        public int OriginEquipWep15;
        [PFieldOffset(0x9C)]
        public float AntiDemonDamageRate;
        [PFieldOffset(0xA0)]
        public float AntSaintDamageRate;
        [PFieldOffset(0xA4)]
        public float AntWeakA_DamageRate;
        [PFieldOffset(0xA8)]
        public float AntWeakB_DamageRate;
        [PFieldOffset(0xAC)]
        public int VagrantItemLotId;
        [PFieldOffset(0xB0)]
        public int VagrantBonusEneDropItemLotId;
        [PFieldOffset(0xB4)]
        public int VagrantItemEneDropItemLotId;
        [PFieldOffset(0xB8)]
        public ushort EquipModelId;
        [PFieldOffset(0xBA)]
        public ushort IconId;
        [PFieldOffset(0xBC)]
        public short Durability;
        [PFieldOffset(0xBE)]
        public short MaxDurability;
        [PFieldOffset(0xC0)]
        public short ParryDamageLife;
        [PFieldOffset(0xC2)]
        public ushort AttackThrowEscape;
        [PFieldOffset(0xC4)]
        public short AttackBasePhysics;
        [PFieldOffset(0xC6)]
        public short AttackBaseMagic;
        [PFieldOffset(0xC8)]
        public short AttackBaseFire;
        [PFieldOffset(0xCA)]
        public short AttackBaseThunder;
        [PFieldOffset(0xCC)]
        public short AttackBaseStamina;
        [PFieldOffset(0xCE)]
        public short AttackBasePoise;
        [PFieldOffset(0xD0)]
        public short SaDurability;
        [PFieldOffset(0xD2)]
        public short GuardAngle;
        [PFieldOffset(0xD4)]
        public short Stability;
        [PFieldOffset(0xD6)]
        public short ReinforceTypeId;
        [PFieldOffset(0xD8)]
        public short TrophySGradeId;
        [PFieldOffset(0xDA)]
        public short ThrophySeqId;
        [PFieldOffset(0xDC)]
        public short ThrowAtkRate;
        [PFieldOffset(0xDE)]
        public short BowDistRate;
        [PFieldOffset(0xE0)]
        public byte EquipModelCategory;
        [PFieldOffset(0xE1)]
        public byte EquipModelGender;
        [PFieldOffset(0xE2)]
        public byte WeaponCategory;
        [PFieldOffset(0xE3)]
        public byte MotionCategory;
        [PFieldOffset(0xE4)]
        public byte GuardMotionCategory;
        [PFieldOffset(0xE5)]
        public byte AtkMaterial;
        [PFieldOffset(0xE6)]
        public byte DefMaterial;
        [PFieldOffset(0xE7)]
        public byte DefSfxMaterial;
        [PFieldOffset(0xE8)]
        public byte PhysicsStatFunc;
        [PFieldOffset(0xE9)]
        public ushort SpAttribute;
        [PFieldOffset(0xEB)]
        public byte SpAtkCategory;
        [PFieldOffset(0xEC)]
        public byte WepMotionOneHandId;
        [PFieldOffset(0xED)]
        public byte WepMotionBothHandId;
        [PFieldOffset(0xEE)]
        public byte ProperStrength;
        [PFieldOffset(0xEF)]
        public byte ProperAgility;
        [PFieldOffset(0xF0)]
        public byte ProperMagic;
        [PFieldOffset(0xF1)]
        public byte ProperFaith;
        [PFieldOffset(0xF2)]
        public byte OverStrength;
        [PFieldOffset(0xF3)]
        public byte AttackBaseParry;
        [PFieldOffset(0xF4)]
        public byte DefenseBaseParry;
        [PFieldOffset(0xF5)]
        public byte GuardBaseRepel;
        [PFieldOffset(0xF6)]
        public byte AttackBaseRepel;
        [PFieldOffset(0xF7)]
        public byte GuardCutCancelRate;
        [PFieldOffset(0xF8)]
        public byte GuardLevel;
        [PFieldOffset(0xF9)]
        public byte SlashGuardCutRate;
        [PFieldOffset(0xFA)]
        public byte BlowGuardCutRate;
        [PFieldOffset(0xFB)]
        public byte ThrustGuardCutRate;
        [PFieldOffset(0xFC)]
        public byte PoisonGuardResist;
        [PFieldOffset(0xFD)]
        public byte DiseaseGuardResist;
        [PFieldOffset(0xFE)]
        public byte BloodGuardResist;
        [PFieldOffset(0xFF)]
        public byte CurseGuardResist;
        [PFieldOffset(0x100)]
        public byte IsDurabilityDivergence;
        [PBitfield(0, 0x101)]
        public bool RightHandEquipable;
        [PBitfield(1, 0x101)]
        public bool LeftHandEquipable;
        [PFieldOffset(0x102)]
        public byte Buffs;
        [PFieldOffset(0x179)]
        public byte MagicStatFunc;
        [PFieldOffset(0x17A)]
        public byte FireStatFunc;
        [PFieldOffset(0x17B)]
        public byte ThunderStatFunc;
        [PFieldOffset(0x188)]
        public short AttackBaseDark;
        [PFieldOffset(0x18A)]
        public byte DarkStatFunc;
        [PFieldOffset(0x18B)]
        public byte PoisonStatFunc;
        [PFieldOffset(0x190)]
        public byte BleedStatFunc;
        [PFieldOffset(0x191)]
        public byte ProperLuck;
        [PFieldOffset(0x194)]
        public byte WeaponArt;
        [PFieldOffset(0x198)]
        public float CorrectLuck;
        [PFieldOffset(0x1A2)]
        public short WeaponClass;
        [PFieldOffset(0x228)]
        public int AecpId;
    }
}
