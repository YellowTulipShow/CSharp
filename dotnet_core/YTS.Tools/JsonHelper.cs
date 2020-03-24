using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using Newtonsoft.Json;

namespace YTS.Tools
{
    /// <summary>
    /// JSON帮助类
    /// </summary>
    public class JsonHelper
    {
        /// <summary>
        /// 对象转JSON
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>JSON格式的字符串</returns>
        public static string ObjectToJSON(object obj)
        {
            try
            {
                byte[] b = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(obj));
                return Encoding.UTF8.GetString(b);
            }
            catch (Exception ex)
            {

                throw new Exception("JSONHelper.ObjectToJSON(): " + ex.Message);
            }
        }

        /// <summary>
        /// 数据表转键值对集合
        /// 把DataTable转成 List集合, 存每一行
        /// 集合中放的是键值对字典,存每一列
        /// </summary>
        /// <param name="dt">数据表</param>
        /// <returns>哈希表数组</returns>
        public static List<Dictionary<string, object>> DataTableToList(DataTable dt)
        {
            List<Dictionary<string, object>> list
                 = new List<Dictionary<string, object>>();

            foreach (DataRow dr in dt.Rows)
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                foreach (DataColumn dc in dt.Columns)
                {
                    dic.Add(dc.ColumnName, dr[dc.ColumnName]);
                }
                list.Add(dic);
            }
            return list;
        }

        /// <summary>
        /// 数据集转键值对数组字典
        /// </summary>
        /// <param name="dataSet">数据集</param>
        /// <returns>键值对数组字典</returns>
        public static Dictionary<string, List<Dictionary<string, object>>> DataSetToDic(DataSet ds)
        {
            Dictionary<string, List<Dictionary<string, object>>> result = new Dictionary<string, List<Dictionary<string, object>>>();

            foreach (DataTable dt in ds.Tables)
                result.Add(dt.TableName, DataTableToList(dt));

            return result;
        }

        /// <summary>
        /// 数据表转JSON
        /// </summary>
        /// <param name="dataTable">数据表</param>
        /// <returns>JSON字符串</returns>
        public static string DataTableToJSON(DataTable dt)
        {
            return ObjectToJSON(DataTableToList(dt));
        }

        /// <summary>
        /// JSON文本转对象,泛型方法
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="jsonText">JSON文本</param>
        /// <returns>指定类型的对象</returns>
        public static T JSONToObject<T>(string jsonText)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(jsonText);
            }
            catch (Exception ex)
            {
                throw new Exception("JSONHelper.JSONToObject(): " + ex.Message);
            }
        }

        /// <summary>
        /// 将JSON文本转换为数据表数据
        /// </summary>
        /// <param name="jsonText">JSON文本</param>
        /// <returns>数据表字典</returns>
        public static Dictionary<string, List<Dictionary<string, object>>> TablesDataFromJSON(string jsonText)
        {
            return JSONToObject<Dictionary<string, List<Dictionary<string, object>>>>(jsonText);
        }

        /// <summary>
        /// 将JSON文本转换成数据行
        /// </summary>
        /// <param name="jsonText">JSON文本</param>
        /// <returns>数据行的字典</returns>
        public static Dictionary<string, object> DataRowFromJSON(string jsonText)
        {
            return JSONToObject<Dictionary<string, object>>(jsonText);
        }


        #region ====== Newtonsoft.Json BLL Json Serialize Deserialize ======
        /// <summary>
        /// 序列化: 对象 转为 JSON格式
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>json字符串</returns>
        public static String SerializeObject(Object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// 反序列化: JSON字符串生成对象实体
        /// </summary>
        /// <typeparam name="T">对象类型 可以是数据</typeparam>
        /// <param name="json">json字符串(eg.{"ID":"112","Name":"石子儿"})</param>
        /// <returns>对象实体</returns>
        public static T DeserializeToObject<T>(String json) where T : class
        {
            JsonSerializer serializer = new JsonSerializer();
            StringReader sr = new StringReader(json);
            object o = serializer.Deserialize(new JsonTextReader(sr), typeof(T));
            T t = o as T;
            return t;
        }

        /// <summary>
        /// 反序列化: JSON到给定的匿名对象.
        /// </summary>
        /// <typeparam name="T">匿名对象类型</typeparam>
        /// <param name="json">json字符串</param>
        /// <param name="anonymousTypeObject">匿名对象</param>
        /// <returns>匿名对象</returns>
        public static T DeserializeAnonymousType<T>(String json, T anonymousTypeObject)
        {
            T t = JsonConvert.DeserializeAnonymousType(json, anonymousTypeObject);
            return t;

            /*
                DeserializeAnonymousType 使用方法:
                //匿名对象解析
                var tempEntity = new { ID = 0, Name = String.Empty };
                String json5 = JsonHelper.SerializeObject(tempEntity);
                //json5 : {"ID":0,"Name":""}
                tempEntity = JsonHelper.DeserializeAnonymousType("{\"ID\":\"112\",\"Name\":\"石子儿\"}", tempEntity);
                Console.WriteLine(tempEntity.ID + ":" + tempEntity.Name);
            */
        }
        #endregion
    }
}
