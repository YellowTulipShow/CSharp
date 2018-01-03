using System;

using CSharp.LibrayFunction;
using Test.ConsoleProgram.ITestCaseSonClass;

namespace Test.ConsoleProgram
{
    public class TestCaseLibray
    {
        public TestCaseLibray() { }

        public ITestCase[] GetTestCase() {
            return new ITestCase[] {
                new AbsTableDAL_AnalysisPropertyColumns(),
                new AbsTableDAL_DefaultModel(),
                new TestAttribute()
            };
        }
    }
}
