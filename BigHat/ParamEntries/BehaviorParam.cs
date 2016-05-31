namespace BigHat.ParamEntries
{
    [Param(32)]
    public class BehaviorParam : ParamEntry
    {
        public int VariationId;
        public int BehaviorJudgeId;
        public byte EzStateBehaviorType_old;
        public byte RefType;
        [Hidden]
        [PArrayLength(2)]
        public byte[] Padding1;
        public int ReferenceId;
        public int SfxVariationId;
        public int Stamina;
        public int Mp;
        public byte Category;
        public byte HeroPoint;
        [Hidden]
        [PArrayLength(2)]
        public byte[] Padding2;
    }
}
