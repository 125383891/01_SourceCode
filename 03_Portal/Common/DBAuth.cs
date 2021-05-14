using System;
using System.Collections.Generic;
using YZ.Utility.Web;
using System.Linq;
using YZ.Utility;
using LYY.AuthCenterService.Service;
using LYY.AuthCenter.Entity;

namespace LYY.EnterpriseWxMgt.WebHost
{
    /// <summary>
    /// 用户授权控制
    /// </summary>
    public class DBAuth : IAuth
    {
        private const string CACHE_KEY_USER_MENU = "User_MenuList";
        private const string CACHE_KEY_ALL_FUNCTION = "AllFunctionKeys";
        private const string Key_Service_User_Auth_Data = "Key_Service_User_Auth_Data";
        private const int USER_LOCAL_CACHE_SEC = 60; //用户信息本地缓存1分钟

        private UserService UserService { get { return new UserService(); } }

        private MenuService MenuService { get { return new MenuService(); } }

        private FunctionService FunctionService { get { return new FunctionService(); } }
        public bool HasAuth(string authKey, AuthUserModel user)
        {
            var result = GetUserMenuList(user);
            var funcList = result.Functions;

            return funcList.Exists(p => p.FunctionKey.ToLower().Trim() == authKey.ToLower().Trim());
        }

        [Obsolete("请使用[LoginV2]代替")]
        public AuthUserModel Login(string userID, string pwd)
        {
            UserEntity userEntity = UserService.GetByLoginNameAndPassword(userID, pwd);
            if (userEntity == null)
            {
                throw new BusinessException("用户或者密码错误");
            }
            if (userEntity.CommonStatus == CommonStatus.DeActived)
            {
                throw new BusinessException("该登录用户已被禁用，请联系管理员");
            }
            AuthUserModel result = new AuthUserModel()
            {
                UserSysNo = userEntity.Id,
                UserDisplayName = userEntity.UserFullName,
                UserID = userEntity.LoginName,
                UsrCommonStatus = (AuthUserStatus)((int)userEntity.CommonStatus)
            };//todo 查询用户信息
            RsetUserLocalCache(result);
            return result;
        }

        /// <summary>
        /// 主动清理服务端缓存
        /// </summary>
        /// <param name="user"></param>
        public void ClearCachedData(AuthUserModel user)
        {
            var cacheKey = CacheManager.GenerateKey(Key_Service_User_Auth_Data, user.UserSysNo.ToString());
            CacheManager.Remove(cacheKey);
            CacheManager.RemoveStartsWith(CacheManager.GenerateKey(CACHE_KEY_USER_MENU, user.UserID, LangHelper.GetLanguageCode()) + "_");
        }
        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public AuthUserDataModel GetUserMenuList(AuthUserModel user)
        {
            AuthUserDataModel authUserData = null;
            if (user == null || user.UserSysNo <= 0)
            {
                authUserData = new AuthUserDataModel();
                authUserData.Menus = new List<AuthMenuModel>();
                authUserData.Functions = new List<AuthFunctionModel>();
                authUserData.Roles = new List<AuthRoleModel>();
                return authUserData;
            }
            string keyWithoutLoginTime = CacheManager.GenerateKey(CACHE_KEY_USER_MENU, user.UserID, LangHelper.GetLanguageCode());
            string keyWithLoginTime = CacheManager.GenerateKey(keyWithoutLoginTime, user.UILoginTime);
            authUserData = CacheManager.GetWithCache(keyWithLoginTime, () =>
            {
                return this.GetAuthUserDataModel(user);
            }, CacheTime.Longest);

            return authUserData;
        }

