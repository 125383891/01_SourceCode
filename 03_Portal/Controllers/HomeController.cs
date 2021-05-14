using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YZ.PortalBase;
using YZ.Utility.Web;

namespace LYY.EnterpriseWxMgt.WebHost.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (AuthManager.ReadUserInfo() != null)
            {
                return Redirect("/OperationMgt/System/HomePage");
            }
            else
            {
                return Redirect("/OperationMgt/Login/Index");
            }

        }
    }
}