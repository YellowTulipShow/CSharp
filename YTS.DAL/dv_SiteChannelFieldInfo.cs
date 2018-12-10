using System;
using YTS.Tools;

namespace YTS.DAL
{
    /// <summary>
    /// 
    /// </summary>
    public class dv_SiteChannelFieldInfo : BasicsDALViewOnlySelect<Model.dv_SiteChannelFieldInfo>
    {
        public readonly static Model.dv_SiteChannelFieldInfo defmodel = new Model.dv_SiteChannelFieldInfo();
        public dv_SiteChannelFieldInfo() : base(defmodel) { }

        public readonly string ColName_ID_ChannelInfo = ReflexHelp.Name(() => defmodel.ID_ChannelInfo); /* 频道信息-ID */
        public readonly string ColName_Channel_SiteID = ReflexHelp.Name(() => defmodel.Channel_SiteID); /* 频道所属站点ID */
        public readonly string ColName_Channel_Name = ReflexHelp.Name(() => defmodel.Channel_Name); /* 频道名称标识 */
        public readonly string ColName_Channel_Title = ReflexHelp.Name(() => defmodel.Channel_Title); /* 频道中文标题 */
        public readonly string ColName_ID_FieldInfo = ReflexHelp.Name(() => defmodel.ID_FieldInfo); /* 字段信息-ID */
        public readonly string ColName_Field_Name = ReflexHelp.Name(() => defmodel.Field_Name); /* 字段名称标识 */
        public readonly string ColName_Field_Title = ReflexHelp.Name(() => defmodel.Field_Title); /* 字段中文标题 */
    }
}
