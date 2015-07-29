using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Config
{
    public class LeAccountConfig : ConfigBase<List<LeAccountEntity>>
    {
        protected override string CACHE_KEY
        {
            get { return "CACHE_LeAccountConfig"; }
        }

        protected override string ConfigFileName
        {
            get { return "leaccount"; }
        }
        private static LeAccountConfig _instance = null;
        private LeAccountConfig()
        {
        }
        static LeAccountConfig()
        {
            //LeAccountConfig.Instance.Current.
            _instance = new LeAccountConfig();
        }
        private Random random = new Random();
        /// <summary>
        /// 随机账号
        /// </summary>
        public LeAccountEntity RandomAcccount
        {
            get
            {
                if (Instance.Current == null ||
                    Instance.Current.Count == 0)
                {
                    return new LeAccountEntity() { BindWeiXinID = "gh_2d0dda3762e5", Token = "59db0978532d3d0cd1a171f874f96a58", InterfaceUrl = "http://www.wxapi.cn/i.php?&i=19757&tk=68ce6e8f1406afce6aecf180aa18a14b" };
                }
                return Instance.Current[random.Next(Instance.Current.Count)];
            }
        }
        public static LeAccountConfig Instance
        {
            get
            {
                return _instance;
            }
        }


    }
}
