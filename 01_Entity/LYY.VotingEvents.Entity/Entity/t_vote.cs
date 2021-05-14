using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYY.VotingEvents.Entity.Entity
{
    public class t_vote
    {
        public int id { get; set; }
        public string title { get; set; }

        public string remark { get; set; }
        public DateTime? start_time { get; set; }

        public DateTime? end_time { get; set; }
        public int? is_delete { get; set; }

        public int? is_publish { get; set; }

        public int? max_vote_num { get; set; }

        public int? is_allow_detail { get; set; }
        public int? is_allow_vote_repeat { get; set; }



    }
}
