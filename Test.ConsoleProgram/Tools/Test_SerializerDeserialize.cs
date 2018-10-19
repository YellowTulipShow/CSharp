using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTS.Tools;

namespace Test.ConsoleProgram.Tools
{
    public class Test_SerializerDeserialize : CaseModel
    {
        public Test_SerializerDeserialize() {
            NameSign = @"序列化反序列化";
            SonCases = new CaseModel[] {
                Obj_JSON(),
                Obj_XML(),
            };
        }

        #region model
        public class Person
        {
            public int Age { get; set; }
            public Customer Bill { get; set; }
        }
        public class Customer
        {
            public string Title { get; set; }
            public string Surname { get; set; }
        }
        #endregion

        public CaseModel Obj_JSON() {
            return new CaseModel() {
                NameSign = @"JSON",
                ExeEvent = () => {
                    return CalcMethod(JSON.Serializer, JSON.Deserialize<Person[]>);
                },
            };
        }

        public CaseModel Obj_XML() {
            return new CaseModel() {
                NameSign = @"JSON",
                ExeEvent = () => {
                    return CalcMethod(XML.Serializer<Person[]>, XML.Deserialize<Person[]>);
                },
            };
        }

        public Person[] GetModels() {
            Person[] array = new Person[10];
            for (int i = 0; i < array.Length; i++) {
                array[i] = new Person() {
                    Age = RandomData.GetInt(1, 52),
                    Bill = new Customer() {
                        Surname = RandomData.GetString(RandomData.GetInt(1, 52)),
                        Title = RandomData.GetString(RandomData.GetInt(1, 52)),
                    },
                };
            }
            return array;
        }

        public bool CalcMethod(Func<Person[], string> serializer, Func<string, Person[]> deserialize) {
            Person[] array = GetModels();
            string str_result = serializer(array);
            Person[] source = deserialize(str_result);

            VerifyIList<Person, Person> verify = new VerifyIList<Person, Person>(CalcWayEnum.SingleCycle) {
                Answer = array,
                Source = source,
                Func_isEquals = (a, s) => {
                    return a.Age == s.Age &&
                        a.Bill.Title == s.Bill.Title &&
                        a.Bill.Surname == s.Bill.Surname;
                },
            };
            return verify.Calc();
        }
    }
}
