using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LYY.Common.Entity;
namespace LYY.Article.Entity
{
    public class ArticleEntity
    {
        /// <summary>
        /// 系统ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 应用类型
        /// </summary>
        public ArticleEnums Apptype { get; set; }

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
    }
}
