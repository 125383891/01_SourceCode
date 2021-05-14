using LYY.AuthCenter.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YZ.Utility.DataAccess;

namespace LYY.AuthCenterService.DataAccess
{
    public class DepartmentDA
    {
        /// <summary>
        /// 获取单位列表
        /// </summary>
        /// <returns>返回单位列表</returns>
        public static List<DepartmentEntity> List()
        {
            var cmd = new DataCommand("Department.List");
            return cmd.ExecuteEntityList<DepartmentEntity>();
        }
    }
}
