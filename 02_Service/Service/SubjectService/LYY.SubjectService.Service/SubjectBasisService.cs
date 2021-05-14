using LYY.Subject.Entity;
using LYY.SubjectService.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YZ.Utility;

namespace LYY.SubjectService.Service
{
    public class SubjectBasisService
    {
        /// <summary>
        /// 根据Id获取活动数据
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>返回活动数据</returns>
        public SubjectVO GetById(int id)
        {
            var result = SubjectDA.GetById(id);
            if (result == null)
            {
                throw new BusinessException("未找到对应的主题活动数据");
            }
            return result;
        }

        /// <summary>
        /// 查询主题列表
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public QueryResult<SubjectVO> SearchPageList(QF_Subject filter)
        {
            return SubjectDA.SearchPageList(filter);
        }

        /// <summary>
        /// 查询回复列表
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static QueryResult<SubjectReplyEntity> SearchReplyPageList(QF_Subject filter)
        {
            return SubjectDA.SearchReplyPageList(filter);
        }

        public SubjectModel LoadSubject(int id)
        {
            return SubjectDA.LoadSubject(id);
        }

        /// <summary>
        /// 修改热度
        /// </summary>
        /// <param name="viewCount"></param>
        /// <param name="SubjectId"></param>
        public void UpdateViewCount(SubjectModel entity)
        {
            SubjectDA.UpdateViewCount(entity);
        }
        /// <summary>
        /// 修改重要置顶
        /// </summary>
        /// <param name="weight"></param>
        /// <param name="subjectId"></param>
        public void UpdateWeight(int? weight, int subjectId)
        {
            if (weight == 0)
            {
                weight = null;
            }
            SubjectDA.UpdateWeight(weight, subjectId);
        }

        public int ValidWeight(int? weight, int subjectId)
        {
            return SubjectDA.ValidWeight(weight, subjectId);
        }

        /// <summary>
        /// 查询主题导出列表
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<SearchExportVO> SearchExportList(QF_Subject filter)
        {
            return SubjectDA.SearchExportList(filter);
        }

        /// <summary>
        /// 获取主题参与活动关系表
        /// </summary>
        /// <param name="subject_id"></param>
        /// <returns></returns>
        public static SubjectPlanScoreExtra LoadSubjectPlan(int subject_id)
        {
            return SubjectDA.LoadSubjectPlan(subject_id);
        }
        /// <summary>
        /// 更新主题参与活动关系表质量达人分
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static int UpdateSubjectPlanScoreExtra(SubjectPlanScoreExtra entity)
        {
            return SubjectDA.UpdateSubjectPlanScoreExtra(entity);
        }


        #region 排行榜导出
        /// <summary>
        /// 查询主题导出列表
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<SearchExportRankingVO> SearchExportRanking(QF_Subject filter)
        {
            return SubjectDA.SearchExportRanking(filter);
        }
        /// <summary>
        /// 查询主题导出列表
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<SearchExportAvgVO> SearchExportAvg(QF_Subject filter)
        {
            return SubjectDA.SearchExportAvg(filter);
        }

        #endregion
    }
}
