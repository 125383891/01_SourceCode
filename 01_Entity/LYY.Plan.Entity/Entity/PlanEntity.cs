using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYY.Plan.Entity
{
    public class PlanEntity
    {
        /// <summary>
        /// 系统id
        /// </summary>
        public int Id { get; set; }

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
}
