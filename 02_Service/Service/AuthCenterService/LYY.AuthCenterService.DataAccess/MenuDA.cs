using LYY.AuthCenter.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YZ.Utility.DataAccess;

namespace LYY.AuthCenterService.DataAccess
{
    public class MenuDA
    {
        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <returns>返回菜单列表</returns>
        public static List<MenuEntity> List()
        {
            var cmd = new DataCommand("Menu.List");
            return cmd.ExecuteEntityList<MenuEntity>();
        }

        /// <summary>
        /// 获取菜单功能权限列表
        /// </summary>
        /// <returns>返回菜单列表</returns>
        public static List<MenuFunctionVO> SearchMenuFunctionList()
        {
            var cmd = new DataCommand("Menu.SearchMenuFunctionList");
            return cmd.ExecuteEntityList<MenuFunctionVO>();
        }
    }
}
