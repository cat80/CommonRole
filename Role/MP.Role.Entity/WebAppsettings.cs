using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using Common;
using System.Web;

namespace MP.Role.Entity
{
    /// <summary>
    /// webconfig配置
    /// </summary>
    public class WebAppsettings
    {

        #region 私有成员
        //------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 注册 用户属于哪个用户管理
        /// </summary>
        private static int _regCreatorAdminID = 0;
        /// <summary>
        /// 注册用户有效的天数
        /// </summary>
        private static int _regUserAvailableDay = 7;

        /// <summary>
        /// 是否忽略权限检查
        /// </summary>
        private readonly static bool _ignoreAuthCheck = false;


        /// <summary>
        /// 静态文件部署域名
        /// </summary>
        private readonly static string _staticFileDeployDomain;

        /// <summary>
        /// 是否有Debug状态
        /// </summary>
        private readonly static bool _isDebug;

        private readonly static int _officialSiteOemID;

        /// <summary>
        /// 官方站点OEMID
        /// </summary>
        public static int OfficialSiteOemID
        {
            get { return WebAppsettings._officialSiteOemID; }
        }

        /// <summary>
        /// 是否为Debug
        /// </summary>
        public static bool IsDebug
        {
            get { return WebAppsettings._isDebug; }
        }


        //------------------------------------------------------------------------------------------------------------
        #endregion


        /// <summary>
        /// 静态文件部署域名
        /// </summary>
        public static string StaticFileDeployDomain
        {
            get
            {
                return _staticFileDeployDomain;
            }
        }
   

        /// <summary>
        /// 是否自动登陆
        /// </summary>
        public static bool AutoLogon
        {
            get
            {
                if (string.Equals(ConfigurationManager.AppSettings["AutoLogon"], "true", StringComparison.CurrentCultureIgnoreCase))
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// 自动登陆用户名
        /// </summary>
        public static string AutoLogonUser
        {
            get
            {
                return ConfigurationManager.AppSettings["AutoLogonUser"];
            }
        }


  
        /// <summary>
        /// 是否忽略权限检查
        /// </summary>
        public static bool IgnoreAuthCheck
        {
            get
            {
                //if (
                //{
                //    return true;
                //}
                //return false;
                return _ignoreAuthCheck;
            }
        }

        /// <summary>
        /// 客户端部署域名
        /// </summary>
        public static string ClientDeployDomain
        {
            get
            {
                //string strHost = "http://" + HttpContext.Current.Request.Url.Host;
                //Log.Default.Debug("当前域名：" + strHost);
                return ConfigurationManager.AppSettings["ClientDeployDomain"] ?? string.Empty;
            }
        }

        /// <summary>
        /// 管理后台部署域名
        /// </summary>
        public static string ServerDeployDomain
        {
            get
            {
                return ConfigurationManager.AppSettings["ServerDeployDomain"] ?? string.Empty;
            }
        }



        /// <summary>
        /// 转换字符为intList
        /// </summary>
        /// <param name="str"></param>
        /// <param name="split"></param>
        /// <returns></returns>
        private static List<int> ConvertStringToList(string str, char split = ',')
        {
            List<int> list = new List<int>();
            if (string.IsNullOrEmpty(str))
            {
                return list;
            }
            string[] arr = str.Split(split);
            foreach (var item in arr)
            {
                int temp = 0;
                if (int.TryParse(item, out temp))
                {
                    list.Add(temp);
                }
            }
            return list;
        }

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static WebAppsettings()
        {
            try
            {

                _ignoreAuthCheck = string.Equals(ConfigurationManager.AppSettings["IgnoreAuthCheck"], "true", StringComparison.CurrentCultureIgnoreCase);

                _isDebug = string.Equals(ConfigurationManager.AppSettings["IsDebug"], "true", StringComparison.CurrentCultureIgnoreCase);

                _staticFileDeployDomain = ConfigurationManager.AppSettings["StaticFileDeployDomain"] ?? string.Empty;

                //静态文件部署域名
                if (_staticFileDeployDomain.EndsWith("/"))
                {
                    _staticFileDeployDomain = _staticFileDeployDomain.Substring(0, _staticFileDeployDomain.Length - 1);
                }


                //_enableGeneralAgentAnti = string.Equals(ConfigurationManager.AppSettings["EnableGeneralAgentAnti"], "true", StringComparison.CurrentCultureIgnoreCase);

                //官方站点ID

             //   int.TryParse(ConfigurationManager.AppSettings["OfficialSiteOemID"], out _officialSiteOemID);
                //_officialSiteOemID = ConfigurationManager.AppSettings["OfficialSiteOemID"] ?? string.Empty;
                //智能配置

            }
            catch (Exception ex)
            {
                Log.Default.Error("配置初始化出现异常：" + ex);
            }
        }


        /// <summary>
        /// 系统结算用户
        /// </summary>
        public static int SystemSettlementUserID
        {
            get
            {
                return 1;
            }
        }
    }
}
