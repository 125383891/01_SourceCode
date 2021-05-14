using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYY.VotingEvents.Entity.Entity
{
    public class t_user_vote_item
    {
        public int user_id { get; set; }
        public int vote_item_id { get; set; }
        public DateTime? create_time { get; set; }
    }
}
