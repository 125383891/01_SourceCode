using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYY.MockTest.Entity.Entity
{
    public class t_exam_rank_setting
    {
        public int id { get; set; }
        public int? rank_level { get; set; }

        public string rank_name { get; set; }

        public decimal? score_require { get; set; }
        public decimal? accuracy_require { get; set; }

    }
}
