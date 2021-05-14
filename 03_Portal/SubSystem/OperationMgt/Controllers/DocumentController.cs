using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YZ.PortalBase;
using LYY.PlanService.Service;
using YZ.Utility;
using LYY.Plan.Entity;
using LYY.DocumentService.Service;
using LYY.Document.Entity;
using LYY.EnterpriseWxMgt.OperationMgt.Portal.Models;
using YZ.Utility.Web;
using LYY.Common.Entity;

namespace LYY.EnterpriseWxMgt.OperationMgt.Portal.Controllers
{
    public class DocumentController : BaseController
    {
        private FolderBasisService FolderBasisService { get { return new FolderBasisService(); } }
        private DocumentBasisService DocumentBasisService { get { return new DocumentBasisService(); } }

        [UserAuthorize(FunctionKeys.OperationMgt_Document_List_View)]
        public ActionResult List()
        {
            return View();
        }

        [UserAuthorize(FunctionKeys.OperationMgt_Document_List_View)]
        [HttpGet]
        public JsonResult SearchTreeList()
        {
            List<FolderVO> list = FolderBasisService.List();
            if (list == null || list.Count == 0)
            {
                return Json(new AjaxResult() { Success = true, Data = new List<FolderVO>() }, JsonRequestBehavior.AllowGet);
            }
            return Json(this.RecursiveQueryList(list), JsonRequestBehavior.AllowGet);
        }

        [UserAuthorize(FunctionKeys.OperationMgt_Document_List_View)]
        [HttpGet]
        public JsonResult SearchDoucmentList(int folderId, int? usertag)
        {
            var folderList = FolderBasisService.ListByParentId(folderId);
            var documentList = DocumentBasisService.ListByFolderId(folderId, usertag);
            List<DocumentComboVO> resultList = new List<DocumentComboVO>();
            if (folderList != null && folderList.Count > 0)
            {
                resultList.AddRange(folderList.Select(p => p.ToDocumentComboVO()));
            }
            if (documentList != null && documentList.Count > 0)
            {
                resultList.AddRange(documentList.Select(p => p.ToDocumentComboVO()));
            }
            //resultList.ForEach(item =>
            //{
            //    if (item.Type == DocumentEnums.File)
            //    {
            //        var temp = item.Name.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            //        if (temp.Length > 0)
            //        {
            //            item.Name = item.Name.Replace("."+temp[temp.Length - 1], "");
            //        }
            //    }
            //});
            return Json(new AjaxResult() { Success = true, Data = resultList }, JsonRequestBehavior.AllowGet);
        }

        [UserAuthorize(FunctionKeys.OperationMgt_Document_List_View)]
        [HttpGet]
        public JsonResult GetById(int folderId)
        {
            return Json(new AjaxResult() { Success = true, Data = FolderBasisService.GetById(folderId) }, JsonRequestBehavior.AllowGet);
        }

        [UserAuthorize(FunctionKeys.OperationMgt_Document_List_View)]
        [HttpPost]
        public JsonResult FolderInsert(FolderVO folderVO)
        {
            FolderBasisService.Insert(folderVO);
            return Json(new AjaxResult() { Success = true, Data = true });
        }

        [UserAuthorize(FunctionKeys.OperationMgt_Document_List_View)]
        [HttpPost]
        public JsonResult FolderUpdate(FolderVO folderVO)
        {
            FolderBasisService.Update(folderVO);
            return Json(new AjaxResult() { Success = true, Data = true });
        }

        [UserAuthorize(FunctionKeys.OperationMgt_Document_List_View)]
        [HttpGet]
        public JsonResult FolderDelete(int folderId)
        {
            FolderBasisService.Delete(folderId);
            return Json(new AjaxResult() { Success = true, Data = true }, JsonRequestBehavior.AllowGet);
        }

        [UserAuthorize(FunctionKeys.OperationMgt_Document_List_View)]
        [HttpPost]
        public JsonResult DocumentInsert(DocumentVO documentVO)
        {
            DocumentBasisService.Insert(documentVO);
            return Json(new AjaxResult() { Success = true, Data = true });
        }
        [UserAuthorize(FunctionKeys.OperationMgt_Document_List_View)]
        [HttpPost]
        public JsonResult DocumentUpdate(DocumentEntity entity)
        {
            entity.Id = entity.ParamEx;
            DocumentBasisService.Update(entity);
            return Json(new AjaxResult() { Success = true, Data = entity });
        }

        [UserAuthorize(FunctionKeys.OperationMgt_Document_List_View)]
        public JsonResult GetDocumentById(int id)
        {
            var result = DocumentBasisService.DocumentById(id);
            return Json(new AjaxResult() { Success = true, Data = result });
        }

        [UserAuthorize(FunctionKeys.OperationMgt_Document_List_View)]
        [HttpGet]
        public JsonResult DocumentDelete(int docuemntId)
        {
            DocumentBasisService.Delete(docuemntId);
            return Json(new AjaxResult() { Success = true, Data = true }, JsonRequestBehavior.AllowGet);
        }

        private List<JsTreeModel> RecursiveQueryList(List<FolderVO> entities)
        {
            List<JsTreeModel> treeList = new List<JsTreeModel>();
            foreach (var item in entities)
            {
                JsTreeModel jsTreeModel = new JsTreeModel()
                {
                    id = item.BusinessId.ToString(),
                    parent = item.ParentId.HasValue ? item.ParentId.Value.ToString() : "#",
                    type = "default",
                    text = item.Name,
                    state = new JsTreeState
                    {
                        // 默认打开第一个节点，并且默认选择第一个节点
                        opened = !item.ParentId.HasValue,
                        selected = !item.ParentId.HasValue,
                        disabled = false
                    },
                    a_attr = "<a>编辑</a><a>删除</a>"
                };
                treeList.Add(jsTreeModel);
            }
            return treeList;

        }
    }
}