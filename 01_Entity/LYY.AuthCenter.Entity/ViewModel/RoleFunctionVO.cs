using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYY.AuthCenter.Entity
{
    public class RoleFunctionVO
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// 功能权限Id
        /// </summary>
        public List<int> FunctionList { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public int Creator { get; set; }
    }
}
