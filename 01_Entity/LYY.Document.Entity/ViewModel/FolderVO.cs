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
    public class FolderVO
    {
        /// <summary>
        /// 系统ID
        /// </summary>
        public int BusinessId { get; set; }

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

    public static class FolderConvert
    {
        private static MapperConfiguration config;
        static FolderConvert()
        {
            config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<FolderEntity, FolderVO>()
                    .ForMember(d => d.BusinessId, p => p.MapFrom(t => t.Id));
                cfg.CreateMap<FolderVO, FolderEntity>()
                    .ForMember(d => d.Id, p => p.MapFrom(t => t.BusinessId));
                cfg.CreateMap<QueryResult<FolderEntity>, QueryResult<FolderVO>>();
            });
        }
        /// <summary>
        /// 分页列表Convert
        /// </summary>
        /// <param name="result">数据列表内容</param>
        /// <returns>返回VO</returns>
        public static QueryResult<FolderVO> ToFolderVOPageList(this QueryResult<FolderEntity> result)
        {
            return config.CreateMapper().Map<QueryResult<FolderEntity>, QueryResult<FolderVO>>(result);
        }

        /// <summary>
        /// convert entity 
        /// </summary>
        /// <param name="result">VO数据内容</param>
        /// <returns>返回entity</returns>
        public static FolderEntity ToFolderEntity(this FolderVO result)
        {
            return config.CreateMapper().Map<FolderVO, FolderEntity>(result);
        }

        /// <summary>
        /// convert VO
        /// </summary>
        /// <param name="result">数据内容</param>
        /// <returns>返回VO</returns>
        public static FolderVO ToFolderVo(this FolderEntity result)
        {
            return config.CreateMapper().Map<FolderEntity, FolderVO>(result);
        }
    }
}
