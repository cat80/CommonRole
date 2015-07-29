using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.AgentMarke
{
    /// <summary>
    /// 页面查询状态
    /// </summary>
    public enum OrderState
    {
        已确认 = 1,
        已成功 = 2,
        待确认 = 3,
        已无效 = 4
    }
}
