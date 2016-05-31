namespace BigHat.ParamEntries
{
    [Param(424)]
    public class AtkParam : ParamEntry
    {
        public float Hit0_Radius;
        public float Hit1_Radius;
        public float Hit2_Radius;
        public float Hit3_Radius;
        public float KnockbackDist;
        public float HitStopTime;
        public int SpEffectId0;
        public int SpEffectId1;
        public int SpEffectId2;
        public int SpEffectId3;
        public int SpEffectId4;
        public short Hit0_DmyPoly1;
        public short Hit1_DmyPoly1;
        public short Hit2_DmyPoly1;
        public short Hit3_DmyPoly1;
        public short Hit0_DmyPoly2;
        public short Hit1_DmyPoly2;
        public short Hit2_DmyPoly2;
        public short Hit3_DmyPoly2;
        public ushort BlowingCorrection;
        public ushort AtkPhysCorrection;
        public ushort AtkMagCorrection;
        public ushort AtkFireCorrection;
        public ushort AtkThunCorrection;
        public ushort AtkStamCorrection;
        public ushort GuardAtkRateCorrection;
        public ushort GuardBreakCorrection;
        public ushort AtkThrowEscapeCorrection;
        public ushort AtkPhys;
        public ushort AtkMag;
        public ushort AtkFire;
        public ushort AtkThun;
        public ushort AtkStam;
        public ushort GuardAtkRate;
        public ushort GuardBreakRate;
        public ushort AtkSuperArmor;
        public ushort AtkThrowEscape;
        public ushort AtkObj;
        public short GuardStaminaCutRate;
        public short GuardRate;
        public short ThrowTypeId;
        public byte Hit0_hitType;
        public byte Hit1_hitType;
        public byte Hit2_hitType;
        public byte Hit3_hitType;
        public byte Hit0_Priority;
        public byte Hit1_Priority;
        public byte Hit2_Priority;
        public byte Hit3_Priority;
        public byte DamageLevel;
        public byte MapHitType;
        public sbyte GuardCutCancelRate;
        public byte AtkAttribute;
        public byte SpecialAttributes;
        public byte AttackType;
        public byte AtkMaterial;
        public byte AtkSize;
        public byte DefMaterial;
        public byte DefSfxMaterial;
        public byte HitSourceType;
        public byte ThrowFlag;
        public byte BitField;
        [Hidden]
        [PArrayLength(265)]
        public byte[] Unknown;
        public ushort AtkDarkCorrection;
    }
}
