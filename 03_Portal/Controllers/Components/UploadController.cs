using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YZ.PortalBase;
using YZ.Utility;
using System.Configuration;
using System.IO;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;

namespace LYY.EnterpriseWxMgt.WebHost.Controllers
{
    public class UploadController : Controller
    {
        private string RemoteUploadFile(HttpPostedFileBase httpPostedFileBase)
        {
            HttpWebRequest request = WebRequest.Create(ConfigurationManager.AppSettings["UploadUrl"].ToString()) as HttpWebRequest;
            CookieContainer cookieContainer = new CookieContainer();
            request.CookieContainer = cookieContainer;
            request.AllowAutoRedirect = true;
            request.Method = "POST";
            string boundary = DateTime.Now.Ticks.ToString("X"); // 随机分隔线
            request.ContentType = "multipart/form-data;charset=utf-8;boundary=" + boundary;
            byte[] itemBoundaryBytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "\r\n");
            byte[] endBoundaryBytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");
            string fileName = httpPostedFileBase.FileName;

            //请求头部信息 
            StringBuilder sbHeader = new StringBuilder(string.Format("Content-Disposition:form-data;name=\"file\";filename=\"{0}\"\r\nContent-Type:application/octet-stream\r\n\r\n", fileName));
            byte[] postHeaderBytes = Encoding.UTF8.GetBytes(sbHeader.ToString());

            BinaryReader fs = new BinaryReader(httpPostedFileBase.InputStream);
            byte[] bArr = new byte[httpPostedFileBase.ContentLength];
            fs.Read(bArr, 0, bArr.Length);
            fs.Close();

            Stream postStream = request.GetRequestStream();
            postStream.Write(itemBoundaryBytes, 0, itemBoundaryBytes.Length);
            postStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);
            postStream.Write(bArr, 0, bArr.Length);
            postStream.Write(endBoundaryBytes, 0, endBoundaryBytes.Length);
            postStream.Close();

            //发送请求并获取相应回应数据
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            //直到request.GetResponse()程序才开始向目标网页发送Post请求
            Stream instream = response.GetResponseStream();
            StreamReader sr = new StreamReader(instream, Encoding.UTF8);
            //返回结果网页（html）代码
            string content = sr.ReadToEnd();
            return content;
        }

        [HttpPost]
        public ContentResult UploadUeditor()
        {
            var result = this.RemoteUploadFile(Request.Files[0]);
            JObject @object = JObject.Parse(result);
            if (@object["code"].ToString().Equals("1"))
            {
                var temp = new
                {
                    state = "SUCCESS",
                    url = string.Format("{0}{1}", ConfigurationManager.AppSettings["PreviewUrl"].ToString(), @object["data"]["newName"]),
                    title = @object["data"]["newName"],
                    original = @object["data"]["originName"],
                    error = ""
                };
                return Content(JsonConvert.SerializeObject(temp));
            }
            else
            {
                var temp = new
                {
                    state = "ERROR",
                    url = string.Empty,
                    title = string.Empty,
                    original = string.Empty,
                    error = @object["error"]
                };
                return Content(JsonConvert.SerializeObject(temp));
            }
        }
    }
}