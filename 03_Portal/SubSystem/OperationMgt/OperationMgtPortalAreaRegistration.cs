using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LYY.EnterpriseWxMgt.OperationMgt.Portal
{
    public class OperationMgtPortalAreaRegistration : AreaRegistration
    {
        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "OperationMgt_default",
                "OperationMgt/{controller}/{action}/{id}",
                new
                {
                    controller = "System",
                    action = "HomePage",
                    area = "OperationMgt",
                    id = ""
                },
                new[] { "LYY.EnterpriseWxMgt.OperationMgt.Portal.Controllers" }
            );
        }

        public override string AreaName
        {
            get { return "OperationMgt"; }
        }
    }
}