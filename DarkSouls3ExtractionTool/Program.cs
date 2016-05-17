using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace DarkSouls3ExtractionTool
{
    class Program
    {
        public enum Arguments
        {
            None        = 0x0, // 0b000
            Headers     = 0x1, // 0b001
            HeadersPlus = 0x3, // 0b011
            Debug       = 0x4, // 0b100
        }
        public static Arguments arguments = Arguments.None;

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

        public static bool ShouldSkip(Type fieldType, Type typeToSkip)
        {
            if ((arguments & Arguments.Debug) == Arguments.Debug)
                return false;

            if (fieldType == typeToSkip ||
                (fieldType.IsArray & fieldType.GetElementType() == typeToSkip))
                return true;
            else return false;
        }

        public static List<T2> GenerateListForeachField<T1,T2>(T1 structure, Action<List<T2>,System.Reflection.FieldInfo> action)
        {
            var fields = structure.GetType().GetFields();
            var returnVal = new List<T2>(fields.Length);
            foreach (var field in fields)
            {
                if (ShouldSkip(field.FieldType, typeof(Unknown)) || 
                    ShouldSkip(field.FieldType, typeof(Padding)))
                    continue;
                action(returnVal, field);
            }
                
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
            {
                if (y.FieldType.IsArray)
                {
                    var length = ((Array)y.GetValue(structure)).Length;
                    for (var i = 0; i < length; i++)
                        x.Add(y.Name + i);
                }
                else x.Add(y.Name);
            });
        }

        public static List<string> GetFieldOffsets<T>(T structure)
        {
            return GenerateListForeachField<T, string>(structure, (x, y) =>
            {
                if (y.FieldType.IsArray)
                {
                    var baseOffset = Marshal.OffsetOf(typeof(T), y.Name).ToInt32();
                    var array = (Array)y.GetValue(structure);
                    var length = array.Length;
                    var sizeOfArrayItem = Marshal.SizeOf(array.GetValue(0).GetType());
                    for (var i = 0; i < length; i++)
                        x.Add((baseOffset + i * sizeOfArrayItem).ToString("X"));
                }
                else x.Add(Marshal.OffsetOf(typeof(T), y.Name).ToString("X"));
            });
        }

        public static void TryExportParam<T>(string filename) where T : struct
        {
            if (!File.Exists(filename))
                return;

            Console.WriteLine($"Exporting {filename}...");

            var binaryData = File.ReadAllBytes(filename);
            var length = BitConverter.ToInt16(binaryData, 0xA);
            var baseOffset = BitConverter.ToUInt32(binaryData, 0x30);
            var fname = Path.GetFileNameWithoutExtension(filename);
            var exportFile = File.CreateText($"{fname}Out.csv");

            if ((arguments & Arguments.Headers) == Arguments.Headers ||
                (arguments & Arguments.Debug) == Arguments.Debug)
            {
                var firstStruct = BytesToStruct<T>(binaryData, (int)baseOffset);
                var fieldNames = GetFieldNames(firstStruct);
                if ((arguments & Arguments.HeadersPlus) == Arguments.HeadersPlus ||
                    (arguments & Arguments.Debug) == Arguments.Debug)
                {
                    var fieldOffsets = GetFieldOffsets(firstStruct);
                    exportFile.WriteLine(",,,{0}", string.Join(",", fieldOffsets));
                }
                exportFile.WriteLine("{0},{1},{2},{3}", "id", "index", "offset", string.Join(",", fieldNames));
            }

            for (var i = 0; i < length; i++)
            {
                var offset = BitConverter.ToUInt32(binaryData, 0x30 + 0x18 * i);
                var realOffset = (int)baseOffset + Marshal.SizeOf(typeof(T)) * i;
                var id = BitConverter.ToUInt32(binaryData, 0x40 + 0x18 * i);

                var structure = BytesToStruct<T>(binaryData, realOffset);
                var fieldValues = GetFieldValues(structure);

                exportFile.WriteLine("{0},{1},{2},{3}", id, i, realOffset, string.Join(",", fieldValues));
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
                        Console.WriteLine("-h\tHelp.");
                        Console.WriteLine("-H\tOutput column headers.");
                        Console.WriteLine("-H+\tOutput structure offsets and headers.");
                        Console.WriteLine("-d\tDebug Mode, outputs hidden columns.");
                        Environment.Exit(0);
                        break;
                    case "-H":
                        arguments |= Arguments.Headers;
                        break;
                    case "-H+":
                        arguments |= Arguments.HeadersPlus;
                        break;
                    case "-d":
                        arguments |= Arguments.Debug;
                        break;
                }
            }
        }

        static string ReadNullTerminatedWideString(byte[] data, int offset, int maxLength)
        {
            List<byte> charList = new List<byte>(maxLength);
            for (var i = 0; i < maxLength; i+=2)
            {
                if (offset + i + 1 >= data.Length)
                    break;
                var a = data[offset + i];
                var b = data[offset + i + 1];
                charList.Add(a);
                charList.Add(b);
                if (a == 0x0 & b == 0x0)
                    break;
            }
            return System.Text.Encoding.Unicode.GetString(charList.ToArray());
        }

        static void TryExportFmg(string filename)
        {
            if (!File.Exists(filename))
                return;

            Console.WriteLine($"Exporting {filename}...");

            var binaryData = File.ReadAllBytes(filename);
            var stringIndexTableBaseOffset = BitConverter.ToInt32(binaryData, 0x18);
            var length = BitConverter.ToInt32(binaryData, 0x0C);
            var fname = Path.GetFileNameWithoutExtension(filename);
            var exportFile = File.CreateText($"{fname}Out.csv");

            for (var i = 0; i < length; i++)
            {
                var IdLow = BitConverter.ToInt32(binaryData, 0x2C + i * 16);
                var IdHigh = BitConverter.ToInt32(binaryData, 0x30 + i * 16);
                var offset = BitConverter.ToInt32(binaryData, 0x28 + i * 16);
                for (var j = IdLow; j <= IdHigh; j++)
                {
                    var index = j - IdLow + offset;
                    var tableOffset = BitConverter.ToInt32(binaryData, stringIndexTableBaseOffset + index * 8);
                    if (tableOffset != 0)
                    {
                        var str = ReadNullTerminatedWideString(binaryData, tableOffset, 200);
                        exportFile.WriteLine("{0},{1},{2},{3},{4}", IdLow, IdHigh, j, tableOffset, str);
                    }
                    
                }
            }
            exportFile.Close();
        }

        static void Initialize()
        {
            var cultureInfo = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            cultureInfo.NumberFormat.NumberDecimalSeparator = ".";

            System.Threading.Thread.CurrentThread.CurrentCulture = cultureInfo;
        }

        static void Main(string[] args)
        {
            Initialize();
            ParseArgs(args);

            //TryExportFmg("武器名.fmg"); // Weapon Names
            //TryExportFmg("NPC名.fmg");
            TryExportParam<EquipParamWeapon>("EquipParamWeapon.param");
            TryExportParam<EquipParamAccesory>("EquipParamAccessory.param");
            TryExportParam<EquipParamProtector>("EquipParamProtector.param");
            TryExportParam<ReinforceParamWeapon>("ReinforceParamWeapon.param");
            TryExportParam<AttackElementCorrectParam>("AttackElementCorrectParam.param");
            TryExportParam<CalcCorrectGraph>("CalcCorrectGraph.param");
            TryExportParam<Magic>("Magic.param");
            TryExportParam<SpEffectParam>("SpEffectParam.param");
            TryExportParam<NpcParam>("NpcParam.param");
        }
    }
}
