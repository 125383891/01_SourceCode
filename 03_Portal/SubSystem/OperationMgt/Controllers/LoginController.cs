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
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoginValidationCode()
        {
            string code = ValidationCodeHelper.CreateValidateCode(5);
            byte[] bytes = ValidationCodeHelper.CreateValidateGraphic(code, 52);
            CookieHelper.SaveCookie<string>(AuthManager.LOGIN_VERIFYCODE_COOKIE, code.Trim());

            return File(bytes, @"image/jpeg");
        }

        [HttpPost]
        public JsonResult Login(string account, string password, string verifycode)
        {
            string encrptedPassword = AuthManager.EncryptPassword(password);
            var loginUser = AuthManager.Login(account, encrptedPassword, verifycode, true);

            return Json(new AjaxResult
            {
                Success = true,
                Data = new
                {
                    UserInfo = loginUser
                }
            });

        }
        public ActionResult Logout()
        {
            var loginUser = AuthManager.ReadUserInfo();
            AuthManager.Logout();

            string returnurl = Request.QueryString["returnurl"];
            return Redirect(System.Configuration.ConfigurationManager.AppSettings["LoginUrl"] + "?returnurl=" + returnurl);
        }
    }
}