        public List<AuthFunctionModel> LoadAllFunctions(AuthUserModel user)
        {
            string keyWithoutLoginTime = CacheManager.GenerateKey(CACHE_KEY_ALL_FUNCTION, user.UserSysNo.ToString());
            CacheManager.Remove(keyWithoutLoginTime);
            return CacheManager.GetWithCache(keyWithoutLoginTime, () =>
            {
                ////移除失效的缓存
                var result = FunctionService.GetFunctionListByUserId(user.UserSysNo).Select(item => new AuthFunctionModel()
                {
                    MenuSysNo = item.MenuId.ToString(),
                    FunctionKey = item.FunctionKey,
                    FunctionName = item.FunctionName,
                    SysNo = item.Id.ToString()
                }).ToList();//todo 获取用户权限列表
                return result;
            }, CacheTime.Longest, false);
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <typeparam name="T">登录信息类</typeparam>
        /// <param name="loginModel">登录信息</param>
        /// <returns></returns>
        public AuthUserModel LoginV2<T>(T loginModel) where T : LoginModel
        {
            UserEntity userEntity = UserService.GetByLoginNameAndPassword(loginModel.Account, loginModel.Password);
            if (userEntity == null)
            {
                throw new BusinessException(LangHelper.GetText("用户或者密码错误！", "PortalBase.DBAuth"));
            }
            if (userEntity.CommonStatus == CommonStatus.DeActived)
            {
                throw new BusinessException("该登录用户已被禁用，请联系管理员");
            }
            AuthUserModel authUser = new AuthUserModel()
            {
                UserSysNo = userEntity.Id,
                UserDisplayName = userEntity.UserFullName,
                UserID = userEntity.LoginName,
                UsrCommonStatus = (AuthUserStatus)((int)userEntity.CommonStatus)
            };//todo 登陆实现
            if (authUser != null && authUser.UserSysNo > 0)
            {
                //清除本机缓存
                RsetUserLocalCache(authUser);
            }
            return authUser;
        }
        /// <summary>
        /// 更新本地用户缓存
        /// </summary>
        /// <param name="user"></param>
        private void RsetUserLocalCache(AuthUserModel user)
        {
            var cacheKey = CacheManager.GenerateKey(Key_Service_User_Auth_Data, user.UserSysNo.ToString());
            CacheManager.Remove(cacheKey);
            //重新加入缓存
            CacheManager.GetWithCache(cacheKey, () =>
            {
                return user;
            }, USER_LOCAL_CACHE_SEC);
        }
        /// <summary>
        /// 获取登录用户信息
        /// </summary>
        /// <param name="userSysNo">登录用户系统编号</param>
        /// <param name="applicationKey">应用程序编码</param>
        /// <param name="isRefresh">是否清空本地缓存,默认false,修改了用户信息后请设置true更新本地缓存</param>
        /// <returns></returns>
        public AuthUserModel LoadUser(int userSysNo, bool isRefresh = false)
        {
            var cacheKey = CacheManager.GenerateKey(Key_Service_User_Auth_Data, userSysNo.ToString());
            if (isRefresh)
            {
                CacheManager.Remove(cacheKey);
            }
            //用户缓存忽略applicationKey
            var user = CacheManager.GetWithCache(cacheKey, () =>
            {
                UserEntity userEntity = UserService.GetById(userSysNo);
                AuthUserModel result = new AuthUserModel()
                {
                    UserSysNo = userEntity.Id,
                    UserDisplayName = userEntity.UserFullName,
                    UserID = userEntity.LoginName,
                    UsrCommonStatus = (AuthUserStatus)((int)userEntity.CommonStatus)
                };//todo 查询用户信息
                //本机缓存1分钟，不然速度太慢了
                return result;//todo 查询用户信息
            }, USER_LOCAL_CACHE_SEC);
            if (isRefresh && user != null)
            {
                //清除菜单和权限缓存
                ClearCachedData(user);
            }
            return user;
        }

        /// <summary>
        /// 得到存入数据库的密码
        /// </summary>
        /// <param name="rsaPassword">js RSA 加密传输过来的密码</param>
        /// <returns></returns>
        private static string GetHashPassword(string rsaPassword)
        {
            //rsa解密
            Asym_RSA rsa = new Asym_RSA();
            rsaPassword = rsa.EncryptForJSDecrypt(rsaPassword);
            //MD5加密
            rsaPassword = AuthManager.EncryptPassword(rsaPassword);
            return rsaPassword;
        }
        /// <summary>
        /// 用户自己重置或者找回密码
        /// </summary>
        /// <param name="loginName">用户账号,UserID</param>
        /// <param name="cellphone"></param>
        /// <param name="validateCode"></param>
        /// <param name="newRsaPassword">js RSA加密传输过来的密码</param>
        /// <returns></returns>
        public AuthUserModel ResetPassword(string loginName, string cellphone, string validateCode, string newRsaPassword)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 用户自己修改密码
        /// </summary>
        /// <param name="loginName">用户账号,UserID</param>
        /// <param name="oldRsaPassword">js RSA加密传输过来的密码</param>
        /// <param name="newRsaPassword">js RSA加密传输过来的密码</param>
        /// <returns></returns>
        public AuthUserModel ChangeUserPassword(string loginName, string oldRsaPassword, string newRsaPassword)
        {
            throw new NotImplementedException();
        }

        public void CheckUserType(AuthUserModel user)
        {
            throw new NotImplementedException();
        }

        public AuthUserModel AutoLogin(string token)
        {
            throw new NotImplementedException();
        }

        private AuthUserDataModel GetAuthUserDataModel(AuthUserModel user)
        {
            var authUserDataModel = new AuthUserDataModel();
            var allMenus = MenuService.List();
            var allFunctions = this.LoadAllFunctions(user);
            var userMenus = new List<AuthMenuModel>();
            foreach (var item in allMenus)
            {
                //如果权限集中包含页面的AuthKey，即拥有查看权限才显示
                if (item.IsDisplay.HasValue && item.IsDisplay.Value)
                {
                    var temp = new AuthMenuModel()
                    {
                        SysNo = item.Id.ToString(),
                        ParentSysNo = item.ParentId.HasValue ? item.ParentId.Value.ToString() : null,
                        MenuName = item.Name,
                        IsDisplay = item.IsDisplay.HasValue ? item.IsDisplay.Value.ToString() : false.ToString(),
                        LinkPath = item.LinkPath,
                        Type = (int)item.Type,
                        IconStyle = item.IconClass
                    };
                    if (item.Type == MenuTypeEnums.Catalog)
                    {
                        userMenus.Add(temp);
                    }
                    else if (item.Type == MenuTypeEnums.Page)
                    {
                        if (allFunctions.Exists(function => string.Compare(function.FunctionKey, item.AuthKey) == 0))
                        {
                            userMenus.Add(temp);
                        }
                    }
                }
            }
            authUserDataModel.Menus = userMenus;
            authUserDataModel.Functions = allFunctions;
            return authUserDataModel;
        }
    }
}