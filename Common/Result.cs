/*
 * Date: 2010-08-30
 * Author: Terry Feng
 * Description: 接收各类操作结果
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public static class ResultHelper
    {
        public static Result ToResult(this bool res)
        {
            return ToResult(res, "操作成功", "操作失败", string.Empty);
        }

        public static Result ToResult(this bool res, string msg)
        {
            return ToResult(res, msg, msg, String.Empty);
        }

        public static Result ToResult(this bool res,string sucMsg,string failMsg,string detail)
        {
            return res ? new Result(1, sucMsg) { Detail = detail } : new Result(0, failMsg) { Detail = detail };
        }

        public static Result<T> ToResult<T>(this bool res)
        {
            return ToResult<T>(res, "操作成功", "操作失败",default(T));            
        }

        public static Result<T> ToResult<T>(this bool res,T detail)
        {
            return ToResult<T>(res, "操作成功", "操作失败", detail);
        }      

        public static Result<T> ToResult<T>(this bool res, string msg)
        {
            return ToResult<T>(res, msg, msg, default(T));
        }

        public static Result<T> ToResult<T>(this bool res, string msg, T detail)
        {
            return ToResult<T>(res, msg, msg, detail);
        }

        public static Result<T> ToResult<T>(this bool res, string sucMsg, string failMsg,T detail)
        {
            return res ? new Result<T>(1, sucMsg, detail) : new Result<T>(0, failMsg, detail);
        }
    }

    public class ResultBase
    {
         #region 构造函数
        /// <summary>
        /// 创建操作结果类
        /// </summary>
        /// <param name="code">结果代码</param>
        /// <param name="msg">结果描述</param>
        public ResultBase(int code, string msg)
        {
            this.Code = code;
            this.Message = msg;
        }

        /// <summary>
        /// 创建操作结果类
        /// </summary>
        public ResultBase()
        { }
        #endregion 构造函数

        /// <summary>
        /// 操作返回码
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 冗余返回判断
        /// </summary>
        public int Judge { get; set; }

        /// <summary>
        /// 操作结果信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 是否执行成功(只读属性)
        /// </summary>
        public bool IsSuccessful
        {
            get { return this.Code == 1; }
        }
    }

    /// <summary>
    /// 操作结果类
    /// </summary>
    public class Result : ResultBase
    {
        #region 构造函数
        /// <summary>
        /// 创建操作结果类
        /// </summary>
        /// <param name="code">结果代码</param>
        /// <param name="msg">结果描述</param>
        public Result(int code, string msg)
            : base(code, msg)
        {
        }

        /// <summary>
        /// 创建操作结果类
        /// </summary>
        public Result()
            : base()
        { }
        #endregion 构造函数

        /// <summary>
        /// 操作结果明细
        /// </summary>
        public string Detail { get; set; }

        public Result<T> ToResult<T>()
        {
            return new Result<T>(Code, Message);
        }

        public Result<T> ToResult<T>(T input)
        {
            return new Result<T>(Code, Message,input);
        }
    }

    /// <summary>
    /// 操作结果类
    /// </summary>
    public class Result<T> : ResultBase
    {
        #region 构造函数
        /// <summary>
        /// 创建操作结果类
        /// </summary>
        /// <param name="code">结果代码</param>
        /// <param name="msg">结果描述</param>
        public Result(int code, string msg) : base(code,msg)
        {
        }

        /// <summary>
        /// 创建操作结果类
        /// </summary>
        /// <param name="code">结果代码</param>
        /// <param name="msg">结果描述</param>
        /// <param name="detail">结果数据</param>
        public Result(int code, string msg, T detail)
            : base(code, msg)
        {
            this.Detail = detail;
        }

        /// <summary>
        /// 创建操作结果类
        /// </summary>
        public Result() : base()
        { }
        #endregion 构造函数
       
        /// <summary>
        /// 操作结果明细
        /// </summary>
        public T Detail { get; set; }

        public Result ToResult()
        {
            return new Result(Code, Message);
        }

        public Result ToResult(string input)
        {
            return new Result(Code, Message) { Detail = input };
        }

        public Result<M> ToResult<M>()
        {
            return new Result<M>(Code, Message);
        }

        public Result<M> ToResult<M>(M input)
        {
            return new Result<M>(Code, Message, input);
        }
    }

    /// <summary>
    /// BossService 接口操作返回类
    /// </summary>
    public class BossServiceResult : ResultBase
    {
        /// <summary>
        /// BossService 调用返回码
        /// </summary>
        public string ReturnCode { get; set; }

        /// <summary>
        /// BossService 调用返回描述
        /// </summary>
        public string Detail { get; set; }
    }
}
