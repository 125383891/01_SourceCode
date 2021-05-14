using LYY.Article.Entity;
using LYY.ArticleService.Service;
using LYY.AuthCenterService.Service;
using LYY.Common.Entity;
using LYY.Report.Entity;
using LYY.ReportService.Service;
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
    public class ReportController : BaseController
    {
        private ReportBigService ReportBigService { get { return new ReportBigService(); } }

        private DepartmentService DepartmentService { get { return new DepartmentService(); } }

        private ArticleBasisService ArticleBasisService { get { return new ArticleBasisService(); } }

        public ActionResult ColumnChart()
        {
            return View();
        }

        public ActionResult LineChart()
        {
            return View();
        }


        [UserAuthorize(FunctionKeys.OperationMgt_Report_ComplaintStatistics_View)]
        public ActionResult ComplaintStatistics()
        {
            return View();
        }

        [UserAuthorize(FunctionKeys.OperationMgt_Report_ActivityStatistics_View)]
        public ActionResult ActivityStatistics()
        {
            return View();
        }

        [UserAuthorize(FunctionKeys.OperationMgt_Report_SubjectStatistics_View)]
        public ActionResult SubjectStatistics()
        {
            return View();
        }

        [UserAuthorize(FunctionKeys.OperationMgt_Report_ArticleInterviewStatistics_View)]
        public ActionResult ArticleInterviewStatistics()
        {
            return View();
        }

        [UserAuthorize(FunctionKeys.OperationMgt_Report_ProductVendorStatistics_View)]
        public ActionResult ProductVendorStatistics()
        {
            return View();
        }


        [UserAuthorize(FunctionKeys.OperationMgt_Report_ExternalDepartmentStatistics_View)]
        public ActionResult ExternalDepartmentStatistics()
        {
            return View();
        }

        [UserAuthorize(new string[] {
            FunctionKeys.OperationMgt_Report_ActivityStatistics_View,
            FunctionKeys.OperationMgt_Report_ComplaintStatistics_View,
            FunctionKeys.OperationMgt_Report_SubjectStatistics_View,
            FunctionKeys.OperationMgt_Report_ProductVendorStatistics_View,
            FunctionKeys.OperationMgt_Report_ExternalDepartmentStatistics_View
        })]
        [HttpGet]
        public JsonResult GetDepartmentList()
        {
            return Json(new AjaxResult()
            {
                Success = true,
                Data = DepartmentService.List()
            }, JsonRequestBehavior.AllowGet);
        }

        [UserAuthorize(FunctionKeys.OperationMgt_Report_ComplaintStatistics_View)]
        [HttpPost]
        public JsonResult QueryComplaintStatistics(QF_Report filter)
        {
            var list = ReportBigService.ComplaintStatisticsList(filter);
            var chartList = ReportBigService.ComplaintStatisticsChart(filter);
            return Json(new AjaxResult()
            {
                Success = true,
                Data = new
                {
                    chartList,
                    list
                }
            });
        }

        [UserAuthorize(FunctionKeys.OperationMgt_Report_ActivityStatistics_View)]
        [HttpPost]
        public JsonResult QueryActivityStatistics(QF_Report filter)
        {
            var list = ReportBigService.ActivityStatisticsList(filter);
            var chartList = ReportBigService.ActivityStatisticsChart(filter);
            return Json(new AjaxResult()
            {
                Success = true,
                Data = new
                {
                    chartList,
                    list
                }
            });
        }

        [UserAuthorize(FunctionKeys.OperationMgt_Report_SubjectStatistics_View)]
        [HttpPost]
        public JsonResult QuerySubjectStatistics(QF_Report filter)
        {
            var chartList = ReportBigService.SubjectStatisticsChart(filter);
            return Json(new AjaxResult()
            {
                Success = true,
                Data = chartList
            });
        }

        [UserAuthorize(FunctionKeys.OperationMgt_Report_ArticleInterviewStatistics_View)]
        [HttpPost]
        public JsonResult QueryArticleInterviewStatistics(QF_Report filter)
        {
            var chartList = ReportBigService.ArticleInterviewStatisticsChart(filter);
            return Json(new AjaxResult()
            {
                Success = true,
                Data = chartList
            });
        }

        [UserAuthorize(FunctionKeys.OperationMgt_Report_ProductVendorStatistics_View)]
        [HttpPost]
        public JsonResult QueryProductVendorStatistics()
        {
            QF_Report filter = BuildQueryFilterEntity<QF_Report>();
            var list = ReportBigService.ProductVendorStatisticsList(filter);
            return AjaxGridJson(list);
        }

        [UserAuthorize(FunctionKeys.OperationMgt_Report_ExternalDepartmentStatistics_View)]
        public JsonResult QueryExternalDepartmentStatistics()
        {
            QF_Report filter = BuildQueryFilterEntity<QF_Report>();
            var list = ReportBigService.ExternalDepartmentStatisticsList(filter);
            return AjaxGridJson(list);
        }

        [UserAuthorize(FunctionKeys.OperationMgt_Report_ArticleInterviewStatistics_View)]
        [HttpGet]
        public JsonResult QueryArticleList()
        {
            QueryResult<ArticleVO> query = ArticleBasisService.SearchPageList(new Article.Entity.QF_Article()
            {
                PageIndex = 0,
                PageSize = int.MaxValue
            });
            return Json(new AjaxResult() { Success = true, Data = query.data }, JsonRequestBehavior.AllowGet);
        }

        [UserAuthorize(FunctionKeys.OperationMgt_Report_ExternalDepartmentStatistics_View)]
        [HttpPost]
        public ActionResult SearchExportExternalDepartmentStatistics()
        {
            var filter = SerializeHelper.JsonDeserializeFixed<QF_Report>(Request.Form["queryString"]);
            filter.PageIndex = 0;
            filter.PageSize = int.MaxValue;

            var list = ReportBigService.ExternalDepartmentStatisticsList(filter);
            list.data.ForEach(p => { p.Id = list.data.IndexOf(p) + 1; });
            var tables = new List<DataTable>
            {
                DataMapper.ListToDataTable(list.data)
            };
            var columns = new List<List<ColumnData>>
            {
                new List<ColumnData>
                {
                    new ColumnData { FieldName = "Id", Width = 20, Title ="序号", HorizontalAlignment= HorizAlignments.Centered },
                    new ColumnData { FieldName = "SubjectDepartmentName", Width = 20, Title = "主题所属公司", HorizontalAlignment= HorizAlignments.Left},
                    new ColumnData { FieldName = "CreateSubjectUserName", Width = 20, Title ="主题发表人" , HorizontalAlignment= HorizAlignments.Left},
                    new ColumnData { FieldName = "ParseTitle", Width = 20 ,Title = "主题标题" , HorizontalAlignment= HorizAlignments.Left},
                    new ColumnData { FieldName = "ExternalUserName", Width = 20, Title ="外协人员" ,HorizontalAlignment= HorizAlignments.Left},
                    new ColumnData { FieldName = "ExternalDepartmentName", Width = 20, Title = "外协部门", HorizontalAlignment= HorizAlignments.Left},
                    new ColumnData { FieldName = "AssignmentTime", Width = 40, Title ="分派时间" ,ValueFormat="yyyy-MM-dd HH:mm:ss", HorizontalAlignment= HorizAlignments.Left},
                    new ColumnData { FieldName = "Stat", Width = 10, Title = "完结评星数" , HorizontalAlignment= HorizAlignments.Left}
                }
            };
            var excelExport = new ExcelFileExporter();
            var fileName = string.Empty;
            var excelByte = excelExport.CreateFile(tables, new List<string>() { "外部门协助统计" }, columns, null, out fileName, "外部门协助统计");
            fileName = "外部门协助统计.xls";
            return File(new MemoryStream(excelByte), "application/ms-excel", fileName);
        }

        [UserAuthorize(FunctionKeys.OperationMgt_Report_ComplaintStatistics_View)]
        [HttpPost]
        public ActionResult SearchExportComplaintStatistics()
        {
            var filter = SerializeHelper.JsonDeserializeFixed<QF_Report>(Request.Form["queryString"]);
            var list = ReportBigService.ComplaintStatisticsList(filter);
            list.ForEach(p => { p.SerialNumber = list.IndexOf(p) + 1; });
            var tables = new List<DataTable>
            {
                DataMapper.ListToDataTable(list)
            };
            var columns = new List<List<ColumnData>>
            {
                new List<ColumnData>
                {
                    new ColumnData { FieldName = "SerialNumber", Width = 10, Title ="序号", HorizontalAlignment= HorizAlignments.Centered },
                    new ColumnData { FieldName = "DepartmentName", Width = 20, Title = "公司名称", HorizontalAlignment= HorizAlignments.Left},
                    new ColumnData { FieldName = "ComplaintCount", Width = 10, Title ="	投诉量" , HorizontalAlignment= HorizAlignments.Left},
                    new ColumnData { FieldName = "LastYearComplaintCount", Width = 20 ,Title = "去年同期投诉量" , HorizontalAlignment= HorizAlignments.Left},
                    new ColumnData { FieldName = "LastYearComplaintCountProportion", Width = 10, Title ="同比" ,HorizontalAlignment= HorizAlignments.Left},
                    new ColumnData { FieldName = "LastMonthComplaintCount", Width = 20, Title = "上月同期投诉量", HorizontalAlignment= HorizAlignments.Left},
                    new ColumnData { FieldName = "LastMonthComplaintCountProportion", Width = 10, Title ="环比" ,HorizontalAlignment= HorizAlignments.Left},
                }
            };
            var excelExport = new ExcelFileExporter();
            var fileName = string.Empty;
            var excelByte = excelExport.CreateFile(tables, new List<string>() { "投诉量统计" }, columns, null, out fileName, "投诉量统计");
            fileName = "投诉量统计.xls";
            return File(new MemoryStream(excelByte), "application/ms-excel", fileName);
        }

        [UserAuthorize(FunctionKeys.OperationMgt_Report_ProductVendorStatistics_View)]
        [HttpPost]
        public ActionResult SearchExportProductVendorStatistics()
        {
            var filter = SerializeHelper.JsonDeserializeFixed<QF_Report>(Request.Form["queryString"]);
            filter.PageIndex = 0;
            filter.PageSize = int.MaxValue;

            var list = ReportBigService.ProductVendorStatisticsList(filter);
            list.data.ForEach(p => { p.SerialNumber = list.data.IndexOf(p) + 1; });
            var tables = new List<DataTable>
            {
                DataMapper.ListToDataTable(list.data)
            };
            var dynamicTitle = filter.StatisticsObject == StatisticsObject.Product ? "产品名称" : "供应商名称";
            var columns = new List<List<ColumnData>>
            {
                new List<ColumnData>
                {
                    new ColumnData { FieldName = "SerialNumber", Width = 10, Title ="序号", HorizontalAlignment= HorizAlignments.Centered },
                    new ColumnData { FieldName = "Name", Width = 20, Title = dynamicTitle, HorizontalAlignment= HorizAlignments.Left},
                    new ColumnData { FieldName = "ComplaintCount", Width = 10, Title ="	投诉量" , HorizontalAlignment= HorizAlignments.Left},
                    new ColumnData { FieldName = "FinishCount", Width = 10 ,Title = "完结数量" , HorizontalAlignment= HorizAlignments.Left},
                    new ColumnData { FieldName = "FinishCountProportion", Width = 10, Title ="完结率" ,HorizontalAlignment= HorizAlignments.Left}
                }
            };
            var excelExport = new ExcelFileExporter();
            var fileName = string.Empty;
            var excelByte = excelExport.CreateFile(tables, new List<string>() { "产品供应商统计" }, columns, null, out fileName, "产品供应商统计");
            fileName = "产品供应商统计.xls";
            return File(new MemoryStream(excelByte), "application/ms-excel", fileName);
        }

        [UserAuthorize(FunctionKeys.OperationMgt_Report_ActivityStatistics_View)]
        [HttpPost]
        public ActionResult SearchExportActivityStatistics()
        {
            var filter = SerializeHelper.JsonDeserializeFixed<QF_Report>(Request.Form["queryString"]);
            var list = ReportBigService.ActivityStatisticsList(filter);
            list.ForEach(p => { p.SerialNumber = list.IndexOf(p) + 1; });
            var tables = new List<DataTable>
            {
                DataMapper.ListToDataTable(list)
            };
            var columns = new List<List<ColumnData>>
            {
                new List<ColumnData>
                {
                    new ColumnData { FieldName = "SerialNumber", Width = 10, Title ="序号", HorizontalAlignment= HorizAlignments.Centered },
                    new ColumnData { FieldName = "DepartmentName", Width = 20, Title = "公司名称", HorizontalAlignment= HorizAlignments.Left},
                    new ColumnData { FieldName = "ActivityCount", Width = 10, Title ="活跃度" , HorizontalAlignment= HorizAlignments.Left},
                    new ColumnData { FieldName = "LastYearActivityCount", Width = 20 ,Title = "去年同期活跃度" , HorizontalAlignment= HorizAlignments.Left},
                    new ColumnData { FieldName = "LastYearActivityCountProportion", Width = 10, Title ="同比" ,HorizontalAlignment= HorizAlignments.Left},
                    new ColumnData { FieldName = "LastMonthActivityCount", Width = 20, Title ="上月同期活跃度" ,HorizontalAlignment= HorizAlignments.Left},
                    new ColumnData { FieldName = "LastMonthComplaintCountProportion", Width = 10, Title ="环比" ,HorizontalAlignment= HorizAlignments.Left}
                }
            };
            var excelExport = new ExcelFileExporter();
            var fileName = string.Empty;
            var excelByte = excelExport.CreateFile(tables, new List<string>() { "活跃度统计" }, columns, null, out fileName, "活跃度统计");
            fileName = "活跃度统计.xls";
            return File(new MemoryStream(excelByte), "application/ms-excel", fileName);
        }
    }
}