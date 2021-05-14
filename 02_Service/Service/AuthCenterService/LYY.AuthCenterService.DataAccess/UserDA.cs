using LYY.AuthCenter.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YZ.Utility;
using YZ.Utility.DataAccess;

namespace LYY.AuthCenterService.DataAccess
{
    public class UserDA
    {
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="user">数据实体</param>
        public static int Insert(UserEntity user)
        {
            var cmd = new DataCommand("User.Insert");
            cmd.SetParameter(user);
            return cmd.ExecuteScalar<int>();
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="user">数据实体</param>
        public static void Update(UserEntity user)
        {
            var cmd = new DataCommand("User.Update");
            cmd.SetParameter(user);
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// 根据Id获取活动数据
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>返回用户数据</returns>
        public static UserEntity GetById(int id)
        {
            var cmd = new DataCommand("User.GetById");
            cmd.SetParameter("@Id", DbType.Int32, id);
            return cmd.ExecuteEntity<UserEntity>();
        }

        /// <summary>
        /// 更改用户状态
        /// </summary>
        public static void UpdateUserStatus(UserEntity user)
        {
            var cmd = new DataCommand("User.UpdateUserStatus");
            cmd.SetParameter(user);
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// 查询活动列表
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static QueryResult<UserEntity> SearchPageList(QF_User filter)
        {
            var cmd = new DataCommand("User.SearchPageList");
            cmd.QuerySetCondition("tau.user_full_name", ConditionOperation.Like, DbType.String, filter.UserFullName);
            cmd.QuerySetCondition("tau.login_name", ConditionOperation.Equal, DbType.String, filter.LoginName);
            cmd.QuerySetCondition("tau.common_status", ConditionOperation.NotEqual, DbType.Int32, CommonStatus.Deleted);
            cmd.CommandText = cmd.CommandText.Replace("#STRWHERE#", cmd.QueryConditionString);
            return cmd.Query<UserEntity>(filter, "tau.create_time DESC", null, true);
        }

        /// <summary>
        /// 根据账号和密码获取用户信息
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="loginPassword"></param>
        /// <returns></returns>
        public static UserEntity GetByLoginNameAndPassword(string loginName, string loginPassword)
        {
            var cmd = new DataCommand("User.GetByLoginNameAndPassword");
            cmd.SetParameter("@LoginName", DbType.String, loginName);
            cmd.SetParameter("@LoginPassword", DbType.String, loginPassword);
            return cmd.ExecuteEntity<UserEntity>();
        }

        public static bool CheckUserLoginNameExist(string loginName, int selfId)
        {
            var cmd = new DataCommand("User.CheckUserLoginNameExist");
            cmd.SetParameter("@LoginName", DbType.String, loginName);
            cmd.SetParameter("@SelfId", DbType.String, selfId);
            return cmd.ExecuteScalar<int>() > 0;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="id">账号id</param>
        /// <param name="newPassword">新密码</param>
        public static void UpadtePassword(UserEntity user)
        {
            var cmd = new DataCommand("User.UpadtePassword");
            cmd.SetParameter(user);
            cmd.ExecuteNonQuery();
        }
    }
}
