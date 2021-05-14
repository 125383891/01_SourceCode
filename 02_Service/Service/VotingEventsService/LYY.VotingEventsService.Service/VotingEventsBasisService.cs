using LYY.VotingEvents.Entity.ViewModel;
using LYY.VotingEventsService.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YZ.Utility;

namespace LYY.VotingEventsService.Service
{
    public class VotingEventsBasisService
    {
        #region 投票活动

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter">id</param>
        /// <returns></returns>
        public QueryResult<VoteVO> SearchVotePageLists(VoteFilter filter)
        {
            return VotingEventsDA.SearchVotePageLists(filter);
        }


        /// <summary>
        /// 读取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VoteVO LoadVote(int id)
        {
            var vote = VotingEventsDA.LoadVote(id);
            vote.vote_item = VotingEventsDA.LoadVoteItem(vote.id);
            return vote;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void SaveVote(VoteVO entity)
        {
            if (entity.id > 0)
            {
                VotingEventsDA.UpdateVote(entity);
            }
            else
            {
                var id = VotingEventsDA.InsertVote(entity);
                entity.id = id;
            }
            entity.vote_item.ForEach(_ => _.vote_id = entity.id);
            int order = 1;
            foreach (var item in entity.vote_item)
            {
                item.vote_id = entity.id;
                item.order_num = order++;
                if (item.id > 0)
                {
                    VotingEventsDA.UpdateVoteItem(item);
                }
                else
                {
                    VotingEventsDA.InsertVoteItem(item);
                }
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteVote(int id)
        {
            return VotingEventsDA.DeleteVote(id);
        }


        public int DeleteVoteItem(int id)
        {
            return VotingEventsDA.DeleteVoteItem(id);
        }
        #endregion


        #region 投票统计
        /// <summary>
        /// 查询统计
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<VoteStatisticsView> StatisticsVoteItem(VoteFilter filter)
        {
            return VotingEventsDA.StatisticsVoteItem(filter);
        }

        public List<UserVoteView> SearchUserVoteItem(int voteitemid)
        {
            return VotingEventsDA.SearchUserVoteItem(voteitemid);

        }

        public List<VoteVO> GetVoteAll()
        {
            return VotingEventsDA.GetVoteAll();
        }
        #endregion
    }
}
