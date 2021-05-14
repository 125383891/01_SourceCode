using LYY.AuthCenter.Entity;
using LYY.AuthCenterService.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YZ.Utility;

namespace LYY.AuthCenterService.Service
{
    public class RoleFunctionService
    {
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="roleFunctionEntities">数据实体</param>
        public void BatchInserFunction(RoleFunctionVO roleFunction)
        {
            using (ITransaction transaction = TransactionManager.Create())
            {
                this.BatchDeleteFunction(roleFunction.RoleId);
                List<RoleFunctionEntity> roleFunctionEntities = new List<RoleFunctionEntity>();
                if (roleFunction.FunctionList != null && roleFunction.FunctionList.Count > 0)
                {
                    foreach (var item in roleFunction.FunctionList)
                    {
                        roleFunctionEntities.Add(new RoleFunctionEntity()
                        {
                            RoleId = roleFunction.RoleId,
                            FunctionId = item,
                            Creator = roleFunction.Creator,
                            CreateTime = DateTime.Now
                        });
                    }
                    RoleFunctionDA.BatchInserFunction(roleFunctionEntities);
                }
                transaction.Complete();
            }
        }

        /// <summary>
        /// 批量删除角色与功能权限关联关系
        /// </summary>
        public void BatchDeleteFunction(int roleId)
        {
            RoleFunctionDA.BatchDeleteFunction(roleId);
        }

        /// <summary>
        /// 根据角色Id查看分配的权限
        /// </summary>
        public List<RoleFunctionEntity> GetRoleFunctionByRoleId(int roleId)
        {
            return RoleFunctionDA.GetRoleFunctionByRoleId(roleId);
        }
    }
}
