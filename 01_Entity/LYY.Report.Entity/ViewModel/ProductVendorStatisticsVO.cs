using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYY.Report.Entity
{
    public class ProductVendorStatisticsVO
    {
        /// <summary>
        /// 产品名称或供应商名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 投诉量
        /// </summary>
        public int ComplaintCount { get; set; }

        /// <summary>
        /// 完结数量
        /// </summary>
        public int FinishCount { get; set; }

        /// <summary>
        /// 完结率
        /// </summary>
        public string FinishCountProportion
        {
            get
            {

                if (ComplaintCount == 0)
                {
                    return string.Format("{0}%", decimal.Parse("0"));
                }
                else
                {
                    var proportion = (decimal.Parse(FinishCount.ToString()) / decimal.Parse(ComplaintCount.ToString())) * 100;
                    return string.Format("{0:N1}%", proportion);
                }
            }
        }

        public int SerialNumber { get; set; }
    }
}
