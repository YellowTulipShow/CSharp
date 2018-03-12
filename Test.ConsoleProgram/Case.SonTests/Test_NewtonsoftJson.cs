using System;
using CSharp.LibrayFunction;

namespace Test.ConsoleProgram.Case.SonTests
{
    public class Test_NewtonsoftJson : AbsCase
    {
        public override string NameSign() {
            return @"测试 执行 Newtonsoft.Json 序列化和反序列化";
        }

        public override void Method() {
            Model f = new Model();
            f.Id = 84668;
            f.MeiJu = MSSFieldTypeCharCount.VarChar;
            Print.WriteLine(f);
            string str = "{\"Id\":0,\"MeiJu\":1}";
            Model m = JsonHelper.DeserializeToObject<Model>(str);
            Print.WriteLine(m.MeiJu.ToString());
        }

        private enum MSSFieldTypeCharCount
        {
            //Char = 5,
            //NChar = 6,
            //VarChar = 7,
            //NVarChar = 0,
            Char,
            NChar,
            VarChar,
            NVarChar,
        }

        private class Model : AbsBasicsDataModel
        {
            public int Id { get { return _id; } set { _id = value; } }
            private int _id = 0;

            public MSSFieldTypeCharCount MeiJu { get { return _meiju; } set { _meiju = value; } }
            private MSSFieldTypeCharCount _meiju = MSSFieldTypeCharCount.NChar;
        }
    }
}
