using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YZ.Utility;

namespace LYY.AuthCenter.Entity
{
    public class QF_User: QueryFilter
    {
        /// <summary>
        /// 登录用户名
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// 用户真实姓名
        /// </summary>
        public string UserFullName { get; set; }
    }
}
