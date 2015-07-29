using Common.Types; 
using MP.Role.Businuss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace CommonRole.Presentation
{
    public class RoleATagHelper
    {
        private UrlHelper urlHelper;
        public RoleATagHelper(UrlHelper urlHelper)
        {
            this.urlHelper = urlHelper;
        }

        /// <summary>
        /// 生成链接
        /// </summary>
        /// <param name="actionName"></param>
        /// <param name="controllerName"></param>
        /// <param name="routeValues"></param>
        /// <param name="text"></param>
        /// <param name="attributes"></param>
        /// <returns></returns>

        public string Link(string controllerName, string actionName, string innerText, object routeValues = null, object attributes = null)
        {
            if (CurrentUserInfo.HasActionAuth(controllerName, actionName) == false)
            {
                return string.Empty;
            }
            string link = urlHelper.Action(actionName, controllerName, routeValues);
            string attrStr = string.Empty;
            if (attributes != null)
            {
                var dic = TypeHelper.ObjectToDictionary(attributes);
                foreach (var item in dic)
                {
                    if (item.Key.Equals("href", StringComparison.InvariantCultureIgnoreCase))
                    {
                        dic.Remove(item.Key);
                    }
                } 
                attrStr = string.Join(" ", dic.Select(item => string.Format("{0}=\"{1}\"", item.Key, item.Value == null ? string.Empty : item.Value.ToString().Replace("\"", "\'"))));
            }
            return string.Format("<a href=\"{0}\" {1}>{2}</a>", link, attrStr, innerText);

        }


    }
}
