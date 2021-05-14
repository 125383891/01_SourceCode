using LYY.MockTest.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YZ.Utility;

namespace LYY.MockTest.Entity.ViewModel
{
    public class ExamFilter : QueryFilter
    {
    }

    public class ExamView : t_exam
    {

        public string UsertagDesc
        {
            get
            {
                if (user_tag.HasValue)
                {
                    return user_tag.GetDescription();
                }
                return "";
            }
            set { }
        }
        public int Exam_Id { get; set; }
        /// <summary>
        /// 开始日期
        /// </summary>
        public string begin_time_str
        {
            get
            {
                if (begin_time.HasValue)
                {
                    return begin_time.Value.ToString("yyyy-MM-dd");
                }
                return "";
            }
            set { }
        }
        /// <summary>
        /// 结束日期
        /// </summary>
        public string end_time_str
        {
            get
            {
                if (end_time.HasValue)
                {
                    return end_time.Value.ToString("yyyy-MM-dd");
                }
                return "";
            }
            set { }
        }
        /// <summary>
        /// 是否启用
        /// </summary>
        public string is_begin_str
        {
            get
            {
                if (is_begin == 1)
                {
                    return "是";
                }
                return "否";
            }
            set { }
        }

        public List<ExamDocumentView> DocumentList { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public string documentStr
        {
            get
            {
                string result = "";
                if (DocumentList != null)
                {
                    foreach (var item in DocumentList)
                    {
                        result += string.Format("{0}\n", item.name);
                    }
                }
                return result;
            }
            set { }
        }

        public string documentStrSave { get; set; }
    }

    public class ExamDocumentView : t_exam_document
    {
        /// <summary>
        /// 文档名词
        /// </summary>
        public string name { get; set; }
    }
}
