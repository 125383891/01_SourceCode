using LYY.MockTest.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYY.MockTest.Entity.Entity
{
    public class t_exam_question
    {
        public int id { get; set; }
        public string content { get; set; }
        public Question_Mode? mode { get; set; }
        public int? document_id { get; set; }
        public string img_url { get; set; }
        public int? is_ip_restrict { get; set; }
        public int? is_user_restrict { get; set; }
        public int? order_num { get; set; }
        public int? is_deleted { get; set; }
        public string create_user { get; set; }
        public DateTime? create_time { get; set; }
        public DateTime? update_time { get; set; }
        public string update_user { get; set; }
        /// <summary>
        /// 用户类型
        /// </summary>
        public UserTagEnums? user_tag { get; set; }
        /// <summary>
        /// 答案解析
        /// </summary>
        public string answer_analysis { get; set; }
    }
}
