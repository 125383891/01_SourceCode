using LYY.Article.Entity;
using LYY.ArticleService.DataAccess;
using LYY.Common.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YZ.Utility;

namespace LYY.ArticleService.Service
{
    public class ArticleReplyService
    {
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id">评论id</param>
        public void Delete(int id)
        {
            ArticleReplyDA.Delete(new ArticleReplyEntity() { Id = id, IsDelete = DeletedEnums.Deleted });
        }

        /// <summary>
        /// 查询推文评论列表
        /// </summary>
        /// <param name="filter">过滤条件</param>
        /// <returns>返回推文评论列表</returns>
        public QueryResult<ArticleReplyVO> SearchPageList(QF_ArticleReply filter)
        {
            return ArticleReplyDA.SearchPageList(filter).ToArticleReplyVOPageList();
        }
    }
}
