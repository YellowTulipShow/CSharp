using System;
using CSharp.LibrayFunction;

namespace Test.ConsoleProgram
{
    public class CaseModel : AbsBasicsDataModel
    {
        public CaseModel() { }
        public string NameSign = string.Empty;
        public delegate void EventMethod();
        public EventMethod ExeEvent = null;
        public CaseModel[] SonCases = new CaseModel[] { };
    }
}
