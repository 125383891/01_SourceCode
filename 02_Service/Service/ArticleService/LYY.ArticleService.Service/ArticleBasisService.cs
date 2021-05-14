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
    public class ArticleBasisService
    {
        private ArticleDepartmentService ArticleDepartmentService { get { return new ArticleDepartmentService(); } }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="article">数据实体</param>
        public int Insert(ArticleVO articleVO)
        {
            using (ITransaction transaction = TransactionManager.Create())
            {
                ArticleEntity article = articleVO.ToArticleEntity();
                this.CheckEntity(article);
                article.IsDelete = DeletedEnums.Actived;
                if ((articleVO.ArticleDepartmentList == null || articleVO.ArticleDepartmentList.Count == 0)
                    && string.IsNullOrEmpty(articleVO.UserIds))
                {
                    throw new BusinessException("请选择推文对象或填写推送用户");
                }
                int articleId = ArticleDA.Insert(article);
                
                if (articleVO.ArticleDepartmentList != null && articleVO.ArticleDepartmentList.Count > 0)
                {
                    articleVO.ArticleDepartmentList.ForEach(item =>
                    {
                        item.ArticleId = articleId;
                    });
                    ArticleDepartmentService.BatchInsert(articleVO.ArticleDepartmentList.Select(p => p.ToArticleDepartmentEntity()).ToList());
                }
                               
                // 插入用户id数据
                if (!string.IsNullOrEmpty(articleVO.UserIds))
                {
                    List<ArticleUser> articleUsers = articleVO.UserIds.Split(new char[] { '|' }).Select(p => new ArticleUser() { ArticleId = articleId, UserId = p }).ToList();
                    ArticleDA.BatchInserArticleUser(articleUsers);
                }
                // 插入分发得公司
                transaction.Complete();

                return articleId;
            }
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="article">数据实体</param>
        public void Update(ArticleVO articleVO)
        {
            using (ITransaction transaction = TransactionManager.Create())
            {
                ArticleEntity article = articleVO.ToArticleEntity();
                this.CheckEntity(article);
                if ((articleVO.ArticleDepartmentList == null || articleVO.ArticleDepartmentList.Count == 0)
                    && string.IsNullOrEmpty(articleVO.UserIds))
                {
                    throw new BusinessException("请选择推文对象或填写推送用户");
                }
                ArticleDA.Update(article);
                // 插入用户id数据
                if (!string.IsNullOrEmpty(articleVO.UserIds))
                {
                    List<ArticleUser> articleUsers = articleVO.UserIds.Split(new char[] { '|' }).Select(p => new ArticleUser() { ArticleId = articleVO.BusinessId, UserId = p }).ToList();
                    ArticleDA.BatchInserArticleUser(articleUsers);
                }
                // 插入分发得公司
                transaction.Complete();
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id">推文id</param>
        public void Delete(int id)
        {
            ArticleDA.Delete(new ArticleEntity() { Id = id, IsDelete = DeletedEnums.Deleted });
        }

        /// <summary>
        /// 根据Id获取推文数据
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>返回推文数据</returns>
        public ArticleVO GetById(int id)
        {
            return ArticleDA.GetById(id).ToArticleVO();
        }

        public List<string> SearchArticleUserList(int articleId)
        {
            return ArticleDA.SearchArticleUserList(articleId);
        }

        /// <summary>
        /// 查询推文列表
        /// </summary>
        /// <param name="filter">过滤条件</param>
        /// <returns>返回推文列表</returns>
        public QueryResult<ArticleVO> SearchPageList(QF_Article filter)
        {
            return ArticleDA.SearchPageList(filter).ToArticleVOPageList();
        }

        public void CheckEntity(ArticleEntity article)
        {
            if (string.IsNullOrEmpty(article.Title))
            {
                throw new BusinessException("请输入推文标题");
            }
            if (string.IsNullOrEmpty(article.PicUrl))
            {
                throw new BusinessException("请上传推文封面图");
            }
            if (string.IsNullOrEmpty(article.Content))
            {
                throw new BusinessException("请输入推文内容");
            }
            if (article.Title.Length > 2000)
            {
                throw new BusinessException("推文标题最大长度为2000");
            }
            if (article.PicUrl.Length > 500)
            {
                throw new BusinessException("封面图Url值最大长度为500");
            }
        }
    }
}
