using LYY.MockTest.Entity.Entity;
using LYY.MockTest.Entity.Enums;
using LYY.MockTest.Entity.ViewModel;
using LYY.Subject.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YZ.Utility;
using YZ.Utility.DataAccess;

namespace LYY.MockTestService.DataAccess
{
    public class MockTestDA
    {
        #region 题库管理

        /// <summary>
        /// 问题查询
        /// </summary>
        /// <param name="filter">id</param>
        /// <returns></returns>
        public static QueryResult<QusetionView> SearchQuestionPageLists(QusetionFilter filter)
        {
            var cmd = new DataCommand("MockTest.SearchQuestionPageList");
            SetCondition(cmd, filter);
            return cmd.Query<QusetionView>(filter, "d.NAME ,q.order_num ", null, true);
        }
        /// <summary>
        /// 设置条件
        /// </summary>
        /// <param name="command"></param>
        /// <param name="filter"></param>
        private static void SetCondition(DataCommand command, QusetionFilter filter)
        {
            if (filter.mode.HasValue)
            {
                command.QuerySetCondition("q.mode", ConditionOperation.Equal, DbType.Int32, filter.mode.Value);
            }
            if (!string.IsNullOrEmpty(filter.document_name))
            {
                command.QuerySetCondition("d.name", ConditionOperation.Like, DbType.String, filter.document_name);
            }
            if (!string.IsNullOrEmpty(filter.content))
            {
                command.QuerySetCondition("q.content", ConditionOperation.Like, DbType.String, filter.content);
            }
            if (filter.usertag.HasValue)
            {
                command.QuerySetCondition("q.user_tag", ConditionOperation.Equal, DbType.Int32, filter.usertag);
            }
            command.CommandText = command.CommandText.Replace("#STRWHERE#", command.QueryConditionString);
        }

        /// <summary>
        /// 读取问题
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static QusetionView LoadQustion(int id)
        {
            var cmd = new DataCommand("MockTest.LoadQustion");
            cmd.SetParameter("@Id", DbType.Int32, id);
            var result = cmd.ExecuteEntity<QusetionView>();
            return result;
        }
        /// <summary>
        /// 查询答案
        /// </summary>
        /// <param name="question_id"></param>
        /// <returns></returns>
        public static List<AnswerView> SearchAnswerLists(int question_id)
        {
            var cmd = new DataCommand("MockTest.SearchAnswerList");
            cmd.SetParameter("@question_id", DbType.Int32, question_id);
            return cmd.ExecuteEntityList<AnswerView>("#STRWHERE#");
            //return new List<AnswerView>();
        }

        /// <summary>
        /// 修改题目
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static int UpdateQustion(QusetionView entity)
        {
            DataCommand cmd = new DataCommand("MockTest.UpdateQustion");
            cmd.SetParameter(entity);
            int result = cmd.ExecuteNonQuery();
            return result;
        }
        /// <summary>
        /// 新增题目
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static int InsertQustion(QusetionView entity)
        {
            DataCommand cmd = new DataCommand("MockTest.InsertQustion");
            cmd.SetParameter(entity);
            int result = cmd.ExecuteScalar<int>();
            return result;
        }

        /// <summary>
        /// 修改题目
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static int UpdateAnswer(AnswerView entity)
        {
            DataCommand cmd = new DataCommand("MockTest.UpdateAnswer");
            cmd.SetParameter(entity);
            int result = cmd.ExecuteNonQuery();
            return result;
        }
        /// <summary>
        /// 新增题目
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static int InsertAnswer(AnswerView entity)
        {
            DataCommand cmd = new DataCommand("MockTest.InsertAnswer");
            cmd.SetParameter(entity);
            int result = cmd.ExecuteScalar<int>();
            return result;
        }
        /// <summary>
        /// 文档 序号验证
        /// </summary>
        /// <param name="documentname"></param>
        /// <param name="orderNum"></param>
        /// <returns></returns>
        public static DocumentValid LoadDocumentValid(string documentname, int orderNum)
        {
            DataCommand cmd = new DataCommand("MockTest.LoadDocumentValid");
            cmd.SetParameter("@order_num", DbType.Int32, orderNum);
            cmd.SetParameter("@name", DbType.String, documentname);
            return cmd.ExecuteEntity<DocumentValid>();
        }

