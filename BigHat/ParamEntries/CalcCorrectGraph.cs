namespace BigHat.ParamEntries
{
    [Param(80)]
    public class CalcCorrectGraph : ParamEntry
    {
        [PArrayLength(5)]
        public float[] StageMaxVal;
        [PArrayLength(5)]
        public float[] StageMaxGrowVal;
        [PArrayLength(5)]
        public float[] AdjPt_maxGrowVal;
        public float Init_inclination_soul;
        public float Adjustment_value;
        public float Boundry_inclination_soul;
        public float Boundry_value;
    }
}
