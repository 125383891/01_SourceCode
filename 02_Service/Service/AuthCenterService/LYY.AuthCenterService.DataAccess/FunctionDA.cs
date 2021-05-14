using LYY.AuthCenter.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YZ.Utility.DataAccess;

namespace LYY.AuthCenterService.DataAccess
{
    public class FunctionDA
    {
        /// <summary>
        /// 获取功能权限列表
        /// </summary>
        /// <returns>返回功能权限列表</returns>
        public static List<FunctionEntity> GetFunctionListByUserId(int userId)
        {
            var cmd = new DataCommand("Function.GetFunctionListByUserId");
            cmd.SetParameter("@UserId", System.Data.DbType.Int32, userId);
            return cmd.ExecuteEntityList<FunctionEntity>();
        }
    }
}
