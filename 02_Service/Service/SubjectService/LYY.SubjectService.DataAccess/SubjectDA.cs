using LYY.Common.Entity;
using LYY.Subject.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YZ.Utility;
using YZ.Utility.DataAccess;

namespace LYY.SubjectService.DataAccess
{
    public class SubjectDA
    {
        /// <summary>
        /// 根据Id获取主题详情
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>主题详情</returns>
        public static SubjectVO GetById(int id)
        {
            var cmd = new DataCommand("Subject.GetById");
            cmd.SetParameter("@Id", DbType.Int32, id);
            return cmd.ExecuteEntity<SubjectVO>();
        }

        /// <summary>
        /// 查询主题列表
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static QueryResult<SubjectVO> SearchPageList(QF_Subject filter)
        {
            var cmd = new DataCommand("Subject.SearchPageList");
            cmd.SetParameter("@keyWords", DbType.String, filter.Keyword + "");
            cmd.SetParameter("@beginDate", DbType.DateTime, filter.StartTime.HasValue ? filter.StartTime.Value.ToString("yyyy-MM-dd") : null);
            cmd.SetParameter("@endDate", DbType.DateTime, filter.EndTime.HasValue ? filter.EndTime.Value.ToString("yyyy-MM-dd") : null);
            //cmd.SetParameter("@beginDate", DbType.DateTime, filter.StartTime);
            //cmd.SetParameter("@endDate", DbType.DateTime, filter.EndTime);
            cmd.SetParameter("@catId", DbType.Int32, filter.CatId);
            cmd.SetParameter("@departmentId", DbType.Int32, filter.DepartmentId);
            cmd.SetParameter("@vendorName", DbType.String, filter.VendorName);
            //cmd.SetParameter("@StartNum", DbType.Int32, filter.sta);
            //cmd.SetParameter("@PageSize", DbType.Int32, id);
            //SetCondition(cmd, filter, "Search");
            var result = cmd.Query<SubjectVO>(filter, "MaxCommentTime DESC, CreateTime DESC", null, true);
            return result;
        }

        /// <summary>
        /// 查询回复列表
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static QueryResult<SubjectReplyEntity> SearchReplyPageList(QF_Subject filter)
        {
            var cmd = new DataCommand("Subject.SearchReplyPageList");
            cmd.SetParameter("@Id", DbType.Int32, filter.SubjectId);
            return cmd.Query<SubjectReplyEntity>(filter, "id ASC", null, true);
        }

        /// <summary>
        /// 修改热度
        /// </summary>
        /// <param name="viewCount"></param>
        /// <param name="subjectId"></param>
        public static void UpdateViewCount(SubjectModel entity)
        {
            var cmd = new DataCommand("Subject.UpdateViewCount");
            cmd.SetParameter(entity);

            //cmd.SetParameter("@Id", DbType.Int32, subjectId);
            //cmd.SetParameter("@View", DbType.Int32, viewCount);
            cmd.ExecuteNonQuery();
        }

        public static SubjectModel LoadSubject(int id)
        {
            var cmd = new DataCommand("Subject.LoadSubject");
            cmd.SetParameter("@Id", DbType.Int32, id);
            return cmd.ExecuteEntity<SubjectModel>();
        }

        /// <summary>
        /// 修改重要置顶
        /// </summary>
        /// <param name="weight"></param>
        /// <param name="subjectId"></param>
        public static void UpdateWeight(int? weight, int subjectId)
        {
            var cmd = new DataCommand("Subject.UpdateWeight");
            cmd.SetParameter("@Id", DbType.Int32, subjectId);
            cmd.SetParameter("@weight", DbType.Int32, weight);
            cmd.ExecuteNonQuery();
        }
        /// <summary>
        /// 修改重要置顶
        /// </summary>
        /// <param name="weight"></param>
        /// <param name="subjectId"></param>
        public static int ValidWeight(int? weight, int subjectId)
        {
            var cmd = new DataCommand("Subject.ValidWeight");
            cmd.SetParameter("@Id", DbType.Int32, subjectId);
            cmd.SetParameter("@weight", DbType.Int32, weight);
            return cmd.ExecuteScalar<int>();
        }

