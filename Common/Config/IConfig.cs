using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Config
{
    /// <summary>
    /// xml配置泛型接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IConfig<T>
    {
        /// <summary>
        /// 获取当前的XML配置内容
        /// </summary>
        T Current { get; }

        /// <summary>
        /// 保存XML配置到文件
        /// </summary>
        /// <param name="entity"></param>
        void SaveConfig(T entity);
    }
}
