using LYY.AuthCenter.Entity.Entity;
using LYY.AuthCenterService.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YZ.Utility;

namespace LYY.AuthCenterService.Service
{
    public class RoleUserService
    {

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="role">数据实体</param>
        public void Insert(RoleUserEntity role)
        {
            if (role.UserId == 0)
            {
                throw new BusinessException("用户Id不允许为空");
            }
            if (role.RoleId == 0)
            {
                throw new BusinessException("角色Id不允许为空");
            }
            RoleUserDA.Insert(role);
        }

        /// <summary>
        /// 查询用户是否存在关联角色
        /// </summary>
        public bool HasExistUserRoleAssociation(int roleId)
        {
            return RoleUserDA.HasExistUserRoleAssociation(roleId);
        }

        /// <summary>
        /// 根据用户Id删除关联关系
        /// </summary>
        /// <param name="role">数据实体</param>
        public void DeleteByUserId(RoleUserEntity role)
        {
            RoleUserDA.DeleteByUserId(role);
        }

        /// <summary>
        /// 根据角色ID删除关联关系
        /// </summary>
        /// <param name="role">数据实体</param>
        public void DeleteByRoleId(RoleUserEntity role)
        {
            RoleUserDA.DeleteByRoleId(role);
        }
    }
}
