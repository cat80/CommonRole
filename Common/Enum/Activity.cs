using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    /// <summary>
    /// 圣诞活动状态
    /// </summary>
    public enum ActivityChristmasState
    {
        未抽中 = 0,
        已抽中 = 1,
        已领取 = 2
    }

     /// <summary>
    /// 圣诞抽奖状态
    /// </summary>
    public enum ActivityChristmasDrawState
    {
        活动结束 = 0,
        没有奖品 = 1,
        抽奖用光 = 2,
        没有中奖 = 3,
        中奖 = 4
    }
}
