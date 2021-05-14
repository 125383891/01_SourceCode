using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYY.AuthCenter.Entity
{
    public class DepartmentEntity
    {
        /// <summary>
        /// 系统Id
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// 父级Id
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 关注度，用户数量
        /// </summary>
        public int UserCount { get; set; }

        /// <summary>
        /// 日活跃度，昨天的活跃用户用户数量
        /// </summary>
        public int UserDailyCount { get; set; }

        /// <summary>
        /// 投诉量，累计主题数量
        /// </summary>
        public int SubjectCount { get; set; }

        /// <summary>
        /// 办结率，有评星的主题占百分比
        /// </summary>
        public decimal StarRate { get; set; }
    }
}
