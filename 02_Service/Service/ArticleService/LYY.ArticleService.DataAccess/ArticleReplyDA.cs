using LYY.Article.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YZ.Utility;
using YZ.Utility.DataAccess;

namespace LYY.ArticleService.DataAccess
{
    public class ArticleReplyDA
    {
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="article">数据实体</param>
        public static void Delete(ArticleReplyEntity article)
        {
            var cmd = new DataCommand("ArticleReply.Delete");
            cmd.SetParameter(article);
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// 查询推文评论列表
        /// </summary>
        /// <param name="filter">过滤条件</param>
        /// <returns>返回推文评论列表</returns>
        public static QueryResult<ArticleReplyEntity> SearchPageList(QF_ArticleReply filter)
        {
            var cmd = new DataCommand("ArticleReply.SearchPageList");
            cmd.SetParameter("@ArticleId", System.Data.DbType.Int32, filter.ArticleId);
            return cmd.Query<ArticleReplyEntity>(filter, "create_time DESC", null, true);
        }
    }
}
