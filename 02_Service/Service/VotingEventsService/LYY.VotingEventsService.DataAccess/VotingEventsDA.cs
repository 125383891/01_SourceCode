using LYY.VotingEvents.Entity.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YZ.Utility;
using YZ.Utility.DataAccess;

namespace LYY.VotingEventsService.DataAccess
{
    public class VotingEventsDA
    {

        #region 投票活动

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter">id</param>
        /// <returns></returns>
        public static QueryResult<VoteVO> SearchVotePageLists(VoteFilter filter)
        {
            var cmd = new DataCommand("VotingEvents.SearchVoteLists");
            return cmd.Query<VoteVO>(filter, "t.start_time", null, true);
        }


     

        /// <summary>
        /// 读取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static VoteVO LoadVote(int id)
        {
            var cmd = new DataCommand("VotingEvents.LoadVote");
            cmd.SetParameter("@Id", DbType.Int32, id);
            var result = cmd.ExecuteEntity<VoteVO>();
            return result;
        }

        public static List<VoteItemVO> LoadVoteItem(int voteid)
        {
            var cmd = new DataCommand("VotingEvents.LoadVoteItem");
            cmd.SetParameter("@voteid", DbType.Int32, voteid);
            var result = cmd.ExecuteEntityList<VoteItemVO>();
            return result;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static int UpdateVote(VoteVO entity)
        {
            var cmd = new DataCommand("VotingEvents.UpdateVote");
            cmd.SetParameter(entity);
            int result = cmd.ExecuteNonQuery();
            return result;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static int UpdateVoteItem(VoteItemVO entity)
        {
            var cmd = new DataCommand("VotingEvents.UpdateVoteItem");
            cmd.SetParameter(entity);
            int result = cmd.ExecuteNonQuery();
            return result;
        }

        public static int DeleteVoteItem(int id)
        {
            var cmd = new DataCommand("VotingEvents.DeleteVoteItem");
            cmd.SetParameter("@Id", DbType.Int32, id);
            int result = cmd.ExecuteNonQuery();
            return result;
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static int InsertVoteItem(VoteItemVO entity)
        {
            var cmd = new DataCommand("VotingEvents.InsertVoteItem");
            cmd.SetParameter(entity);
            int result = cmd.ExecuteScalar<int>();
            return result;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static int InsertVote(VoteVO entity)
        {
            var cmd = new DataCommand("VotingEvents.InsertVote");
            cmd.SetParameter(entity);
            int result = cmd.ExecuteScalar<int>();
            return result;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DeleteVote(int id)
        {
            var cmd = new DataCommand("VotingEvents.DeleteVote");
            cmd.SetParameter("@Id", DbType.Int32, id);
            int result = cmd.ExecuteNonQuery();
            return result;
        }

        #endregion

        #region 投票统计
        /// <summary>
        /// 查询统计
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static List<VoteStatisticsView> StatisticsVoteItem(VoteFilter filter)
        {
            var cmd = new DataCommand("VotingEvents.StatisticsVoteItem");
            SetCondition(cmd,filter);
            return cmd.ExecuteEntityList<VoteStatisticsView>();
        }
        /// <summary>
        /// 设置条件
        /// </summary>
        /// <param name="command"></param>
        /// <param name="filter"></param>
        private static void SetCondition(DataCommand command, VoteFilter filter)
        {
            //测验类型
            if (filter.voteid.HasValue)
            {
                command.QuerySetCondition("tvi.vote_id", ConditionOperation.Equal, DbType.Int32, filter.voteid.Value);
            }
            command.CommandText = command.CommandText.Replace("#STRWHERE#", command.QueryConditionString);
        }

        public static List<UserVoteView> SearchUserVoteItem(int voteid)
        {
            var cmd = new DataCommand("VotingEvents.ExportUserVote");
            cmd.SetParameter("@id", DbType.Int32, voteid);
            return cmd.ExecuteEntityList<UserVoteView>();
        }

        public static List<VoteVO> GetVoteAll()
        {
            var cmd = new DataCommand("VotingEvents.GetVoteAll");
            return cmd.ExecuteEntityList<VoteVO>();
        }
        #endregion
    }
}
