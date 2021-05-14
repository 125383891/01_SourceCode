using LYY.AuthCenterService.Service;
using LYY.Common.Entity;
using LYY.Common.Service;
using LYY.Subject.Entity;
using LYY.SubjectService.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YZ.PortalBase;
using YZ.Utility;
using YZ.Utility.Web;

namespace LYY.EnterpriseWxMgt.OperationMgt.Portal.Controllers
{
    public class SubjectController : BaseController
    {
        private SubjectBasisService SubjectBasisService { get { return new SubjectBasisService(); } }

        private CommonBasisService CommonBasisService { get { return new CommonBasisService(); } }

        private DepartmentService DepartmentService { get { return new DepartmentService(); } }

        [UserAuthorize(FunctionKeys.OperationMgt_Subject_List_View)]
        public ActionResult List()
        {
            return View();
        }

        [UserAuthorize(FunctionKeys.OperationMgt_Subject_Detail_View)]
        public ActionResult Detail(int? subjectId)
        {
            if (!subjectId.HasValue)
            {
                throw new BusinessException("主题Id不允许为空");
            }
            ViewBag.SubjectId = subjectId;
            return View();
        }

        [UserAuthorize(FunctionKeys.OperationMgt_Subject_List_View)]
        [HttpPost]
        public JsonResult Query()
        {
            QF_Subject filter = BuildQueryFilterEntity<QF_Subject>();
            return AjaxGridJson(SubjectBasisService.SearchPageList(filter));
        }

        [UserAuthorize(FunctionKeys.OperationMgt_Subject_List_View)]
        [HttpGet]
        public JsonResult QueryTagAndDepartmentList()
        {
            return Json(new AjaxResult()
            {
                Success = true,
                Data = new
                {
                    CatList = CommonBasisService.SearchCatList(),
                    Departmentlist = DepartmentService.List()
                }
            }, JsonRequestBehavior.AllowGet);
        }

