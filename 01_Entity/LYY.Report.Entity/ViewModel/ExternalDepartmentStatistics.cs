using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYY.Report.Entity
{
    public class ExternalDepartmentStatistics
    {
        /// <summary>
        /// 主题Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 主题所属部门名称
        /// </summary>
        public string SubjectDepartmentName { get; set; }

        /// <summary>
        /// 主题创建者姓名
        /// </summary>
        public string CreateSubjectUserName { get; set; }

        /// <summary>
        /// 主题名称
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 解析title
        /// </summary>
        public string ParseTitle
        {
            get
            {
                if (!string.IsNullOrEmpty(Title))
                {
                    return YZ.Utility.DynamicJson.Parse(Title).v;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// 外部协助人员姓名
        /// </summary>
        public string ExternalUserName { get; set; }

        /// <summary>
        /// 外部协助人员部门名称
        /// </summary>
        public string ExternalDepartmentName { get; set; }

        /// <summary>
        /// 外派时间
        /// </summary>
        public DateTime? AssignmentTime { get; set; }

        /// <summary>
        /// 完结评星数
        /// </summary>
        public int? Stat { get; set; }
    }
}
