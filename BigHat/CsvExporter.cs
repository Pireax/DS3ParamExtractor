using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace BigHat
{
    public class CsvExporter
    {
        public static void Export(string filename, IList<TableEntry> data, bool includeHeaderRow = false,
            bool includeOffsetRow = false, bool ignoreHidden = true)
        {
            if (data.Count < 1)
                throw new ArgumentException("The list is empty.");

            var exportFile = File.CreateText(filename);

            if (includeHeaderRow)
            {
                var fieldNames = data[0].GetFieldNames(ignoreHidden);
                if (includeOffsetRow)
                {
                    var fieldOffsets = data[0].GetFieldOffsets(ignoreHidden).Select(x => x == -1 ? string.Empty : x.ToString("X"));
                    exportFile.WriteLine("{0}", string.Join(",", fieldOffsets));
                }
                exportFile.WriteLine("{0}", string.Join(",", fieldNames));
            }

            foreach (var entry in data)
            {
                var fieldValues = entry.GetFieldValues(ignoreHidden).Select(x =>
                {
                    if (x is bool)
                    {
                        if ((bool) x == true)
                            return 1;
                        else return 0;
                    }
                    return x;
                });
                exportFile.WriteLine("{0}", string.Join(",", fieldValues));
            }

            exportFile.Close();
        }
    }
}