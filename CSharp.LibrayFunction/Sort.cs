using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp.LibrayFunction
{
    /// <summary>
    /// 排序方法
    /// </summary>
    public class Sort
    {
        public Sort() { }

        /// <summary>
        /// 插入排序
        /// </summary>
        /// <param name="intlist"></param>
        /// <returns></returns>
        public int[] InsertionSort(int[] intlist)
        {
            int[] returnList = { };

            for (int j=2; j<intlist.Length; j++)
            {
                int key = intlist[j];

                int i = j-1;
                while (i>0 && intlist[i]>key)
                {
                    intlist[i+1] = intlist[i];
                    i = i-1;
                }
                intlist[i+1] = key;
            }

            return intlist;
        }

        private void TestMethodBody(Output outputObj)
        {
            Sort SortObj = new Sort();

            outputObj.ShowString("原始数据列表:");
            int[] testList = { 2, 2, 12, 54, 232, 52, 33, 53, 9 };
            outputObj.ShowIntList(testList);

            outputObj.ShowString("=========================================");

            outputObj.ShowString("结果数据列表");
            int[] returnList = SortObj.InsertionSort(testList);
            outputObj.ShowIntList(returnList);
        }
    }
}
