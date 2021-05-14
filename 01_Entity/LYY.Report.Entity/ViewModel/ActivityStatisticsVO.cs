using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYY.Report.Entity
{
    public class ActivityStatisticsVO
    {
        /// <summary>
        /// 公司名称
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// 活跃度
        /// </summary>
        public int ActivityCount { get; set; }

        /// <summary>
        /// 去年同期活跃度
        /// </summary>
        public int LastYearActivityCount { get; set; }

        public string LastYearActivityCountProportion
        {
            get
            {
                if (LastYearActivityCount == 0)
                {
                    return string.Format("{0}%", decimal.Parse("0")); 
                }
                else
                {
                    var proportion = (decimal.Parse(ActivityCount.ToString()) / decimal.Parse(LastYearActivityCount.ToString())) * 100;
                    return string.Format("{0:N1}%", proportion);
                }
            }
        }

        /// <summary>
        /// 上月同期活跃度
        /// </summary>
        public int LastMonthActivityCount { get; set; }

        public string LastMonthComplaintCountProportion
        {
            get
            {
                if (LastMonthActivityCount == 0)
                {
                    return string.Format("{0}%", decimal.Parse("0"));
                }
                else
                {
                    var proportion = (decimal.Parse(ActivityCount.ToString()) / decimal.Parse(LastMonthActivityCount.ToString())) * 100;
                    return string.Format("{0:N1}%", proportion);
                }
            }
        }

        public int SerialNumber { get; set; }
    }
}
