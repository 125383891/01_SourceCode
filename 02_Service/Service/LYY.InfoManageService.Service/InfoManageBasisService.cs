using LYY.InfoManage.Entity.ViewModel;
using LYY.InfoManageService.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YZ.Utility;

namespace LYY.InfoManageService.Service
{
    public class InfoManageBasisService
    {
        #region 公告

        /// <summary>
        /// 公告查询
        /// </summary>
        /// <param name="filter">id</param>
        /// <returns></returns>
        public  QueryResult<BulletinVO> SearchBulletinPageLists(BulletinFilter filter)
        {
            return InfoManageDA.SearchBulletinPageLists(filter);
        }
        /// <summary>
        /// 读取公告
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public  BulletinVO LoadBulletin(int id)
        {
            return InfoManageDA.LoadBulletin(id);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public  int UpdateBulletion(BulletinVO entity)
        {
            return InfoManageDA.UpdateBulletion(entity);
        }

        public int InsertBulletion(BulletinVO entity)
        {
            entity.create_time = DateTime.Now;
            entity.is_delete = 0;
            return InfoManageDA.InsertBulletion(entity);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public  int DeleteBulletin(int id)
        {
            return InfoManageDA.DeleteBulletin(id);
        }

        #endregion
    }
}
