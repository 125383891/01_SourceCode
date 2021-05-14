using LYY.MockTest.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYY.MockTest.Entity.Entity
{
    public class t_exam_test
    {
        /// <summary>
        /// id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string user_id { get; set; }
        /// <summary>
        /// 测验时间
        /// </summary>
        public DateTime? begin_time { get; set; }
        /// <summary>
        /// 测验完成时间（如果为空表示未交卷）
        /// </summary>
        public DateTime? end_time { get; set; }
        /// <summary>
        /// 测验耗时（秒，在前端计算时间，等于测验页面处于打开状态的时间，不是结束-开始）
        /// </summary>
        public int? duration { get; set; }
        /// <summary>
        /// 测验类型（1=个人练习，2=模拟考试）
        /// </summary>
        public Test_Mode? mode { get; set; }
        /// <summary>
        /// 针对文档ID（只针对个人练习）
        /// </summary>
        public int? document_id { get; set; }
        /// <summary>
        /// 模拟考试方案ID（只针对模拟考试）
        /// </summary>
        public int? exam_id { get; set; }
        /// <summary>
        /// 总题目数
        /// </summary>
        public int? total_num { get; set; }
        /// <summary>
        /// 正确题目数
        /// </summary>
        public int? right_num { get; set; }
        /// <summary>
        /// 满分
        /// </summary>
        public int? total_score { get; set; }
        /// <summary>
        /// 实际得分
        /// </summary>
        public int? score { get; set; }
        public int? is_deleted { get; set; }
        /// <summary>
        /// 客户端IP，用于IP限制
        /// </summary>
        public string ip { get; set; }

    }
}
