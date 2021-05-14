using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYY.AuthCenter.Entity
{
    public class RoleFunctionEntity
    {
        /// <summary>
        /// 系统Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 角色编号
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// 功能权限Id
        /// </summary>
        public int FunctionId { get; set; }

        /// <summary>
        /// 创建用户id
        /// </summary>
        public int Creator { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
