using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSharp.LibrayFunction;

namespace Test.ConsoleProgram.Case.Topic
{
    public class MissQueue : CaseModel
    {
        public MissQueue() {
            this.NameSign = @"小姐队列测试题";
            this.ExeEvent = Method;
        }

        public class Miss : AbsBasicDataModel
        {
            /// <summary>
            /// 姓名
            /// </summary>
            public string name = string.Empty;
            /// <summary>
            /// 宠物
            /// </summary>
            public string pet = string.Empty;
            /// <summary>
            /// 衣服
            /// </summary>
            public string clothes = string.Empty;
            /// <summary>
            /// 饮料
            /// </summary>
            public string drink = string.Empty;
            /// <summary>
            /// 水果
            /// </summary>
            public string fruit = string.Empty;
        }

        public void Method() {
            Miss[] arr = InitCreateMissArray();
            arr = RuleContentAdd(arr);
        }

        public Miss[] InitCreateMissArray() {
            List<Miss> arr = new List<Miss>();
            for(int i = 0; i < 5; i++) {
                arr.Add(new Miss());
            }
            return arr.ToArray();
        }

        public Miss[] RuleContentAdd(Miss[] arr) {
            arr[0].name = @"赵小姐";
            arr[1].clothes = @"蓝衣服";


            return arr;
        }
    }
}
