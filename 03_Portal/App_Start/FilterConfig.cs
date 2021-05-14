using System.Web;
using System.Web.Mvc;

using YZ.Utility.Web;

namespace LYY.EnterpriseWxMgt.WebHost
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new WebHandleErrorAttribute());
        }
    }
}
