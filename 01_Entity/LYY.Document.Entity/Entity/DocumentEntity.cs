using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LYY.Common.Entity;
namespace LYY.Document.Entity
{
    public class DocumentEntity
    {
        /// <summary>
        /// 系统ID
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// 目录id
        /// </summary>
        public int FolderId { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public DeletedEnums IsDeleted { get; set; }

        /// <summary>
        /// 文档名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 文件类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 文件url
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// 阅读次数
        /// </summary>
        public int ViewCount { get; set; }

        /// <summary>
        /// 关键词
        /// </summary>
        public string Words { get; set; }

        /// <summary>
        /// 排序顺序
        /// </summary>
        public int OrderNum { get; set; }
        /// <summary>
        /// 用户类型
        /// </summary>
        public int? Usertag { get; set; }
        /// <summary>
        /// 需要学习时间
        /// </summary>
        public int? Minminutes { get; set; }

        public bool? IsStudyMaterials { get; set; }

        public int ParamEx { get; set; }
    }
}
