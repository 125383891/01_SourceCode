using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YZ.PortalBase;
using YZ.Utility;
using LYY.ArticleService.Service;
using LYY.Article.Entity;
using LYY.AuthCenterService.Service;
using YZ.Utility.Web;
using LYY.Common.Entity;
using System.Configuration;
using LYY.MockTestService.Service;
using LYY.MockTest.Entity.ViewModel;
using System.IO;
using System.Data;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using LYY.MockTest.Entity.Entity;
using LYY.Common.Entity.Enums;

namespace LYY.EnterpriseWxMgt.OperationMgt.Portal.Controllers
{
    /// <summary>
    /// 模拟测验
    /// </summary>
    public class MockTestController : BaseController
    {
        private MockTestBasisService MockTestBasisService { get { return new MockTestBasisService(); } }

        #region 题库管理
        [UserAuthorize(FunctionKeys.OperationMgt_Mock_Question_List_View)]
        public ActionResult QuestionList()
        {
            return View();
        }
        /// <summary>
        /// 题库查询
        /// </summary>
        /// <returns></returns>
        [UserAuthorize(FunctionKeys.OperationMgt_Mock_Question_List_View)]
        public ActionResult QuestionQuery()
        {
            QusetionFilter filter = BuildQueryFilterEntity<QusetionFilter>();
            return AjaxGridJson(MockTestBasisService.SearchQuestionPageLists(filter));
        }
        /// <summary>
        /// 获取问题
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [UserAuthorize(FunctionKeys.OperationMgt_Mock_Question_List_View)]
        public ActionResult QuestionDetail(int id)
        {
            var result = MockTestBasisService.LoadQustion(id);
            return Json(new AjaxResult() { Success = true, Data = result }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 修改题目
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [UserAuthorize(FunctionKeys.OperationMgt_Mock_Question_Detail_View)]
        public ActionResult SaveQusetion(QusetionView data)
        {
            data.update_user = CurrUser.UserID;
            data.id = data.qusetionId;
            var result = MockTestBasisService.UpdateQustion(data);
            if (string.IsNullOrEmpty(result))
            {
                return Json(new AjaxResult() { Data = data, Success = true, Message = "保存成功!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new AjaxResult() { Data = data, Success = false, Message = result }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 导入
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [UserAuthorize(FunctionKeys.OperationMgt_Mock_Question_List_View)]
        public ActionResult ImportQusetion(HttpPostedFileBase file)
        {
            try
            {
                DataTable dtData = ExcelHelper.RenderDataTableFromExcel(file.FileName, file.InputStream, 0, 0);
                //非空验证
                var message = MockTestBasisService.EmptyValid(dtData);
                if (string.IsNullOrEmpty(message))
                {
                    List<DocumentValid> documentList = new List<DocumentValid>();
                    List<DocumentValid> userTagList = new List<DocumentValid>();
                    //数据验证
                    message = MockTestBasisService.DocumentValid(dtData, documentList);
                    message += MockTestBasisService.UserTagValid(dtData, userTagList);
                    if (string.IsNullOrEmpty(message))
                    {
                        MockTestBasisService.ImportQusetion(dtData, documentList, userTagList, CurrUser.UserID);
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


        [UserAuthorize(FunctionKeys.OperationMgt_Mock_Question_List_View)]
        public JsonResult DeleteQuestion(List<int> ids)
        {
            try
            {
                int i = 0;
                foreach (var item in ids)
                {
                    i += MockTestBasisService.DeleteQuestion(item);
                }
                return Json(new AjaxResult() { Data = "", Success = true, Message = "(" + i.ToString() + ")删除成功!" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new AjaxResult() { Data = "", Success = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region 考试配置管理
        [UserAuthorize(FunctionKeys.OperationMgt_Mock_ExamConfig_List_View)]
        public ActionResult ExamConfig()
        {
            return View();
        }
        /// <summary>
        /// 考试配置分页查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [UserAuthorize(FunctionKeys.OperationMgt_Mock_ExamConfig_List_View)]
        public ActionResult ExamQuery()
        {
            ExamFilter filter = BuildQueryFilterEntity<ExamFilter>();
            return AjaxGridJson(MockTestBasisService.SearchExamPageLists(filter));
        }
        /// <summary>
        /// 读取考试配置
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [UserAuthorize(FunctionKeys.OperationMgt_Mock_ExamConfig_List_View)]
        public ActionResult ExamDetail(int id)
        {

            var result = MockTestBasisService.LoadExam(id);
            return Json(new AjaxResult() { Success = true, Data = result }, JsonRequestBehavior.AllowGet);
        }

        [UserAuthorize(FunctionKeys.OperationMgt_Mock_ExamConfig_List_View)]
        public ActionResult SaveExam(ExamView data)
        {
            data.update_user = CurrUser.UserID;
            data.id = data.Exam_Id;
            string message = MockTestBasisService.SaveExam(data);
            if (string.IsNullOrEmpty(message))
            {
                return Json(new AjaxResult() { Data = data, Success = true, Message = "保存成功!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new AjaxResult() { Data = data, Success = false, Message = message }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region 测验历史记录
        [UserAuthorize(FunctionKeys.OperationMgt_Mock_TestHistory_List_View)]
        public ActionResult TestHistory()
        {
            return View();
        }
        /// <summary>
        /// 测验历史记录查询
        /// </summary>
        /// <returns></returns>
        [UserAuthorize(FunctionKeys.OperationMgt_Mock_TestHistory_List_View)]
        public ActionResult ExamTestQuery()
        {
            ExamTestFilter filter = BuildQueryFilterEntity<ExamTestFilter>();
            return AjaxGridJson(MockTestBasisService.SearchExamTestPageLists(filter));
        }
        [UserAuthorize(FunctionKeys.OperationMgt_Mock_TestHistory_List_View)]
        public ActionResult GetExamList()
        {
            var examList = MockTestBasisService.SearchExamLists();
            return Json(new AjaxResult() { Data = examList, Success = true, Message = "" }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 测验历史记录导出
        /// </summary>
        /// <returns></returns>
        [UserAuthorize(FunctionKeys.OperationMgt_Mock_TestHistory_List_View)]
        public ActionResult ExportExamTestList()
        {
            var filter = SerializeHelper.JsonDeserializeFixed<ExamTestFilter>(Request.Form["queryString"]);
            var list = MockTestBasisService.ExportExamTestList(filter);
            var tables = new List<DataTable>
            {
                DataMapper.ListToDataTable(list)
            };
            var columns = new List<List<ColumnData>>
            {
                new List<ColumnData>
                {
                    new ColumnData { FieldName = "begin_time", Width = 20, Title = "测验时间", ValueFormat="yyyy-MM-dd", HorizontalAlignment= HorizAlignments.Centered, VerticalAlignment= VertiAlignments.Centered},
                    new ColumnData { FieldName = "ModeStr", Width = 20 ,Title = "测验类型" , HorizontalAlignment= HorizAlignments.Centered, VerticalAlignment= VertiAlignments.Centered},
                    new ColumnData { FieldName = "Document_name", Width = 20, Title ="针对文档" ,HorizontalAlignment= HorizAlignments.Centered, VerticalAlignment= VertiAlignments.Centered},
                    new ColumnData { FieldName = "Title", Width = 20, Title = "针对模拟考试", HorizontalAlignment= HorizAlignments.Centered, VerticalAlignment= VertiAlignments.Centered},
                    new ColumnData { FieldName = "UserName", Width = 20, Title = "测验人", HorizontalAlignment= HorizAlignments.Centered, VerticalAlignment= VertiAlignments.Centered},
                    new ColumnData { FieldName = "DeptName", Width = 40, Title ="所在部门" , HorizontalAlignment= HorizAlignments.Centered, VerticalAlignment= VertiAlignments.Centered},
                    new ColumnData { FieldName = "total_num", Width = 20, Title = "总题目数" , HorizontalAlignment= HorizAlignments.Centered, VerticalAlignment= VertiAlignments.Centered},
                    new ColumnData { FieldName = "right_num", Width = 20, Title ="正确题目数", HorizontalAlignment= HorizAlignments.Centered, VerticalAlignment= VertiAlignments.Centered },
                    new ColumnData { FieldName = "RightRate", Width = 20, Title = "正确率", HorizontalAlignment= HorizAlignments.Centered, VerticalAlignment= VertiAlignments.Centered},
                    new ColumnData { FieldName = "score", Width = 20, Title ="实际得分" , HorizontalAlignment= HorizAlignments.Centered, VerticalAlignment= VertiAlignments.Centered},
                    new ColumnData { FieldName = "total_score", Width = 20, Title = "测验总分" , HorizontalAlignment= HorizAlignments.Centered, VerticalAlignment= VertiAlignments.Centered},
                    new ColumnData { FieldName = "duration_m", Width = 20, Title ="测验耗时(分钟)" , HorizontalAlignment= HorizAlignments.Centered, VerticalAlignment= VertiAlignments.Centered},
                    new ColumnData { FieldName = "IsCompleteStr", Width = 20, Title = "是否已完成" , HorizontalAlignment= HorizAlignments.Centered, VerticalAlignment= VertiAlignments.Centered}
                }
            };
            string fileName = string.Empty;
            var excelExport = new ExcelFileExporter();
            byte[] excelByte = excelExport.CreateFile(tables, new List<string>() { "测验历史记录" }, columns, null, out fileName, "测验历史记录");
            fileName = "测验历史记录.xls";
            return File(new MemoryStream(excelByte), "application/ms-excel", fileName);
        }
        #endregion

        #region 测验统计
        [UserAuthorize(FunctionKeys.OperationMgt_Mock_TestStatistics_List_View)]
        public ActionResult TestStatistics()
        {
            return View();
        }

        [UserAuthorize(FunctionKeys.OperationMgt_Mock_TestStatistics_List_View)]
        public ActionResult TestStatisticsQuery(TestStatisticsFilter filter)
        {
            //TestStatisticsFilter filter = BuildQueryFilterEntity<TestStatisticsFilter>();
            var resultList = MockTestBasisService.SearchTestStatisticsLists(filter);
            return Json(new AjaxResult() { Success = true, Data = resultList }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 测验统计
        /// </summary>
        /// <returns></returns>
        [UserAuthorize(FunctionKeys.OperationMgt_Mock_TestStatistics_List_View)]
        public ActionResult ExportTestStatisticsList()
        {
            var filter = SerializeHelper.JsonDeserializeFixed<TestStatisticsFilter>(Request.Form["queryString"]);
            var list = MockTestBasisService.SearchTestStatisticsLists(filter);
            var tables = new List<DataTable>
            {
                DataMapper.ListToDataTable(list)
            };
            var columns = new List<List<ColumnData>>
            {
                new List<ColumnData>
                {
                    new ColumnData { FieldName = "Name", Width = 20, Title = filter.statisticsObj==1?"部门":"名称",  HorizontalAlignment= HorizAlignments.Centered, VerticalAlignment= VertiAlignments.Centered},
                    new ColumnData { FieldName = "TestCount", Width = 20 ,Title = "测验次数" , HorizontalAlignment= HorizAlignments.Centered, VerticalAlignment= VertiAlignments.Centered},
                    new ColumnData { FieldName = "AvgRightRate", Width = 20, Title ="平均正确率" ,HorizontalAlignment= HorizAlignments.Centered, VerticalAlignment= VertiAlignments.Centered},
                    new ColumnData { FieldName = "MaxRightRate", Width = 20, Title = "最高正确率", HorizontalAlignment= HorizAlignments.Centered, VerticalAlignment= VertiAlignments.Centered},
                    new ColumnData { FieldName = "MinRightRate", Width = 20, Title = "最低正确率", HorizontalAlignment= HorizAlignments.Centered, VerticalAlignment= VertiAlignments.Centered},
                    new ColumnData { FieldName = "AvgScore", Width = 40, Title ="平均得分" , HorizontalAlignment= HorizAlignments.Centered, VerticalAlignment= VertiAlignments.Centered},
                    new ColumnData { FieldName = "MaxScore", Width = 20, Title = "最高得分" , HorizontalAlignment= HorizAlignments.Centered, VerticalAlignment= VertiAlignments.Centered},
                    new ColumnData { FieldName = "MinScore", Width = 20, Title ="最低得分", HorizontalAlignment= HorizAlignments.Centered, VerticalAlignment= VertiAlignments.Centered },
                    new ColumnData { FieldName = "SumDuration", Width = 20, Title = "总耗时", HorizontalAlignment= HorizAlignments.Centered, VerticalAlignment= VertiAlignments.Centered},
                  }
            };
            string fileName = string.Empty;
            var excelExport = new ExcelFileExporter();
            byte[] excelByte = excelExport.CreateFile(tables, new List<string>() { "测验统计" }, columns, null, out fileName, "测验统计");
            fileName = "测验统计.xls";
            return File(new MemoryStream(excelByte), "application/ms-excel", fileName);
        }
        #endregion


        #region 段位参数设置

        public ActionResult RankSetting()
        {
            return View();
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public ActionResult RankSettingQuery()
        {
            var filter = BuildQueryFilterEntity<RankSettingFilter>();
            return AjaxGridJson(MockTestBasisService.SearchRankSettingPageLists(filter));
        }

        public ActionResult RankSettingDetail(int id)
        {
            var result = MockTestBasisService.LoadRankSettingDetail(id);
            return Json(new AjaxResult() { Success = true, Data = result }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveRankSetting(RankSettingVO entity)
        {
            entity.id = entity.rankid;
            int count = MockTestBasisService.UpdateRankSetting(entity);
            if (count > 0)
            {
                return Json(new AjaxResult() { Data = entity, Success = true, Message = "修改成功!" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new AjaxResult() { Data = entity, Success = false, Message = "修改失败" }, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region 个人积分
        public ActionResult IndividualPoints()
        {
            return View();
        }

        public ActionResult IndividualPointsQuery(int userTag, string statisticsYear, int statisticsMode)
        {
            var filter = new IndividualPointsFilter()
            {
                UserTag = userTag,
                statisticsYear = statisticsYear,
                statisticsMode = statisticsMode
            };
            var result = MockTestBasisService.SearchIndividualPointsLists(filter);
            return Json(new AjaxResult() { Data = result, Success = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult IndividualPointsExport()
        {
            var filter = SerializeHelper.JsonDeserializeFixed<IndividualPointsFilter>(Request.Form["queryString"]);
            var list = MockTestBasisService.SearchIndividualPointsLists(filter);
            string filename = "";

            var tables = new List<DataTable>
            {
                DataMapper.ListToDataTable(list)
            };

            var columndata = new List<ColumnData>
                {
                    new ColumnData { FieldName = "name", Width = 20, Title = "用户姓名", HorizontalAlignment= HorizAlignments.Centered, VerticalAlignment= VertiAlignments.Centered},
                    new ColumnData { FieldName = "tag_name", Width = 20 ,Title = "用户类型" , HorizontalAlignment= HorizAlignments.Centered, VerticalAlignment= VertiAlignments.Centered},
                };
            switch (filter.statisticsMode)
            {
                case 1:
                    filename = "总积分榜";
                    columndata.Add(
                    new ColumnData { FieldName = "score_total", Width = 20, Title = "积分", HorizontalAlignment = HorizAlignments.Centered, VerticalAlignment = VertiAlignments.Centered }
                        );
                    break;
                case 2:
                    filename = "年度积分榜";
                    columndata.Add(
                   new ColumnData { FieldName = "score_year", Width = 20, Title = "年度积分", HorizontalAlignment = HorizAlignments.Centered, VerticalAlignment = VertiAlignments.Centered }
                       );
                    break;
                case 3:
                    filename = "学霸排行榜";
                    columndata.Add(
                   new ColumnData { FieldName = "exam_accuracyEx", Width = 20, Title = "考试正确率%", HorizontalAlignment = HorizAlignments.Centered, VerticalAlignment = VertiAlignments.Centered }
                       );
                    break;
                default:
                    break;
            }
            var columns = new List<List<ColumnData>>
            {
              columndata
            };
            string name = string.Empty;
            var excelExport = new ExcelFileExporter();
            byte[] excelByte = excelExport.CreateFile(tables, new List<string>() { filename }, columns, null, out name, filename);
            return File(new MemoryStream(excelByte), "application/ms-excel", filename + ".xls");
        }
        #endregion

        public ActionResult GetUserTag()
        {
            var typeDic = CommonEnums.EnumListDic<UserTagEnums>("").Select(_ => new { id = _.Key, name = _.Value }).ToList();
            return Json(new AjaxResult() { Success = true, Data = typeDic }, JsonRequestBehavior.AllowGet);
        }

        #region 团队积分
        public ActionResult TeamPoints()
        {
            return View();
        }

        public ActionResult TeamPointsQuery(int userTag, string statisticsYear, int statisticsMode)
        {
            var filter = new TeamPointsFilter()
            {
                UserTag = userTag,
                statisticsYear = statisticsYear,
                statisticsMode = statisticsMode
            };
            var result = MockTestBasisService.SearchTeamPointsLists(filter);
            return Json(new AjaxResult() { Success = true, Data = result }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TeamPointsExport()
        {
            var filter = SerializeHelper.JsonDeserializeFixed<TeamPointsFilter>(Request.Form["queryString"]);
            var list = MockTestBasisService.SearchTeamPointsLists(filter);
            string filename = "";

            var tables = new List<DataTable>
            {
                DataMapper.ListToDataTable(list)
            };

            var columndata = new List<ColumnData>
                {
                    new ColumnData { FieldName = "name", Width = 20, Title = "用户姓名", HorizontalAlignment= HorizAlignments.Centered, VerticalAlignment= VertiAlignments.Centered},
                    new ColumnData { FieldName = "tag_name", Width = 20 ,Title = "用户类型" , HorizontalAlignment= HorizAlignments.Centered, VerticalAlignment= VertiAlignments.Centered},
                };
            switch (filter.statisticsMode)
            {
                case 1:
                    filename = "年度总积分榜";
                    columndata.Add(
                   new ColumnData { FieldName = "score_year", Width = 20, Title = "年度积分", HorizontalAlignment = HorizAlignments.Centered, VerticalAlignment = VertiAlignments.Centered }
                       );
                    break;
                case 2:
                    filename = "年度人均积分榜";
                    columndata.Add(
                   new ColumnData { FieldName = "score_average", Width = 20, Title = "年度人均积分", HorizontalAlignment = HorizAlignments.Centered, VerticalAlignment = VertiAlignments.Centered }
                       );
                    break;
                default:
                    break;
            }
            var columns = new List<List<ColumnData>>
            {
              columndata
            };

            string name = string.Empty;
            var excelExport = new ExcelFileExporter();
            byte[] excelByte = excelExport.CreateFile(tables, new List<string>() { filename }, columns, null, out name, filename);
            return File(new MemoryStream(excelByte), "application/ms-excel", filename + ".xls");
        }
        #endregion
    }

}