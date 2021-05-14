using LYY.AuthCenter.Entity;
using LYY.AuthCenterService.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYY.AuthCenterService.Service
{
    public class FunctionService
    {
        /// <summary>
        /// 获取功能权限列表
        /// </summary>
        /// <returns>返回功能权限列表</returns>
        public List<FunctionEntity> GetFunctionListByUserId(int userId)
        {
            return FunctionDA.GetFunctionListByUserId(userId);
        }
    }
}
