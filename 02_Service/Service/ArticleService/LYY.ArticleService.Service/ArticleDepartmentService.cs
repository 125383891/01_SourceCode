using LYY.Article.Entity;
using LYY.ArticleService.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YZ.Utility;

namespace LYY.ArticleService.Service
{
    public class ArticleDepartmentService
    {
        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <param name="list">数据列表</param>
        public void BatchInsert(List<ArticleDepartmentEntity> list)
        {
            if (list == null || list.Count == 0)
            {
                throw new BusinessException("请选择推文对象");
            }
            ArticleDepartmentDA.BatchInsert(list);
        }

        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <param name="list">数据列表</param>
        public List<ArticleDepartmentVO> ListByArticleId(int articleId)
        {
            return ArticleDepartmentDA.ListByArticleId(articleId).Select(p => p.ToArticleDepartmentVO()).ToList();
        }
    }
}
