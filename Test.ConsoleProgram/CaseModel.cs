using System;
using System.Text;
using System.Collections.Generic;
using YTS.Model;
using YTS.Tools;

namespace Test.ConsoleProgram
{
    /// <summary>
    /// 测试实例模型
    /// </summary>
    public class CaseModel : AbsBasicDataModel
    {
        public CaseModel() { }

        /// <summary>
        /// 名称标志
        /// </summary>
        public string NameSign = @"实例模型初始名称";

        /// <summary>
        /// 执行事件
        /// </summary>
        public Func<bool> ExeEvent = null;

        /// <summary>
        /// 子类实例
        /// </summary>
        public CaseModel[] SonCases = new CaseModel[] { };

        #region === tools method ===
        public delegate bool Func_IsEquals<TA, TS>(TA answer_item, TS source_item);
        public delegate void Func_LengthNotEquals(int answer_length, int source_length);
        public delegate void Func_NotFindPrint<TA>(TA answer_item);
        /// <summary>
        /// 是否枚举数列值都相等
        /// </summary>
        /// <typeparam name="TA">答案数据类型</typeparam>
        /// <typeparam name="TS">数据源数据类型</typeparam>
        /// <param name="answer">答案数据源</param>
        /// <param name="source">需要验证的数据源</param>
        /// <param name="func_isEquals">方法: 判断单个选项是否相等</param>
        /// <param name="func_lengthNotEquals">方法: 长度不相等执行</param>
        /// <param name="func_notFindPrint">方法: 答案没找到执行打印</param>
        /// <returns>个数, 值都相等, 符合答案</returns>
        public bool IsIEnumerableEqual<TA, TS>(IEnumerable<TA> answer, IEnumerable<TS> source,
            Func_IsEquals<TA, TS> func_isEquals = null,
            Func_LengthNotEquals func_lengthNotEquals = null,
            Func_NotFindPrint<TA> func_notFindPrint = null) {
            int a_len = GetIEnumerableLength(answer);
            int s_len = GetIEnumerableLength(source);

            if (func_lengthNotEquals == null) {
                func_lengthNotEquals = (answer_length, source_length) => {
                    Console.WriteLine("answer_length: {0} source_length: {1} 不相等", answer_length, source_length);
                };
            }
            if (a_len != s_len) {
                func_lengthNotEquals(a_len, s_len);
                return false;
            }
            if (func_isEquals == null) {
                func_isEquals = (a, s) => a.Equals(s);
            }
            if (func_notFindPrint == null) {
                func_notFindPrint = (a) => {
                    Console.WriteLine("答案选项: {0} 没找到", JSON.SerializeObject(a));
                };
            }
            foreach (TA a in answer) {
                bool isfind = false;
                foreach (TS s in source) {
                    if (func_isEquals(a, s)) {
                        isfind = true;
                        break;
                    }
                }
                if (!isfind) {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// 获取公开迭代没剧情数据源的迭代个数长度
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="enumer">迭代数据源</param>
        /// <returns>个数长度</returns>
        public int GetIEnumerableLength<T>(IEnumerable<T> enumer) {
            int len = 0;
            foreach (T a in enumer) {
                len += 1;
            }
            return len;
        }
        #endregion
    }
    /*
     * 正则:
     * \s*public\s?(virtual|abstract)? (.*) (\w+)\(.*\) \{\s*.*\s*\}
     * 替换:
     *  public CaseModel Func_$3() {
            return new CaseModel() {
                NameSign = @"",
                ExeEvent = () => {
                    return true;
                },
            };
        }
     */
}
