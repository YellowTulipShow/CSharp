using System;

namespace Test.ConsoleProgram
{
    public class TestCaseLibray
    {
        public TestCaseLibray() { }

        public ITestCase[] GetTestCase() {
            return new ITestCase[] {
                new ITestCaseSonClass.AbsTableDAL_AnalysisPropertyColumns(),
                new ITestCaseSonClass.AbsTableDAL_DefaultModel(),
                new ITestCaseSonClass.TestAttribute(),
                new ITestCaseSonClass.AbsTableDAL_ICreateSQL()
            };
        }
    }
}
