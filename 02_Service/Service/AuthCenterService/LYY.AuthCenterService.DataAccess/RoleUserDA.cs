using LYY.AuthCenter.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YZ.Utility.DataAccess;

namespace LYY.AuthCenterService.DataAccess
{
    public class RoleUserDA
    {
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="role">数据实体</param>
        public static void Insert(RoleUserEntity role)
        {
            var cmd = new DataCommand("RoleUser.Insert");
            cmd.SetParameter(role);
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// 查询用户是否存在关联角色
        /// </summary>
        public static bool HasExistUserRoleAssociation(int roleId)
        {
            var cmd = new DataCommand("RoleUser.HasExistUserRoleAssociation");
            cmd.SetParameter("@RoleId", DbType.Int32, roleId); ;
            return cmd.ExecuteScalar<int?>().HasValue;
        }

        /// <summary>
        /// 根据用户Id删除关联关系
        /// </summary>
        /// <param name="role">数据实体</param>
        public static void DeleteByUserId(RoleUserEntity role)
        {
            var cmd = new DataCommand("RoleUser.Delete");
            cmd.QuerySetCondition("user_id", ConditionOperation.Equal, DbType.Int32, role.UserId);
            cmd.CommandText = cmd.CommandText.Replace("#STRWHERE#", cmd.QueryConditionString);
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// 根据角色ID删除关联关系
        /// </summary>
        /// <param name="role">数据实体</param>
        public static void DeleteByRoleId(RoleUserEntity role)
        {
            var cmd = new DataCommand("RoleUser.Delete");
            cmd.QuerySetCondition("role_id", ConditionOperation.Equal, DbType.Int32, role.RoleId);
            cmd.CommandText = cmd.CommandText.Replace("#STRWHERE#", cmd.QueryConditionString);
            cmd.ExecuteNonQuery();
        }
    }
}
