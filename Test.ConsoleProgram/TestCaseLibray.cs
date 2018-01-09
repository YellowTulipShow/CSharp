using System;

namespace Test.ConsoleProgram
{
    public class TestCaseLibray
    {
        public TestCaseLibray() { }

        public ITestCase[] GetTestCase() {
            return new ITestCase[] {
                //new ITestCaseSonClass.TestAbsBasicsDataModel(),
                //new ITestCaseSonClass.AbsTableDAL_AnalysisPropertyColumns(),
                //new ITestCaseSonClass.AbsTableDAL_DefaultModel(),
                new ITestCaseSonClass.AbsTableDAL_ICreateSQL(),
                //new ITestCaseSonClass.DALSQLServer_IAutoTable(),
                new ITestCaseSonClass.AbsTableDAL_ITableBasicFunction(),
            };
        }
    }
}
