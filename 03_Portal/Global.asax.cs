using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using YZ.Utility.Web;
using System.Web.Optimization;

using YZ.Utility.Web.Mvc;
using YZ.Utility;

namespace LYY.EnterpriseWxMgt.WebHost
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //设置System.Net.ServicePointManager参数以期提升WebRequest的性能
            System.Net.ServicePointManager.Expect100Continue = false;
            //设置以支持 RSA 加验签
            System.Security.Cryptography.RSACryptoServiceProvider.UseMachineKeyStore = true;
            System.Security.Cryptography.DSACryptoServiceProvider.UseMachineKeyStore = true;

            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new ThemeableRazorViewEngine());

            ValueProviderFactories.Factories.Remove(ValueProviderFactories.Factories.OfType<JsonValueProviderFactory>().FirstOrDefault());
            ValueProviderFactories.Factories.Add(new JsonValueProviderFactoryFixed());

            BundleConfig.RegisterBundles(BundleTable.Bundles);
            bool enable = false;
            bool.TryParse(System.Configuration.ConfigurationManager.AppSettings["EnableOptimizations"], out enable);
            BundleTable.EnableOptimizations = enable;
            System.Web.Optimization.Styles.DefaultTagFormat = "<link href=\"{0}?v=" + YZ.Utility.Web.Config.ResourceVersion + "\" rel=\"stylesheet\"/>";
            System.Web.Optimization.Scripts.DefaultTagFormat = "<script src=\"{0}?v=" + YZ.Utility.Web.Config.ResourceVersion + "\"></script>";
        }

        protected void Application_PreRequestHandlerExecute(object sender, EventArgs e)
        {
            var currentCulture = System.Threading.Thread.CurrentThread.CurrentCulture;

            if (currentCulture.Name.StartsWith("fr"))
            {
                currentCulture.DateTimeFormat.SetAllDateTimePatterns(new[] { "yyyy/MM/dd", "yy/MM/dd", "yy.MM.dd", "yy-MM-dd", "yyyy-MM-dd" }, 'd');

                currentCulture.NumberFormat.CurrencyGroupSeparator =
                currentCulture.NumberFormat.PercentGroupSeparator =
                currentCulture.NumberFormat.NumberGroupSeparator = ",";

                currentCulture.NumberFormat.CurrencyDecimalSeparator =
                currentCulture.NumberFormat.PercentDecimalSeparator =
                currentCulture.NumberFormat.NumberDecimalSeparator = ".";
            }
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var ex = Server.GetLastError();
            if (ex != null)
            {
                var httpContext = HttpContext.Current;
                if (ex is HttpException)
                {
                    var httpEx = ex as HttpException;
                    var httpStatusCode = httpEx.GetHttpCode();
                    if (httpStatusCode == 404)
                    {
                        Server.ClearError();
                        Server.TransferRequest(string.Format("/Error/404?requestUrl={0}", httpContext.Request.Url.PathAndQuery));
                        return;
                    }
                }
            }
        }
    }
}
