using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Xml.Serialization;
using System.Configuration;
namespace Common.Config
{
    /// <summary>
    /// 站点配置
    /// </summary>
    [XmlRoot("config")]
    public class SiteConfig
    {

        #region 私有成员
        //------------------------------------------------------------------------------------------------------------
        private static string ConfigFilePath;                                   //站点配置文件路径
        private const string CACHE_KEY = "SITECONFIG_CHACHE_KEY";               //缓存key
        //------------------------------------------------------------------------------------------------------------
        #endregion

        #region 序列化成员
        //------------------------------------------------------------------------------------------------------------

        [XmlArray("smsconfig"), XmlArrayItem("item")]
        public List<SmsConfig> SmsConfig { get; set; }
        //------------------------------------------------------------------------------------------------------------
        #endregion

        #region 辅助方法
        //------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 静态配置
        /// </summary>
        static SiteConfig()
        {
            Log.Default.Debug("当前服务器地址：AppDomain.CurrentDomain.BaseDirectory:" + AppDomain.CurrentDomain.BaseDirectory);
            ConfigFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "site.config");
            //ConfigFilePath=System.Web.HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["site.config"].ToString());
        }

        /// <summary>
        /// 获取获取内容根据类别名称
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public SmsConfig GetSmsByType(string typeName)
        {
            Log.Default.Debug("--site.config：地址：" + ConfigFilePath);
            Log.Default.Debug("SmsConfig:数量" + SmsConfig.Count.ToString());
            if (SmsConfig == null)
            {
                return null;
            }
            return SmsConfig.FirstOrDefault(item => item.SmsType == typeName);

        }
        //------------------------------------------------------------------------------------------------------------
        #endregion


        /// <summary>
        /// 当前配置
        /// </summary>
        [XmlIgnore]
        public static SiteConfig Current
        {
            get
            {
                var config = MPCache.Get<SiteConfig>(CACHE_KEY);
                Log.Default.Debug("--site.config：地址：" + ConfigFilePath);
                if (config == null)
                {
                    Log.Default.Debug("Current方法:" + AppDomain.CurrentDomain.BaseDirectory);
                    config = SerializationHelper.Load<SiteConfig>(ConfigFilePath);
                    Log.Default.Debug("Current方法config:" + config);

                    MPCache.Max(CACHE_KEY, config, new System.Web.Caching.CacheDependency(ConfigFilePath)); //加入缓存
                }
                return config;
            }
        }

        /// <summary>
        /// 保存配置
        /// </summary>
        /// <param name="config"></param>
        public static void SaveConfig(SiteConfig config)
        {
            //保存序列化对象
            SerializationHelper.Save(config, ConfigFilePath);

        }


    }
}
