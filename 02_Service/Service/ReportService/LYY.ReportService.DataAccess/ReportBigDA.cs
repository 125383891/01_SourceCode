using LYY.Common.Entity;
using LYY.Report.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YZ.Utility;
using YZ.Utility.DataAccess;

namespace LYY.ReportService.DataAccess
{
    public class ReportBigDA
    {

        public static List<ReportChartVO> ComplaintStatisticsChart(QF_Report filter)
        {
            var cmd = new DataCommand("Report.ComplaintStatisticsChart");
            string formatKey = GetFormatKey(filter.DateTimeType.Value);
            cmd.SetParameter("@FormatDate", DbType.String, formatKey);
            cmd.QuerySetCondition("create_time", ConditionOperation.MoreThanEqual, DbType.DateTime, filter.StartTime);
            cmd.QuerySetCondition("create_time", ConditionOperation.LessThan, DbType.DateTime, handleEndTime(filter.EndTime));
            cmd.QuerySetCondition("department_id", ConditionOperation.Equal, DbType.Int32, filter.DepartmentId);
            cmd.QuerySetCondition("is_delete", ConditionOperation.Equal, DbType.Int32, DeletedEnums.Actived);
            cmd.CommandText = cmd.CommandText.Replace("#STRWHERE#", cmd.QueryConditionString);
            return cmd.ExecuteEntityList<ReportChartVO>();
        }

        public static List<ComplaintStatisticsVO> ComplaintStatisticsList(QF_Report filter)
        {
            var cmd = new DataCommand("Report.ComplaintStatisticsList");
            cmd.SetParameter("@StartTime", DbType.DateTime, filter.StartTime);
            cmd.SetParameter("@EndTime", DbType.DateTime, handleEndTime(filter.EndTime));
            cmd.QuerySetCondition("ts.create_time", ConditionOperation.MoreThanEqual, DbType.DateTime, filter.StartTime);
            cmd.QuerySetCondition("ts.create_time", ConditionOperation.LessThan, DbType.DateTime, handleEndTime(filter.EndTime));
            cmd.QuerySetCondition("ts.department_id", ConditionOperation.Equal, DbType.Int32, filter.DepartmentId);
            cmd.QuerySetCondition("ts.is_delete", ConditionOperation.Equal, DbType.Int32, DeletedEnums.Actived);
            cmd.CommandText = cmd.CommandText.Replace("#STRWHERE#", cmd.QueryConditionString);
            return cmd.ExecuteEntityList<ComplaintStatisticsVO>();
        }


        public static List<ReportChartVO> ActivityStatisticsChart(QF_Report filter)
        {
            var cmd = new DataCommand("Report.ActivityStatisticsChart");
            string formatKey = GetFormatKey(filter.DateTimeType.Value);
            cmd.SetParameter("@FormatDate", DbType.String, formatKey);
            cmd.QuerySetCondition("tud.create_time", ConditionOperation.MoreThanEqual, DbType.DateTime, filter.StartTime);
            cmd.QuerySetCondition("tud.create_time", ConditionOperation.LessThan, DbType.DateTime, handleEndTime(filter.EndTime));
            if (filter.DepartmentId.HasValue)
            {
                cmd.ReplaceAndSetSQLTag("TagFilterDepartment");
                cmd.QuerySetCondition("tud2.department_id", ConditionOperation.Equal, DbType.Int32, filter.DepartmentId);
            }
            else
            {
                cmd.ReplaceSQLTag("TagFilterDepartment", string.Empty);
            }

            if (filter.DateTimeType.Value==DateTimeTypeEnum.Week)
            {
                cmd.CommandText= cmd.CommandText.Replace("#filterDistinct#", string.Empty);
            }
            else
            {
                cmd.CommandText = cmd.CommandText.Replace("#filterDistinct#", "distinct ");
            }
            cmd.CommandText = cmd.CommandText.Replace("#STRWHERE#", cmd.QueryConditionString);
            return cmd.ExecuteEntityList<ReportChartVO>();
        }

        public static List<ActivityStatisticsVO> ActivityStatisticsList(QF_Report filter)
        {
            var cmd = new DataCommand("Report.ActivityStatisticsList");
            cmd.SetParameter("@StartTime", DbType.DateTime, filter.StartTime);
            cmd.SetParameter("@EndTime", DbType.DateTime, handleEndTime(filter.EndTime));
            cmd.QuerySetCondition("tud2.create_time", ConditionOperation.MoreThanEqual, DbType.DateTime, filter.StartTime);
            cmd.QuerySetCondition("tud2.create_time", ConditionOperation.LessThan, DbType.DateTime, handleEndTime(filter.EndTime));
            cmd.QuerySetCondition("td.id", ConditionOperation.Equal, DbType.Int32, filter.DepartmentId);
            if (filter.DateTimeType != null && filter.DateTimeType.Value == DateTimeTypeEnum.Week)
            {
                cmd.CommandText = cmd.CommandText.Replace("#filterDistinct#", string.Empty);
            }
            else
            {
                cmd.CommandText = cmd.CommandText.Replace("#filterDistinct#", "distinct ");
            }
            cmd.CommandText = cmd.CommandText.Replace("#STRWHERE#", cmd.QueryConditionString);
            return cmd.ExecuteEntityList<ActivityStatisticsVO>();
        }


