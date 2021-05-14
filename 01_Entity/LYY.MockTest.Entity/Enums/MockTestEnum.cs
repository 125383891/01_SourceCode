using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYY.MockTest.Entity.Enums
{
    /// <summary>
    /// 题型
    /// </summary>
    public enum Question_Mode
    {
        [Description("单选")]
        Single = 1,

        [Description("多选")]
        Multiple = 2,

        [Description("判断")]
        Judge = 3
    }
    /// <summary>
    /// 是否正确答案
    /// </summary>
    public enum Answer_Is_Right
    {
        [Description("否")]
        Not = 0,
        [Description("是")]
        Yes = 1
    }
    /// <summary>
    /// 测验是否完成
    /// </summary>
    public enum Test_Is_Complete
    {
        [Description("未完成")]
        NotComplete = 0,
        [Description("已完成")]
        Complete = 1
    }
    public enum Test_Mode
    {
        [Description("个人练习")]
        Personal = 1,
        [Description("模拟考试")]
        Simulation = 2
    }

    public enum UserTagEnums
    {
        [Description("管理员")]
        UserTag1 = 1,

        [Description("采购经理")]
        UserTag2 = 2,

        [Description("领导")]
        UserTag3 = 3,
        [Description("活动专员")]
        UserTag4 = 4,

        [Description("供应链内部")]
        UserTag5 = 5,

        [Description("供应商")]
        UserTag6 = 6,

        [Description("分公司质量管理员")]
        UserTag7 = 7,

        [Description("服务专区成员")]
        UserTag8 = 8,

        [Description("评审专家")]
        UserTag9 = 9,

        [Description("采购人员")]
        UserTag10 = 10,

        [Description("代理人员")]
        UserTag11 = 11
    }
}
