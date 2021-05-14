using LYY.Plan.Entity;
using System.Collections.Generic;
using System.Data;
using YZ.Utility;
using YZ.Utility.DataAccess;

namespace LYY.PlanService.DataAccess
{
    public class PlanDA
    {
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="plan">数据实体</param>
        public static void Insert(PlanEntity plan)
        {
            var cmd = new DataCommand("Plan.Insert");
            cmd.SetParameter(plan);
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="plan">数据实体</param>
        public static void Update(PlanEntity plan)
        {
            var cmd = new DataCommand("Plan.Update");
            cmd.SetParameter(plan);
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// 根据Id获取活动数据
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>返回活动数据</returns>
        public static PlanEntity GetById(int id)
        {
            var cmd = new DataCommand("Plan.GetById");
            cmd.SetParameter("@Id", DbType.Int32, id);
            return cmd.ExecuteEntity<PlanEntity>();
        }

        /// <summary>
        /// 查询活动列表
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static QueryResult<PlanEntity> SearchPageList(QueryFilter filter)
        {
            var cmd = new DataCommand("Plan.SearchPageList");
            return cmd.Query<PlanEntity>(filter, "begin_time DESC", null, true);
        }

        #region 单位积分
        /// <summary>
        /// 单位积分查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static QueryResult<Department_Plan_RankVO> SearchDepartmentPlanRankPageList(Department_Plan_RankFilter filter)
        {
            var cmd = new DataCommand("Plan.SearchDepartmentPlanRankPageList");
            SetCondition(cmd, filter);
            return cmd.Query<Department_Plan_RankVO>(filter, "tdpr.create_time desc", null, true);
        }

        /// <summary>
        /// 设置条件
        /// </summary>
        /// <param name="command"></param>
        /// <param name="filter"></param>
        private static void SetCondition(DataCommand command, Department_Plan_RankFilter filter)
        {
            if (filter.plan_id.HasValue)
            {
                command.QuerySetCondition("tdpr.plan_id", ConditionOperation.Equal, DbType.Int32, filter.plan_id.Value);
            }

            command.CommandText = command.CommandText.Replace("#STRWHERE#", command.QueryConditionString);
        }

        /// <summary>
        /// 单位、活动查询
        /// </summary>
        /// <param name="documentname"></param>
        /// <param name="orderNum"></param>
        /// <returns></returns>
        public static DepartmentPlanRankValid LoadPlanRankDeptAndPlanValid(string plan_name, string dept_name)
        {
            DataCommand cmd = new DataCommand("Plan.PlanRankDeptAndPlan");
            cmd.SetParameter("@plan_name", DbType.String, plan_name);
            cmd.SetParameter("@dept_name", DbType.String, dept_name);
            return cmd.ExecuteEntity<DepartmentPlanRankValid>();
        }
        /// <summary>
        /// 新增单位积分
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static int InsertDepartmentPlanRank(t_department_plan_rank entity)
        {
            DataCommand cmd = new DataCommand("Plan.InsertDepartmentPlanRank");
            cmd.SetParameter(entity);
            int result = cmd.ExecuteScalar<int>();
            return result;
        }
        #endregion

        #region 处理人积分
        /// <summary>
        /// 单位积分查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static QueryResult<User_Plan_RankVO> SearchUserPlanRankPageList(User_Plan_RankFilter filter)
        {
            var cmd = new DataCommand("Plan.SearchUserPlanRankPageList");
            SetCondition(cmd, filter);
            return cmd.Query<User_Plan_RankVO>(filter, "tupr.create_time desc ", null, true);
        }

        /// <summary>
        /// 设置条件
        /// </summary>
        /// <param name="command"></param>
        /// <param name="filter"></param>
        private static void SetCondition(DataCommand command, User_Plan_RankFilter filter)
        {
            if (filter.plan_id.HasValue)
            {
                command.QuerySetCondition("tupr.plan_id", ConditionOperation.Equal, DbType.Int32, filter.plan_id.Value);
            }
            command.CommandText = command.CommandText.Replace("#STRWHERE#", command.QueryConditionString);
        }


        /// <summary>
        /// 单位、活动查询
        /// </summary>
        /// <param name="documentname"></param>
        /// <param name="orderNum"></param>
        /// <returns></returns>
        public static UserPlanRankValid LoadPlanRankUserAndPlanValid(string plan_name, string user_name)
        {
            DataCommand cmd = new DataCommand("Plan.PlanRankUserAndPlan");
            cmd.SetParameter("@plan_name", DbType.String, plan_name);
            cmd.SetParameter("@user_name", DbType.String, user_name);
            return cmd.ExecuteEntity<UserPlanRankValid>();
        }


        /// <summary>
        /// 新增处理人积分
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static int InsertUserPlanRank(t_user_plan_rank entity)
        {
            DataCommand cmd = new DataCommand("Plan.InsertUserPlanRank");
            cmd.SetParameter(entity);
            int result = cmd.ExecuteScalar<int>();
            return result;
        }
        #endregion


        /// <summary>
        /// 查询活动列表
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static List<CommboxMode> SearchPlanList()
        {
            var cmd = new DataCommand("Plan.SearchPlanList");
            return cmd.ExecuteEntityList<CommboxMode>("#STRWHERE#");
        }
    }
}