        public static int DeleteQuestion(int id)
        {
            DataCommand cmd = new DataCommand("MockTest.DeleteQuestion");
            cmd.SetParameter("@id", DbType.Int32, id);
            return cmd.ExecuteNonQuery();
        }
        public static DocumentValid LoadUserTagValid(string name)
        {
            DataCommand cmd = new DataCommand("MockTest.LoadUserTag");
            cmd.SetParameter("@name", DbType.String, name);
            return cmd.ExecuteEntity<DocumentValid>();
        }

        #endregion

        #region 考试配置管理
        /// <summary>
        /// 考试配置分页查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static QueryResult<ExamView> SearchExamPageLists(ExamFilter filter)
        {
            var cmd = new DataCommand("MockTest.SearchExamPageList");
            SetCondition(cmd, filter);
            return cmd.Query<ExamView>(filter, "begin_time desc ", null, true);
        }
        /// <summary>
        /// 设置条件
        /// </summary>
        /// <param name="command"></param>
        /// <param name="filter"></param>
        private static void SetCondition(DataCommand command, ExamFilter filter)
        {
            command.CommandText = command.CommandText.Replace("#STRWHERE#", command.QueryConditionString);
        }

        /// <summary>
        /// 读取考试配置
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ExamView LoadExam(int id)
        {
            var cmd = new DataCommand("MockTest.LoadExam");
            cmd.SetParameter("@Id", DbType.Int32, id);
            var result = cmd.ExecuteEntity<ExamView>();
            return result;
        }

        /// <summary>
        /// 查询关联文档
        /// </summary>
        /// <param name="exam_id"></param>
        /// <returns></returns>
        public static List<ExamDocumentView> SearchExamDocumentLists(int exam_id)
        {
            var cmd = new DataCommand("MockTest.LoadExamDocument");
            cmd.SetParameter("@exam_id", DbType.Int32, exam_id);
            return cmd.ExecuteEntityList<ExamDocumentView>("#STRWHERE#");
        }

        /// <summary>
        /// 修改考试配置
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static int UpdateExam(ExamView entity)
        {
            DataCommand cmd = new DataCommand("MockTest.UpdateExam");
            cmd.SetParameter(entity);
            int result = cmd.ExecuteNonQuery();
            return result;
        }

        /// <summary>
        /// 新增考试配置
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static int InsertExam(ExamView entity)
        {
            DataCommand cmd = new DataCommand("MockTest.InsertExam");
            cmd.SetParameter(entity);
            int result = cmd.ExecuteScalar<int>();
            return result;
        }
        /// <summary>
        /// 新增考试配置文档关联表
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static int InsertExamDocument(ExamDocumentView entity)
        {
            DataCommand cmd = new DataCommand("MockTest.InsertExamDocument");
            cmd.SetParameter(entity);
            int result = cmd.ExecuteScalar<int>();
            return result;
        }

        public static int DeleteExamDocument(int exam_id)
        {
            DataCommand cmd = new DataCommand("MockTest.DeleteExamDocument");
            cmd.SetParameter("@exam_id", DbType.String, exam_id);
            int result = cmd.ExecuteScalar<int>();
            return result;
        }
        #endregion
        #region 测验历史记录

        /// <summary>
        /// 测验历史记录分页查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static QueryResult<ExamTestView> SearchExamTestPageLists(ExamTestFilter filter)
        {
            var cmd = new DataCommand("MockTest.SearchExamTestPageLists");
            SetCondition(cmd, filter);
            //排序
            return cmd.Query<ExamTestView>(filter, filter.sortColumn + " desc ", null, true);
        }
        /// <summary>
        /// 查询历史记录 不分页
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static List<ExamTestView> ExportExamTestList(ExamTestFilter filter)
        {
            var cmd = new DataCommand("MockTest.ExportExamTestList");
            SetCondition(cmd, filter);
            return cmd.ExecuteEntityList<ExamTestView>();
        }


