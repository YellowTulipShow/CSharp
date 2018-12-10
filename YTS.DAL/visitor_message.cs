using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTS.Tools;

namespace YTS.DAL
{
    /// <summary>
    /// DAL: 访客留言
    /// </summary>
    public class visitor_message : BasicsDAL<Model.visitor_message>
    {
        public readonly static Model.visitor_message defmodel = new Model.visitor_message();
        public visitor_message() : base(defmodel) { }

        //public readonly string ColName_id = ReflexHelp.Name(() => defmodel.id);
        public readonly string ColName_name = ReflexHelp.Name(() => defmodel.name);
        //public readonly string ColName_tel = ReflexHelp.Name(() => defmodel.tel);
        //public readonly string ColName_msg = ReflexHelp.Name(() => defmodel.msg);
        public readonly string ColName_ipaddress = ReflexHelp.Name(() => defmodel.ipaddress);
        public readonly string ColName_TimeAdd = ReflexHelp.Name(() => defmodel.TimeAdd);
        //public readonly string ColName_Remark = ReflexHelp.Name(() => defmodel.Remark);
    }
}
