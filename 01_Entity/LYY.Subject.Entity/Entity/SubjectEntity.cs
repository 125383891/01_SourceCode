using LYY.Common.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYY.Subject.Entity
{
    public class SubjectEntity
    {
        /// <summary>
        /// 系统id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 创建用户
        /// </summary>
        public string CreateUser { get; set; }

        /// <summary>
        /// 创建事件
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 主题标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 主题内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 图片Url
        /// </summary>
        public string Images { get; set; }

        /// <summary>
        /// 赞数量
        /// </summary>
        public int? Zan { get; set; }

        /// <summary>
        /// 主题分类
        /// </summary>
        public int CatId { get; set; }

        /// <summary>
        /// 阅读数量
        /// </summary>
        public int? View { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public DeletedEnums IsDelete { get; set; }

        /// <summary>
        /// 回复超时时间，秒，为空忽略该记录
        /// </summary>
        public DateTime? ReplyTimeout { get; set; }

        /// <summary>
        /// 是否停止回复时间统计
        /// </summary>
        public int? ReplyTimeoutStop { get; set; }

        /// <summary>
        /// 当前分派对象
        /// </summary>
        public int ReplyerId { get; set; }

        /// <summary>
        /// 产品ID
        /// </summary>
        public int? ProductId { get; set; }

        /// <summary>
        /// 供应商
        /// </summary>
        public string Vendor { get; set; }

        /// <summary>
        /// 排序时间，最新的回复时间
        /// </summary>
        public DateTime? OrderTime { get; set; }

        /// <summary>
        /// 下一次题时间，只有当前时间>本时间才可以提醒
        /// </summary>
        public DateTime? NextRemindTime { get; set; }

        /// <summary>
        /// 附件标题，原上传名称
        /// </summary>
        public string DocTitles { get; set; }

        /// <summary>
        /// 附件路径，实际保存路径
        /// </summary>
        public string DocPaths { get; set; }

        /// <summary>
        /// 所属部门ID
        /// </summary>
        public int DepartmentId { get; set; }
    }
}