        /// <summary>
        /// 设置条件
        /// </summary>
        /// <param name="command"></param>
        /// <param name="filter"></param>
        private static void SetCondition(DataCommand command, ExamTestFilter filter)
        {
            //测验类型
            if (filter.mode.HasValue)
            {
                command.QuerySetCondition("t.mode", ConditionOperation.Equal, DbType.Int32, filter.mode.Value);
            }
            //针对文档
            if (!string.IsNullOrEmpty(filter.document_name))
            {
                command.QuerySetCondition("d.name", ConditionOperation.Like, DbType.String, filter.document_name);
            }
            //针对模拟考试
            if (filter.examid.HasValue)
            {
                command.QuerySetCondition("t.exam_id", ConditionOperation.Equal, DbType.Int32, filter.examid.Value);
            }
            //开始日期
            if (filter.StartTime.HasValue)
            {
                command.QuerySetCondition("t.begin_time", ConditionOperation.MoreThanEqual, DbType.DateTime, filter.StartTime);
            }
            //结束日期
            if (filter.EndTime.HasValue)
            {
                command.QuerySetCondition("t.begin_time", ConditionOperation.LessThan, DbType.DateTime, filter.EndTime.Value.AddDays(1));
            }
            command.ReplaceAndSetSQLTag("TagOrder" + filter.sortColumn, filter.sortColumn);
            command.CommandText = command.CommandText.Replace("#STRWHERE#", command.QueryConditionString);
        }

        /// <summary>
        /// 查询模拟考试  下拉数据源
        /// </summary>
        /// <returns></returns>
        public static List<t_exam> SearchExamLists()
        {
            var cmd = new DataCommand("MockTest.SearchExamLists");
            return cmd.ExecuteEntityList<t_exam>("#STRWHERE#");
        }
        #endregion
        #region 测验统计

        /// <summary>
        /// 查询测验统计
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static List<TestStatisticsView> SearchTestStatisticsLists(TestStatisticsFilter filter)
        {
            var cmd = new DataCommand("MockTest.SearchTestStatisticsLists");
            SetCondition(cmd, filter);
            return cmd.ExecuteEntityList<TestStatisticsView>();
        }

        /// <summary>
        /// 设置条件
        /// </summary>
        /// <param name="command"></param>
        /// <param name="filter"></param>
        private static void SetCondition(DataCommand command, TestStatisticsFilter filter)
        {
            //测验类型
            if (filter.mode.HasValue)
            {
                command.QuerySetCondition("t.mode", ConditionOperation.Equal, DbType.Int32, filter.mode.Value);
            }
            //统计模式
            if (filter.statisticsMode == 1)//1:个人了联系
            {
                //个人练习
                command.ReplaceAndSetSQLTag("TagFilterdocument_id", "");
                //针对文档  个人练习才有文档
                if (!string.IsNullOrEmpty(filter.document_name))
                {
                    command.QuerySetCondition("d.name", ConditionOperation.Like, DbType.String, filter.document_name);
                }
            }
            else
            {
                //模拟考试
                command.ReplaceAndSetSQLTag("TagFilterexam_id", "");
            }
            if (filter.statisticsObj == 1)
            {
                //单位
                command.ReplaceAndSetSQLTag("TagGroupDpName", "");
                command.ReplaceAndSetSQLTag("TagColumnDpName", "");

            }
            else
            {
                //个人
                command.ReplaceAndSetSQLTag("TagGroupUName", "");
                command.ReplaceAndSetSQLTag("TagColumnUName", "");
            }

            //针对模拟考试
            if (filter.examid.HasValue)
            {
                command.QuerySetCondition("t.exam_id", ConditionOperation.Equal, DbType.Int32, filter.examid.Value);
            }
            //开始日期
            if (filter.StartTime.HasValue)
            {
                command.QuerySetCondition("t.begin_time", ConditionOperation.MoreThanEqual, DbType.DateTime, filter.StartTime);
            }
            //结束日期
            if (filter.EndTime.HasValue)
            {
                command.QuerySetCondition("t.begin_time", ConditionOperation.LessThan, DbType.DateTime, filter.EndTime.Value.AddDays(1));
            }
            command.ReplaceAndSetSQLTag("TagOrder" + filter.sortColumn, filter.sortColumn);
            command.CommandText = command.CommandText.Replace("#STRWHERE#", command.QueryConditionString);
        }
        #endregion

