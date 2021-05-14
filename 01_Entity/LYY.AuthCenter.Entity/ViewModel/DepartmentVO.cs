using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYY.AuthCenter.Entity
{
    public class DepartmentVO
    {
        /// <summary>
        /// 系统Id
        /// </summary>
        public int BusinessId { get; set; }
        
        /// <summary>
        /// 父级Id
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 关注度，用户数量
        /// </summary>
        public int UserCount { get; set; }

        /// <summary>
        /// 日活跃度，昨天的活跃用户用户数量
        /// </summary>
        public int UserDailyCount { get; set; }

        /// <summary>
        /// 投诉量，累计主题数量
        /// </summary>
        public int SubjectCount { get; set; }

        /// <summary>
        /// 办结率，有评星的主题占百分比
        /// </summary>
        public decimal StarRate { get; set; }
    }

    public static class DepartMentConvert
    {
        private static MapperConfiguration config;
        static DepartMentConvert()
        {
            config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DepartmentEntity, DepartmentVO>()
                    .ForMember(d => d.BusinessId, p => p.MapFrom(t => t.Id));
                cfg.CreateMap<DepartmentVO, DepartmentEntity>()
                    .ForMember(d => d.Id, p => p.MapFrom(t => t.BusinessId));
            });
        }

        /// <summary>
        /// convert entity 
        /// </summary>
        /// <param name="result">VO数据内容</param>
        /// <returns>返回entity</returns>
        public static DepartmentEntity ToDepartmentEntity(this DepartmentVO result)
        {
            return config.CreateMapper().Map<DepartmentVO, DepartmentEntity>(result);
        }

        /// <summary>
        /// convert VO
        /// </summary>
        /// <param name="result">数据内容</param>
        /// <returns>返回VO</returns>
        public static DepartmentVO ToDepartmentVO(this DepartmentEntity result)
        {
            return config.CreateMapper().Map<DepartmentEntity, DepartmentVO>(result);
        }
    }
}
