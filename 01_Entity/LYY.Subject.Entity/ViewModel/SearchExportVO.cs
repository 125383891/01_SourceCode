using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LYY.Subject.Entity
{
    public class SearchExportVO
    {
        /// <summary>
        /// 主题Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 发表时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 发表时间
        /// </summary>
        public string CreateTimeStr
        {
            get
            {
                return CreateTime.ToString("yyyy/MM/dd HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            }
        }


        /// <summary>
        /// 最新回复时间
        /// </summary>
        public DateTime OrderTime { get; set; }

        /// <summary>
        /// 最新回复时间
        /// </summary>
        public string OrderTimeStr
        {
            get
            {
                return OrderTime.ToString("yyyy/MM/dd HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            }
        }

        public string CreateUser { get; set; }

        /// <summary>
        /// 发表人
        /// </summary>
        public string CreateUserName { get; set; }

        /// <summary>
        /// 主题所属单位
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// 标签名称
        /// </summary>
        public string CatName { get; set; }

        /// <summary>
        /// 发表人手机号码
        /// </summary>
        public string Telephone { get; set; }

        /// <summary>
        /// 标题
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
        /// 正文
        /// </summary>
        public string Content { get; set; }

        public string ContentDesc
        {
            get
            {
                if (!string.IsNullOrEmpty(Content))
                {
                    var v = YZ.Utility.DynamicJson.Parse(Content).v;
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
        /// 供应商
        /// </summary>
        public string Vendor { get; set; }
        /// <summary>
        /// 投诉供应商
        /// </summary>
        public string ComplainVendor { get; set; }

        /// <summary>
        /// 分配对象
        /// </summary>
        public string ReplyerUser { get; set; }

        public string ReplyerUserDesc
        {
            get
            {
                if (string.IsNullOrEmpty(ReplyerUser))
                {
                    return string.Empty;
                }
                else
                {
                    return string.Join("->", ReplyerUser.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Distinct());
                }
            }
        }

        /// <summary>
        /// 赞
        /// </summary>
        public int? Zan { get; set; }

        /// <summary>
        /// 阅读
        /// </summary>
        public int? View { get; set; }

        /// <summary>
        /// 回复数
        /// </summary>
        public int? CommentCount { get; set; }

        /// <summary>
        /// 首响回复时间，即第1个分派人的第1个回复内容
        /// </summary>
        public DateTime? FirstCommentTime { get; set; }


        /// <summary>
        /// 最新回复时间
        /// </summary>
        public string FirstCommentTimeStr
        {
            get
            {
                if (FirstCommentTime.HasValue)
                {
                    return FirstCommentTime.Value.ToString("yyyy/MM/dd HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                }
                return "";
            }
        }
        /// <summary>
        /// 首响回复人
        /// </summary>
        public string FirstCommentUser { get; set; }

        /// <summary>
        /// 首响回复内容
        /// </summary>
        public string FirstComment { get; set; }

        public string FirstCommentDesc
        {
            get
            {
                if (!string.IsNullOrEmpty(FirstComment))
                {
                    return RemoveHtmlLabel(FirstComment);
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// 评星回复时间
        /// </summary>
        public DateTime? FirstStatTime { get; set; }

        public string FirstStatTimeStr
        {
            get
            {
                if (FirstStatTime.HasValue)
                {
                    return FirstStatTime.Value.ToString("yyyy/MM/dd HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                }
                return "";
            }
        }

        /// <summary>
        /// 评星回复内容
        /// </summary>
        public string FirstStatComment { get; set; }

        public string FirstStatCommentDesc
        {
            get
            {
                if (!string.IsNullOrEmpty(FirstStatComment))
                {
                    return RemoveHtmlLabel(FirstStatComment);
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        /// <summary>
        /// 评星回复人
        /// </summary>
        public string FirstStatUser { get; set; }

        /// <summary>
        /// 评星数
        /// </summary>
        public int? FirstStar { get; set; }

        /// <summary>
        /// 评星意见
        /// </summary>
        public string StarRemark { get; set; }


        /// <summary>
        /// 发表时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        public string StartTimeStr
        {
            get
            {
                if (StartTime.HasValue)
                {
                    return StartTime.Value.ToString("yyyy/MM/dd HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                }
                return "";
            }
        }

        /// <summary>
        /// 追评意见
        /// </summary>
        public string StarRemarkAppend { get; set; }

        /// <summary>
        /// 供应商评价
        /// </summary>
        public int? VendorStar { get; set; }


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

    public class SearchExportRankingVO
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public string user_id { get; set; }
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string user_name { get; set; }
        /// <summary>
        /// 投诉量统计
        /// </summary>
        public decimal number1 { get; set; }
        /// <summary>
        /// 处理质量
        /// </summary>
        public decimal number2 { get; set; }
        /// <summary>
        /// 处理效率
        /// </summary>
        public decimal number3 { get; set; }
    }

    public class SearchExportAvgVO
    {
        /// <summary>
        /// 单位
        /// </summary>
        public string dept_name { get; set; }
        /// <summary>
        /// 单位平均未读数
        /// </summary>
        public decimal dd { get; set; }
    }
}
