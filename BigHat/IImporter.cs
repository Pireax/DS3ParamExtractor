using System.Collections.Generic;

namespace BigHat
{
    public interface IImporter
    {
        IList<TableEntry> Import();
    }
}
