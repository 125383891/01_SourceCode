using LYY.MockTest.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYY.MockTest.Entity.Entity
{
    public class t_exam
    {
        public int id { get; set; }
        public string title { get; set; }
        public string introducation { get; set; }
        public int? total_num { get; set; }
        public int? score { get; set; }
        public int? pct_mode1 { get; set; }
        public int? pct_mode2 { get; set; }
        public int? pct_mode3 { get; set; }
        public int? time_restrict { get; set; }
        public int? order_mode { get; set; }
        public DateTime? begin_time { get; set; }
        public DateTime? end_time { get; set; }
        public int limit_num { get; set; }
        public int is_begin { get; set; }
        public string create_user { get; set; }
        public DateTime? create_time { get; set; }
        public string update_user { get; set; }
        public DateTime? update_time { get; set; }

        public UserTagEnums? user_tag { get; set; }

    }
}
