using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYY.Common.Entity
{
    public class FunctionKeys
    {
        #region 系统用户管理
        public const string OperationMgt_System_UserList_View = "OperationMgt_System_UserList_View";

        public const string OperationMgt_System_RoleList_View = "OperationMgt_System_RoleList_View"; 
        #endregion

        #region 活动任务管理
        public const string OperationMgt_Plan_List_View = "OperationMgt_Plan_List_View";

        //导入处理人积分
        public const string OperationMgt_Plan_UserPlanRank_View = "OperationMgt_Plan_UserPlanRank_View";
        //导入单位积分
        public const string OperationMgt_Plan_DeptPlanRank_View = "OperationMgt_Plan_DeptPlanRank_View";

        #endregion

        #region 文档管理
        public const string OperationMgt_Document_List_View = "OperationMgt_Document_List_View"; 
        #endregion

        #region 推文管理
        public const string OperationMgt_Article_List_View = "OperationMgt_Article_List_View";

        public const string OperationMgt_Article_Edit_View = "OperationMgt_Article_Edit_View";

        public const string OperationMgt_Article_ArticleReplyList_View = "OperationMgt_Article_ArticleReplyList_View";
        #endregion

        #region 论坛主题管理

        public const string OperationMgt_Subject_List_View = "OperationMgt_Subject_List_View";

        public const string OperationMgt_Subject_Detail_View = "OperationMgt_Subject_Detail_View";

        public const string OperationMgt_Subject_Export_View = "OperationMgt_Subject_Export_View";

        
        #endregion

        #region 报表相关
        public const string OperationMgt_Report_ComplaintStatistics_View = "OperationMgt_Report_ComplaintStatistics_View";

        public const string OperationMgt_Report_ProductVendorStatistics_View = "OperationMgt_Report_ProductVendorStatistics_View";

        public const string OperationMgt_Report_ActivityStatistics_View = "OperationMgt_Report_ActivityStatistics_View";

        public const string OperationMgt_Report_ExternalDepartmentStatistics_View = "OperationMgt_Report_ExternalDepartmentStatistics_View";

        public const string OperationMgt_Report_SubjectStatistics_View = "OperationMgt_Report_SubjectStatistics_View";

        public const string OperationMgt_Report_ArticleInterviewStatistics_View = "OperationMgt_Report_ArticleInterviewStatistics_View";
        #endregion

        #region 模拟考试
        //题库管理
        public const string OperationMgt_Mock_Question_List_View = "OperationMgt_Mock_Question_List_View";
        public const string OperationMgt_Mock_Question_Detail_View = "OperationMgt_Mock_Question_Detail_View";
        //考试配置
        public const string OperationMgt_Mock_ExamConfig_List_View = "OperationMgt_Mock_ExamConfig_List_View";
        public const string OperationMgt_Mock_ExamConfig_Detail_View = "OperationMgt_Mock_ExamConfig_Detail_View";
        //测验历史
        public const string OperationMgt_Mock_TestHistory_List_View = "OperationMgt_Mock_TestHistory_List_View";
        //测验统计
        public const string OperationMgt_Mock_TestStatistics_List_View = "OperationMgt_Mock_Question_List_View";

        #endregion
        #region 资讯管理
        /// <summary>
        /// 公告
        /// </summary>
        public const string OperationMgt_Info_Bulletin_List_View = "OperationMgt_Info_Bulletin_List_View";
        #endregion

        #region 投票活动
        /// <summary>
        /// 投票活动
        /// </summary>
        public const string OperationMgt_VotingEvents_Vote_List_View = "OperationMgt_VotingEvents_Vote_List_View";
        /// <summary>
        /// 投票活动统计
        /// </summary>
        public const string OperationMgt_VotingEvents_VoteStatistics_List_View = "OperationMgt_VotingEvents_VoteStatistics_List_View";

        #endregion
    }
}
