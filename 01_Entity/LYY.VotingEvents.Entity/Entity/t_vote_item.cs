using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYY.VotingEvents.Entity.Entity
{
    public class t_vote_item
    {
        public int id { get; set; }

        public int vote_id { get; set; }

        public string content { get; set; }

        public string ext_val { get; set; }
        public int order_num { get; set; }

    }
}
