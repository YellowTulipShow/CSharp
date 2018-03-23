using System;
using System.Text;
using System.Collections.Generic;
using CSharp.LibrayFunction;

namespace Test.ConsoleProgram
{
    public class CaseModel : AbsBasicDataModel
    {
        public CaseModel() { }
        public string NameSign = string.Empty;
        public delegate void EventMethod();
        public EventMethod ExeEvent = null;
        public CaseModel[] SonCases = new CaseModel[] { };

        #region ====== Tool Region: ======
        #endregion
    }
}
