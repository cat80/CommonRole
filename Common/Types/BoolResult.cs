using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    /// <summary>
    /// bool函数返回值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BoolResult<T>
    {

        public BoolResult(bool success, T result)
        {
            this._result = result;
            this._success = success;
        }

        public BoolResult(bool success)
            : this(success, default(T))
        {
        }

        private readonly T _result;
        private readonly bool _success;

        /// <summary>
        /// 结果
        /// </summary>
        public bool Success
        {
            get { return _success; }
        }
        /// <summary>
        /// 返回值
        /// </summary>
        public T Result
        {
            get
            {
                return _result;
            }
        }


    }
}
