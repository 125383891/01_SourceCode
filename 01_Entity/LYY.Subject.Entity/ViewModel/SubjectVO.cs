using LYY.Common.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LYY.Subject.Entity
{
    public class SubjectVO
    {
        /// <summary>
        /// 系统id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 创建用户
        /// </summary>
        public string CreateUser { get; set; }

        /// <summary>
        /// 创建用户姓名
        /// </summary>
        public string CreateUserName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 主题标题
        /// </summary>
        public string Title { get; set; }

        public string TitleDesc
        {
            get
            {
                if (!string.IsNullOrEmpty(Title))
                {
                    var v = YZ.Utility.DynamicJson.Parse(Title).v;
                    return RemoveHtmlLabel(v);
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 标签名称
        /// </summary>
        public string CatName { get; set; }

        /// <summary>
        /// 主题内容
        /// </summary>
        public string Content { get; set; }

        public string ContentDesc
        {
            get
            {
                if (!string.IsNullOrEmpty(Content))
                {
                    string v = YZ.Utility.DynamicJson.Parse(Content).v + "";
                    return RemoveHtmlLabel(v);
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// 是否评星
        /// </summary>
        public bool? IsScoreStar { get; set; }

        /// <summary>
        /// 评论数
        /// </summary>
        public int? CommentCount { get; set; }

        /// <summary>
        /// 图片Url
        /// </summary>
        public string Images { get; set; }

        /// <summary>
        /// 赞数量
        /// </summary>
        public int? Zan { get; set; }

        /// <summary>
        /// 主题分类
        /// </summary>
        public int CatId { get; set; }

        /// <summary>
        /// 阅读数量
        /// </summary>
        public int? View { get; set; }
        /// <summary>
        /// 重点置顶
        /// </summary>
        public int? Weight { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public DeletedEnums IsDelete { get; set; }

        /// <summary>
        /// 供应商
        /// </summary>
        public string Vendor { get; set; }

        /// <summary>
        /// 指派用户
        /// </summary>
        public string ReplyerUser { private get; set; }

        /// <summary>
        /// 分派用户列表
        /// </summary>
        public List<string> ReplyerUserList
        {
            get
            {
                if (string.IsNullOrEmpty(ReplyerUser))
                {
                    return new List<string>();
                }
                else
                {
                    return ReplyerUser.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Distinct().ToList();
                }
            }
        }

        /// <summary>
        /// 评星数量
        /// </summary>
        public int? StarCount { get; set; }

        /// <summary>
        /// 所属部门ID
        /// </summary>
        public int DepartmentId { get; set; }

        /// <summary>
        /// 供应商评价
        /// </summary>
        public int? VendorStar { get; set; }

        public decimal? Score_Base { get; set; }
        public decimal? Score_Extra { get; set; }
        public int Allow_Score { get; set; }

        private string RemoveHtmlLabel(string v)
        {
            string pattern = @"<(.+?)\>";
            foreach (Match match in Regex.Matches(v, pattern))
                v = v.Replace(match.Value, "");
            //Console.WriteLine(match.Value);
            pattern = @"&(.+?)\;";
            foreach (Match match in Regex.Matches(v, pattern))
                v = v.Replace(match.Value, "");
            return v;
        }
    }
    public class SubjectPlanScoreExtra
    {
        public int subject_id { get; set; }
        public int score_extra { get; set; }
    }

    public class SubjectModel {
        public int id { get; set; }
        public int cat_id { get; set; }
        public string cat_extra { get; set; }

        public int view { get; set; }

        public int subjectid { get; set; }

    }
}
