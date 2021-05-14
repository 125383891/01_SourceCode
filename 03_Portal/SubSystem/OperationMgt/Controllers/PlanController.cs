using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YZ.PortalBase;
using LYY.PlanService.Service;
using YZ.Utility;
using LYY.Plan.Entity;
using YZ.Utility.Web;
using LYY.Common.Entity;
using System.Data;

namespace LYY.EnterpriseWxMgt.OperationMgt.Portal.Controllers
{
    public class PlanController : BaseController
    {
        private PlanBasisService PlanBasisService
        {
            get
            {
                return new PlanBasisService();
            }
        }

        [UserAuthorize(FunctionKeys.OperationMgt_Plan_List_View)]
        public ActionResult List()
        {
            return View();
        }

        [UserAuthorize(FunctionKeys.OperationMgt_Plan_List_View)]
        public PartialViewResult Edit()
        {
            return PartialView();
        }

        [UserAuthorize(FunctionKeys.OperationMgt_Plan_List_View)]
        [HttpGet]
        public JsonResult GetById(int id)
        {
            PlanVO planEntity = PlanBasisService.GetById(id);
            return Json(new AjaxResult() { Success = true, Data = planEntity }, JsonRequestBehavior.AllowGet);
        }

        [UserAuthorize(FunctionKeys.OperationMgt_Plan_List_View)]
        [HttpPost]
        public JsonResult PlanInsert(PlanVO planEntity)
        {
            PlanBasisService.Insert(planEntity);
            return Json(new AjaxResult() { Success = true, Data = true });
        }

        [UserAuthorize(FunctionKeys.OperationMgt_Plan_List_View)]
        [HttpPost]
        public JsonResult PlanUpdate(PlanVO planEntity)
        {
            PlanBasisService.Update(planEntity);
            return Json(new AjaxResult() { Success = true, Data = planEntity });
        }

        [UserAuthorize(FunctionKeys.OperationMgt_Plan_List_View)]
        [HttpPost]
        public JsonResult SearchList()
        {
            QueryFilter filter = BuildQueryFilterEntity<QueryFilter>();
            QueryResult<PlanVO> result = PlanBasisService.SearchPageList(filter);
            return AjaxGridJson(result);
        }

        #region 处理人积分

        /// <summary>
        /// 处理人积分
        /// </summary>
        /// <returns></returns>
        [UserAuthorize(FunctionKeys.OperationMgt_Plan_UserPlanRank_View)]
        public ActionResult UserPlanRank()
        {
            return View();
        }

        /// <summary>
        /// 处理人积分查询
        /// </summary>
        /// <returns></returns>
        [UserAuthorize(FunctionKeys.OperationMgt_Plan_UserPlanRank_View)]
        public JsonResult UserPlanRankQuery()
        {
            var filter = BuildQueryFilterEntity<User_Plan_RankFilter>();
            QueryResult<User_Plan_RankVO> result = PlanBasisService.SearchUserPlanRankPageList(filter);
            return AjaxGridJson(result);
        }

        /// <summary>
        ///导入
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [UserAuthorize(FunctionKeys.OperationMgt_Plan_UserPlanRank_View)]
        public ActionResult ImportUserPlanRank(HttpPostedFileBase file)
        {
            try
            {
                DataTable dtData = ExcelHelper.RenderDataTableFromExcel(file.FileName, file.InputStream, 0, 0);
                //非空验证
                var message = PlanBasisService.UserEmptyValid(dtData);
                if (string.IsNullOrEmpty(message))
                {

                    List<UserPlanRankValid> planList = new List<UserPlanRankValid>();
                    //数据验证
                    message = PlanBasisService.UserPlanRankValid(dtData, planList);
                    if (string.IsNullOrEmpty(message))
                    {
                        PlanBasisService.ImportUserPlanRank(dtData, planList);
                        return Json(new AjaxResult() { Data = null, Success = true, Message = "导入成功" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new AjaxResult() { Data = null, Success = false, Message = message }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    //必填项验证不过
                    return Json(new AjaxResult() { Data = null, Success = false, Message = message }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new AjaxResult() { Data = null, Success = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }
        #endregion
        #region 单位积分
        /// <summary>
        /// 单位积分
        /// </summary>
        /// <returns></returns>
        [UserAuthorize(FunctionKeys.OperationMgt_Plan_DeptPlanRank_View)]
        public ActionResult DeptPlanRank()
        {
            return View();
        }
        /// <summary>
        /// 单位积分查询
        /// </summary>
        /// <returns></returns>
        [UserAuthorize(FunctionKeys.OperationMgt_Plan_DeptPlanRank_View)]
        public JsonResult DeptPlanRankQuery()
        {
            var filter = BuildQueryFilterEntity<Department_Plan_RankFilter>();
            QueryResult<Department_Plan_RankVO> result = PlanBasisService.SearchDepartmentPlanRankPageList(filter);
            return AjaxGridJson(result);
        }
        /// <summary>
        /// 导入
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [UserAuthorize(FunctionKeys.OperationMgt_Plan_DeptPlanRank_View)]
        public ActionResult ImportDeptPlanRank(HttpPostedFileBase file)
        {
            try
            {
                DataTable dtData = ExcelHelper.RenderDataTableFromExcel(file.FileName, file.InputStream, 0, 0);
                //非空验证
                var message = PlanBasisService.DeptEmptyValid(dtData);
                if (string.IsNullOrEmpty(message))
                {

                    List<DepartmentPlanRankValid> deptList = new List<DepartmentPlanRankValid>();
                    //数据验证
                    message = PlanBasisService.DepartmentPlanRankValid(dtData, deptList);
                    if (string.IsNullOrEmpty(message))
                    {
                        PlanBasisService.ImportDepartmentPlanRank(dtData, deptList);
                        return Json(new AjaxResult() { Data = null, Success = true, Message = "导入成功" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new AjaxResult() { Data = null, Success = false, Message = message }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    //必填项验证不过
                    return Json(new AjaxResult() { Data = null, Success = false, Message = message }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new AjaxResult() { Data = null, Success = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }
        #endregion


        public ActionResult SearchPlanList()
        {
            var list = PlanBasisService.SearchPlanList();
            return Json(new AjaxResult() { Data = list, Success = true, Message = "" }, JsonRequestBehavior.AllowGet);
        }
    }
}