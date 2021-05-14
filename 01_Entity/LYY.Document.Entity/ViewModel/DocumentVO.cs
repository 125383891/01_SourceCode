using AutoMapper;
using LYY.Common.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YZ.Utility;

namespace LYY.Document.Entity
{
    public class DocumentVO
    {
        public int Id { get; set; }
        /// <summary>
        /// 系统ID
        /// </summary>
        public int BusinessId { get; set; }

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

        public bool? IsStudyMaterials { get; set; }
        /// <summary>
        /// 用户类型
        /// </summary>
        public int? Usertag { get; set; }
        /// <summary>
        /// 需要学习时间
        /// </summary>
        public int? Minminutes { get; set; }
    }

    public static class DocumentConvert
    {
        private static MapperConfiguration config;
        static DocumentConvert()
        {
            config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DocumentEntity, DocumentVO>()
                    .ForMember(d => d.BusinessId, p => p.MapFrom(t => t.Id));
                cfg.CreateMap<DocumentVO, DocumentEntity>()
                    .ForMember(d => d.Id, p => p.MapFrom(t => t.BusinessId));
                cfg.CreateMap<QueryResult<DocumentEntity>, QueryResult<DocumentVO>>();
            });
        }
        /// <summary>
        /// 分页列表Convert
        /// </summary>
        /// <param name="result">数据列表内容</param>
        /// <returns>返回VO</returns>
        public static QueryResult<DocumentVO> ToDocumentVOPageList(this QueryResult<DocumentEntity> result)
        {
            return config.CreateMapper().Map<QueryResult<DocumentEntity>, QueryResult<DocumentVO>>(result);
        }

        /// <summary>
        /// convert entity 
        /// </summary>
        /// <param name="result">VO数据内容</param>
        /// <returns>返回entity</returns>
        public static DocumentEntity ToDocumentEntity(this DocumentVO result)
        {
            return config.CreateMapper().Map<DocumentVO, DocumentEntity>(result);
        }

        /// <summary>
        /// convert VO
        /// </summary>
        /// <param name="result">数据内容</param>
        /// <returns>返回VO</returns>
        public static DocumentVO ToDocumentVo(this DocumentEntity result)
        {
            return config.CreateMapper().Map<DocumentEntity, DocumentVO>(result);
        }
    }
}
