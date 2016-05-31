namespace BigHat.ParamEntries
{
    [Param(128)]
    public class ReinforceParamWeapon : ParamEntry
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
        [Hidden]
        [PArrayLength(12)]
        public byte[] Unknown;
    }
}
