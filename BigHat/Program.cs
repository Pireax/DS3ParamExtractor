using System;
using System.Globalization;
using System.IO;
using System.Linq;
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

        public static void TryExport(string filename, IImporter importer)
        {
            Console.WriteLine($"Exporting {filename}...");
            var outputPath =
                $"{Path.Combine(Path.GetDirectoryName(filename), Path.GetFileNameWithoutExtension(filename))}Out.csv";

            if (_exportFormat == ExportFormat.Csv)
                CsvExporter.Export(outputPath, importer.Import(), (_arguments & Arguments.Headers) == Arguments.Headers,
                    (_arguments & Arguments.HeadersPlus) == Arguments.HeadersPlus,
                    (_arguments & Arguments.Debug) == Arguments.Debug);
            else if (_exportFormat == ExportFormat.Proto)
                ProtoBufDataExporter.Export(outputPath, importer.Import());
        }

        public static void TryExportFmg(string filename)
        {
            FmgImporter importer;
            try
            {
                importer = new FmgImporter(filename);
            }
            catch (Exception)
            {
                return;
            }
            TryExport(filename, importer);
        }

        public static void TryExportParam(string filename)
        {
            ParamImporter importer;
            try
            {
                importer = new ParamImporter(filename);
            }
            catch (Exception)
            {
                return;
            }
            TryExport(filename, importer);
        }

        private static void ExportDefault(string folderPath = "")
        {
            TryExportFmg(Path.Combine(folderPath, "武器名.fmg")); // Weapon Names
            TryExportFmg(Path.Combine(folderPath, "NPC名.fmg")); // Npc Names
            TryExportParam(Path.Combine(folderPath, "EquipParamWeapon.param"));
            TryExportParam(Path.Combine(folderPath, "EquipParamAccessory.param"));
            TryExportParam(Path.Combine(folderPath, "EquipParamProtector.param"));
            TryExportParam(Path.Combine(folderPath, "ReinforceParamWeapon.param"));
            TryExportParam(Path.Combine(folderPath, "ReinforceParamProtector.param"));
            TryExportParam(Path.Combine(folderPath, "AttackElementCorrectParam.param"));
            TryExportParam(Path.Combine(folderPath, "CalcCorrectGraph.param"));
            TryExportParam(Path.Combine(folderPath, "Magic.param"));
            TryExportParam(Path.Combine(folderPath, "SpEffectParam.param"));
            TryExportParam(Path.Combine(folderPath, "NpcParam.param"));
            TryExportParam(Path.Combine(folderPath, "BehaviorParam.param"));
            TryExportParam(Path.Combine(folderPath, "BehaviorParam_PC.param"));
            TryExportParam(Path.Combine(folderPath, "AtkParam_Pc.param"));
            TryExportParam(Path.Combine(folderPath, "AtkParam_Npc.param"));
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

        private static void Initialize()
        {
            var cultureInfo = (CultureInfo) Thread.CurrentThread.CurrentCulture.Clone();
            cultureInfo.NumberFormat.NumberDecimalSeparator = ".";

            Thread.CurrentThread.CurrentCulture = cultureInfo;
        }

        private static bool IsFilePath(string str)
        {
            try
            {
                Path.GetFullPath(str);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
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
                else if (string.IsNullOrEmpty(extension))
                    ExportDefault(path);
                else return;
            }
            else ExportDefault();
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