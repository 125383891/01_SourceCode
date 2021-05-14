using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYY.AuthCenter.Entity
{
    public class UserEntity
    {
        /// <summary>
        /// 系统Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 业务id
        /// </summary>
        public int? BusinessId { get; set; }

        /// <summary>
        /// 登录用户名
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        public string LoginPassword { get; set; }

        /// <summary>
        /// 用户真实名
        /// </summary>
        public string UserFullName { get; set; }

        /// <summary>
        /// 系统状态
        /// </summary>
        public CommonStatus CommonStatus { get; set; }

        /// <summary>
        /// 创建用户id
        /// </summary>
        public int Creator { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 修改用户id
        /// </summary>
        public int? Updater { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        #region 扩展字段
        /// <summary>
        /// 角色Id
        /// </summary>
        public int? RoleId { get; set; }

        /// <summary>
        /// 当前角色名称
        /// </summary>
        public string RoleName { get; set; }
        #endregion
    }
}
