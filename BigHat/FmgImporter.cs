using System;
using System.Collections.Generic;
using System.Linq;

namespace BigHat
{
    public class FmgImporter : IImporter
    {
        private readonly byte[] _data;

        public FmgImporter(string path)
        {
            _data = Utils.ReadBinaryDataFromFile(path);
            Count = BitConverter.ToInt32(_data, 0x0C);
        }

        public int Count { get; }

        public IList<TableEntry> Import()
        {
            return ImportFmg().Cast<TableEntry>().ToList();
        }

        public IList<FmgEntry> ImportFmg()
        {
            var stringIndexTableBaseOffset = BitConverter.ToInt32(_data, 0x18);
            var ret = new List<FmgEntry>(Count);

            for (var i = 0; i < Count; i++)
            {
                var idMin = BitConverter.ToInt32(_data, 0x2C + i*16);
                var idMax = BitConverter.ToInt32(_data, 0x30 + i*16);
                var offset = BitConverter.ToInt32(_data, 0x28 + i*16);
                for (var j = idMin; j <= idMax; j++)
                {
                    var index = j - idMin + offset;
                    var tableOffset = BitConverter.ToInt32(_data, stringIndexTableBaseOffset + index*8);
                    if (tableOffset != 0)
                    {
                        var str = Utils.ReadNullTerminatedWideString(_data, tableOffset, 2048);
                        ret.Add(new FmgEntry(idMin, idMax, j, str));
                    }
                }
            }
            return ret;
        }
    }
}