using LYY.Report.Entity;
using LYY.ReportService.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YZ.Utility;

namespace LYY.ReportService.Service
{
    public class ReportBigService
    {
        public List<ReportChartVO> ComplaintStatisticsChart(QF_Report filter)
        {
            if (!filter.DateTimeType.HasValue)
            {
                throw new BusinessException("请选择统计模式");
            }
            if (!filter.StartTime.HasValue || !filter.EndTime.HasValue)
            {
                throw new BusinessException("请选择开始时间/结束时间");
            }
            List<ReportChartVO> reportChartVOs = ReportBigDA.ComplaintStatisticsChart(filter);
            return this.BindDateTimeFullKey(filter, reportChartVOs);
        }

        public List<ComplaintStatisticsVO> ComplaintStatisticsList(QF_Report filter)
        {
            if (!filter.DateTimeType.HasValue)
            {
                throw new BusinessException("请选择统计模式");
            }
            if (!filter.StartTime.HasValue || !filter.EndTime.HasValue)
            {
                throw new BusinessException("请选择开始时间/结束时间");
            }
            return ReportBigDA.ComplaintStatisticsList(filter);
        }

        public QueryResult<ProductVendorStatisticsVO> ProductVendorStatisticsList(QF_Report filter)
        {
            return ReportBigDA.ProductVendorStatisticsList(filter);
        }

        public List<ReportChartVO> ActivityStatisticsChart(QF_Report filter)
        {
            if (!filter.DateTimeType.HasValue)
            {
                throw new BusinessException("请选择统计模式");
            }
            if (!filter.StartTime.HasValue || !filter.EndTime.HasValue)
            {
                throw new BusinessException("请选择开始时间/结束时间");
            }
            List<ReportChartVO> reportChartVOs = ReportBigDA.ActivityStatisticsChart(filter);
            return this.BindDateTimeFullKey(filter, reportChartVOs);
        }

        public List<ActivityStatisticsVO> ActivityStatisticsList(QF_Report filter)
        {
            if (!filter.DateTimeType.HasValue)
            {
                throw new BusinessException("请选择统计模式");
            }
            if (!filter.StartTime.HasValue || !filter.EndTime.HasValue)
            {
                throw new BusinessException("请选择开始时间/结束时间");
            }
            return ReportBigDA.ActivityStatisticsList(filter);
        }

        public List<ReportChartVO> SubjectStatisticsChart(QF_Report filter)
        {
            return ReportBigDA.SubjectStatisticsChart(filter);
        }

        public List<ReportChartVO> ArticleInterviewStatisticsChart(QF_Report filter)
        {
            if (!filter.DateTimeType.HasValue)
            {
                throw new BusinessException("请选择统计模式");
            }
            if (!filter.StartTime.HasValue || !filter.EndTime.HasValue)
            {
                throw new BusinessException("请选择开始时间/结束时间");
            }
            List<ReportChartVO> reportChartVOs = ReportBigDA.ArticleInterviewStatisticsChart(filter);
            return this.BindDateTimeFullKey(filter, reportChartVOs);
        }

        public QueryResult<ExternalDepartmentStatistics> ExternalDepartmentStatisticsList(QF_Report filter)
        {
            return ReportBigDA.ExternalDepartmentStatisticsList(filter);
        }

        private List<ReportChartVO> BindDateTimeFullKey(QF_Report filter, List<ReportChartVO> list)
        {
            List<DateTime> dateTimes = new List<DateTime>();
            switch (filter.DateTimeType.Value)
            {
                case DateTimeTypeEnum.Month:
                    dateTimes = this.GetFullMonthKeyList();
                    break;
                case DateTimeTypeEnum.Year:
                    dateTimes = this.GetFullYearKeyList();
                    break;
            }
            dateTimes.ForEach(p =>
            {
                var hasExists = list.Exists(item => DateTime.Parse(item.Key) == p);
                if (!hasExists)
                {
                    list.Add(new ReportChartVO()
                    {
                        Key = p.ToString("yyyy-MM-dd"),
                        Value = 0
                    });
                }
            });
            list = list.OrderBy(p => DateTime.Parse(p.Key)).ToList();
            list.ForEach(item =>
            {
                switch (filter.DateTimeType.Value)
                {
                    case DateTimeTypeEnum.Week:
                        item.Key = DateTime.Parse(item.Key).ToString("MM-dd");
                        break;
                    case DateTimeTypeEnum.Month:
                        item.Key = DateTime.Parse(item.Key).ToString("MM月");
                        break;
                    case DateTimeTypeEnum.Year:
                        item.Key = DateTime.Parse(item.Key).ToString("yyyy年");
                        break;
                    default:
                        item.Key = DateTime.Parse(item.Key).ToString("MM-dd");
                        break;
                }
            });
            return list;
        }

        private List<DateTime> GetFullMonthKeyList()
        {
            List<DateTime> list = new List<DateTime>();
            for (var startMonth = 1; startMonth <= 12; startMonth++)
            {
                list.Add(new DateTime(DateTime.Now.Year, startMonth, 1));
            }
            return list;
        }

        private List<DateTime> GetFullYearKeyList()
        {
            List<DateTime> list = new List<DateTime>();
            for (var startYear = 2019; startYear <= DateTime.Now.Year; startYear++)
            {
                list.Add(new DateTime(startYear, 1, 1));
            }
            return list;
        }
    }
}
