using LYY.AuthCenter.Entity;
using LYY.AuthCenterService.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYY.AuthCenterService.Service
{
    public class DepartmentService
    {
        /// <summary>
        /// 获取单位列表
        /// </summary>
        /// <returns>返回单位列表</returns>
        public List<DepartmentVO> List()
        {
            return DepartmentDA.List().Select(p => p.ToDepartmentVO()).ToList();
        }
    }
}
