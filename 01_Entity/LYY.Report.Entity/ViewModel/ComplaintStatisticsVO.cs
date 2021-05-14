using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYY.Report.Entity
{
    public class ComplaintStatisticsVO
    {
        /// <summary>
        /// 序号
        /// </summary>
        public int SerialNumber { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// 投诉量
        /// </summary>
        public int ComplaintCount { get; set; }

        /// <summary>
        /// 去年同期投诉量
        /// </summary>
        public int LastYearComplaintCount { get; set; }

        public string LastYearComplaintCountProportion
        {
            get
            {
                if (LastYearComplaintCount == 0)
                {
                    return string.Format("{0}%", decimal.Parse("0")); ;
                }
                else
                {
                    var proportion = (decimal.Parse(ComplaintCount.ToString()) / decimal.Parse(LastYearComplaintCount.ToString())) * 100;
                    return string.Format("{0:N1}%", proportion);
                }
            }
        }

        /// <summary>
        /// 上月同期投诉量
        /// </summary>
        public int LastMonthComplaintCount { get; set; }

        public string LastMonthComplaintCountProportion
        {
            get
            {
                if (LastMonthComplaintCount == 0)
                {
                    return string.Format("{0}%", decimal.Parse("0")); 
                }
                else
                {
                    var proportion = (decimal.Parse(ComplaintCount.ToString()) / decimal.Parse(LastMonthComplaintCount.ToString())) * 100;
                    return string.Format("{0:N1}%", proportion);
                }
            }
        }
    }
}
