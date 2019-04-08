using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using BigHat.ParamEntries;

namespace BigHat
{
    internal class Program
    {
        private static Arguments _arguments;
        private static ExportFormat _exportFormat;

        [Flags]
        public enum Arguments
        {
            None = 0x0, // 0b000
            Headers = 0x1, // 0b001
            HeadersPlus = 0x3, // 0b011
            Debug = 0x7 // 0b111
        }

        public enum ExportFormat
        {
            Csv,
            Proto
        }

        public static void TryExport(string filename)
        {
            try
            {
                Console.WriteLine($"Exporting {filename}...");
                TryExport(filename, CreateImporterForFile(filename));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static void TryExport(string filename, IImporter importer)
        {
            if (filename == null) throw new ArgumentNullException(nameof(filename));
            if (filename == string.Empty) throw new ArgumentException("filename is an empty string", nameof(filename));
            if (importer == null) throw new ArgumentNullException(nameof(importer));

            var outputPath =
                $"{Path.Combine(Path.GetDirectoryName(filename) ?? string.Empty, Path.GetFileNameWithoutExtension(filename))}Out.csv";

            if (_exportFormat == ExportFormat.Csv)
                CsvExporter.Export(outputPath, importer.Import(), (_arguments & Arguments.Headers) == Arguments.Headers,
                    (_arguments & Arguments.HeadersPlus) == Arguments.HeadersPlus,
                    (_arguments & Arguments.Debug) == Arguments.Debug);
            else if (_exportFormat == ExportFormat.Proto)
                ProtoBufDataExporter.Export(outputPath, importer.Import());
        }

        private static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            SetupNumberFormat();
            var path = ParseArgs(args);

            if (path == null)
            {
                ExportDefault();
                return;
            }

            var extension = Path.GetExtension(path);
            if (string.IsNullOrEmpty(extension))
                ExportDefault(path);
            else
                TryExport(path);
        }

        private static IImporter CreateImporterForFile(string filename)
        {
            switch (Path.GetExtension(filename))
            {
                case ".param":
                    return new ParamImporter(filename);
                case ".fmg":
                    return new FmgImporter(filename);
                default:
                    throw new ArgumentOutOfRangeException(nameof(filename), filename,
                        "Expected .param or .fmg file extension.");
            }
        }

        private static void ExportDefault(string folderPath = "")
        {
            string[] defaultFiles = {
                "NPC名.fmg", // Npc names
                "アイテムうんちく.fmg",
                "アイテム名.fmg",
                "アイテム説明.fmg",
                "アクセサリうんちく.fmg",
                "アクセサリ名.fmg",
                "アクセサリ説明.fmg",
                "地名.fmg",
                "武器うんちく.fmg",
                "武器名.fmg", // Weapon names
                // "武器説明.fmg", // Doesn't contain anything as far as I can see.
                "防具うんちく.fmg",
                "防具名.fmg",
                "防具説明.fmg",
                "魔法うんちく.fmg",
                "魔法名.fmg",
                "魔法説明.fmg",
                "EquipParamWeapon.param",
                "EquipParamAccessory.param",
                "EquipParamProtector.param",
                "ReinforceParamWeapon.param",
                "ReinforceParamProtector.param",
                "AttackElementCorrectParam.param",
                "CalcCorrectGraph.param",
                "Magic.param",
                "SpEffectParam.param",
                "NpcParam.param",
                "BehaviorParam.param",
                "BehaviorParam_PC.param",
                "AtkParam_Pc.param",
                "AtkParam_Npc.param"
            };

            foreach (var file in defaultFiles)
            {
                var filepath = Path.Combine(folderPath, file);
                if (File.Exists(filepath)) TryExport(filepath);
            }
        }

        private static void GenerateProto()
        {
            using (var t = new ProtoBufWriter("ds3ext.proto", "ds3ext"))
            {
                t.WriteMessage<EquipParamWeapon>();
                t.WriteMessage<EquipParamAccessory>();
                t.WriteMessage<EquipParamProtector>();
                t.WriteMessage<ReinforceParamWeapon>();
                t.WriteMessage<ReinforceParamProtector>();
                t.WriteMessage<AttackElementCorrectParam>();
                t.WriteMessage<CalcCorrectGraph>();
                t.WriteMessage<Magic>();
                t.WriteMessage<SpEffectParam>();
                t.WriteMessage<NpcParam>();
                t.WriteMessage<BehaviorParam>();
                t.WriteMessage<AtkParam>();
                t.WriteMessage<FmgEntry>();
            }
        }

        private static void SetupNumberFormat()
        {
            var cultureInfo = (CultureInfo) Thread.CurrentThread.CurrentCulture.Clone();
            cultureInfo.NumberFormat.NumberDecimalSeparator = ".";
            Thread.CurrentThread.CurrentCulture = cultureInfo;
        }

        private static bool IsFilePath(string str)
        {
            try
            {
                // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
                Path.GetFullPath(str);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static void OutputHelpAndExit()
        {
            Console.WriteLine(
                $"Usage: {Path.GetFileNameWithoutExtension(AppDomain.CurrentDomain.FriendlyName)} [<argument>] [<path>]");
            Console.WriteLine("Arguments:");
            Console.WriteLine(" -h\tHelp.");
            Console.WriteLine(" -H\tOutput column headers.");
            Console.WriteLine(" -H+\tOutput structure offsets and headers.");
            Console.WriteLine(" -d\tDebug Mode, outputs hidden columns.");
            Console.WriteLine(" -g\tGenerate .proto file.");
            Console.WriteLine(" -p\tOutput in protobuf format.");
            Environment.Exit(0);
        }

        private static string ParseArgs(string[] args)
        {
            if (!args.Any())
                return null;

            string path = null;
            switch (args[0])
            {
                case "-h":
                    OutputHelpAndExit();
                    break;
                case "-H":
                    _arguments |= Arguments.Headers;
                    break;
                case "-H+":
                    _arguments |= Arguments.HeadersPlus;
                    break;
                case "-d":
                    _arguments |= Arguments.Debug;
                    break;
                case "-g":
                    GenerateProto();
                    Environment.Exit(0);
                    break;
                case "-p":
                    _exportFormat = ExportFormat.Proto;
                    break;
                default:
                    if (IsFilePath(args[0]))
                        path = args[0];
                    else OutputHelpAndExit();
                    break;
            }

            if (args.Length > 1)
            {
                if (IsFilePath(args[1]))
                    path = args[1];
                else OutputHelpAndExit();
            }

            return path;
        }
    }
}