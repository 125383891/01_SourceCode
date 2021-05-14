using LYY.AuthCenter.Entity;
using LYY.AuthCenterService.Service;
using LYY.Common.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YZ.PortalBase;
using YZ.Utility;
using YZ.Utility.Web;

namespace LYY.EnterpriseWxMgt.OperationMgt.Portal.Controllers
{
    public class SystemController : BaseController
    {
        private UserService UserService { get { return new UserService(); } }

        private RoleService RoleService { get { return new RoleService(); } }

        private MenuService MenuService { get { return new MenuService(); } }

        private RoleFunctionService RoleFunctionService { get { return new RoleFunctionService(); } }

        // GET: System
        public ActionResult HomePage()
        {
            return View();
        }

        [UserAuthorize(FunctionKeys.OperationMgt_System_RoleList_View)]
        public ActionResult RoleList()
        {
            return View();
        }

        [UserAuthorize(FunctionKeys.OperationMgt_System_UserList_View)]
        public ActionResult UserList()
        {
            return View();
        }

        [UserAuthorize(FunctionKeys.OperationMgt_System_RoleList_View)]
        public ActionResult RoleFunction(int id)
        {
            var roleFunctionList = RoleFunctionService.GetRoleFunctionByRoleId(id);
            ViewBag.RoleFunctionList = JsonConvert.SerializeObject(roleFunctionList);
            return View(id);
        }

        [UserAuthorize(FunctionKeys.OperationMgt_System_UserList_View)]
        [HttpPost]
        public JsonResult SearchUserList()
        {
            var filter = BuildQueryFilterEntity<QF_User>();
            return AjaxGridJson(UserService.SearchPageList(filter));
        }

        [UserAuthorize(FunctionKeys.OperationMgt_System_UserList_View)]
        [HttpGet]
        public JsonResult GetByUserId(int id)
        {
            return Json(this.Ok(UserService.GetById(id)), JsonRequestBehavior.AllowGet);
        }

        [UserAuthorize(FunctionKeys.OperationMgt_System_UserList_View)]
        [HttpPost]
        public JsonResult InserUser(UserEntity userEntity)
        {
            string encrptedPassword = AuthManager.EncryptPassword(userEntity.LoginPassword);
            userEntity.LoginPassword = encrptedPassword;
            UserService.Insert(userEntity, AuthManager.ReadUserInfo());
            return Json(this.Ok(true));
        }

        [UserAuthorize(FunctionKeys.OperationMgt_System_UserList_View)]
        [HttpPost]
        public JsonResult UpdateUser(UserEntity userEntity)
        {
            UserService.Update(userEntity, AuthManager.ReadUserInfo());
            return Json(this.Ok(true));
        }

        [UserAuthorize(FunctionKeys.OperationMgt_System_UserList_View)]
        [HttpPost]
        public JsonResult UpdateUserStatus(int userId, CommonStatus status)
        {
            // 更改用户状态启用/禁用/删除
            UserService.UpdateUserStatus(new UserEntity()
            {
                Id = userId,
                CommonStatus = status
            }, AuthManager.ReadUserInfo());
            return Json(this.Ok(true));
        }

        [UserAuthorize(FunctionKeys.OperationMgt_System_UserList_View)]
        [HttpPost]
        public JsonResult UpdatePassword(UserUpdatePasswordVo userUpdate)
        {
            userUpdate.OldPassword = AuthManager.EncryptPassword(userUpdate.OldPassword);
            userUpdate.NewPassword = AuthManager.EncryptPassword(userUpdate.NewPassword);
            UserService.UpdatePassword(userUpdate, AuthManager.ReadUserInfo());
            return Json(this.Ok(true));
        }

        [UserAuthorize(FunctionKeys.OperationMgt_System_RoleList_View)]
        [HttpGet]
        public JsonResult SearchRoleList()
        {
            var result = RoleService.SearchPageList(new QueryFilter() { PageIndex = 0, PageSize = int.MaxValue });
            return Json(this.Ok(result.data), JsonRequestBehavior.AllowGet);
        }

        [UserAuthorize(FunctionKeys.OperationMgt_System_RoleList_View)]
        [HttpPost]
        public JsonResult SearchRolePageList()
        {
            var filter = BuildQueryFilterEntity<QueryFilter>();
            var result = RoleService.SearchPageList(filter);
            return AjaxGridJson(result);
        }

        [UserAuthorize(FunctionKeys.OperationMgt_System_RoleList_View)]
        [HttpGet]
        public JsonResult GetByRoleId(int id)
        {
            return Json(this.Ok(RoleService.GetById(id)), JsonRequestBehavior.AllowGet);
        }

        [UserAuthorize(FunctionKeys.OperationMgt_System_RoleList_View)]
        [HttpPost]
        public JsonResult InserRole(RoleEntity role)
        {
            RoleService.Insert(role, AuthManager.ReadUserInfo());
            return Json(this.Ok(true));
        }

        [UserAuthorize(FunctionKeys.OperationMgt_System_RoleList_View)]
        [HttpPost]
        public JsonResult UpdateRole(RoleEntity role)
        {
            RoleService.Update(role, AuthManager.ReadUserInfo());
            return Json(this.Ok(true));
        }

        [UserAuthorize(FunctionKeys.OperationMgt_System_RoleList_View)]
        [HttpGet]
        public JsonResult DeleteRole(int id)
        {
            RoleService.Delete(id);
            return Json(this.Ok(true), JsonRequestBehavior.AllowGet);
        }

        [UserAuthorize(FunctionKeys.OperationMgt_System_RoleList_View)]
        [HttpPost]
        public JsonResult BatchInsertRoleFunctionList(RoleFunctionVO roleFunction)
        {
            roleFunction.Creator = AuthManager.ReadUserInfo().UserSysNo;
            RoleFunctionService.BatchInserFunction(roleFunction);
            return Json(this.Ok(true));
        }

        [UserAuthorize(FunctionKeys.OperationMgt_System_RoleList_View)]
        [HttpGet]
        public JsonResult SearchMenuFunctionList()
        {
            var list = MenuService.SearchMenuFunctionList();
            return Json(this.Ok(list), JsonRequestBehavior.AllowGet);
        }

        private AjaxResult Ok(object data)
        {
            return new AjaxResult() { Success = true, Data = data };
        }
    }
}