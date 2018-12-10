using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTS.DBUtility;
using YTS.Common;

namespace YTS.DAL
{
    /// <summary>
    /// DAL: 访客留言
    /// </summary>
    public class visitor_message : BasicsDAL<Model.visitor_message>
    {
        public readonly static Model.visitor_message defmodel = new Model.visitor_message();
        public visitor_message() : base(defmodel) { }

        //public readonly string ColName_id = ReflexHelper.Name(() => defmodel.id);
        public readonly string ColName_name = ReflexHelper.Name(() => defmodel.name);
        //public readonly string ColName_tel = ReflexHelper.Name(() => defmodel.tel);
        //public readonly string ColName_msg = ReflexHelper.Name(() => defmodel.msg);
        public readonly string ColName_ipaddress = ReflexHelper.Name(() => defmodel.ipaddress);
        public readonly string ColName_TimeAdd = ReflexHelper.Name(() => defmodel.TimeAdd);
        //public readonly string ColName_Remark = ReflexHelper.Name(() => defmodel.Remark);
    }
}
