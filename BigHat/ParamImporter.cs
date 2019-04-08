using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using BigHat.ParamEntries;

namespace BigHat
{
    public enum ParamType
    {
        AtkParam,
        AttackElementCorrectParam,
        BehaviorParam,
        CalcCorrectGraph,
        EquipParamAccessory,
        EquipParamProtector,
        EquipParamWeapon,
        MagicParam,
        NpcParam,
        ReinforceParamProtector,
        ReinforceParamWeapon,
        SpEffectParam
    }

    public class ParamImporter : IImporter
    {
        private uint _baseOffset;
        private readonly byte[] _data;

        public ParamImporter(string path)
        {
            _data = Utils.ReadBinaryDataFromFile(path);
            ReadHeaderData();
        }

        public int Count { get; private set; }
        public ParamType Type { get; private set; }

        private static string GetTableName(byte[] binaryData)
        {
            var tableName = new StringBuilder();
            var tableNameOffset = BitConverter.ToInt32(binaryData, 0);
            byte nextChar;
            while (tableNameOffset < binaryData.Length && (nextChar = binaryData[tableNameOffset]) != 0x00)
            {
                tableName.Append((char) nextChar);
                tableNameOffset++;
            }
            return tableName.ToString();
        }

        public IList<TableEntry> Import()
        {
            return ImportParam().Cast<TableEntry>().ToList();
        }

        public IList<ParamEntry> ImportParam()
        {
            switch (Type)
            {
                case ParamType.AtkParam:
                    return ImportParamAs<AtkParam>().Cast<ParamEntry>().ToList();
                case ParamType.AttackElementCorrectParam:
                    return ImportParamAs<AttackElementCorrectParam>().Cast<ParamEntry>().ToList();
                case ParamType.BehaviorParam:
                    return ImportParamAs<BehaviorParam>().Cast<ParamEntry>().ToList();
                case ParamType.CalcCorrectGraph:
                    return ImportParamAs<CalcCorrectGraph>().Cast<ParamEntry>().ToList();
                case ParamType.EquipParamAccessory:
                    return ImportParamAs<EquipParamAccessory>().Cast<ParamEntry>().ToList();
                case ParamType.EquipParamProtector:
                    return ImportParamAs<EquipParamProtector>().Cast<ParamEntry>().ToList();
                case ParamType.EquipParamWeapon:
                    return ImportParamAs<EquipParamWeapon>().Cast<ParamEntry>().ToList();
                case ParamType.MagicParam:
                    return ImportParamAs<Magic>().Cast<ParamEntry>().ToList();
                case ParamType.NpcParam:
                    return ImportParamAs<NpcParam>().Cast<ParamEntry>().ToList();
                case ParamType.ReinforceParamProtector:
                    return ImportParamAs<ReinforceParamProtector>().Cast<ParamEntry>().ToList();
                case ParamType.ReinforceParamWeapon:
                    return ImportParamAs<ReinforceParamWeapon>().Cast<ParamEntry>().ToList();
                case ParamType.SpEffectParam:
                    return ImportParamAs<SpEffectParam>().Cast<ParamEntry>().ToList();
                default:
                    throw new Exception("This param type is not supported");
            }
        }

        public IList<T> ImportParamAs<T>() where T : ParamEntry, new()
        {
            var ret = new List<T>(Count);

            for (var i = 0; i < Count; i++)
            {
                var realOffset = (int) _baseOffset + ParamEntry.SizeOf<T>()*i;
                var id = BitConverter.ToUInt32(_data, 0x40 + 0x18*i);

                var structure = ParamEntry.FromBytes<T>(_data, realOffset);
                structure.Id = id;
                structure.Index = i;
                structure.Offset = realOffset;
                ret.Add(structure);
            }

            ret = ret.OrderBy(x => x.Id).ToList();
            return ret;
        }

        public void ReadHeaderData()
        {
            Type = GetParamType();
            Count = BitConverter.ToInt16(_data, 0xA);
            _baseOffset = BitConverter.ToUInt32(_data, 0x30);
        }

        [SuppressMessage("ReSharper", "StringLiteralTypo")]
        [SuppressMessage("ReSharper", "RedundantCaseLabel")]
        private ParamType GetParamType()
        {
            var structName = GetTableName(_data);
            switch (structName)
            {
                case "ATK_PARAM_ST":
                    return ParamType.AtkParam;
                case "ATTACK_ELEMENT_CORRECT_PARAM_ST":
                    return ParamType.AttackElementCorrectParam;
                case "BEHAVIOR_PARAM_ST":
                    return ParamType.BehaviorParam;
                case "CACL_CORRECT_GRAPH_ST":
                    return ParamType.CalcCorrectGraph;
                case "EQUIP_PARAM_ACCESSORY_ST":
                    return ParamType.EquipParamAccessory;
                case "EQUIP_PARAM_PROTECTOR_ST":
                    return ParamType.EquipParamProtector;
                case "EQUIP_PARAM_WEAPON_ST":
                    return ParamType.EquipParamWeapon;
                case "MAGIC_PARAM_ST":
                    return ParamType.MagicParam;
                case "NPC_PARAM_ST":
                    return ParamType.NpcParam;
                case "REINFORCE_PARAM_PROTECTOR_ST":
                    return ParamType.ReinforceParamProtector;
                case "REINFORCE_PARAM_WEAPON_ST":
                    return ParamType.ReinforceParamWeapon;
                case "SP_EFFECT_PARAM_ST":
                    return ParamType.SpEffectParam;
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
                    throw new Exception("This structure type is not supported");
            }
        }
    }
}