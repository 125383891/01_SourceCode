using YZ.Utility.Web.Error;
using System;
using System.ServiceModel;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using YZ.Utility;

namespace LYY.EnterpriseWxMgt.WebHost
{
    public class WebHandleErrorAttribute : CustomHandleErrorAttribute
    {
        protected override bool HandleException(Exception ex)
        {
            if (!IsBizException(ex))
            {
                if (!(ex is FaultException))
                {
                    string logSource = "ProductDeliveryPortal";
                    var currentArea = HttpContext.Current.Request.RequestContext.RouteData.DataTokens["area"];
                    string areaName = (currentArea != null ? currentArea.ToString() : "CommonMgt");
                    try
                    {
                        string requestUrl = HttpContext.Current.Request.RawUrl;
                        string message = GetExceptionInfo(ex, true);
                        Logger.WriteLog(message, logSource, areaName);
                    }
                    catch (Exception logException)
                    {
                        Logger.WriteLog(logException.ToString(), logSource);
                        Logger.WriteLog(ex.ToString(), logSource);
                    }
                }
            }
            return true;
        }

        private bool IsBizException(Exception ex)
        {
            if (ex is BusinessException
                || ((ex is FaultException) && ((FaultException)ex).Code.Name == "1")
                || (ex is HttpRequestValidationException))
            {
                return true;
            }
            return false;
        }

        private string GetExceptionInfo(Exception ex, bool isLocalRequest)
        {
            if (IsBizException(ex))
            {
                string errMsg = ex.Message;

                if (ex is HttpRequestValidationException)
                {
                    errMsg = "检测到非法输入（例如：html标签），不能提交。";
                }

                return errMsg;
            }
            if (isLocalRequest)
            {
                if (ex is FaultException)    // throw on wcf service
                {
                    return ((FaultException)ex).Reason.ToString();
                }
                else  // throw on web portal
                {
                    return ex.ToString();
                }
            }
            else
            {
                return ex.Message;
                //return "系统发生异常，请稍后再试。";
            }
        }


        protected override System.Web.Mvc.ActionResult BuildAjaxJsonActionResult(Exception ex, bool isLocalRequest)
        {
            int code = 0;
            var bizEx = ex as BusinessException;
            if (bizEx != null)
            {
                code = bizEx.Code;
            }
            JsonResult jr = new JsonResult();
            jr.Data = new AjaxResult() { Success = false, Message = GetExceptionInfo(ex, isLocalRequest), Code = code };
            jr.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return jr;
        }

        protected override System.Web.Mvc.ActionResult BuildAjaxHtmlActionResult(Exception ex, bool isLocalRequest)
        {
            string message = GetExceptionInfo(ex, isLocalRequest);
            StringBuilder sb = new StringBuilder();
            sb.Append("<div id=\"service_Error_Message_Panel\">");
            sb.AppendFormat("<input id=\"errorMessage\" type=\"hidden\" value=\"{0}\" />", HttpUtility.HtmlEncode(message));
            sb.Append("</div>");
            return new ContentResult
            {
                Content = sb.ToString(),
                ContentEncoding = Encoding.UTF8,
                ContentType = "text/html"
            };
        }

        protected override System.Web.Mvc.ActionResult BuildAjaxXmlActionResult(Exception ex, bool isLocalRequest)
        {
            string message = GetExceptionInfo(ex, isLocalRequest);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<?xml version=\"1.0\"?>");
            sb.AppendLine("<result>");
            sb.AppendLine("<error>true</error>");
            sb.AppendLine("<message>" + message.Replace("<", "&lt;").Replace(">", "&gt;") + "</message>");
            sb.AppendLine("</result>");
            return new ContentResult
            {
                Content = sb.ToString(),
                ContentEncoding = Encoding.UTF8,
                ContentType = "application/xml"
            };
        }

        protected override ActionResult BuildWebPageActionResult(Exception ex, bool isLocalRequest, ExceptionContext filterContext)
        {
            string errorStr = GetExceptionInfo(ex, isLocalRequest);
            Exception exception = new Exception(errorStr, ex);

            string controller = filterContext.RouteData.Values["controller"].ToString();
            string action = filterContext.RouteData.Values["action"].ToString();
            exception.HelpLink = IsBizException(ex) ? "BizEx" : "";

            var controllerViewData = filterContext.Controller.ViewData;
            var viewData = new ViewDataDictionary(controllerViewData)
            {
                Model = new HandleErrorInfo(exception, controller, action)
            };

            ViewResult viewResult = new ViewResult
            {
                ViewName = this.View,
                MasterName = this.Master,
                ViewData = viewData,
                TempData = filterContext.Controller.TempData
            };

            return viewResult;
        }
    }
}