using LYY.AuthCenter.Entity;
using LYY.AuthCenterService.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYY.AuthCenterService.Service
{
    public class MenuService
    {
        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <returns>返回菜单列表</returns>
        public List<MenuEntity> List()
        {
            return MenuDA.List();
        }

        /// <summary>
        /// 获取菜单功能权限列表
        /// </summary>
        /// <returns>返回菜单功能权限列表</returns>
        public List<MenuFunctionVO> SearchMenuFunctionList()
        {
            return MenuDA.SearchMenuFunctionList();
        }
    }
}
