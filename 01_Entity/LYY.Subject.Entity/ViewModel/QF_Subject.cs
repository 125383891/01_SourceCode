using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YZ.Utility;

namespace LYY.Subject.Entity
{
    public class QF_Subject: QueryFilter
    {
        /// <summary>
        /// 关键字
        /// </summary>
        public string Keyword { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 主题分类
        /// </summary>
        public int? CatId { get; set; }

        /// <summary>
        /// 单位Id
        /// </summary>
        public int? DepartmentId { get; set; }

        /// <summary>
        /// 主题ID
        /// </summary>
        public int SubjectId { get; set; }

        /// <summary>
        /// 供应商名称
        /// </summary>
        public string VendorName { get; set; }
    }
}
