using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LYY.EnterpriseWxMgt.OperationMgt.Portal.Models
{
    public class JsTreeModel
    {
        public string id { get; set; }

        public string type { get; set; }

        public string text { get; set; }

        public string parent { get; set; }

        public bool children { get; set; }

        public JsTreeState state { get; set; }

        public object a_attr { get; set; }
    }

    public class JsTreeState
    {
        public bool opened { get; set; }

        public bool disabled { get; set; }

        public bool selected { get; set; }

        public bool @checked { get; set; }
    }
}