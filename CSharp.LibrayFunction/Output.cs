using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp.LibrayFunction
{
    /// <summary>
    /// 输出类
    /// </summary>
    public class Output
    {
        private bool isNull(object obj)
        {
            if (obj == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #region IntList 委托显示的方法
        public delegate void IntList(int[] list);
        public event IntList IntListEventHandler;
        public void ShowIntList(int[] intlist)
        {
            if (!isNull(IntListEventHandler))
            {
                IntListEventHandler(intlist);
            }
        }
        #endregion

        #region String 委托显示的方法
        public delegate void String(string str);
        public event String StringEventHandler;
        public void ShowString(string str)
        {
            if (!isNull(StringEventHandler))
            {
                StringEventHandler(str);
            }
        }
        #endregion
    }
}
