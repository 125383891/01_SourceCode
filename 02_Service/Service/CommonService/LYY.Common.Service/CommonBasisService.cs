using LYY.Common.DataAccess;
using LYY.Common.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYY.Common.Service
{
    public class CommonBasisService
    {
        /// <summary>
        /// 查询标签列表
        /// </summary>
        /// <returns></returns>
        public List<TagEntity> SearchTagList()
        {
            return CommonDA.SearchTagList();
        }

        public List<CatEntity> SearchCatList() {
            return CommonDA.SearchCatList();
        }
    }
}
