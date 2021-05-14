using LYY.Plan.Entity;
using LYY.PlanService.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YZ.Utility;

namespace LYY.PlanService.Service
{
    public class PlanBasisService
    {
        /// <summary>
        /// 查询活动列表
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public QueryResult<PlanVO> SearchPageList(QueryFilter filter)
        {
            return PlanDA.SearchPageList(filter).ToPlanVOPageList();
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="plan">数据实体</param>
        public void Insert(PlanVO plan)
        {
            PlanEntity entity = plan.ToPlanEntity();
            this.CheckPlanEntity(entity);
            this.HandlePlanDateTime(entity);
            PlanDA.Insert(entity);
        }



        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="plan">数据实体</param>
        public void Update(PlanVO plan)
        {
            PlanEntity entity = plan.ToPlanEntity();
            if (entity.Id == 0)
            {
                throw new BusinessException("活动id为空");
            }
            this.CheckPlanEntity(entity);

            this.HandlePlanDateTime(entity);
            PlanDA.Update(entity);
        }

        /// <summary>
        /// 根据Id获取活动数据
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>返回活动数据</returns>
        public PlanVO GetById(int id)
        {
            return PlanDA.GetById(id).ToPlanVo();
        }

        /// <summary>
        /// 处理活动时间，开始时间 00：00：00 开始 
        ///               结束时间 18：00：00 结束
        /// </summary>
        /// <param name="entity"></param>
        private void HandlePlanDateTime(PlanEntity entity)
        {
            if (entity.IsClose) entity.CloseTime = DateTime.Now; else entity.CloseTime = null;
            entity.BeginTime = new DateTime(entity.BeginTime.Year, entity.BeginTime.Month, entity.BeginTime.Day, 0, 0, 0);
            entity.EndTime = new DateTime(entity.EndTime.Year, entity.EndTime.Month, entity.EndTime.Day, 18, 0, 0);
        }

        private void CheckPlanEntity(PlanEntity planEntity)
        {
            if (planEntity == null)
            {
                throw new BusinessException("数据实体不能为空");
            }
            if (string.IsNullOrEmpty(planEntity.PlanName))
            {
                throw new BusinessException("请输入活动名称");
            }
            if (planEntity.BeginTime == DateTime.MinValue || planEntity.EndTime == DateTime.MinValue)
            {
                throw new BusinessException("请选择活动开始时间/结束时间");
            }
            if (planEntity.PlanName.Length > 200)
            {
                throw new BusinessException("活动名称最大长度为200");
            }
            if (!string.IsNullOrEmpty(planEntity.PlanDesc) && planEntity.PlanDesc.Length > 2000)
            {
                throw new BusinessException("活动说明最大长度为2000");
            }
        }

        #region 处理人积分
        /// <summary>
        /// 处理人积分查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public QueryResult<User_Plan_RankVO> SearchUserPlanRankPageList(User_Plan_RankFilter filter)
        {
            return PlanDA.SearchUserPlanRankPageList(filter);
        }
        /// <summary>
        /// 处理人积分导入 必填验证
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string UserEmptyValid(DataTable data)
        {
            string errorMsg = "";
            int rowIndex = 1;
            foreach (System.Data.DataRow item in data.Rows)
            {
                var row = item.ItemArray;
                for (int i = 0; i < row.Length; i++)
                {
                    if (string.IsNullOrEmpty(row[i] + ""))
                    {
                        errorMsg += string.Format("{0}行{1}列为空<br/>", rowIndex, i + 1);
                    }
                }
                var count = data.Select("所属活动='" + row[0] + "' and 名次='" + row[1] + "' and 用户姓名='" + row[2] + "'").Count();
                if (count > 1)
                {
                    errorMsg += string.Format("{0}行数据与其他行存在重复<br/>", rowIndex);
                }
                rowIndex++;
            }
            return errorMsg;
        }

        /// <summary>
        /// 用户 活动数据验证
        /// </summary>
        /// <param name="data"></param>
        /// <param name="deptList"></param>
        /// <param name="planList"></param>
        /// <returns></returns>
        public string UserPlanRankValid(DataTable data, List<UserPlanRankValid> list)
        {
            string errorMsg = "";
            int rowIndex = 1;
            foreach (System.Data.DataRow item in data.Rows)
            {
                var row = item.ItemArray;
                var plan_name = row[0] + "";
                var user_name = row[2] + "";
                var model = PlanDA.LoadPlanRankUserAndPlanValid(plan_name, user_name);
                if (model == null || (string.IsNullOrEmpty(model.user_id) && !model.plan_id.HasValue))
                {
                    errorMsg += string.Format("{0}行活动和单位不存在<br/>", rowIndex);
                }
                else if (string.IsNullOrEmpty(model.user_id))
                {
                    errorMsg += string.Format("{0}行单位不存在<br/>", rowIndex);
                }
                else if (!model.plan_id.HasValue)
                {
                    errorMsg += string.Format("{0}行活动不存在<br/>", rowIndex);
                }
                else
                {
                    list.Add(new UserPlanRankValid()
                    {
                        user_id = model.user_id,
                        user_name = user_name,
                        plan_id = model.plan_id,
                        plan_name = plan_name
                    });
                }
                rowIndex++;
            }
            return errorMsg;
        }

        /// <summary>
        /// 导入处理人积分
        /// </summary>
        /// <param name="data"></param>
        /// <param name="deptList"></param>
        /// <param name="planList"></param>
        public void ImportUserPlanRank(DataTable data, List<UserPlanRankValid> planList)
        {
            List<t_user_plan_rank> list = new List<t_user_plan_rank>();
            foreach (System.Data.DataRow item in data.Rows)
            {
                var row = item.ItemArray;
                var plan_name = row[0] + "";
                var rank = int.Parse(row[1] + "");
                var user_name = row[2] + "";
                var score = decimal.Parse(row[3] + "");
                //查询关联文档 判断关联文档与序号是否存在
                var validModel = planList.Where(_ => _.user_name.Equals(user_name) && _.plan_name.Equals(plan_name)).FirstOrDefault();
                t_user_plan_rank model = new t_user_plan_rank()
                {
                    create_time = DateTime.Now,
                    user_id = validModel.user_id,
                    plan_id = validModel.plan_id.Value,
                    rank = rank,
                    score = score
                };
                list.Add(model);
            }
            InsertUserPlanRank(list);

        }

        /// <summary>
        /// 新增题目
        /// </summary>
        /// <param name="list"></param>
        /// <param name="userid"></param>
        public void InsertUserPlanRank(List<t_user_plan_rank> list)
        {
            foreach (var item in list)
            {
                item.create_time = DateTime.Now;
                var id = PlanDA.InsertUserPlanRank(item);
            }
        }
        #endregion

        #region 单位积分
        public QueryResult<Department_Plan_RankVO> SearchDepartmentPlanRankPageList(Department_Plan_RankFilter filter)
        {
            return PlanDA.SearchDepartmentPlanRankPageList(filter);
        }

        public string DeptEmptyValid(DataTable data)
        {
            string errorMsg = "";
            int rowIndex = 1;
            foreach (System.Data.DataRow item in data.Rows)
            {
                var row = item.ItemArray;
                for (int i = 0; i < row.Length; i++)
                {
                    if (string.IsNullOrEmpty(row[i] + ""))
                    {
                        errorMsg += string.Format("{0}行{1}列为空<br/>", rowIndex, i + 1);
                    }
                }
                var count = data.Select("所属活动='" + row[0] + "' and 名次='" + row[1] + "' and 单位名称='" + row[2] + "'").Count();
                if (count > 1)
                {
                    errorMsg += string.Format("{0}行数据与其他行存在重复<br/>", rowIndex);
                }
                rowIndex++;
            }
            return errorMsg;
        }
        /// <summary>
        /// 单位 活动数据验证
        /// </summary>
        /// <param name="data"></param>
        /// <param name="deptList"></param>
        /// <param name="planList"></param>
        /// <returns></returns>
        public string DepartmentPlanRankValid(DataTable data, List<DepartmentPlanRankValid> list)
        {
            string errorMsg = "";
            int rowIndex = 1;
            foreach (System.Data.DataRow item in data.Rows)
            {
                var row = item.ItemArray;
                var plan_name = row[0] + "";
                var dept_name = row[2] + "";
                var model = PlanDA.LoadPlanRankDeptAndPlanValid(plan_name, dept_name);
                if (model == null || (!model.dept_id.HasValue && !model.plan_id.HasValue))
                {
                    errorMsg += string.Format("{0}行活动和单位不存在<br/>", rowIndex);
                }
                else if (!model.dept_id.HasValue)
                {
                    errorMsg += string.Format("{0}行单位不存在<br/>", rowIndex);
                }
                else if (!model.plan_id.HasValue)
                {
                    errorMsg += string.Format("{0}行活动不存在<br/>", rowIndex);
                }
                else
                {
                    list.Add(new DepartmentPlanRankValid()
                    {
                        dept_id = model.dept_id,
                        dept_name = dept_name,
                        plan_id = model.plan_id,
                        plan_name = plan_name
                    });
                }
                rowIndex++;
            }
            return errorMsg;
        }

        public void ImportDepartmentPlanRank(DataTable data, List<DepartmentPlanRankValid> deptList)
        {
            List<t_department_plan_rank> list = new List<t_department_plan_rank>();
            foreach (System.Data.DataRow item in data.Rows)
            {
                var row = item.ItemArray;
                var plan_name = row[0] + "";
                var rank = int.Parse(row[1] + "");
                var dept_name = row[2] + "";
                var score = decimal.Parse(row[3] + "");
                var validModel = deptList.Where(_ => _.dept_name.Equals(dept_name) && _.plan_name.Equals(plan_name)).FirstOrDefault();
                //查询关联文档 判断关联文档与序号是否存在
                t_department_plan_rank model = new t_department_plan_rank()
                {
                    create_time = DateTime.Now,
                    department_id = validModel.dept_id.Value,
                    plan_id = validModel.plan_id.Value,
                    rank = rank,
                    score = score
                };
                list.Add(model);
            }
            InsertDepartmentPlanRank(list);

        }

        /// <summary>
        /// 新增题目
        /// </summary>
        /// <param name="list"></param>
        /// <param name="userid"></param>
        public void InsertDepartmentPlanRank(List<t_department_plan_rank> list)
        {
            foreach (var item in list)
            {
                item.create_time = DateTime.Now;
                PlanDA.InsertDepartmentPlanRank(item);
            }
        }
        #endregion

        public List<CommboxMode> SearchPlanList()
        {
            return PlanDA.SearchPlanList();
        }

    }
}
