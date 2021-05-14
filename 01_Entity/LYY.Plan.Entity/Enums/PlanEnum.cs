using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYY.Plan.Entity
{
    public enum PlanEnum
    {
        [Description("未开始")]
        NotStarted = 0,

        [Description("进行中")]
        InProgress = 5,

        [Description("已截止")]
        Deadline = 10,

        [Description("已关闭")]
        Closed = 15
    }
}
