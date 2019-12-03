using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test.ConsoleProgram.Module
{
    public class Login : CaseModel
    {
        public Login() {
            this.NameSign = @"尝试着开始写测试案例模块";
            this.ExeEvent = Method;
        }
        public void Method() {
        }
    }
}
