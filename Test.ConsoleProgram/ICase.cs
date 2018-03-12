using System;
using CSharp.LibrayFunction;

namespace Test.ConsoleProgram
{
    ///// <summary>
    ///// 接口-测试-实例
    ///// </summary>
    //public interface ICase
    //{
    //    string NameSign();
    //    void Method();
    //}

    ///// <summary>
    ///// 抽象类-测试-实例-可定制子属性类-称为 "大测试类"
    ///// </summary>
    //public abstract class AbsCase
    //{
    //    public abstract string NameSign();
    //    public abstract void Method();
    //    public virtual AbsCase[] SonCaseArray() {
    //        return new AbsCase[] { };
    //    }
    //}

    public class CaseModel : AbsBasicsDataModel
    {
        public CaseModel() { }
        public string NameSign = string.Empty;
        public delegate void Method();
        public Method ExeEvent = null;
        public CaseModel[] SonCases = new CaseModel[] { };
    }
}