        public static List<ReportChartVO> SubjectStatisticsChart(QF_Report filter)
        {
            var cmd = new DataCommand("Report.SubjectStatisticsChart");
            cmd.QuerySetCondition("ts.create_time", ConditionOperation.MoreThanEqual, DbType.DateTime, filter.StartTime);
            cmd.QuerySetCondition("ts.create_time", ConditionOperation.LessThan, DbType.DateTime, handleEndTime(filter.EndTime));
            cmd.QuerySetCondition("ts.department_id", ConditionOperation.Equal, DbType.Int32, filter.DepartmentId);
            cmd.QuerySetCondition("ts.is_delete", ConditionOperation.Equal, DbType.Int32, DeletedEnums.Actived);
            cmd.CommandText = cmd.CommandText.Replace("#STRWHERE#", cmd.QueryConditionString);
            return cmd.ExecuteEntityList<ReportChartVO>();
        }

        public static List<ReportChartVO> ArticleInterviewStatisticsChart(QF_Report filter)
        {
            var cmd = new DataCommand("Report.ArticleInterviewStatisticsChart");
            string formatKey = GetFormatKey(filter.DateTimeType.Value);
            cmd.SetParameter("@FormatDate", DbType.String, formatKey);
            cmd.QuerySetCondition("ta.create_time", ConditionOperation.MoreThanEqual, DbType.DateTime, filter.StartTime);
            cmd.QuerySetCondition("ta.create_time", ConditionOperation.LessThan, DbType.DateTime, handleEndTime(filter.EndTime));
            cmd.QuerySetCondition("ta.apptype", ConditionOperation.Equal, DbType.Int32, filter.ArticleApplicationType);
            cmd.QuerySetCondition("ta.id", ConditionOperation.Equal, DbType.Int32, filter.ArticleId);
            cmd.QuerySetCondition("ta.is_delete", ConditionOperation.Equal, DbType.Int32, DeletedEnums.Actived);
            cmd.CommandText = cmd.CommandText.Replace("#STRWHERE#", cmd.QueryConditionString);
            return cmd.ExecuteEntityList<ReportChartVO>();
        }

        public static QueryResult<ProductVendorStatisticsVO> ProductVendorStatisticsList(QF_Report filter)
        {
            var cmd = new DataCommand("Report.ProductVendorStatisticsList");
            if (filter.StatisticsObject == StatisticsObject.Product)
            {
                cmd.ReplaceAndSetSQLTag("TagProductFragment");
                cmd.ReplaceSQLTag("TagVendorFragment", string.Empty);
            }
            else
            {
                cmd.ReplaceAndSetSQLTag("TagVendorFragment");
                cmd.ReplaceSQLTag("TagProductFragment", string.Empty);
            }
            cmd.QuerySetCondition("ts.create_time", ConditionOperation.MoreThanEqual, DbType.DateTime, filter.StartTime);
            cmd.QuerySetCondition("ts.create_time", ConditionOperation.LessThan, DbType.DateTime, handleEndTime(handleEndTime(filter.EndTime)));
            cmd.QuerySetCondition("ts.department_id", ConditionOperation.Equal, DbType.Int32, filter.DepartmentId);
            cmd.QuerySetCondition("ts.is_delete", ConditionOperation.Equal, DbType.Int32, DeletedEnums.Actived);
            cmd.CommandText = cmd.CommandText.Replace("#STRWHERE#", cmd.QueryConditionString);
            return cmd.Query<ProductVendorStatisticsVO>(filter, "count(1) DESC", null, true);
        }

        public static QueryResult<ExternalDepartmentStatistics> ExternalDepartmentStatisticsList(QF_Report filter)
        {
            var externalTagValue = int.Parse(AppSettingManager.GetSetting("ExternalDepartmentTag", "ExternalTag"));
            var cmd = new DataCommand("Report.ExternalDepartmentStatisticsList");
            cmd.SetParameter("@TagValue", DbType.Int32, externalTagValue);
            cmd.QuerySetCondition("a.create_time", ConditionOperation.MoreThanEqual, DbType.DateTime, filter.StartTime);
            cmd.QuerySetCondition("a.create_time", ConditionOperation.LessThan, DbType.DateTime, handleEndTime(handleEndTime(filter.EndTime)));
            cmd.QuerySetCondition("c.department_id", ConditionOperation.Equal, DbType.Int32, filter.DepartmentId);
            cmd.QuerySetCondition("c.is_delete", ConditionOperation.Equal, DbType.Int32, DeletedEnums.Actived);
            cmd.CommandText = cmd.CommandText.Replace("#STRWHERE#", cmd.QueryConditionString);
            return cmd.Query<ExternalDepartmentStatistics>(filter, "a.create_time DESC", null, true);
        }

        private static DateTime? handleEndTime(DateTime? endTime)
        {
            if (endTime.HasValue)
            {
                return endTime.Value.AddDays(1);
            }
            else
            {
                return null;
            }
        }

        private static string GetFormatKey(DateTimeTypeEnum dateTimeType)
        {
            switch (dateTimeType)
            {
                case DateTimeTypeEnum.Week: return "%Y-%m-%d";
                case DateTimeTypeEnum.Month: return "%Y-%m-1";
                case DateTimeTypeEnum.Year: return "%Y-1-1";
                default: return "%Y-%m-%d";
            }
        }
    }
}
