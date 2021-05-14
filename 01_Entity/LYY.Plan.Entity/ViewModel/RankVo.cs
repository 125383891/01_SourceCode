using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YZ.Utility;
using AutoMapper;
using AutoMapper.Configuration;

namespace LYY.Plan.Entity
{
    /// <summary>
    /// 单位积分查询模型
    /// </summary>
    public class Department_Plan_RankVO : t_department_plan_rank
    {
        public string name { get; set; }
        public string plan_name { get; set; }
    }

    public class Department_Plan_RankFilter : QueryFilter
    {
        public int? plan_id { get; set; }
    }

    public class DepartmentPlanRankValid
    {
        public int? plan_id { get; set; }
        public string plan_name { get; set; }
        public int? dept_id { get; set; }
        public string dept_name { get; set; }
    }

    /// <summary>
    /// 处理人积分 查询模型
    /// </summary>
    public class User_Plan_RankVO : t_user_plan_rank
    {
        public string name { get; set; }
        public string plan_name { get; set; }
    }

    public class User_Plan_RankFilter : QueryFilter
    {
        public int? plan_id { get; set; }
    }


    public class UserPlanRankValid
    {
        public int? plan_id { get; set; }
        public string plan_name { get; set; }
        public string user_id { get; set; }
        public string user_name { get; set; }
    }

    public class CommboxMode
    {
        public int value { get; set; }
        public string label { get; set; }
    }

}
