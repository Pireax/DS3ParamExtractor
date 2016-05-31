using System.Collections.Generic;
using System.Reflection;

namespace BigHat
{
    public class FmgEntry : TableEntry
    {
        public int Id;
        public int IdMax;
        public int IdMin;
        public string Text;

        public FmgEntry()
        {
        }

        public FmgEntry(int idMin, int idMax, int id, string text)
        {
            IdMin = idMin;
            IdMax = idMax;
            Id = id;
            Text = text;
        }

        public override IReadOnlyList<FieldInfo> GetFields()
        {
            return GetType().GetFields();
        }
    }
}