using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;

namespace DarkSouls3ExtractionTool
{
    internal class Program
    {
        [Flags]
        public enum Arguments
        {
            None        = 0x0, // 0b000
            Headers     = 0x1, // 0b001
            HeadersPlus = 0x3, // 0b011
            Debug       = 0x4, // 0b100
        }

        private static Arguments _arguments;
        private static readonly StructureHelper StructHelper = new StructureHelper();

        private static string ReadNullTerminatedWideString(IReadOnlyList<byte> data, int offset, int maxLength)
        {
            List<byte> charList = new List<byte>(maxLength);
            for (var i = 0; i < maxLength; i += 2)
            {
                if (offset + i + 1 >= data.Count)
                    break;
                var a = data[offset + i];
                var b = data[offset + i + 1];
                charList.Add(a);
                charList.Add(b);
                if (a == 0x0 & b == 0x0)
                    break;
            }
            return Encoding.Unicode.GetString(charList.ToArray());
        }

        public static void TryExportFmg(string filename)
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
                var idMin = BitConverter.ToInt32(binaryData, 0x2C + i * 16);
                var idMax = BitConverter.ToInt32(binaryData, 0x30 + i * 16);
                var offset = BitConverter.ToInt32(binaryData, 0x28 + i * 16);
                for (var j = idMin; j <= idMax; j++)
                {
                    var index = j - idMin + offset;
                    var tableOffset = BitConverter.ToInt32(binaryData, stringIndexTableBaseOffset + index * 8);
                    if (tableOffset != 0)
                    {
                        var str = ReadNullTerminatedWideString(binaryData, tableOffset, 200);
                        exportFile.WriteLine("{0},{1},{2},{3},{4}", idMin, idMax, j, tableOffset, str);
                    }

                }
            }
            exportFile.Close();
        }

        public static byte[] GetBinaryDataFromFile(string filename)
        {
            if (!File.Exists(filename))
                throw new ArgumentException();

            Console.WriteLine($"Exporting {filename}...");

            return File.ReadAllBytes(filename);
        }

        public static IList<Param<T>> TryExportParam<T>(byte[] binaryData) where T : struct
        {
            var length = BitConverter.ToInt16(binaryData, 0xA);
            var ret = new List<Param<T>>(length);
            var baseOffset = BitConverter.ToUInt32(binaryData, 0x30);

            for (var i = 0; i < length; i++)
            {
                var realOffset = (int)baseOffset + Marshal.SizeOf(typeof(T)) * i;
                var id = BitConverter.ToUInt32(binaryData, 0x40 + 0x18 * i);

                var structure = StructureHelper.BytesToStruct<T>(binaryData, realOffset);
                ret.Add(new Param<T>(id, i, realOffset, structure));
            }
            return ret;
        }

        public static void TryExportParam<T>(string filename, byte[] binaryData) where T : struct
        {
            var fname = Path.GetFileNameWithoutExtension(filename);
            var exportFile = File.CreateText($"{fname}Out.csv");

            if ((_arguments & Arguments.Headers) == Arguments.Headers ||
                (_arguments & Arguments.Debug) == Arguments.Debug)
            {
                var fieldNames = StructHelper.GetFieldNames<T>();
                if ((_arguments & Arguments.HeadersPlus) == Arguments.HeadersPlus ||
                    (_arguments & Arguments.Debug) == Arguments.Debug)
                {
                    var fieldOffsets = StructHelper.GetFieldOffsets<T>().Select(x => x.ToString("X"));
                    exportFile.WriteLine(",,,{0}", string.Join(",", fieldOffsets));
                }
                exportFile.WriteLine("{0},{1},{2},{3}", "id", "index", "offset", string.Join(",", fieldNames));
            }

            var paramData = TryExportParam<T>(binaryData);
            foreach (var param in paramData)
            {
                var fieldValues = StructHelper.GetFieldValues(param.Structure);
                exportFile.WriteLine("{0},{1},{2},{3}", param.Id, param.Index, param.Offset, string.Join(",", fieldValues));
            }

            exportFile.Close();
        }

        public static void TryExportParam<T>(string filename) where T : struct
        {
            var binaryData = GetBinaryDataFromFile(filename);
            TryExportParam<T>(filename, binaryData);
        }

        public static void TryExportParam(string filename)
        {
            var binaryData = GetBinaryDataFromFile(filename);
            var id = BitConverter.ToInt32(binaryData, 0);
            switch (id)
            {
                case 0x00030828:
                case 0x0003CDB0:
                    TryExportParam<BehaviorParam>(filename, binaryData);
                    break;
                case 0x00162D40:
                case 0x00182000:
                    TryExportParam<AtkParam>(filename, binaryData);
                    break;
                case 0x00036918:
                    TryExportParam<EquipParamProtector>(filename, binaryData);
                    break;
                case 0x000039E8:
                    TryExportParam<EquipParamAccesory>(filename, binaryData);
                    break;
                case 0x00001C48:
                    TryExportParam<CalcCorrectGraph>(filename, binaryData);
                    break;
                case 0x00002180:
                    TryExportParam<AttackElementCorrectParam>(filename, binaryData);
                    break;
                case 0x00164988:
                    TryExportParam<EquipParamWeapon>(filename, binaryData);
                    break;
                case 0x00007300:
                    TryExportParam<Magic>(filename, binaryData);
                    break;
                case 0x00073A00:
                    TryExportParam<NpcParam>(filename, binaryData);
                    break;
                case 0x00000618:
                    TryExportParam<ReinforceParamProtector>(filename, binaryData);
                    break;
                case 0x00010320:
                    TryExportParam<ReinforceParamWeapon>(filename, binaryData);
                    break;
                case 0x001E5D88:
                    TryExportParam<SpEffectParam>(filename, binaryData);
                    break;
                default:
                    Console.WriteLine("This .param file is not yet supported.");
                    break;
            }
        }

        private static void OutputHelp()
        {
            Console.WriteLine("To use this tool simply place .param files from the game in the same folder and run it.");
            Console.WriteLine("Alternatively give this tool the file path as argument, this also supports .fmg files.");
            Console.WriteLine("Arguments (for .param):");
            Console.WriteLine("-h\tHelp.");
            Console.WriteLine("-H\tOutput column headers.");
            Console.WriteLine("-H+\tOutput structure offsets and headers.");
            Console.WriteLine("-d\tDebug Mode, outputs hidden columns.");
        }

        private static bool IsFilePath(string str)
        {
            try
            {
                var fullPath = Path.GetFullPath(str);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static string ParseArgs(IEnumerable<string> args)
        {
            string path = null;
            foreach(var arg in args)
            {
                switch(arg)
                {
                    case "-h":
                        OutputHelp();
                        Environment.Exit(0);
                        break;
                    case "-H":
                        _arguments |= Arguments.Headers;
                        break;
                    case "-H+":
                        _arguments |= Arguments.HeadersPlus;
                        break;
                    case "-d":
                        _arguments |= Arguments.Debug;
                        StructHelper.TypesToSkip = new List<Type>();
                        break;
                    default:
                        if (IsFilePath(arg))
                            path = arg;
                        break;
                }
            }
            return path;
        }

        private static void Initialize()
        {
            var cultureInfo = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            cultureInfo.NumberFormat.NumberDecimalSeparator = ".";

            System.Threading.Thread.CurrentThread.CurrentCulture = cultureInfo;

            StructHelper.TypesToSkip.AddRange(new[] { typeof(Unknown), typeof(Padding) });
        }

        private static void Main(string[] args)
        {
            Initialize();
            var path = ParseArgs(args);

            if (path != null)
            {
                var extension = Path.GetExtension(path);
                if (extension == ".fmg")
                    TryExportFmg(path);
                else if (extension == ".param")
                    TryExportParam(path);
                return;
            }

            //TryExportFmg("武器名.fmg"); // Weapon Names
            //TryExportFmg("NPC名.fmg"); // Npc Names
            TryExportParam<EquipParamWeapon>("EquipParamWeapon.param");
            TryExportParam<EquipParamAccesory>("EquipParamAccessory.param");
            TryExportParam<EquipParamProtector>("EquipParamProtector.param");
            TryExportParam<ReinforceParamWeapon>("ReinforceParamWeapon.param");
            TryExportParam<AttackElementCorrectParam>("AttackElementCorrectParam.param");
            TryExportParam<CalcCorrectGraph>("CalcCorrectGraph.param");
            TryExportParam<Magic>("Magic.param");
            TryExportParam<SpEffectParam>("SpEffectParam.param");
            TryExportParam<NpcParam>("NpcParam.param");
            TryExportParam<BehaviorParam>("BehaviorParam.param");
            TryExportParam<BehaviorParam>("BehaviorParam_PC.param");
            TryExportParam<AtkParam>("AtkParam_Pc.param");
            TryExportParam<AtkParam>("AtkParam_Npc.param");
            //var t = new ProtoBufWriter("test.proto");
            //t.WriteMessage<EquipParamWeapon>("EquipParamWeapon");
            //t.Dispose();
        }

        public class Param<T>
        {
            public uint Id;
            public int Index;
            public int Offset;
            public T Structure;

            public Param(uint id, int index, int offset, T structure)
            {
                Id = id;
                Index = index;
                Offset = offset;
                Structure = structure;
            } 
        }

        public class FmgEntry
        {
            public int IdMin;
            public int IdMax;
            public string Text;

            public FmgEntry(int idMin, int idMax, string text)
            {
                IdMin = idMin;
                IdMax = idMax;
                Text = text;
            }
        }
    }
}
