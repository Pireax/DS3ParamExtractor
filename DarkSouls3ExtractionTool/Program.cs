using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace DarkSouls3ExtractionTool
{
    class Program
    {
        public static bool headers;

        public static T BytesToStruct<T>(byte[] value, int startIndex) where T : struct
        {
            int size = Marshal.SizeOf(typeof(T));
            byte[] bytes = new byte[size];
            Array.ConstrainedCopy(value, startIndex, bytes, 0, size);

            var handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            T structure = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            handle.Free();

            return structure;
        }

        public static List<T2> GenerateListForeachField<T1,T2>(T1 structure, Action<List<T2>,System.Reflection.FieldInfo> action)
        {
            var fields = structure.GetType().GetFields();
            var returnVal = new List<T2>(fields.Length);
            foreach (var field in fields)
                action(returnVal, field);
            return returnVal;
        }

        public static List<object> GetFieldValues<T>(T structure)
        {
            return GenerateListForeachField<T, object>(structure, (x, y) =>
             {
                 var val = y.GetValue(structure);
                 if (val.GetType().IsArray)
                 {
                     var array = (Array)val;
                     foreach (var value in array)
                         x.Add(value);
                 }
                 else x.Add(val);
             });
        }

        public static List<string> GetFieldNames<T>(T structure)
        {
            return GenerateListForeachField<T, string>(structure, (x, y) =>
            x.Add(y.Name));
        }

        public static void TryExportParam<T>(string filename) where T : struct
        {
            if (!File.Exists(filename))
                return;

            var binaryData = File.ReadAllBytes(filename);
            var length = BitConverter.ToInt16(binaryData, 0xA);
            var baseOffset = BitConverter.ToUInt32(binaryData, 0x30);
            var fname = Path.GetFileNameWithoutExtension(filename);
            var exportFile = File.CreateText($"{fname}Out.csv");

            if (headers)
            {
                var fieldNames = GetFieldNames(new T());
                exportFile.WriteLine("{0},{1},{2}", "id", "index", string.Join(",", fieldNames));
            }

            for (var i = 0; i < length; i++)
            {
                var offset = BitConverter.ToUInt32(binaryData, 0x30 + 0x18 * i);
                var id = BitConverter.ToUInt32(binaryData, 0x40 + 0x18 * i);
                Console.WriteLine($"ID: {id}, INDEX: {i}, OFFSET: {offset}");
                var structure = BytesToStruct<T>(binaryData, (int)baseOffset + Marshal.SizeOf(typeof(T)) * i);

                var fieldValues = GetFieldValues(structure);
                exportFile.WriteLine("{0},{1},{2}", id, i, string.Join(",", fieldValues));
            }
            exportFile.Close();
        }

        static void ParseArgs(string[] args)
        {
            if (args.Length == 0)
                return;

            foreach(var arg in args)
            {
                switch(arg)
                {
                    case "-h":
                        Console.WriteLine("To use this tool simply place .param files from the game in the same folder and run it.");
                        Console.WriteLine("Arguments:");
                        Console.WriteLine("-h\tHelp");
                        Console.WriteLine("-H\tOutput column headers");
                        Environment.Exit(0);
                        break;
                    case "-H":
                        headers = true;
                        break;
                }
            }
        }

        static void Main(string[] args)
        {
            ParseArgs(args);

            TryExportParam<CalcCorrectGraph>("CalcCorrectGraph.param");
            TryExportParam<EquipParamWeapon>("EquipParamWeapon.param");
            TryExportParam<ReinforceParamWeapon>("ReinforceParamWeapon.param");
            TryExportParam<AttackElementCorrectParam>("AttackElementCorrectParam.param");
            TryExportParam<SpEffectParam>("SpEffectParam.param");
        }
    }
}
