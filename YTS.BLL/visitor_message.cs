using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTS.DBUtility;
using YTS.Common;

namespace YTS.BLL
{
    /// <summary>
    /// BLL: 访客留言
    /// </summary>
    public class visitor_message : BasicsBLL<Model.visitor_message>
    {
        public readonly static DAL.visitor_message defDAL = new DAL.visitor_message();
        public visitor_message() : base(defDAL) { }
        public readonly string ColName_name = defDAL.ColName_name;
        public readonly string ColName_ipaddress = defDAL.ColName_ipaddress;
        public readonly string ColName_TimeAdd = defDAL.ColName_TimeAdd;

        /// <summary>
        /// 获得IP重复数量
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="time_where"></param>
        /// <param name="minute_interval"></param>
        /// <returns></returns>
        public int GetIPRepeatCount(string ip, DateTime time_where, int minute_interval = 15) {
            if (CheckData.IsStringNull(ip)) {
                return 0;
            }
            List<string> wheres = new List<string>();
            wheres.Add(CreateSQL.WhereEqual(ColName_ipaddress, ip));
            wheres.Add(string.Format("DATEADD(MINUTE, -{0}, GETDATE()) <= {1}", minute_interval, ColName_TimeAdd));
            wheres.Add(string.Format("{0} <= GETDATE()", ColName_TimeAdd));
            string sqlwhere = ConvertTool.IListToString(wheres, CreateSQL.WHERE_AND);
            return base.GetCount(sqlwhere);
        }
    }
}
