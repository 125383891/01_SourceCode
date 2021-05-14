using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LYY.Common.Entity;
using YZ.Utility;

namespace LYY.Article.Entity
{
    public class ArticleVO
    {
        /// <summary>
        /// 系统ID
        /// </summary>
        public int BusinessId { get; set; }

        /// <summary>
        /// 应用
        /// </summary>
        public ArticleEnums Application { get; set; }

        /// <summary>
        /// 应用描述
        /// </summary>
        public string ApplicationDesc { get { return Application.GetDescription(); } }

        /// <summary>
        /// 推文标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 封面url
        /// </summary>
        public string PicUrl { get; set; }

        /// <summary>
        /// 推文内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 赞count
        /// </summary>
        public int? Zan { get; set; }

        /// <summary>
        /// 阅读count
        /// </summary>
        public int? View { get; set; }

        /// <summary>
        /// 数据状态
        /// </summary>
        public DeletedEnums IsDelete { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 评论数
        /// </summary>
        public int CommentCount { get; set; }

        /// <summary>
        /// 推文对象列表
        /// </summary>
        public List<ArticleDepartmentVO> ArticleDepartmentList { get; set; }
        /// <summary>
        /// 消息推送用户Id
        /// </summary>
        public string UserIds { get; set; }
    }

    public static class ArticleConvert
    {
        private static MapperConfiguration config;
        static ArticleConvert()
        {
            config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ArticleEntity, ArticleVO>()
                    .ForMember(d => d.BusinessId, p => p.MapFrom(t => t.Id))
                    .ForMember(d => d.Application, p => p.MapFrom(t => t.Apptype));
                cfg.CreateMap<ArticleVO, ArticleEntity>()
                    .ForMember(d => d.Id, p => p.MapFrom(t => t.BusinessId))
                    .ForMember(d => d.Apptype, p => p.MapFrom(t => t.Application));
                cfg.CreateMap<QueryResult<ArticleEntity>, QueryResult<ArticleVO>>();
                cfg.CreateMap<ArticleDepartmentVO, ArticleDepartmentEntity>();
                cfg.CreateMap<ArticleDepartmentEntity, ArticleDepartmentVO>();
            });
        }
        /// <summary>
        /// 分页列表Convert
        /// </summary>
        /// <param name="result">数据列表内容</param>
        /// <returns>返回VO</returns>
        public static QueryResult<ArticleVO> ToArticleVOPageList(this QueryResult<ArticleEntity> result)
        {
            return config.CreateMapper().Map<QueryResult<ArticleEntity>, QueryResult<ArticleVO>>(result);
        }

        /// <summary>
        /// convert entity 
        /// </summary>
        /// <param name="result">VO数据内容</param>
        /// <returns>返回entity</returns>
        public static ArticleEntity ToArticleEntity(this ArticleVO result)
        {
            return config.CreateMapper().Map<ArticleVO, ArticleEntity>(result);
        }

        /// <summary>
        /// convert VO
        /// </summary>
        /// <param name="result">数据内容</param>
        /// <returns>返回VO</returns>
        public static ArticleVO ToArticleVO(this ArticleEntity result)
        {
            return config.CreateMapper().Map<ArticleEntity, ArticleVO>(result);
        }

        /// <summary>
        /// convert VO
        /// </summary>
        /// <param name="result">数据内容</param>
        /// <returns>返回VO</returns>
        public static ArticleDepartmentEntity ToArticleDepartmentEntity(this ArticleDepartmentVO result)
        {
            return config.CreateMapper().Map<ArticleDepartmentVO, ArticleDepartmentEntity>(result);
        }

        /// <summary>
        /// convert VO
        /// </summary>
        /// <param name="result">数据内容</param>
        /// <returns>返回VO</returns>
        public static ArticleDepartmentVO ToArticleDepartmentVO(this ArticleDepartmentEntity result)
        {
            return config.CreateMapper().Map<ArticleDepartmentEntity, ArticleDepartmentVO>(result);
        }
    }
}
