using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYY.Report.Entity
{
    public enum DateTimeTypeEnum
    {
        [Description("日")]
        Week = 1,

        [Description("月")]
        Month = 2,

        [Description("年")]
        Year = 3
    }

    public enum StatisticsObject
    {
        [Description("产品")]
        Product = 1,

        [Description("供应商")]
        Vendor = 2
    }
}
