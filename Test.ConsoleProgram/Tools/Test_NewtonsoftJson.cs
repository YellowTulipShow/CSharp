using System;
using CSharp.LibrayFunction;

namespace Test.ConsoleProgram.Case.SonTests
{
    public class Test_NewtonsoftJson : CaseModel
    {
        public Test_NewtonsoftJson() {
            base.NameSign = @"测试 执行 Newtonsoft.Json 序列化和反序列化";
            base.ExeEvent = Method;
        }

        public void Method() {
            Model f = new Model();
            f.Id = 84668;
            f.MeiJu = new MSSFTCC[] { MSSFTCC.Char, MSSFTCC.NVarChar };
            Print.WriteLine(f);
            //string str = "{\"Id\":0,\"MeiJu\":1}";
            //Model m = JsonHelper.DeserializeToObject<Model>(str);
            //Print.WriteLine(m.MeiJu.ToString());
        }

        private enum MSSFTCC
        {
            Char,
            NChar,
            VarChar,
            NVarChar,
        }

        private class Model : AbsBasicDataModel
        {
            public int Id { get { return _id; } set { _id = value; } }
            private int _id = 0;

            public MSSFTCC[] MeiJu { get { return _meiju; } set { _meiju = value; } }
            private MSSFTCC[] _meiju = new MSSFTCC[] { };
        }
    }
}
