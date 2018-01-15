using System;

namespace Test.ConsoleProgram
{
    public class TestCaseLibray
    {
        public TestCaseLibray() { }

        public ITestCase[] GetTestCase() {
            return new ITestCase[] {
                //new ITestCaseSonClass.AbsTableDAL_ICreateSQL(),
                //new ITestCaseSonClass.DALSQLServer_IAutoTable(),
                //new ITestCaseSonClass.AbsTableDAL_ITableBasicFunction(),
                //new ITestCaseSonClass.TestObject(),
                //new ITestCaseSonClass.TestDateTime(),
                new ITestCaseSonClass.TestReflex(),
                new ITestCaseSonClass.TestLambda(),
            };
        }
    }
}
