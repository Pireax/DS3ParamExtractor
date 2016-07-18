using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Google.Protobuf;

namespace BigHat
{
    public class ProtoBufDataExporter
    {
        public static void Export(string filename, IList<TableEntry> data)
        {
            if (data.Count < 1)
                throw new ArgumentException("The list is empty.");

            var exportFile =
                File.Create(
                    $"{Path.Combine(Path.GetDirectoryName(filename), Path.GetFileNameWithoutExtension(filename))}Out.dat");

            var fieldNames = data[0].GetFieldNames();
            var entry0 = data[0];

            var pbName = entry0.GetType().Name;
            var protoClass = Type.GetType($"Ds3Ext.{entry0.GetType().Name}");
            var protoProperties = new List<PropertyInfo>(fieldNames.Count);
            protoProperties.AddRange(fieldNames.Select(fieldName => protoClass.GetProperty(FixStringProto(fieldName))));

            foreach (var entry in data)
            {
                var protoInstance = Activator.CreateInstance(protoClass);
                var fieldValues = entry.GetFieldValues();
                for (var i = 0; i < protoProperties.Count; i++)
                    protoProperties[i].SetValue(protoInstance, fieldValues[i]);
                ((IMessage) protoInstance).WriteDelimitedTo(exportFile);
            }

            exportFile.Close();
        }

        // protobuf removes '_' characters and uppercases the first character so we need to do that also.
        private static string FixStringProto(string fieldName)
        {
            return string.Concat(fieldName.Split('_').Select(str => str.First().ToString().ToUpper() + str.Substring(1)));
        }
    }
}