using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYY.AuthCenter.Entity
{
    public class FunctionEntity
    {
        /// <summary>
        /// 系统Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 菜单id
        /// </summary>
        public int MenuId { get; set; }

        /// <summary>
        /// 功能权限Key
        /// </summary>
        public string FunctionKey { get; set; }

        /// <summary>
        /// 功能权限名称
        /// </summary>
        public string FunctionName { get; set; }

        /// <summary>
        /// 功能权限描述
        /// </summary>
        public string FunctionMemo { get; set; }

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
    }
}
