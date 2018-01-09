using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp.LibrayDataBase
{
    /// <summary>
    /// 自动化
    /// </summary>
    interface IAutoTable
    {
        string SQLCreateTable();
        string SQLClearTable();
        string SQLKillTable();
    }
}