        [UserAuthorize(FunctionKeys.OperationMgt_Subject_List_View)]
        [HttpPost]
        public ActionResult SearchExportList()
        {
            var filter = SerializeHelper.JsonDeserializeFixed<QF_Subject>(Request.Form["queryString"]);
            var list = SubjectBasisService.SearchExportList(filter);
            var tables = new List<DataTable>
            {
                DataMapper.ListToDataTable(list)
            };
            var columns = new List<List<ColumnData>>
            {
                new List<ColumnData>
                {
                    new ColumnData { FieldName = "Id", Width = 20, Title ="主题Id", HorizontalAlignment= HorizAlignments.Centered },
                    new ColumnData { FieldName = "CreateTimeStr", Width = 20, Title = "发表时间", HorizontalAlignment= HorizAlignments.Left},//ValueFormat="yyyy/MM/dd HH:mm:ss"
                    new ColumnData { FieldName = "OrderTimeStr", Width = 20, Title ="最新回复时间" , HorizontalAlignment= HorizAlignments.Left},//ValueFormat="yyyy/MM/dd HH:mm:ss"
                    new ColumnData { FieldName = "CreateUserName", Width = 20 ,Title = "发表人" , HorizontalAlignment= HorizAlignments.Left},
                    new ColumnData { FieldName = "DepartmentName", Width = 20, Title ="主题所属单位" ,HorizontalAlignment= HorizAlignments.Left},
                    new ColumnData { FieldName = "CatName", Width = 20, Title = "主题分类", HorizontalAlignment= HorizAlignments.Left},
                    new ColumnData { FieldName = "Telephone", Width = 20, Title = "发表人手机号码", HorizontalAlignment= HorizAlignments.Left},
                    new ColumnData { FieldName = "TitleDesc", Width = 40, Title ="标题" , HorizontalAlignment= HorizAlignments.Left},
                    new ColumnData { FieldName = "ContentDesc", Width = 40, Title = "正文" , HorizontalAlignment= HorizAlignments.Left},
                    new ColumnData { FieldName = "ProductName", Width = 30, Title ="产品名称", HorizontalAlignment= HorizAlignments.Left },
                    new ColumnData { FieldName = "Vendor", Width = 30, Title = "供应商", HorizontalAlignment= HorizAlignments.Left},
                    new ColumnData { FieldName = "ComplainVendor", Width = 30, Title = "投诉供应商", HorizontalAlignment= HorizAlignments.Left},
                    new ColumnData { FieldName = "ReplyerUserDesc", Width = 30, Title ="分配对象" , HorizontalAlignment= HorizAlignments.Left},
                    new ColumnData { FieldName = "Zan", Width = 10, Title = "赞数" , HorizontalAlignment= HorizAlignments.Right},
                    new ColumnData { FieldName = "View", Width = 10, Title ="热度" , HorizontalAlignment= HorizAlignments.Right},
                    new ColumnData { FieldName = "CommentCount", Width = 10, Title = "回复数" , HorizontalAlignment= HorizAlignments.Right},
                    new ColumnData { FieldName = "FirstCommentTimeStr", Width = 20, Title ="首响回复时间",HorizontalAlignment= HorizAlignments.Left },// ValueFormat="yyyy/MM/dd HH:mm:ss",
                    new ColumnData { FieldName = "FirstCommentUser", Width = 20, Title = "首响回复人", HorizontalAlignment= HorizAlignments.Left},
                    new ColumnData { FieldName = "FirstCommentDesc", Width = 30, Title ="首响回复内容" , HorizontalAlignment= HorizAlignments.Left},
                    new ColumnData { FieldName = "FirstStatTimeStr", Width = 20, Title = "评星回复时间" , HorizontalAlignment= HorizAlignments.Left},
                    new ColumnData { FieldName = "FirstStatCommentDesc", Width = 30, Title = "评星回复内容" , HorizontalAlignment= HorizAlignments.Left},//ValueFormat="yyyy/MM/dd HH:mm:ss",
                    new ColumnData { FieldName = "FirstStatUser", Width = 20, Title = "评星回复人" , HorizontalAlignment= HorizAlignments.Left},
                    new ColumnData { FieldName = "FirstStar", Width = 10, Title = "评星数" , HorizontalAlignment= HorizAlignments.Right},
                    new ColumnData { FieldName = "StarRemark", Width = 30, Title = "评星意见" , HorizontalAlignment= HorizAlignments.Left},
                    new ColumnData { FieldName = "StartTimeStr", Width = 20, Title ="评星时间" , HorizontalAlignment= HorizAlignments.Left},//ValueFormat="yyyy/MM/dd HH:mm:ss" ,
                    new ColumnData { FieldName = "StarRemarkAppend", Width = 30, Title = "追评意见" , HorizontalAlignment= HorizAlignments.Left},
                    new ColumnData { FieldName = "VendorStar", Width = 20, Title = "供应商评价" , HorizontalAlignment= HorizAlignments.Right}
                }
            };
            string fileName = string.Empty;
            var excelExport = new ExcelFileExporter();
            byte[] excelByte = excelExport.CreateFile(tables, new List<string>() { "论坛活动主题" }, columns, null, out fileName, "论坛活动主题");
            fileName = "论坛活动主题.xls";
            return File(new MemoryStream(excelByte), "application/ms-excel", fileName);
        }

