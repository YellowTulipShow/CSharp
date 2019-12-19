using System;

namespace YTS.Data.Models.Spread
{
    public class Spread_Unit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Type { get; set; }
        public string Remark { get; set; }
        public int? AddUserID { get; set; }
        public DateTime? AddTime { get; set; }
    }
}