        /// <summary>
        /// 查询主题导出列表
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static List<SearchExportVO> SearchExportList(QF_Subject filter)
        {
            var cmd = new DataCommand("Subject.SearchExportList");
            SetCondition(cmd, filter, "Export");
            return cmd.ExecuteEntityList<SearchExportVO>();
        }

        private static void SetCondition(DataCommand command, QF_Subject filter, string type)
        {
            if (!string.IsNullOrEmpty(filter.Keyword))
            {
                command.ReplaceAndSetSQLTag("TagFilterKeyword", filter.Keyword);
                command.ReplaceSQLTag("TagFilterKeyword", string.Empty);
            }
            else
            {
                command.ReplaceSQLTag("TagFilterKeyword", string.Empty);
            }
            command.QuerySetCondition("ts.create_time", ConditionOperation.MoreThanEqual, DbType.DateTime, filter.StartTime);
            if (filter.EndTime.HasValue)
            {
                command.QuerySetCondition("ts.create_time", ConditionOperation.LessThan, DbType.DateTime, filter.EndTime.Value.AddDays(1));
            }
            command.QuerySetCondition("tc.id", ConditionOperation.Equal, DbType.Int32, filter.CatId);
            command.QuerySetCondition("ts.department_id", ConditionOperation.Equal, DbType.Int32, filter.DepartmentId);
            command.QuerySetCondition("ts.is_delete", ConditionOperation.Equal, DbType.Int32, DeletedEnums.Actived);
            if (type == "Search")
            {
                command.QuerySetCondition("d.name", ConditionOperation.Like, DbType.String, filter.VendorName);
            }
            else
            {
                command.QuerySetCondition("vn.name", ConditionOperation.Like, DbType.String, filter.VendorName);
            }
            command.CommandText = command.CommandText.Replace("#STRWHERE#", command.QueryConditionString);
        }



        /// <summary>
        /// 获取主题参与活动关系表
        /// </summary>
        /// <param name="subject_id"></param>
        /// <returns></returns>
        public static SubjectPlanScoreExtra LoadSubjectPlan(int subject_id)
        {
            var cmd = new DataCommand("Subject.LoadSubjectPlan");
            cmd.SetParameter("@subject_id", DbType.Int32, subject_id);
            return cmd.ExecuteEntity<SubjectPlanScoreExtra>();
        }
        /// <summary>
        /// 更新主题参与活动关系表质量达人分
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static int UpdateSubjectPlanScoreExtra(SubjectPlanScoreExtra entity)
        {
            var cmd = new DataCommand("Subject.UpdateSubjectPlanByScoreExtra");
            cmd.SetParameter(entity);
            return cmd.ExecuteNonQuery();
        }


        #region 排行榜导出

        /// <summary>
        /// 查询主题导出列表
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static List<SearchExportRankingVO> SearchExportRanking(QF_Subject filter)
        {
            var cmd = new DataCommand("Subject.SearchExportRanking");
            cmd.SetParameter("@startTime", DbType.String, filter.StartTime?.ToString("yyyy-MM-dd"));
            cmd.SetParameter("@endTime", DbType.String, filter.EndTime?.ToString("yyyy-MM-dd"));
            return cmd.ExecuteEntityList<SearchExportRankingVO>();
        }
        /// <summary>
        /// 查询主题导出列表
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static List<SearchExportAvgVO> SearchExportAvg(QF_Subject filter)
        {
            var cmd = new DataCommand("Subject.SearchExportAvg");
            cmd.SetParameter("@startTime", DbType.String, filter.StartTime?.ToString("yyyy-MM-dd"));
            cmd.SetParameter("@endTime", DbType.String, filter.EndTime?.ToString("yyyy-MM-dd"));
            return cmd.ExecuteEntityList<SearchExportAvgVO>();
        }
        #endregion

    }
}