        [UserAuthorize(FunctionKeys.OperationMgt_Subject_List_View)]
        [HttpPost]
        public JsonResult UpdateViews(SubjectModel entity)
        {
            entity.id = entity.subjectid;
            SubjectBasisService.UpdateViewCount(entity);
            return Json(new AjaxResult() { Success = true, Data = true }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LoadSubject(int id)
        {
            var entity = SubjectBasisService.LoadSubject(id);
            return Json(new AjaxResult() { Success = true, Data = entity }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 修改重要置顶
        /// </summary>
        /// <param name="viewCount"></param>
        /// <param name="subjectId"></param>
        /// <returns></returns>
        [UserAuthorize(FunctionKeys.OperationMgt_Subject_List_View)]
        [HttpGet]
        public JsonResult UpdateWeight(int? weight, int subjectId)
        {
            int count = 0;
            if (weight.HasValue && weight != 0)
            {
                count = SubjectBasisService.ValidWeight(weight, subjectId);
            }
            if (count > 0)
            {
                return Json(new AjaxResult() { Success = false, Data = true, Message = "重要置顶与其他重复" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                SubjectBasisService.UpdateWeight(weight, subjectId);
            }
            return Json(new AjaxResult() { Success = true, Data = true }, JsonRequestBehavior.AllowGet);
        }
        [UserAuthorize(FunctionKeys.OperationMgt_Subject_Detail_View)]
        [HttpGet]
        public JsonResult GetById(int subjectId)
        {
            return Json(new AjaxResult() { Success = true, Data = SubjectBasisService.GetById(subjectId) }, JsonRequestBehavior.AllowGet);
        }

        [UserAuthorize(FunctionKeys.OperationMgt_Subject_Detail_View)]
        [HttpPost]
        public JsonResult SearchReplyPageList()
        {
            QF_Subject filter = BuildQueryFilterEntity<QF_Subject>();
            return AjaxGridJson(SubjectBasisService.SearchReplyPageList(filter));
        }



        /// <summary>
        /// 获取主题参与活动关系表
        /// </summary>
        /// <param name="subject_id"></param>
        /// <returns></returns>
        public ActionResult LoadSubjectPlan(int subject_id)
        {
            var result = SubjectBasisService.LoadSubjectPlan(subject_id);
            return Json(new AjaxResult() { Success = true, Data = result }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 更新主题参与活动关系表质量达人分
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ActionResult UpdateSubjectPlanScoreExtra(SubjectPlanScoreExtra entity)
        {
            var result = SubjectBasisService.UpdateSubjectPlanScoreExtra(entity);
            return Json(new AjaxResult() { Data = entity, Success = true, Message = "保存成功!" }, JsonRequestBehavior.AllowGet);
        }


        #region 排行榜导出
        public ActionResult Export()
        {
            return View();
        }

        /// <summary>
        /// 导出排行
        /// </summary>
        /// <returns></returns>
        [UserAuthorize(FunctionKeys.OperationMgt_Subject_Export_View)]
        [HttpPost]
        public ActionResult SearchExportRanking()
        {
            var filter = SerializeHelper.JsonDeserializeFixed<QF_Subject>(Request.Form["queryString"]);
            var list = SubjectBasisService.SearchExportRanking(filter);
            var tables = new List<DataTable>
            {
                DataMapper.ListToDataTable(list)
            };
            var columns = new List<List<ColumnData>>
            {
                new List<ColumnData>
                {
                    new ColumnData { FieldName = "user_id", Width = 30, Title ="用户Id", HorizontalAlignment= HorizAlignments.Centered },
                    new ColumnData { FieldName = "user_name", Width = 30, Title = "用户姓名", HorizontalAlignment= HorizAlignments.Left},
                    new ColumnData { FieldName = "number1", Width = 40, Title ="投诉量统计" , HorizontalAlignment= HorizAlignments.Right},
                    new ColumnData { FieldName = "number2", Width = 30 ,Title = "处理质量" , HorizontalAlignment= HorizAlignments.Right},
                    new ColumnData { FieldName = "number3", Width = 30, Title ="处理效率" ,HorizontalAlignment= HorizAlignments.Right}

                }
            };
            string fileName = string.Empty;
            var excelExport = new ExcelFileExporter();
            byte[] excelByte = excelExport.CreateFile(tables, new List<string>() { "排行榜" }, columns, null, out fileName, "排行榜");
            fileName = "排行榜.xls";
            return File(new MemoryStream(excelByte), "application/ms-excel", fileName);
        }
        /// <summary>
        /// 导出平均
        /// </summary>
        /// <returns></returns>
        [UserAuthorize(FunctionKeys.OperationMgt_Subject_Export_View)]
        [HttpPost]
        public ActionResult SearchExportAvg()
        {
            var filter = SerializeHelper.JsonDeserializeFixed<QF_Subject>(Request.Form["queryString"]);
            var list = SubjectBasisService.SearchExportAvg(filter);
            var tables = new List<DataTable>
            {
                DataMapper.ListToDataTable(list)
            };
            var columns = new List<List<ColumnData>>
            {
                new List<ColumnData>
                {
                    new ColumnData { FieldName = "dept_name", Width = 40, Title ="单位", HorizontalAlignment= HorizAlignments.Centered },
                    new ColumnData { FieldName = "dd", Width = 30, Title = "单位平均未读数", HorizontalAlignment= HorizAlignments.Right}//ValueFormat="yyyy/MM/dd HH:mm:ss"
                }
            };
            string fileName = string.Empty;
            var excelExport = new ExcelFileExporter();
            byte[] excelByte = excelExport.CreateFile(tables, new List<string>() { "单位平均未读数" }, columns, null, out fileName, "单位平均未读数");
            fileName = "单位平均未读数.xls";
            return File(new MemoryStream(excelByte), "application/ms-excel", fileName);
        }
        #endregion
    }
}