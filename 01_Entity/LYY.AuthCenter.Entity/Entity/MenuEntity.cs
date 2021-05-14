using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYY.AuthCenter.Entity
{
    public class MenuEntity
    {
        /// <summary>
        /// 系统Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 父级Id
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool? IsDisplay { get; set; }

        /// <summary>
        /// 菜单链接
        /// </summary>
        public string LinkPath { get; set; }

        /// <summary>
        /// 菜单类型
        /// </summary>
        public MenuTypeEnums Type { get; set; }

        /// <summary>
        /// 图标class名称
        /// </summary>
        public string IconClass { get; set; }

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

        /// <summary>
        /// 菜单权限key
        /// </summary>
        public string AuthKey { get; set; }
    }
}
