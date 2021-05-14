using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Optimization;

namespace LYY.EnterpriseWxMgt.WebHost
{
    public class CssResourceBundle : Bundle
    {
        public CssResourceBundle(string virtualPath)
        : base(virtualPath)
        {
            this.Builder = new CssResourceBuilder();
        }

        public CssResourceBundle(string virtualPath, string cdnPath)
        : base(virtualPath, cdnPath)
        {
            this.Builder = new CssResourceBuilder();
        }
    }

    public class CssResourceBuilder : IBundleBuilder
    {
        public virtual string BuildBundleContent(Bundle bundle, BundleContext context, IEnumerable<BundleFile> files)
        {
            var content = new StringBuilder();
            foreach (var file in files)
            {
                FileInfo f = new FileInfo(HttpContext.Current.Server.MapPath(file.VirtualFile.VirtualPath));

                string readFile = File.ReadAllText(f.FullName);
                content.Append(readFile);
                content.AppendLine(Environment.NewLine);
            }
            return content.ToString();
        }
    }
}