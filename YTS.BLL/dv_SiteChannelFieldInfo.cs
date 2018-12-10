using System;
using System.Collections.Generic;
using YTS.DBUtility;
using YTS.Common;
using System.Data;

namespace YTS.BLL
{
    public class dv_SiteChannelFieldInfo : BasicsBLL<Model.dv_SiteChannelFieldInfo>
    {
        public readonly static DAL.dv_SiteChannelFieldInfo defDAL = new DAL.dv_SiteChannelFieldInfo();
        public dv_SiteChannelFieldInfo() : base(defDAL) { }

        public readonly string ColName_ID_ChannelInfo = defDAL.ColName_ID_ChannelInfo; /* 频道信息-ID */
        public readonly string ColName_Channel_SiteID = defDAL.ColName_Channel_SiteID; /* 频道所属站点ID */
        public readonly string ColName_Channel_Name = defDAL.ColName_Channel_Name; /* 频道名称标识 */
        public readonly string ColName_Channel_Title = defDAL.ColName_Channel_Title; /* 频道中文标题 */
        public readonly string ColName_ID_FieldInfo = defDAL.ColName_ID_FieldInfo; /* 字段信息-ID */
        public readonly string ColName_Field_Name = defDAL.ColName_Field_Name; /* 字段名称标识 */
        public readonly string ColName_Field_Title = defDAL.ColName_Field_Title; /* 字段中文标题 */

        /// <summary>
        /// 获得字段
        /// </summary>
        /// <param name="field_name"></param>
        /// <param name="site_id"></param>
        /// <param name="channel_name"></param>
        /// <param name="channel_id"></param>
        /// <returns></returns>
        public Model.dv_SiteChannelFieldInfo[] GetFields(string field_name, int site_id = 0, string channel_name = "", int channel_id = 0) {
            List<string> wheres = new List<string>();
            wheres.Add(CreateSQL.WhereEqual(ColName_Field_Name, field_name));
            if (site_id > 0) {
                wheres.Add(CreateSQL.WhereEqual(ColName_Channel_SiteID, site_id.ToString()));
            }
            if (!CheckData.IsStringNull(channel_name)) {
                wheres.Add(CreateSQL.WhereEqual(ColName_Channel_Name, channel_name.ToString()));
            }
            if (channel_id > 0) {
                wheres.Add(CreateSQL.WhereEqual(ColName_ID_ChannelInfo, channel_id.ToString()));
            }

            string sqlwhere = ConvertTool.IListToString(wheres, CreateSQL.WHERE_AND);
            DataTable dt = base.GetList(0, sqlwhere, string.Empty).Tables[0];
            if (CheckData.IsSizeEmpty(dt)) {
                return new Model.dv_SiteChannelFieldInfo[] { };
            }
            return base.GetModelList(dt).ToArray();
        }
    }
}
