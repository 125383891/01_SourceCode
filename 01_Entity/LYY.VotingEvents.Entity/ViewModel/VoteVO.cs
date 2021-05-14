using LYY.VotingEvents.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YZ.Utility;

namespace LYY.VotingEvents.Entity.ViewModel
{
    public class VoteVO : t_vote
    {
        public int voteid { get; set; }

        public string is_publish_desc
        {
            get
            {
                if (is_publish.HasValue)
                {
                    return is_publish == 1 ? "是" : "否";
                }
                return "";
            }
            set { }
        }

        public string start_time_desc
        {
            get
            {
                if (start_time.HasValue)
                {
                    return start_time.Value.ToString("yyyy-MM-dd HH:mm:ss");
                }
                return "";
            }
            set { }
        }

        public string end_time_desc
        {
            get
            {
                if (end_time.HasValue)
                {
                    return end_time.Value.ToString("yyyy-MM-dd HH:mm:ss");
                }
                return "";
            }
            set { }
        }

        public List<VoteItemVO> vote_item { get; set; }
    }


    public class VoteItemVO : t_vote_item
    {
        public int voteitemid { get; set; }


    }
    public class VoteFilter : QueryFilter
    {
        public int? voteid { get; set; }
    }


    public class VoteStatisticsView
    {
        public int id { get; set; }
        public int vote_id { get; set; }
        public string content { get; set; }
        public string ext_val { get; set; }
        public int order_num { get; set; }
        public int count { get; set; }
        public string title { get; set; }

    }

    public class UserVoteView : t_vote_item
    {
        public string username { get; set; }
        public DateTime? create_time { get; set; }
        public string CreateTimeDesc
        {
            get
            {
                if (create_time.HasValue)
                {
                    return create_time.Value.ToString("yyyy-MM-dd hh:mm:ss");
                }
                return "";
            }
            set { }
        }
    }

    public class ExportFilter
    {
        public int voteid { get; set; }
    }
}
