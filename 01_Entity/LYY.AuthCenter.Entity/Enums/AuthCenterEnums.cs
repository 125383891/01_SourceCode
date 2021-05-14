using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYY.AuthCenter.Entity
{
    public enum CommonStatus
    {
        [Description("有效")]
        Actived = 1,
        [Description("无效")]
        DeActived = 0,
        [Description("已删除")]
        Deleted = -999
    }

    public enum VisibleStatus
    {
        [Description("可见")]
        Visible = 1,
        [Description("不可见")]
        NotVisible = 0
    }
}
