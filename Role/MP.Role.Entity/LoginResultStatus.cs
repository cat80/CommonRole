using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MP.Role.Entity
{
    /// <summary>
    /// 登陆状态
    /// </summary>
    public enum LoginResultStatus
    {
        用户名和密码不匹配 = 1,
        账户被禁用 = 2,
        登陆成功 = 3,
        系统出错 = 4,
        账号已经到期 = 8,
        账号不存在 = 16
    }
}
