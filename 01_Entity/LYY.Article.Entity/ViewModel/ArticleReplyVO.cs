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
    public class ArticleReplyVO
    {
        /// <summary>
        /// 系统Id
        /// </summary>
        public int BusinessId { get; set; }

        /// <summary>
        /// 推文id
        /// </summary>
        public int ArticleId { get; set; }

        /// <summary>
        /// 评论内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 赞count
        /// </summary>
        public int Zan { get; set; }

        /// <summary>
        /// 评论用户Id
        /// </summary>
        public string UserId { get; set; }


        /// <summary>
        /// 评论用户姓名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 单位Id
        /// </summary>
        public int DepartmentId { get; set; }

        /// <summary>
        /// 数据状态
        /// </summary>
        public DeletedEnums IsDelete { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime? DeleteTime { get; set; }

        /// <summary>
        /// 是否为管理员删除
        /// </summary>
        public bool? IsAdminDelete { get; set; }
    }

    public static class ArticleReplyConvert
    {
        private static MapperConfiguration config;
        static ArticleReplyConvert()
        {
            config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ArticleReplyEntity, ArticleReplyVO>()
                    .ForMember(d => d.BusinessId, p => p.MapFrom(t => t.Id));
                cfg.CreateMap<ArticleReplyVO, ArticleReplyEntity>()
                    .ForMember(d => d.Id, p => p.MapFrom(t => t.BusinessId));
                cfg.CreateMap<QueryResult<ArticleReplyEntity>, QueryResult<ArticleReplyVO>>();
            });
        }
        /// <summary>
        /// 分页列表Convert
        /// </summary>
        /// <param name="result">数据列表内容</param>
        /// <returns>返回VO</returns>
        public static QueryResult<ArticleReplyVO> ToArticleReplyVOPageList(this QueryResult<ArticleReplyEntity> result)
        {
            return config.CreateMapper().Map<QueryResult<ArticleReplyEntity>, QueryResult<ArticleReplyVO>>(result);
        }

        /// <summary>
        /// convert entity 
        /// </summary>
        /// <param name="result">VO数据内容</param>
        /// <returns>返回entity</returns>
        public static ArticleReplyEntity ToArticleReplyEntity(this ArticleReplyVO result)
        {
            return config.CreateMapper().Map<ArticleReplyVO, ArticleReplyEntity>(result);
        }

        /// <summary>
        /// convert VO
        /// </summary>
        /// <param name="result">数据内容</param>
        /// <returns>返回VO</returns>
        public static ArticleReplyVO ToArticleReplyVO(this ArticleReplyEntity result)
        {
            return config.CreateMapper().Map<ArticleReplyEntity, ArticleReplyVO>(result);
        }
    }
}