        /// <summary>
        /// 根据名称查询文档
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static t_document LoadDocumentByName(string name, UserTagEnums? user_tag)
        {
            var cmd = new DataCommand("MockTest.LoadDocumentByName");
            cmd.SetParameter("@name", DbType.String, name);
            cmd.SetParameter("@user_tag", DbType.Int32, user_tag);
            var result = cmd.ExecuteEntity<t_document>();
            return result;
        }

        #region 段位参数设置
        public static QueryResult<RankSettingVO> SearchRankSettingPageLists(RankSettingFilter filter)
        {
            var cmd = new DataCommand("MockTest.SearchRankSettingLists");
            return cmd.Query<RankSettingVO>(filter, "drank_level asc", null, true);
        }

        public static RankSettingVO LoadRankSettingDetail(int id)
        {
            var cmd = new DataCommand("MockTest.LoadRankSettingDetail");
            cmd.SetParameter("@Id", DbType.Int32, id);
            var result = cmd.ExecuteEntity<RankSettingVO>();
            return result;
        }

        public static int UpdateRankSetting(RankSettingVO entity)
        {
            var cmd = new DataCommand("MockTest.UpdateRankSetting");
            cmd.SetParameter(entity);
            int result = cmd.ExecuteNonQuery();
            return result;
        }
        #endregion


        #region 个人积分
        public static List<IndividualPointsVO> SearchIndividualPointsLists(IndividualPointsFilter filter)
        {
            switch (filter.statisticsMode)
            {
                case 1:
                    var cmd1 = new DataCommand("MockTest.IndividualPoints1");
                    cmd1.SetParameter("@userTag", DbType.Int32, filter.UserTag);
                    return cmd1.ExecuteEntityList<IndividualPointsVO>();
                case 2:
                    var cmd2 = new DataCommand("MockTest.IndividualPoints2");
                    cmd2.SetParameter("@userTag", DbType.Int32, filter.UserTag);
                    cmd2.SetParameter("@year", DbType.String, filter.statisticsYear);
                    return cmd2.ExecuteEntityList<IndividualPointsVO>();
                case 3:
                    var cmd3 = new DataCommand("MockTest.IndividualPoints3");
                    cmd3.SetParameter("@userTag", DbType.Int32, filter.UserTag);
                    cmd3.SetParameter("@year", DbType.String, filter.statisticsYear);
                    return cmd3.ExecuteEntityList<IndividualPointsVO>();
                default:
                    break;
            }
            return new List<IndividualPointsVO>();
        }

        #endregion
        #region 团队积分

        public static List<TeamPointsVO> SearchTeamPointsLists(TeamPointsFilter filter)
        {

            switch (filter.statisticsMode)
            {
                case 1:
                    var cmd1 = new DataCommand("MockTest.TeamPoints1");
                    cmd1.SetParameter("@userTag", DbType.Int32, filter.UserTag);
                    cmd1.SetParameter("@year", DbType.String, filter.statisticsYear);
                    return cmd1.ExecuteEntityList<TeamPointsVO>();
                case 2:
                    var cmd2 = new DataCommand("MockTest.TeamPoints2");
                    cmd2.SetParameter("@userTag", DbType.Int32, filter.UserTag);
                    cmd2.SetParameter("@year", DbType.String, filter.statisticsYear);
                    return cmd2.ExecuteEntityList<TeamPointsVO>();
                default:
                    break;
            }
            return new List<TeamPointsVO>();
        }
        #endregion
    }
}
