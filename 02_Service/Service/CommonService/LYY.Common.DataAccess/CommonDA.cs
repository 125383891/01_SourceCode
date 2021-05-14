using LYY.Common.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YZ.Utility.DataAccess;

namespace LYY.Common.DataAccess
{
    public class CommonDA
    {

        /// <summary>
        /// 查询标签列表
        /// </summary>
        /// <returns></returns>
        public static List<TagEntity> SearchTagList()
        {
            var cmd = new DataCommand("Common.SearchTagList");
            return cmd.ExecuteEntityList<TagEntity>();
        }

        public static List<CatEntity> SearchCatList()
        {
            var cmd = new DataCommand("Common.SearchCatList");
            return cmd.ExecuteEntityList<CatEntity>();
        }
    }
}
