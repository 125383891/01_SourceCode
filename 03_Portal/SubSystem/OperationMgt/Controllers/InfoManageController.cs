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
using LYY.InfoManageService.Service;
using LYY.InfoManage.Entity.ViewModel;
using LYY.InfoManage.Entity.Enums;
using LYY.Common.Entity.Enums;

namespace LYY.EnterpriseWxMgt.OperationMgt.Portal.Controllers
{
    /// <summary>
    /// 模拟测验
    /// </summary>
    public class InfoManageController : BaseController
    {
        private InfoManageBasisService _basisService { get { return new InfoManageBasisService(); } }
        [UserAuthorize(FunctionKeys.OperationMgt_Info_Bulletin_List_View)]
        public ActionResult Bulletin()
        {
            ViewBag.EndTime = DateTime.Now.ToString("yyyy-MM-dd");
            ViewBag.StartTime = DateTime.Now.AddYears(-1).ToString("yyyy-MM-dd");
            return View();
        }

        [UserAuthorize(FunctionKeys.OperationMgt_Info_Bulletin_List_View)]
        public ActionResult BulletinQuery()
        {
            BulletinFilter filter = BuildQueryFilterEntity<BulletinFilter>();
            return AjaxGridJson(_basisService.SearchBulletinPageLists(filter));
        }

        [UserAuthorize(FunctionKeys.OperationMgt_Info_Bulletin_List_View)]
        public ActionResult BulletinDetail(int id)
        {
            var result = _basisService.LoadBulletin(id);
            return Json(new AjaxResult() { Success = true, Data = result }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BulletinModel()
        {
            var result = new BulletinVO()
            {
                is_delete = 0,
                create_time = DateTime.Now,
                user_id = CurrUser.UserID
            };
            return Json(new AjaxResult() { Success = true, Data = result }, JsonRequestBehavior.AllowGet);
        }


        [UserAuthorize(FunctionKeys.OperationMgt_Info_Bulletin_List_View)]
        public ActionResult SaveBulletin(BulletinVO entity)
        {
            entity.id = entity.bulleid;
            if (entity.id > 0)
            {
                var result = _basisService.UpdateBulletion(entity);
                return Json(new AjaxResult() { Success = true, Message = string.Format("修改成功!") }, JsonRequestBehavior.AllowGet);

            }
            else
            {
                entity.user_id = CurrUser.UserID;
                var result = _basisService.InsertBulletion(entity);
                return Json(new AjaxResult() { Success = true, Message = string.Format("修改成功!") }, JsonRequestBehavior.AllowGet);
            }
        }

        [UserAuthorize(FunctionKeys.OperationMgt_Info_Bulletin_List_View)]
        public ActionResult DeleteBulletin(int[] ids)
        {
            int i = 0;
            foreach (var id in ids)
            {
                i += _basisService.DeleteBulletin(id);
            }
            return Json(new AjaxResult() { Success = true, Message = string.Format("{0}条数据删除成功!", i) }, JsonRequestBehavior.AllowGet);
        }
        #region 枚举
        public ActionResult GetTypeDic()
        {
            var typeDic = CommonEnums.EnumListDic<BulletinType>("").Select(_ => new { id = _.Key, name = _.Value }).ToList();
            return Json(new AjaxResult() { Success = true, Data = typeDic }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }

}