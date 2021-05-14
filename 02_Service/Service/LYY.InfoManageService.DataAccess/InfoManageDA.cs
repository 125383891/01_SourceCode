using LYY.InfoManage.Entity.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YZ.Utility;
using YZ.Utility.DataAccess;

namespace LYY.InfoManageService.DataAccess
{
    public class InfoManageDA
    {
        #region 公告

        /// <summary>
        /// 公告查询
        /// </summary>
        /// <param name="filter">id</param>
        /// <returns></returns>
        public static QueryResult<BulletinVO> SearchBulletinPageLists(BulletinFilter filter)
        {
            var cmd = new DataCommand("InfoManage.SearchBulletinLists");
            SetCondition(cmd, filter);
            return cmd.Query<BulletinVO>(filter, "t.create_time", null, true);
        }


        /// <summary>
        /// 设置条件
        /// </summary>
        /// <param name="command"></param>
        /// <param name="filter"></param>
        private static void SetCondition(DataCommand command, BulletinFilter filter)
        {
            command.QuerySetCondition("t.is_delete", ConditionOperation.Equal, DbType.Int32, 0);
            //测验类型
            if (filter.CreateTimeStart.HasValue)
            {
                command.QuerySetCondition("t.create_time", ConditionOperation.MoreThanEqual, DbType.DateTime, filter.CreateTimeStart.Value);
            }
            if (filter.CreateTimeEnd.HasValue)
            {
                command.QuerySetCondition("t.create_time", ConditionOperation.LessThan, DbType.DateTime, filter.CreateTimeEnd.Value.AddDays(1));
            }

            command.CommandText = command.CommandText.Replace("#STRWHERE#", command.QueryConditionString);
        }

        public static int InsertBulletion(BulletinVO entity)
        {
            var cmd = new DataCommand("InfoManage.InsertBulletion");
            cmd.SetParameter(entity);
            int result = cmd.ExecuteNonQuery();
            return result;
        }


        /// <summary>
        /// 读取公告
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static BulletinVO LoadBulletin(int id)
        {
            var cmd = new DataCommand("InfoManage.LoadBulletin");
            cmd.SetParameter("@Id", DbType.Int32, id);
            var result = cmd.ExecuteEntity<BulletinVO>();
            return result;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static int UpdateBulletion(BulletinVO entity)
        {
            var cmd = new DataCommand("InfoManage.UpdateBulletin");
            cmd.SetParameter(entity);
            int result = cmd.ExecuteNonQuery();
            return result;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DeleteBulletin(int id)
        {
            var cmd = new DataCommand("InfoManage.DeleteBulletin");
            cmd.SetParameter("@Id", DbType.Int32, id);
            int result = cmd.ExecuteNonQuery();
            return result;
        }

        #endregion
    }
}
