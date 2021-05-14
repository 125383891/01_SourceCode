using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYY.Common.Entity
{
    public enum DeletedEnums
    {
        [Description("有效")]
        Actived = 0,
        [Description("已删除")]
        Deleted = 1
    }
}
