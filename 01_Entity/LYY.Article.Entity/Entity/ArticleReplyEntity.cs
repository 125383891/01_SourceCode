using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LYY.Common.Entity;
namespace LYY.Article.Entity
{
    public class ArticleReplyEntity
    {
        /// <summary>
        /// 系统Id
        /// </summary>
        public int Id { get; set; }

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
}
