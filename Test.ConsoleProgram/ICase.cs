using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test.ConsoleProgram
{
    /// <summary>
    /// 接口-测试-实例
    /// </summary>
    public interface ICase
    {
        string NameSign();
        void Method();
    }

    /// <summary>
    /// 抽象类-测试-实例-可定制子属性类-称为 "大测试类"
    /// </summary>
    public abstract class AbsCase : ICase
    {
        public abstract string NameSign();
        public abstract void Method();
        public virtual ICase[] SonCaseArray() {
            return new ICase[] { };
        }
    }
}
