namespace BigHat.ParamEntries
{
    [Param(400)]
    public class EquipParamProtector : ParamEntry
    {
        [PFieldOffset(0x0)]
        public int SortId;
        [PFieldOffset(0x4)]
        public int WanderingEquipId;
        [PFieldOffset(0x8)]
        public int VagrantItemLotId;
        [PFieldOffset(0x0C)]
        public int VagrantBonusEneDropItemLotId;
        [PFieldOffset(0x10)]
        public int VagrantItemEneDropItemLotId;
        [PFieldOffset(0x14)]
        public int FixPrice;
        [PFieldOffset(0x18)]
        public int BasicPrice;
        [PFieldOffset(0x1C)]
        public int SellValue;
        [PFieldOffset(0x20)]
        public float Weight;
        [PFieldOffset(0x24)]
        public int ResidentSpEffectId1;
        [PFieldOffset(0x28)]
        public int ResidentSpEffectId2;
        [PFieldOffset(0x2C)]
        public int ResidentSpEffectId3;
        [PFieldOffset(0x30)]
        public int MaterialSetId;
        [PFieldOffset(0x34)]
        public float PartsDamageRate;
        [PFieldOffset(0xC0)]
        public short ResistPoison;
        [PFieldOffset(0xC2)]
        public short ResistToxic;
        [PFieldOffset(0xC4)]
        public short ResistBlood;
        [PFieldOffset(0xC6)]
        public byte ResistCurse;
        [PFieldOffset(0xE0)]
        public float DefensePhysics;
        [PFieldOffset(0xE4)]
        public float DefenseSlash;
        [PFieldOffset(0xE8)]
        public float DefenseStrike;
        [PFieldOffset(0xEC)]
        public float DefenseThrust;
        [PFieldOffset(0xF0)]
        public float DefenseMagic;
        [PFieldOffset(0xF4)]
        public float DefenseFire;
        [PFieldOffset(0xF8)]
        public float DefenseThunder;
        [PFieldOffset(0xA0)]
        public short EquipModelId;
        [PFieldOffset(0xA8)]
        public short KnockbackBounceRate;
        [PFieldOffset(0xAA)]
        public short Durability;
        [PFieldOffset(0xAC)]
        public short DurabilityMax;
        [PFieldOffset(0xAE)]
        public byte SaDurability;
        [PFieldOffset(0x110)]
        public float Poise;
        [PFieldOffset(0x118)]
        public float DefenseDark;
        [PFieldOffset(0x12C)]
        public short ResistFrost;
    }
}
