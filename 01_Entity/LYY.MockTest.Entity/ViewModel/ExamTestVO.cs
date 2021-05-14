using LYY.MockTest.Entity.Entity;
using LYY.MockTest.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YZ.Utility;

namespace LYY.MockTest.Entity.ViewModel
{
    public class ExamTestFilter : QueryFilter
    {
        public Test_Mode? mode { get; set; }
        public string document_name { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int? examid { get; set; }
        public string sortColumn { get; set; }
    }

    public class ExamTestView : t_exam_test
    {
        public decimal? duration_m
        {
            get
            {
                if (duration.HasValue)
                {
                    return (decimal)(duration / 60.0);
                }
                return 0;
            }
            set { }
        }
        /// <summary>
        /// 是否完成
        /// </summary>
        public string IsCompleteStr
        {
            get
            {
                if (end_time.HasValue)
                {
                    return Test_Is_Complete.Complete.GetDescription();
                }
                return Test_Is_Complete.NotComplete.GetDescription();
            }
            set { }
        }

        /// <summary>
        /// 是否完成
        /// </summary>
        public int IsComplete
        {
            get
            {
                if (end_time.HasValue)
                {
                    return (int)Test_Is_Complete.Complete;
                }
                return (int)Test_Is_Complete.NotComplete;
            }
            set { }
        }
        /// <summary>
        /// 测验类型
        /// </summary>
        public string ModeStr
        {
            get
            {
                if (mode.HasValue)
                {
                    return mode.GetDescription();
                }
                return "";
            }
            set { }
        }
        /// <summary>
        /// 测验标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 文档名称
        /// </summary>
        public string Document_name { get; set; }
        /// <summary>
        /// 测验时间
        /// </summary>
        public string Begin_time_str
        {
            get
            {
                if (begin_time.HasValue)
                {
                    return begin_time.Value.ToString("yyyy-MM-dd");
                }
                return "";
            }
            set { }
        }
        /// <summary>
        /// 正确率
        /// </summary>
        public decimal? RightRate { get; set; }
        /// <summary>
        /// 测验人
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 所属部门
        /// </summary>
        public string DeptName { get; set; }
    }


    public class TestStatisticsFilter : QueryFilter
    {
        public Test_Mode? mode { get; set; }
        public string document_name { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int? examid { get; set; }
        public string sortColumn { get; set; }
        public int statisticsObj { get; set; }
        public int statisticsMode { get; set; }
    }
    public class TestStatisticsView
    {
        public string Name { get; set; }
        public decimal? TestCount { get; set; }
        public decimal? AvgRightRate { get; set; }
        public decimal? MaxRightRate { get; set; }
        public decimal? MinRightRate { get; set; }
        public decimal? AvgScore { get; set; }
        public decimal? MaxScore { get; set; }
        public decimal? MinScore { get; set; }
        public decimal? SumDuration { get; set; }
    }

}
