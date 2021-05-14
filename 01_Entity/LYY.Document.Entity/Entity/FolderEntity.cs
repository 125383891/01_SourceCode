using LYY.Common.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYY.Document.Entity
{
    public class FolderEntity
    {
        /// <summary>
        /// 系统ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 父节点id
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public DeletedEnums IsDeleted { get; set; }

        /// <summary>
        /// 目录名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 排序顺序
        /// </summary>
        public int OrderNum { get; set; }
    }
}
