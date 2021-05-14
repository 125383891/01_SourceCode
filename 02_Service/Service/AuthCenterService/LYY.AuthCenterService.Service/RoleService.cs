using LYY.AuthCenter.Entity;
using LYY.AuthCenterService.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YZ.Utility;
using YZ.Utility.Web;

namespace LYY.AuthCenterService.Service
{
    public class RoleService
    {
        private RoleUserService RoleUserService { get { return new RoleUserService(); } }

        private UserService UserService { get { return new UserService(); } }

        private RoleFunctionService RoleFunctionService { get { return new RoleFunctionService(); } }
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="role">数据实体</param>
        public void Insert(RoleEntity role, AuthUserModel authUserModel)
        {
            this.CheckEntity(role);
            this.BindBasisInfo(role, authUserModel);
            RoleDA.Insert(role);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="role">数据实体</param>
        public void Update(RoleEntity role, AuthUserModel authUserModel)
        {
            if (!role.BusinessId.HasValue)
            {
                throw new BusinessException("角色Id不允许为空");
            }
            role.Id = role.BusinessId.Value;
            this.CheckEntity(role);
            this.BindBasisInfo(role, authUserModel);
            RoleDA.Update(role);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="role">数据实体</param>
        public void Delete(int roleId)
        {
            var hasAassociationRoleUser = RoleUserService.HasExistUserRoleAssociation(roleId);
            if (hasAassociationRoleUser)
            {
                throw new BusinessException("该角色下已存在关联用户，不允许删除该角色");
            }
            using (ITransaction transaction = TransactionManager.Create())
            {
                // 删除角色 以及删除角色对应的功能权限数据
                RoleDA.Delete(new RoleEntity() { Id = roleId, CommonStatus = CommonStatus.Deleted });
                RoleFunctionService.BatchDeleteFunction(roleId);
                transaction.Complete();
            }
        }

        /// <summary>
        /// 根据Id获取活动数据
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>返回用户数据</returns>
        public RoleEntity GetById(int id)
        {
            return RoleDA.GetById(id);
        }

        /// <summary>
        /// 查询活动列表
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public QueryResult<RoleEntity> SearchPageList(QueryFilter filter)
        {
            return RoleDA.SearchPageList(filter);
        }

        public void BindBasisInfo(RoleEntity roleEntity, AuthUserModel authUserModel)
        {
            roleEntity.Creator = authUserModel.UserSysNo;
            roleEntity.CreateTime = DateTime.Now;
            roleEntity.Updater = authUserModel.UserSysNo;
            roleEntity.UpdateTime = DateTime.Now;
        }

        private void CheckEntity(RoleEntity entity)
        {
            if (string.IsNullOrEmpty(entity.Name))
            {
                throw new BusinessException("角色名称不允许为空");
            }
            else if (entity.Name.Length > 50)
            {
                throw new BusinessException("角色名称最大长度为50");
            }
            else if (!string.IsNullOrEmpty(entity.Memo) && entity.Memo.Length > 200)
            {
                throw new BusinessException("描述信息最大长度为200");
            }
        }
    }
}
