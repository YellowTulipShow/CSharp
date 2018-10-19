using System;
using System.Collections.Generic;
using CSharp.LibrayDataBase;
using CSharp.LibrayFunction;

namespace Test.ConsoleProgram.Case.SonTests
{
    public class Test_addRecord: CaseModel
    {
        public Test_addRecord() {
            this.NameSign = @"添加数据";
            this.ExeEvent = TestMethod;
        }

        public  void TestMethod() {
            DateTime min_time = new DateTime(2018, 3, 1, 0, 0, 0);
            DateTime max_time = new DateTime(2018, 3, 31, 23, 59, 59);

            int[] telsigns = new int[] { 58, 68, 8, 49, 66, 51, 55, 56, 57, 6, 60, 19, 63, 52, 14, 53, 40, 21, 64, 22, 67, 59, 16, 45, 61, 62, 35, 38, 54, 44, 15, 41, 36, 39, 17, 65, 26, 10, 33, 27, 50, 1, 25, 11, 13, 3, 28, 2, 29, 34, 30, 7, 5, 12, 4, 23, 32, 18, 47, 24, 42, 43, 46, 31, 20, 37, 69, 70, 73, 72, 71, 77, 79, 80, 78, 85, 86, 87, 88, 81, 82, 83, 84, 89, 90, 91, 92, 74, 75, 76, 48, 0 };

            int[] managerIDs = new int[] { 1, 10, 15, 16, 17, 18, 20, 22, 26, 75, 76, 77, 78, 79, 80, 81, 82, 83, 84, 85, 86, 87, 88, 89, 90, 91, 92, 93, 94, 95, 96, 97, 98, 99 };

            char[] chars = Get_wechatno_chararray();

            BLLspread_wxUserInfo BLLwxuser = new BLLspread_wxUserInfo();
            for (int i = 0; i < 10000; i++) {
                string user_wechatno = RandomData.GetString(chars, RandomData.R.Next(1, 21));

                bool isSuccess = BLLwxuser.Insert(new Modelspread_wxUserInfo() {
                    ManagerID = RandomData.Item(managerIDs),
                    Remark = string.Empty,
                    TelSign = RandomData.Item(telsigns),
                    TimeAdd = RandomData.GetDateTime(min_time, max_time),
                    WeChatNo = user_wechatno,
                    VoucherPictures = user_wechatno,
                });
                Console.WriteLine("forNo:{0}  recordIDno: {1}", i, isSuccess);
            }
        }
        private char[] Get_wechatno_chararray() {
            List<char> chars = new List<char>() { '_', '-' };
            chars.AddRange(CommonData.ASCII_Number());
            chars.AddRange(CommonData.ASCII_UpperEnglish());
            chars.AddRange(CommonData.ASCII_LowerEnglish());
            return chars.ToArray();
        }

        [TableAttribute]
        public class Modelspread_wxUserInfo : AbsModel_ID
        {
            public override string GetTableName() {
                return @"dt_spread_wxUserInfo";
            }

            #region Model

            /// <summary>
            /// 管理员ID
            /// </summary>
            [Column(MSQLServerDTParser.DTEnum.Int)]
            public int ManagerID { set { _managerID = value; } get { return _managerID; } }
            private int _managerID = 0;

            /// <summary>
            /// 手机编号
            /// </summary>
            [Column(MSQLServerDTParser.DTEnum.Int)]
            public int TelSign { set { _telsign = value; } get { return _telsign; } }
            private int _telsign = 0;

            /// <summary>
            /// 微信号
            /// </summary>
            [Column(MSQLServerDTParser.DTEnum.NVarChar)]
            public string WeChatNo { get { return _weChatNo; } set { _weChatNo = value; } }
            private string _weChatNo = string.Empty;

            /// <summary>
            /// 凭证图片
            /// </summary>
            [Column(MSQLServerDTParser.DTEnum.NVarChar)]
            public string VoucherPictures { get { return _voucherPictures; } set { _voucherPictures = value; } }
            private string _voucherPictures = string.Empty;

            #endregion
        }

        public class BLLspread_wxUserInfo : BLLSQLServer<DALSQLServer<Modelspread_wxUserInfo>, Modelspread_wxUserInfo>
        {
            public BLLspread_wxUserInfo() : base(new DALSQLServer<Modelspread_wxUserInfo>()) { }
        }
    }
}
