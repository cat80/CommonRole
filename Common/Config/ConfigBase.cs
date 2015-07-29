using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Common.Config
{
    /// <summary>
    /// 配置基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ConfigBase<T> : IConfig<T>
    {
        #region 私有成员
        //------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// 配置文件保存路径。默认为当前应用程序域/config/filename.config
        /// </summary>
        protected virtual string ConfigFilePath
        {
            get
            {
                string configDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config");
                if (Directory.Exists(configDir) == false)
                {
                    Directory.CreateDirectory(configDir);
                }
                return Path.Combine(configDir, ConfigFileName + ".config");
            }
        }

        // 站点配置文件路径
        /// <summary>
        /// 配置缓存key,子类提供
        /// </summary>
        protected abstract string CACHE_KEY { get; }              //  缓存key
        /// <summary>
        /// 配置文件名称,子类提供
        /// </summary>
        protected abstract string ConfigFileName { get; }
        //------------------------------------------------------------------------------------------------------------
        #endregion

        /// <summary>
        /// 当前配置内容。
        /// 从指定路径获取配置文件，并设置文件依赖缓存。
        /// </summary>
        public virtual T Current
        {
            get
            {
                var config = MPCache.Get<T>(CACHE_KEY);

                if (config == null && File.Exists(ConfigFilePath))
                {
                    try
                    {

                        config = SerializationHelper.Load<T>(ConfigFilePath);
                        MPCache.Max(CACHE_KEY, config, new System.Web.Caching.CacheDependency(ConfigFilePath)); //加入缓存
                    }
                    catch (Exception ex)
                    {
                        Log.Default.Error("加载配置出错:" + ex);
                    }
                }
                return config;
            }
        }

        /// <summary>
        /// 保存配置配置到文件
        /// </summary>
        /// <param name="entity"></param>
        public void SaveConfig(T entity)
        {
            SerializationHelper.Save(entity, ConfigFilePath);
            // throw new NotImplementedException();
        }


    }
}