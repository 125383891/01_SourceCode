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
    public class RoleDA
    {
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="role">数据实体</param>
        public static void Insert(RoleEntity role)
        {
            var cmd = new DataCommand("Role.Insert");
            cmd.SetParameter(role);
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="role">数据实体</param>
        public static void Update(RoleEntity role)
        {
            var cmd = new DataCommand("Role.Update");
            cmd.SetParameter(role);
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="role">数据实体</param>
        public static void Delete(RoleEntity role)
        {
            var cmd = new DataCommand("Role.Delete");
            cmd.SetParameter(role);
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// 根据Id获取活动数据
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>返回用户数据</returns>
        public static RoleEntity GetById(int id)
        {
            var cmd = new DataCommand("Role.GetById");
            cmd.SetParameter("@Id", DbType.Int32, id);
            return cmd.ExecuteEntity<RoleEntity>();
        }

        /// <summary>
        /// 查询活动列表
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static QueryResult<RoleEntity> SearchPageList(QueryFilter filter)
        {
            var cmd = new DataCommand("Role.SearchPageList");
            cmd.QuerySetCondition("common_status", ConditionOperation.Equal, DbType.Int32, CommonStatus.Actived);
            cmd.CommandText = cmd.CommandText.Replace("#STRWHERE#", cmd.QueryConditionString);
            return cmd.Query<RoleEntity>(filter, "create_time DESC", null, true);
        }
    }
}
