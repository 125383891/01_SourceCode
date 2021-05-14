using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YZ.Utility;
using AutoMapper;
using AutoMapper.Configuration;

namespace LYY.Plan.Entity
{
    public class PlanVO
    {
        /// <summary>
        /// 系统id
        /// </summary>
        public int BusinessId { get; set; }

        /// <summary>
        /// 活动名称
        /// </summary>
        public string PlanName { get; set; }

        /// <summary>
        /// 活动图标序号
        /// </summary>
        public int PicNo { get; set; }

        /// <summary>
        /// 活动开始时间
        /// </summary>
        public DateTime BeginTime { get; set; }

        /// <summary>
        /// 活动结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 是否关闭活动
        /// </summary>
        public bool IsClose { get; set; }

        /// <summary>
        /// 关闭时间
        /// </summary>
        public DateTime? CloseTime { get; set; }

        /// <summary>
        /// 活动说明
        /// </summary>
        public string PlanDesc { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 活动状态描述
        /// </summary>
        public string PlanEnumDesc
        {
            get
            {
                DateTime nowTime = DateTime.Now;
                if (IsClose) //活动已关闭 则优先显示活动已关闭
                {
                    return PlanEnum.Closed.GetDescription();
                }
                else if (nowTime < BeginTime) // 当前时间小于活动开始时间 活动未开始
                {
                    return PlanEnum.NotStarted.GetDescription();
                }
                else if (nowTime >= BeginTime && nowTime <= EndTime) // 当前时间大于开始时间 and 小于等于结束时间  活动进行中
                {
                    return PlanEnum.InProgress.GetDescription();
                }
                else if (nowTime > EndTime) //当前时间大于结束时间 活动截至
                {
                    return PlanEnum.Deadline.GetDescription();
                }
                else
                {
                    return "";
                }
            }
        }

        /// <summary>
        /// 发表人基础分
        /// </summary>
        public decimal? ScoreBase { get; set; }

        /// <summary>
        /// 额外奖励分最大值
        /// </summary>
        public decimal? ScoreExtra { get; set; }

        /// <summary>
        /// 效果评估分最大值
        /// </summary>
        public decimal? ScoreEffect { get; set; }

        /// <summary>
        /// 办理人1星积分
        /// </summary>
        public decimal? ScoreStar1 { get; set; }

        /// <summary>
        /// 办理人2星积分
        /// </summary>
        public decimal? ScoreStar2 { get; set; }

        /// <summary>
        /// 办理人3星积分
        /// </summary>
        public decimal? ScoreStar3 { get; set; }

        /// <summary>
        /// 办理人4星积分
        /// </summary>
        public decimal? ScoreStar4 { get; set; }

        /// <summary>
        /// 办理人5星积分
        /// </summary>
        public decimal? ScoreStar5 { get; set; }

        /// <summary>
        /// 抢单积分
        /// </summary>
        public decimal? ScoreGrab { get; set; }

        /// <summary>
        /// 其他积分设置1
        /// </summary>
        public decimal? ScoreOther1 { get; set; }

        /// <summary>
        /// 其他积分设置2
        /// </summary>
        public decimal? ScoreOther2 { get; set; }

        /// <summary>
        /// 其他积分设置3
        /// </summary>
        public decimal? ScoreOther3 { get; set; }

    }

    public static class PlanConvert
    {
        private static MapperConfiguration config;
        static PlanConvert()
        {
            config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PlanEntity, PlanVO>()
                    .ForMember(d => d.BusinessId, p => p.MapFrom(t => t.Id));
                cfg.CreateMap<PlanVO, PlanEntity>()
                    .ForMember(d => d.Id, p => p.MapFrom(t => t.BusinessId));
                cfg.CreateMap<QueryResult<PlanEntity>, QueryResult<PlanVO>>();
            });
        }
        /// <summary>
        /// 分页列表Convert
        /// </summary>
        /// <param name="result">数据列表内容</param>
        /// <returns>返回VO</returns>
        public static QueryResult<PlanVO> ToPlanVOPageList(this QueryResult<PlanEntity> result)
        {
            return config.CreateMapper().Map<QueryResult<PlanEntity>, QueryResult<PlanVO>>(result);
        }

        /// <summary>
        /// convert entity 
        /// </summary>
        /// <param name="result">VO数据内容</param>
        /// <returns>返回entity</returns>
        public static PlanEntity ToPlanEntity(this PlanVO result)
        {
            return config.CreateMapper().Map<PlanVO, PlanEntity>(result);
        }

        /// <summary>
        /// convert VO
        /// </summary>
        /// <param name="result">数据内容</param>
        /// <returns>返回VO</returns>
        public static PlanVO ToPlanVo(this PlanEntity result)
        {
            return config.CreateMapper().Map<PlanEntity, PlanVO>(result);
        }
    }
}
