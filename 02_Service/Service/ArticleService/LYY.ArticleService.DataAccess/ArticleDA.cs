using LYY.Article.Entity;
using LYY.Common.Entity;
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
    public class ArticleDA
    {
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="article">数据实体</param>
        public static int Insert(ArticleEntity article)
        {
            var cmd = new DataCommand("Article.Insert");
            cmd.SetParameter(article);
            return cmd.ExecuteScalar<int>();
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="article">数据实体</param>
        public static void Update(ArticleEntity article)
        {
            var cmd = new DataCommand("Article.Update");
            cmd.SetParameter(article);
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="article">数据实体</param>
        public static void Delete(ArticleEntity article)
        {
            var cmd = new DataCommand("Article.Delete");
            cmd.SetParameter(article);
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// 根据Id获取推文数据
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>返回推文数据</returns>
        public static ArticleEntity GetById(int id)
        {
            var cmd = new DataCommand("Article.GetById");
            cmd.SetParameter("@Id", DbType.Int32, id);
            return cmd.ExecuteEntity<ArticleEntity>();
        }

        /// <summary>
        /// 查询推文列表
        /// </summary>
        /// <param name="filter">过滤条件</param>
        /// <returns>返回推文列表</returns>
        public static QueryResult<ArticleEntity> SearchPageList(QF_Article filter)
        {
            var cmd = new DataCommand("Article.SearchPageList");
            cmd.QuerySetCondition("title", ConditionOperation.Like, DbType.String, filter.Title);
            cmd.QuerySetCondition("is_delete", ConditionOperation.Equal, DbType.Int32, (int)DeletedEnums.Actived);
            if (filter.StartTime.HasValue)
            {
                cmd.QuerySetCondition("create_time", ConditionOperation.MoreThanEqual, DbType.DateTime, filter.StartTime);
            }
            if (filter.EndTime.HasValue)
            {
                cmd.QuerySetCondition("create_time", ConditionOperation.LessThan, DbType.DateTime, filter.EndTime.Value.AddDays(1));
            }
            cmd.CommandText = cmd.CommandText.Replace("#STRWHERE#", cmd.QueryConditionString);
            return cmd.Query<ArticleEntity>(filter, "create_time DESC", null, true);
        }

        public static void BatchInserArticleUser(List<ArticleUser> list)
        {
            var cmd = new DataCommand("Article.BatchInserArticleUser");
            var temp = list.First();
            var templateScript = cmd.GetTemporaryTableScript<ArticleUser>("LYYTEAMPLATE", list);
            cmd.SetParameter("@ArticleId", DbType.Int32, temp.ArticleId);
            cmd.CommandText = cmd.CommandText.Replace("#TEMPLATETABLE#", templateScript);
            cmd.ExecuteNonQuery();
        }

        public static List<string> SearchArticleUserList(int articleId)
        {
            var cmd = new DataCommand("Article.SearchArticleUserList");
            cmd.SetParameter("@ArticleId", DbType.Int32, articleId);
            DataTable dt = cmd.ExecuteDataTable();
            List<string> list = new List<string>();
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(dr[0].ToString());
            }
            return list;
        }
    }
}
