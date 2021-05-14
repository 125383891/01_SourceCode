using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YZ.Utility;

namespace LYY.Report.Entity
{
    public class QF_Report : QueryFilter
    {
        public DateTime? DateTimeTypeStartTime
        {
            get
            {
                if (DateTimeType.HasValue)
                {
                    var cuurent = DateTime.Now;
                    switch (DateTimeType.Value)
                    {
                        case DateTimeTypeEnum.Week:
                            return new DateTime(cuurent.Year, cuurent.Month, cuurent.Day).AddDays(-7);
                        case DateTimeTypeEnum.Month:
                            return new DateTime(cuurent.Year, 1, 1);
                        case DateTimeTypeEnum.Year:
                            return new DateTime(2019, 1, 1);
                        default:
                            return null;
                    }
                }
                else
                {
                    return null;
                }
            }
        }

        public DateTime? DateTimeTypeEndTime
        {
            get
            {
                if (DateTimeType.HasValue)
                {
                    var cuurent = DateTime.Now;
                    switch (DateTimeType.Value)
                    {
                        case DateTimeTypeEnum.Week:
                        case DateTimeTypeEnum.Month:
                        case DateTimeTypeEnum.Year:
                            return new DateTime(cuurent.Year, cuurent.Month, cuurent.Day).AddDays(1);
                        default:
                            return null;
                    }
                }
                else
                {
                    return null;
                }
            }
        }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public int? DepartmentId { get; set; }

        public StatisticsObject StatisticsObject { get; set; }

        public DateTimeTypeEnum? DateTimeType { get; set; }

        public int? ArticleApplicationType { get; set; }

        public int? ArticleId { get; set; }
    }
}
