using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Reflection;
using Google.Protobuf;

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

        public enum ExportFormat
        {
            Csv,
            Proto
        }

        private static ExportFormat _exportFormat;
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

            if ((_arguments & Arguments.Headers) == Arguments.Headers ||
                (_arguments & Arguments.Debug) == Arguments.Debug)
            {
                if ((_arguments & Arguments.HeadersPlus) == Arguments.HeadersPlus ||
                    (_arguments & Arguments.Debug) == Arguments.Debug)
                    exportFile.WriteLine("2C,30,,,");
                exportFile.WriteLine("IdMin,IdMax,Index,TableOffset,Text");
            }

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
                        var str = ReadNullTerminatedWideString(binaryData, tableOffset, 2048);
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
            return File.ReadAllBytes(filename);
        }

        // protobuf removes '_' characters and uppercases the first character so we need to do that also.
        public static string FixStringProto(string fieldName)
        {
            return String.Concat(fieldName.Split('_').Select(str => str.First().ToString().ToUpper() + str.Substring(1)));
        }

        public static void TryExportParamProto<T>(string filename, byte[] binaryData) where T : struct
        {
            var fname = Path.GetFileNameWithoutExtension(filename);
            var fieldNames = StructHelper.GetFieldNames<T>();
            var exportFile = File.Create($"{fname}Out.dat");
            var stream = new CodedOutputStream(exportFile);

            var protoClass = Type.GetType($"Ds3Ext.{typeof(T).Name}");
            var protoWriteTo = protoClass.GetMethod("WriteTo");
            var protoId = protoClass.GetProperty("Id");
            var protoProperties = new List<PropertyInfo>(fieldNames.Count);
            protoProperties.AddRange(fieldNames.Select(fieldName => protoClass.GetProperty(FixStringProto(fieldName))));

            var paramData = TryExportParam<T>(binaryData);
            foreach (var param in paramData)
            {
                var protoInstance = Activator.CreateInstance(protoClass);
                var fieldValues = StructHelper.GetFieldValues(param.Structure);
                protoId.SetValue(protoInstance, param.Id);
                for (var i = 0; i < protoProperties.Count; i++)
                    protoProperties[i].SetValue(protoInstance, fieldValues[i]);

                protoWriteTo.Invoke(protoInstance, new object[] { stream });
            }

            exportFile.Close();
        }

        public static void TryExportParamCsv<T>(string filename, byte[] binaryData) where T : struct
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
            if (_exportFormat == ExportFormat.Csv)
                TryExportParamCsv<T>(filename, binaryData);
            else if (_exportFormat == ExportFormat.Proto)
                TryExportParamProto<T>(filename, binaryData);
        }

        public static void TryExportParam(string filename)
        {
            byte[] binaryData;
            try
            {
                binaryData = GetBinaryDataFromFile(filename);
            }
            catch (ArgumentException)
            {
                return;
            }
            Console.WriteLine($"Exporting {filename}...");
            string structName = GetStructName(binaryData);
            switch (structName)
            {
                case "ATK_PARAM_ST":
                    TryExportParam<AtkParam>(filename, binaryData);
                    break;
                case "ATTACK_ELEMENT_CORRECT_PARAM_ST":
                    TryExportParam<AttackElementCorrectParam>(filename, binaryData);
                    break;
                case "BEHAVIOR_PARAM_ST":
                    TryExportParam<BehaviorParam>(filename, binaryData);
                    break;
                case "CACL_CORRECT_GRAPH_ST":
                    TryExportParam<CalcCorrectGraph>(filename, binaryData);
                    break;
                case "EQUIP_PARAM_ACCESSORY_ST":
                    TryExportParam<EquipParamAccessory>(filename, binaryData);
                    break;
                case "EQUIP_PARAM_PROTECTOR_ST":
                    TryExportParam<EquipParamProtector>(filename, binaryData);
                    break;
                case "EQUIP_PARAM_WEAPON_ST":
                    TryExportParam<EquipParamWeapon>(filename, binaryData);
                    break;
                case "MAGIC_PARAM_ST":
                    TryExportParam<Magic>(filename, binaryData);
                    break;
                case "NPC_PARAM_ST":
                    TryExportParam<NpcParam>(filename, binaryData);
                    break;
                case "REINFORCE_PARAM_PROTECTOR_ST":
                    TryExportParam<ReinforceParamProtector>(filename, binaryData);
                    break;
                case "REINFORCE_PARAM_WEAPON_ST":
                    TryExportParam<ReinforceParamWeapon>(filename, binaryData);
                    break;
                case "SP_EFFECT_PARAM_ST":
                    TryExportParam<SpEffectParam>(filename, binaryData);
                    break;
                case "ACTIONBUTTON_PARAM_ST":
                case "AI_SOUND_PARAM_ST":
                case "EQUIP_PARAM_GOODS_ST":
                case "BONFIRE_WARP_PARAM_ST":
                case "BUDGET_PARAM_ST":
                case "BULLET_CREATE_LIMIT_PARAM_ST":
                case "BULLET_PARAM_ST":
                case "CEREMONY_PARAM_ST":
                case "CHARACTER_INIT_PARAM":
                case "CHARMAKEMENUTOP_PARAM_ST":
                case "CHARMAKEMENU_LISTITEM_PARAM_ST":
                case "CLEAR_COUNT_CORRECT_PARAM_ST":
                case "COOL_TIME_PARAM_ST":
                case "CULT_SETTING_PARAM_ST":
                case "DECAL_PARAM_ST":
                case "DIRECTION_CAMERA_PARAM_ST":
                case "EQUIP_MTRL_SET_PARAM_ST":
                case "ESTUS_FLASK_RECOVERY_PARAM_ST":
                case "FACE_GEN_PARAM_ST":
                case "FACE_PARAM_ST":
                case "FACE_RANGE_PARAM_ST":
                case "FOOT_SFX_PARAM_ST":
                case "GAME_AREA_PARAM_ST":
                case "GAME_PROGRESS_PARAM_ST":
                case "GEMEFFECT_PARAM_ST":
                case "GEM_CATEGORY_PARAM_ST":
                case "GEM_DROP_DOPING_PARAM_ST":
                case "GEM_DROP_MODIFY_PARAM_ST":
                case "GEM_GEN_PARAM_ST":
                case "HIT_EFFECT_SE_PARAM_ST":
                case "HIT_EFFECT_SFX_CONCEPT_PARAM_ST":
                case "HIT_EFFECT_SFX_PARAM_ST":
                case "HIT_MTRL_PARAM_ST":
                case "ITEMLOT_PARAM_ST":
                case "KNOCKBACK_PARAM_ST":
                case "KNOWLEDGE_LOADSCREEN_ITEM_PARAM_ST":
                case "LOAD_BALANCER_DRAW_DIST_SCALE_PARAM_ST":
                case "LOAD_BALANCER_PARAM_ST":
                case "LOCK_CAM_PARAM_ST":
                case "LOD_BANK":
                case "MAP_MIMICRY_ESTABLISHMENT_PARAM_ST":
                case "MENUPROPERTY_LAYOUT":
                case "MENUPROPERTY_SPEC":
                case "MENU_OFFSCR_REND_PARAM_ST":
                case "MENU_PARAM_COLOR_TABLE_ST":
                case "MENU_VALUE_TABLE_SPEC":
                case "MODEL_SFX_PARAM_ST":
                case "MOVE_PARAM_ST":
                case "MULTI_ESTUS_FLASK_BONUS_PARAM_ST":
                case "MULTI_PLAY_CORRECTION_PARAM_ST":
                case "MULTI_SOUL_BONUS_RATE_PARAM_ST":
                case "NETWORK_AREA_PARAM_ST":
                case "NETWORK_MSG_PARAM_ST":
                case "NETWORK_PARAM_ST":
                case "NPC_AI_ACTION_PARAM_ST":
                case "NPC_THINK_PARAM_ST":
                case "OBJECT_MATERIAL_SFX_PARAM_ST":
                case "OBJECT_PARAM_ST":
                case "OBJ_ACT_PARAM_ST":
                case "PHANTOM_PARAM_ST":
                case "PLAY_REGION_PARAM_ST":
                case "PROTECTOR_GEN_PARAM_ST":
                case "RAGDOLL_PARAM_ST":
                case "ROLE_PARAM_ST":
                case "SE_MATERIAL_CONVERT_PARAM_ST":
                case "SHOP_LINEUP_PARAM":
                case "SKELETON_PARAM_ST":
                case "SP_EFFECT_VFX_PARAM_ST":
                case "SWORD_ARTS_PARAM_ST":
                case "TALK_PARAM_ST":
                case "THROW_DIRECTION_SFX_PARAM_ST":
                case "THROW_INFO_BANK":
                case "TOUGHNESS_PARAM_ST":
                case "UPPER_ARM_PARAM_ST":
                case "WEAPON_GEN_PARAM_ST":
                case "WEP_ABSORP_POS_PARAM_ST":
                case "WET_ASPECT_PARAM_ST":
                case "WIND_PARAM_ST":
                default:
                    Console.WriteLine("This .param file is not yet supported.");
                    break;
            }
        }
        
        private static string GetStructName(byte[] binaryData)
        {
            StringBuilder structName = new StringBuilder();
            int structNameOffset = BitConverter.ToInt32(binaryData, 0);
            byte nextChar;
            while (structNameOffset < binaryData.Length && (nextChar = binaryData[structNameOffset]) != 0x00)
            {
                structName.Append((char)nextChar);
                structNameOffset++;
            }

            return structName.ToString();
        }

        private static void OutputHelpAndExit()
        {
            Console.WriteLine($"Usage: {Path.GetFileNameWithoutExtension(AppDomain.CurrentDomain.FriendlyName)} [<argument>] [<path>]");
            Console.WriteLine("Arguments:");
            Console.WriteLine(" -h\tHelp.");
            Console.WriteLine(" -H\tOutput column headers.");
            Console.WriteLine(" -H+\tOutput structure offsets and headers.");
            Console.WriteLine(" -d\tDebug Mode, outputs hidden columns.");
            Console.WriteLine(" -g\tGenerate .proto file.");
            Console.WriteLine(" -p\tOutput in protobuf format.");
            Environment.Exit(0);
        }

        private static void GenerateProto()
        {
            using (var t = new ProtoBufWriter("ds3ext.proto", "ds3ext"))
            {
                t.WriteMessage<EquipParamWeapon>("EquipParamWeapon");
                t.WriteMessage<EquipParamAccessory>("EquipParamAccessory");
                t.WriteMessage<EquipParamProtector>("EquipParamProtector");
                t.WriteMessage<ReinforceParamWeapon>("ReinforceParamWeapon");
                t.WriteMessage<AttackElementCorrectParam>("AttackElementCorrectParam");
                t.WriteMessage<CalcCorrectGraph>("CalcCorrectGraph");
                t.WriteMessage<Magic>("Magic");
                t.WriteMessage<SpEffectParam>("SpEffectParam");
                t.WriteMessage<NpcParam>("NpcParam");
                t.WriteMessage<BehaviorParam>("BehaviorParam");
                t.WriteMessage<AtkParam>("AtkParam");
            }
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

        private static string ParseArgs(IEnumerable<string> args)
        {
            var enumerable = args as string[] ?? args.ToArray();
            if (!enumerable.Any())
                return null;

            string path = null;
            switch (enumerable[0])
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
                    StructHelper.TypesToSkip = new List<Type>();
                    break;
                case "-g":
                    GenerateProto();
                    Environment.Exit(0);
                    break;
                case "-p":
                    _exportFormat = ExportFormat.Proto;
                    break;
                default:
                    if (IsFilePath(enumerable[0]))
                        path = enumerable[0];
                    else OutputHelpAndExit();
                    break;
            }

            if (enumerable.Length > 1)
            {
                if (IsFilePath(enumerable[1]))
                    path = enumerable[1];
                else OutputHelpAndExit();
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
            TryExportParam("EquipParamWeapon.param");
            TryExportParam("EquipParamAccessory.param");
            TryExportParam("EquipParamProtector.param");
            TryExportParam("ReinforceParamWeapon.param");
            TryExportParam("AttackElementCorrectParam.param");
            TryExportParam("CalcCorrectGraph.param");
            TryExportParam("Magic.param");
            TryExportParam("SpEffectParam.param");
            TryExportParam("NpcParam.param");
            TryExportParam("BehaviorParam.param");
            TryExportParam("BehaviorParam_PC.param");
            TryExportParam("AtkParam_Pc.param");
            TryExportParam("AtkParam_Npc.param");
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
