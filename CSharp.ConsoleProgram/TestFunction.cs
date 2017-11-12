using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.IO;
using CSharp.LibrayDataBase;
using CSharp.LibrayFunction;
using CSharp.SystemService;
using CSharp.Model;

namespace CSharp.ConsoleProgram
{
    public class TestFunction
    {
        public TestFunction() { }

        public void Init() {
            TestJsonHelperJson();
        }

        #region Test SystemConfig
        private void TestSystemConfig() {
        }
        #endregion

        #region Test JsonHelper Json
        private void TestJsonHelperJson() {
            CSharp.Model.Table.Articles artBasic = new CSharp.Model.Table.Articles();
            artBasic.id = 12;
            artBasic.Remark = "sdfj";
            artBasic.TimeAdd = DateTime.Now;
            string str = JsonHelper.SerializeObject(artBasic);
            Print.WriteLine(str);
            artBasic.Remark = artBasic.TimeAdd.ToString(LFKeys.TABLE_DATETIME_FORMAT_SECOND);
            str = JsonHelper.SerializeObject(artBasic);
            Print.WriteLine(str);

            //string desStr = "{\"id\":12,\"add_time\":\"2017-10-06T16:24:42.9742994+08:00\",\"remark\":\"2017-10-06 16:24:42\"}";
            //CSharp.Model.Table.Articles deserArt = JsonHelper.DeserializeToObject<CSharp.Model.Table.Articles>(desStr);
            //Print.WriteLine(deserArt.Remark);

            ////匿名对象解析
            //var tempEntity = new { ID = 0, Name = string.Empty };
            //string json5 = JsonHelper.SerializeObject(tempEntity);
            ////json5 : {"ID":0,"Name":""}
            //Print.WriteLine(json5);
            //tempEntity = JsonHelper.DeserializeAnonymousType("{\"ID\":\"112\",\"Name\":\"石子儿\"}", tempEntity);
            //Print.WriteLine(tempEntity.ID + ":" + tempEntity.Name);
        }
        #endregion

        #region Test BasicsDataModel
        private void TestBasicsDataModel() {
            CSharp.Model.Table.Articles artBasic = new CSharp.Model.Table.Articles();
            artBasic.id = 12;
            artBasic.Remark = "sdfj";
            artBasic.TimeAdd = DateTime.Now;
            Print.WriteLine(JsonHelper.SerializeObject(artBasic));

            CSharp.Model.Table.Articles clart_1 = (CSharp.Model.Table.Articles)artBasic.CloneModelData();
            clart_1.id = 58;
            clart_1.Remark = "clone poryjisdf";
            Print.WriteLine(JsonHelper.SerializeObject(artBasic));
            Print.WriteLine(JsonHelper.SerializeObject(clart_1));

        }
        #endregion

        #region Test SystemLog
        private void SystemLogTest() {
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();
            string filepath = String.Empty;

            SystemLog.LogModel log = new SystemLog.LogModel();
            log.Type = SystemLog.LogTypeEnum.Daily;
            log.Position = "CSharp.LibrayFLog.LogModel";
            log.Message = "第一条日志记录圣诞节覅数据量发简历圣诞节覅俩就是第六房间阿里斯顿减肥路上!";
            filepath = SystemLog.Write(log);

            log.Type = SystemLog.LogTypeEnum.Error;
            log.Position = "CSharp.ConsoleProgram.TestFunction.Init()";
            log.Message = "测试SystemLog类错误日志是否正常!";
            filepath = SystemLog.Write(log);

            stopwatch.Stop();
            Print.WriteLine(String.Format("运行时间:{0}   写入日志路径:{1}", stopwatch.Elapsed.TotalSeconds, filepath));
        }
        #endregion
    }
}
