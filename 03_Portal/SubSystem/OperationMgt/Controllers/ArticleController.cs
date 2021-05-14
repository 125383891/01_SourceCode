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

namespace LYY.EnterpriseWxMgt.OperationMgt.Portal.Controllers
{
    public class ArticleController : BaseController
    {
        private ArticleBasisService ArticleBasisService { get { return new ArticleBasisService(); } }

        private ArticleDepartmentService ArticleDepartmentService { get { return new ArticleDepartmentService(); } }

        private ArticleReplyService ArticleReplyService { get { return new ArticleReplyService(); } }

        private DepartmentService DepartmentService { get { return new DepartmentService(); } }

        [UserAuthorize(FunctionKeys.OperationMgt_Article_List_View)]
        public ActionResult List()
        {
            return View();
        }

        [UserAuthorize(FunctionKeys.OperationMgt_Article_Edit_View)]
        public ActionResult Edit(int? articleId)
        {
            ViewBag.ArticleId = articleId;
            return View();
        }

        [UserAuthorize(FunctionKeys.OperationMgt_Article_ArticleReplyList_View)]
        public ActionResult ArticleReplyList(int articleId)
        {
            ViewBag.ArticleId = articleId;
            return View();
        }

        [UserAuthorize(FunctionKeys.OperationMgt_Article_List_View)]
        [HttpPost]
        public JsonResult Query()
        {
            QF_Article filter = BuildQueryFilterEntity<QF_Article>();
            return AjaxGridJson(ArticleBasisService.SearchPageList(filter));
        }

        [UserAuthorize(FunctionKeys.OperationMgt_Article_Edit_View)]
        [HttpPost]
        public JsonResult ArticleInsert(ArticleVO articleVO)
        {
            articleVO.Content = HttpUtility.UrlDecode(articleVO.Content);
            int articleId = ArticleBasisService.Insert(articleVO);
            int agentId = 0;
            if (articleVO.Application == ArticleEnums.LegalComplianceApplication)
            {
                agentId = int.Parse(AppSettingManager.GetSetting("WorkWXConfig", "LegalComplianceApplicationAgentId"));
            }
            else if (articleVO.Application == ArticleEnums.ExpertManagement)
            {
                agentId = int.Parse(AppSettingManager.GetSetting("WorkWXConfig", "ExpertManagementAgentId"));
            }
            // 微信推送消息
            new RestClient().Post(ConfigurationManager.AppSettings["WxMessageUrl"].ToString(), new
            {
                agentId = agentId,
                content = string.Format("新推文：{0}", articleVO.Title),
                partyIds = articleVO.ArticleDepartmentList != null && articleVO.ArticleDepartmentList.Count >0 
                ? string.Join("|", articleVO.ArticleDepartmentList.Select(p => p.DepartmentId)) : "",
                userIds = articleVO.UserIds
            }, RequestFormat.Json);
            return Json(new AjaxResult() { Success = true, Data = articleId });
        }

        [UserAuthorize(FunctionKeys.OperationMgt_Article_Edit_View)]
        [HttpPost]
        public JsonResult ArticleUpdate(ArticleVO articleVO)
        {
            articleVO.Content = HttpUtility.UrlDecode(articleVO.Content);
            ArticleBasisService.Update(articleVO);
            return Json(new AjaxResult() { Success = true, Data = true });
        }

        [UserAuthorize(FunctionKeys.OperationMgt_Article_Edit_View)]
        [HttpGet]
        public JsonResult GetById(int articleId)
        {
            var temp = ArticleBasisService.GetById(articleId);
            List<string> userIds = ArticleBasisService.SearchArticleUserList(articleId);
            temp.ArticleDepartmentList = ArticleDepartmentService.ListByArticleId(articleId);
            temp.UserIds = string.Join("|", userIds);
            return Json(new AjaxResult() { Success = true, Data = temp }, JsonRequestBehavior.AllowGet);
        }

        [UserAuthorize(FunctionKeys.OperationMgt_Article_List_View)]
        [HttpGet]
        public JsonResult ArticleDelete(int articleId)
        {
            ArticleBasisService.Delete(articleId);
            return Json(new AjaxResult() { Success = true, Data = true }, JsonRequestBehavior.AllowGet);
        }

        [UserAuthorize(FunctionKeys.OperationMgt_Article_ArticleReplyList_View)]
        [HttpPost]
        public JsonResult ArticleReplyQuery()
        {
            QF_ArticleReply filter = BuildQueryFilterEntity<QF_ArticleReply>();
            return AjaxGridJson(ArticleReplyService.SearchPageList(filter));
        }

        [UserAuthorize(FunctionKeys.OperationMgt_Article_ArticleReplyList_View)]
        [HttpGet]
        public JsonResult ArticleReplyDelete(int articleReplyId)
        {
            ArticleReplyService.Delete(articleReplyId);
            return Json(new AjaxResult() { Success = true, Data = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult DepartmentList()
        {
            return Json(new AjaxResult() { Success = true, Data = DepartmentService.List() }, JsonRequestBehavior.AllowGet);
        }
    }
}