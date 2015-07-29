using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    /// <summary>
    /// 页面查询状态
    /// </summary>
    public enum PageCommandType
    {
        增加,
        更新,
        删除,
        查看
    }

    #region Mall
    /// <summary>
    /// 存放类型
    /// </summary>
    public enum MallSaveType
    {
        下架 = 0,
        上架 = 1
    }

    /// <summary>
    /// 显示类型
    /// </summary>
    public enum MallShowType
    {
        不显示 = 0,
        第一个 = 1,
        后一个 = 2
    }

    #endregion
}
