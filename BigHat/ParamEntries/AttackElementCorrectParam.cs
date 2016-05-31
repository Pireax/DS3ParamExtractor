namespace BigHat.ParamEntries
{
    [Param(128)]
    public class AttackElementCorrectParam : ParamEntry
    {
        public int Bitmask;
        [PArrayLength(50)]
        public short[] v;
    }
}
