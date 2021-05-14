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
    public class UserService
    {
        private RoleUserService RoleUserService { get { return new RoleUserService(); } }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="user">数据实体</param>
        public void Insert(UserEntity user, AuthUserModel authUserModel)
        {
            if (authUserModel == null)
            {
                throw new BusinessException("未获取到当前登录用户信息");
            }
            this.CheckUserEntity(user);
            var hasExitsLoginName = UserDA.CheckUserLoginNameExist(user.LoginName, 0);
            if (hasExitsLoginName)
            {
                throw new BusinessException(string.Format("{0}用户名已存在", user.LoginName));
            }
            using (ITransaction transaction = TransactionManager.Create())
            {
                this.BindBasisInfo(user, authUserModel);
                int userId = UserDA.Insert(user);
                // 写入角色关联
                if (user.RoleId.HasValue)
                {
                    RoleUserService.Insert(new AuthCenter.Entity.Entity.RoleUserEntity()
                    {
                        RoleId = user.RoleId.Value,
                        UserId = userId,
                        Creator = authUserModel.UserSysNo
                    });
                }
                transaction.Complete();
            }
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="user">数据实体</param>
        public void Update(UserEntity user, AuthUserModel authUserModel)
        {
            if (authUserModel == null)
            {
                throw new BusinessException("未获取到当前登录用户信息");
            }
            if (!user.BusinessId.HasValue)
            {
                throw new BusinessException("用户Id不允许为空");
            }
            user.Id = user.BusinessId.Value;
            this.CheckUserEntity(user);
            var hasExitsLoginName = UserDA.CheckUserLoginNameExist(user.LoginName, user.Id);
            if (hasExitsLoginName)
            {
                throw new BusinessException(string.Format("{0}用户名已存在", user.LoginName));
            }
            using (ITransaction transaction = TransactionManager.Create())
            {
                this.BindBasisInfo(user, authUserModel);
                UserDA.Update(user);
                // 删除当前用户关联的角色
                RoleUserService.DeleteByUserId(new AuthCenter.Entity.Entity.RoleUserEntity() { UserId = user.Id });
                if (user.RoleId.HasValue)
                {
                    RoleUserService.Insert(new AuthCenter.Entity.Entity.RoleUserEntity()
                    {
                        RoleId = user.RoleId.Value,
                        UserId = user.Id,
                        Creator = authUserModel.UserSysNo
                    });
                }
                transaction.Complete();
            }
            // 更新角色关联
        }

        /// <summary>
        /// 更改用户状态
        /// </summary>
        public void UpdateUserStatus(UserEntity user, AuthUserModel auth)
        {
            var temp = this.GetById(user.Id);
            if (user.Id == auth.UserSysNo)
            {
                throw new BusinessException("请勿当前登录账号进行操作");
            }
            using (ITransaction transaction = TransactionManager.Create())
            {
                // 如果是删除用户，则删除对应的角色关联关系
                if (user.CommonStatus == CommonStatus.Deleted)
                {
                    RoleUserService.DeleteByUserId(new AuthCenter.Entity.Entity.RoleUserEntity() { UserId = temp.Id });
                }
                this.BindBasisInfo(user, auth);
                UserDA.UpdateUserStatus(user);
                transaction.Complete();
            }
        }

        /// <summary>
        /// 根据Id获取活动数据
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>返回用户数据</returns>
        public UserEntity GetById(int id)
        {
            var temp = UserDA.GetById(id);
            if (temp == null)
            {
                throw new BusinessException("用户不存在");
            }
            temp.LoginPassword = string.Empty;
            //不允许输出密码
            return temp;
        }

        /// <summary>
        /// 查询用户分页列表
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public QueryResult<UserEntity> SearchPageList(QF_User filter)
        {
            return UserDA.SearchPageList(filter);
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        public void UpdatePassword(UserUpdatePasswordVo userUpdatePassword, AuthUserModel authUserModel)
        {
            if (authUserModel == null)
            {
                throw new BusinessException("未获取到当前登录用户信息");
            }
            if (!userUpdatePassword.BusinessId.HasValue)
            {
                throw new BusinessException("用户Id不允许为空");
            }
            if (string.IsNullOrEmpty(userUpdatePassword.OldPassword))
            {
                throw new BusinessException("请输入原始密码");
            }
            if (userUpdatePassword.OldPassword.Length > 50)
            {
                throw new BusinessException("原始密码最大长度为50");
            }
            if (string.IsNullOrEmpty(userUpdatePassword.NewPassword))
            {
                throw new BusinessException("请输入新密码");
            }
            if (userUpdatePassword.NewPassword.Length > 50)
            {
                throw new BusinessException("新密码最大长度为50");
            }
            var temp = UserDA.GetById(userUpdatePassword.BusinessId.Value);
            if (!userUpdatePassword.OldPassword.Equals(temp.LoginPassword))
            {
                throw new BusinessException("对不起，您输入的密码错误");
            }
            UserDA.UpadtePassword(new UserEntity()
            {
                Id = userUpdatePassword.BusinessId.Value,
                LoginPassword = userUpdatePassword.NewPassword,
                Updater = authUserModel.UserSysNo,
                UpdateTime = DateTime.Now
            });
        }

        /// <summary>
        /// 根据账号和密码获取用户信息
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="loginPassword"></param>
        /// <returns></returns>
        public UserEntity GetByLoginNameAndPassword(string loginName, string loginPassword)
        {
            if (string.IsNullOrEmpty(loginName))
            {
                throw new BusinessException("请输入登录账号");
            }
            if (string.IsNullOrEmpty(loginPassword))
            {
                throw new BusinessException("请输入登录密码");
            }
            return UserDA.GetByLoginNameAndPassword(loginName, loginPassword);
        }

        public void BindBasisInfo(UserEntity userEntity, AuthUserModel authUserModel)
        {
            userEntity.Creator = authUserModel.UserSysNo;
            userEntity.CreateTime = DateTime.Now;
            userEntity.Updater = authUserModel.UserSysNo;
            userEntity.UpdateTime = DateTime.Now;
        }

        private void CheckUserEntity(UserEntity entity)
        {
            if (string.IsNullOrEmpty(entity.LoginName))
            {
                throw new BusinessException("请输入登录账号");
            }
            else if (entity.LoginName.Length > 50)
            {
                throw new BusinessException("登录账号最大长度为50");
            }
            else if (entity.Id == 0)
            {
                if (string.IsNullOrEmpty(entity.LoginPassword))
                {
                    throw new BusinessException("请输入登录密码");
                }
                else if (entity.LoginPassword.Length > 50)
                {
                    throw new BusinessException("登录密码最大长度为50");
                }
            }
            else if (entity.UserFullName.Length > 40)
            {
                throw new BusinessException("用户真实姓名最大长度为40");
            }
        }
    }
}
