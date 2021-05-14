using LYY.Article.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YZ.Utility;
using YZ.Utility.DataAccess;

namespace LYY.ArticleService.DataAccess
{
    public class ArticleDepartmentDA
    {
        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <param name="list">数据列表</param>
        public static void BatchInsert(List<ArticleDepartmentEntity> list)
        {
            var temp = list.First();
            var cmd = new DataCommand("ArticleDepartment.BatchInsert");
            cmd.SetParameter("@ArticleId", DbType.Int32, temp.ArticleId);
            string tag = cmd.GetSQLTag("TagBatchInsert");
            List<string> tagList = new List<string>();
            foreach (var item in list)
            {
                tagList.Add(string.Format(tag, item.ArticleId, item.DepartmentId));
            }
            cmd.ReplaceSQLTag("TagBatchInsert", string.Empty);
            cmd.CommandText = cmd.CommandText.Replace("#LIST#", string.Join(",", tagList));
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <param name="list">数据列表</param>
        public static List<ArticleDepartmentEntity> ListByArticleId(int articleId)
        {
            var cmd = new DataCommand("ArticleDepartment.ListByArticleId");
            cmd.SetParameter("@ArticleId", DbType.Int32, articleId);
            return cmd.ExecuteEntityList<ArticleDepartmentEntity>();
        }
    }
}
