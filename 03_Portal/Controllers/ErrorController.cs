using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YZ.Utility;

namespace LYY.EnterpriseWxMgt.WebHost.Controllers
{
    public class ErrorController : Controller
    {
        [ActionName("404")]
        public ActionResult Error404(string requestUrl)
        {
            Response.TrySkipIisCustomErrors = true;
            Response.StatusCode = 404;
            Response.StatusDescription = "Page not found";
            if (string.IsNullOrEmpty(requestUrl))
            {
                requestUrl = Request.QueryString["aspxerrorpath"];
            }
            ViewBag.RequestUrl = requestUrl;
            return View("~/Views/Shared/Error404.cshtml");
        }

        /// <summary>
        /// 友好显示其他异常信息
        /// </summary>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public ActionResult Error500(string errorMsg)
        {
            var ex = new BusinessException(errorMsg);
            ex.HelpLink = "BizEx";
            return View("~/Views/Shared/Error.cshtml", new HandleErrorInfo(ex, "Error", "Error500"));
        }
    }
}