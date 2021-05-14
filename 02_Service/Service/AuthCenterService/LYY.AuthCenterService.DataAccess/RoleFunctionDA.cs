using LYY.AuthCenter.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YZ.Utility.DataAccess;

namespace LYY.AuthCenterService.DataAccess
{
    public class RoleFunctionDA
    {
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="roleFunctionEntities">数据实体</param>
        public static void BatchInserFunction(List<RoleFunctionEntity> roleFunctionEntities)
        {
            var cmd = new DataCommand("Function.BatchInserFunction");
            cmd.CommandText = cmd.CommandText.Replace("#TEMPLATE#", cmd.GetTemporaryTableScript("templateTable", roleFunctionEntities));
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// 批量删除角色与功能权限关联关系
        /// </summary>
        public static void BatchDeleteFunction(int roleId)
        {
            var cmd = new DataCommand("Function.BatchDeleteFunction");
            cmd.SetParameter("@RoleId", DbType.Int32, roleId); ;
            cmd.ExecuteNonQuery();
        }

        public static List<RoleFunctionEntity> GetRoleFunctionByRoleId(int roleId)
        {
            var cmd = new DataCommand("Function.GetRoleFunctionByRoleId");
            cmd.SetParameter("@RoleId", DbType.Int32, roleId); ;
            return cmd.ExecuteEntityList<RoleFunctionEntity>();
        }
    }
}
