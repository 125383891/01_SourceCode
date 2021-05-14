using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYY.Plan.Entity
{
    public class t_user_plan_rank
    {
        public int plan_id { get; set; }
        public string user_id { get; set; }
        public int? rank { get; set; }
        public decimal? score { get; set; }
        public DateTime? create_time { get; set; }
    }
}
