using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YZ.PortalBase;
using YZ.Utility;
using YZ.Utility.Web;
using LYY.Common.Entity;
using System.Data;
using LYY.Common.Entity.Enums;
using LYY.VotingEventsService.Service;
using LYY.VotingEvents.Entity.ViewModel;
using System.IO;

namespace LYY.EnterpriseWxMgt.OperationMgt.Portal.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class VotingEventsController : BaseController
    {
        private VotingEventsBasisService _basisService => new VotingEventsBasisService();

        [UserAuthorize(FunctionKeys.OperationMgt_VotingEvents_Vote_List_View)]
        public ActionResult Vote()
        {
            return View();
        }

        [UserAuthorize(FunctionKeys.OperationMgt_VotingEvents_Vote_List_View)]
        public ActionResult VoteQuery()
        {
            VoteFilter filter = BuildQueryFilterEntity<VoteFilter>();
            return AjaxGridJson(_basisService.SearchVotePageLists(filter));
        }

        [UserAuthorize(FunctionKeys.OperationMgt_VotingEvents_Vote_List_View)]
        public ActionResult VoteDetail(int id)
        {
            var result = _basisService.LoadVote(id);
            return Json(new AjaxResult() { Success = true, Data = result }, JsonRequestBehavior.AllowGet);
        }

        [UserAuthorize(FunctionKeys.OperationMgt_VotingEvents_Vote_List_View)]
        public ActionResult SaveVote(VoteVO entity)
        {
            entity.id = entity.voteid;
            _basisService.SaveVote(entity);
            return Json(new AjaxResult() { Success = true, Message = string.Format("保存成功!") }, JsonRequestBehavior.AllowGet);
        }

        [UserAuthorize(FunctionKeys.OperationMgt_VotingEvents_Vote_List_View)]
        public ActionResult DeleteVote(int[] ids)
        {
            int i = 0;
            foreach (var id in ids)
            {
                i += _basisService.DeleteVote(id);
            }
            return Json(new AjaxResult() { Success = true, Message = string.Format("{0}条数据删除成功!", i) }, JsonRequestBehavior.AllowGet);
        }

        [UserAuthorize(FunctionKeys.OperationMgt_VotingEvents_Vote_List_View)]
        public ActionResult DeleteVoteItem(int id)
        {
            _basisService.DeleteVoteItem(id);
            return Json(new AjaxResult() { Success = true, Message = string.Format("数据删除成功!") }, JsonRequestBehavior.AllowGet);
        }

        #region 投票活动统计

        [UserAuthorize(FunctionKeys.OperationMgt_VotingEvents_VoteStatistics_List_View)]
        public ActionResult VoteStatistics()
        {
            return View();
        }
        [UserAuthorize(FunctionKeys.OperationMgt_VotingEvents_VoteStatistics_List_View)]
        public ActionResult VoteStatisticsQuery(int? voteid)
        {
            //VoteFilter filter = BuildQueryFilterEntity<VoteFilter>();
            VoteFilter filter = new VoteFilter() { voteid = voteid };
            var result = _basisService.StatisticsVoteItem(filter);
            return Json(new AjaxResult() { Success = true, Data = result }, JsonRequestBehavior.AllowGet);

        }

        [UserAuthorize(FunctionKeys.OperationMgt_VotingEvents_VoteStatistics_List_View)]
        public ActionResult ExportUserVoteStatistics()
        {
            var filter = SerializeHelper.JsonDeserializeFixed<ExportFilter>(Request.Form["queryString"]);
            var result = _basisService.SearchUserVoteItem(filter.voteid);
            var tables = new List<DataTable>
            {
                DataMapper.ListToDataTable(result)
            };
            var columns = new List<List<ColumnData>>
            {
                new List<ColumnData>
                {
                    new ColumnData { FieldName = "content", Width = 20 ,Title = "选项内容" , HorizontalAlignment= HorizAlignments.Centered, VerticalAlignment= VertiAlignments.Centered},
                    new ColumnData { FieldName = "ext_val", Width = 20, Title ="选项备注" ,HorizontalAlignment= HorizAlignments.Centered, VerticalAlignment= VertiAlignments.Centered},
                    new ColumnData { FieldName = "username", Width = 20, Title = "用户姓名", HorizontalAlignment= HorizAlignments.Centered, VerticalAlignment= VertiAlignments.Centered},
                    new ColumnData { FieldName = "create_time", Width = 20, Title = "投票时间", ValueFormat="yyyy-MM-dd", HorizontalAlignment= HorizAlignments.Centered, VerticalAlignment= VertiAlignments.Centered},
                 }
            };
            string fileName = string.Empty;
            var excelExport = new ExcelFileExporter();
            byte[] excelByte = excelExport.CreateFile(tables, new List<string>() { "投票明细" }, columns, null, out fileName, "投票明细");
            fileName = "投票明细.xls";
            return File(new MemoryStream(excelByte), "application/ms-excel", fileName);


        }


        public ActionResult GetVoteAll()
        {
            var result = _basisService.GetVoteAll();
            return Json(new AjaxResult() { Success = true, Data = result }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }

}