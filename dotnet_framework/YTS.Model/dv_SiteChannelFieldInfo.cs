using System;

namespace YTS.Model
{
    public class dv_SiteChannelFieldInfo : AbsTableModel
    {
        public override string GetTableName() {
            return @"dv_SiteChannelFieldInfo";
        }

        #region Model
        /// <summary>
        /// 频道信息-ID
        /// </summary>
        public int ID_ChannelInfo { get { return _ID_ChannelInfo; } set { _ID_ChannelInfo = value; } }
        private int _ID_ChannelInfo = 0;


        /// <summary>
        /// 频道所属站点ID
        /// </summary>
        public int Channel_SiteID { get { return _Channel_SiteID; } set { _Channel_SiteID = value; } }
        private int _Channel_SiteID = 0;


        /// <summary>
        /// 频道名称标识
        /// </summary>
        public string Channel_Name { get { return _Channel_Name; } set { _Channel_Name = value; } }
        private string _Channel_Name = string.Empty;


        /// <summary>
        /// 频道中文标题
        /// </summary>
        public string Channel_Title { get { return _Channel_Title; } set { _Channel_Title = value; } }
        private string _Channel_Title = string.Empty;


        /// <summary>
        /// 字段信息-ID
        /// </summary>
        public int ID_FieldInfo { get { return _ID_FieldInfo; } set { _ID_FieldInfo = value; } }
        private int _ID_FieldInfo = 0;


        /// <summary>
        /// 字段名称标识
        /// </summary>
        public string Field_Name { get { return _Field_Name; } set { _Field_Name = value; } }
        private string _Field_Name = string.Empty;


        /// <summary>
        /// 字段中文标题
        /// </summary>
        public string Field_Title { get { return _Field_Title; } set { _Field_Title = value; } }
        private string _Field_Title = string.Empty;
        #endregion
    }
}
