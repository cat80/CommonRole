using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
namespace CommonRole.Presentation
{
    public abstract class WebViewPage<TModel> : System.Web.Mvc.WebViewPage<TModel>
    {
        private RoleATagHelper _roleATag;

        /// <summary>
        /// 角色Url辅助类
        /// </summary>
        public RoleATagHelper RoleATag
        {

            get
            {
                if (_roleATag == null)
                {
                    _roleATag = new RoleATagHelper(Url);
                }
                return _roleATag;
            } 
        }

        //public string RoleALink(string actionName,string controlName,)
    } 
    public abstract class WebViewPage : WebViewPage<dynamic>
    {
    }
}
