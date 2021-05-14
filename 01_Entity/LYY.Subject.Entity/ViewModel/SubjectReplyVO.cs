using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYY.Subject.Entity
{
    public class SubjectReplyVO
    {
        /// <summary>
        /// 系统Id
        /// </summary>
        public int BusinessId { get; set; }

        /// <summary>
        /// 创建用户
        /// </summary>
        public string CreateUser { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 主题Id
        /// </summary>
        public long SubjectId { get; set; }

        /// <summary>
        /// 上级回复ID，即本记录是评论
        /// </summary>
        public long? ParentId { get; set; }

        /// <summary>
        /// 是否屏蔽
        /// </summary>
        public bool? IsVisible { get; set; }

        /// <summary>
        /// 评论内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        public string Images { get; set; }

        /// <summary>
        /// 赞数量
        /// </summary>
        public int? Zan { get; set; }

        /// <summary>
        /// 评星等级
        /// </summary>
        public int? Star { get; set; }

        /// <summary>
        /// 评星意见
        /// </summary>
        public string StarRemark { get; set; }

        /// <summary>
        /// 评星意见追评
        /// </summary>
        public string StarRemark2 { get; set; }
    }
}
