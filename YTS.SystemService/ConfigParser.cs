using System;
using System.Collections.Generic;
using YTS.Tools;

namespace YTS.SystemService
{
    public class ConfigParser
    {
        private Dictionary<string, AbsConfig> AbsConfigDictionary;

        public ConfigParser() {
            this.AbsConfigDictionary = new Dictionary<string, AbsConfig>();
        }

        /// <summary>
        /// 获取 配置信息
        /// </summary>
        /// <typeparam name="C">配置信息类型</typeparam>
        /// <returns>结果配置信息</returns>
        public C Get<C>() where C : AbsConfig, new() {
            Type tc = typeof(C);
            AbsConfig absconfig = Get(tc.FullName);
            if (CheckData.IsObjectNull(absconfig)) {
                C newconfig = new C();
                newconfig.Load();
                Set(tc.FullName, newconfig);
                return newconfig;
            }
            if (CheckData.IsTypeEqual(absconfig.GetType(), tc)) {
                return (C)absconfig;
            } else {
                throw new Exception("需要的配置类型与结果的配置类型不一样!");
            }
        }

        /// <summary>
        /// 获取 配置信息
        /// </summary>
        /// <param name="config_key_name">配置信息的键名称</param>
        /// <returns>抽象的配置信息</returns>
        public AbsConfig Get(string config_key_name) {
            config_key_name = ConvertTool.ToStringTrim(config_key_name);
            if (CheckData.IsStringNull(config_key_name)) {
                return null;
	        }
            if (this.AbsConfigDictionary.ContainsKey(config_key_name)) {
                return this.AbsConfigDictionary[config_key_name];
            }
            return null;
        }

        /// <summary>
        /// 设置 配置信息
        /// </summary>
        /// <param name="config_key_name">配置信息的键名称</param>
        /// <param name="config">需要设置的配置信息</param>
        private void Set(string config_key_name, AbsConfig config) {
            config_key_name = ConvertTool.ToStringTrim(config_key_name);
            if (CheckData.IsStringNull(config_key_name)) {
                throw new Exception("配置键值名称为空");
            }
            if (CheckData.IsObjectNull(config)) {
                throw new Exception("配置实例为空");
            }
            this.AbsConfigDictionary[config_key_name] = config;
        }

        ~ConfigParser() {
            SaveALLConfig();
        }

        public void SaveALLConfig() {
            foreach (KeyValuePair<string, AbsConfig> kv in this.AbsConfigDictionary) {
                kv.Value.Save();
            }
        }
    }
}
