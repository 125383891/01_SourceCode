using LYY.MockTest.Entity.Entity;
using LYY.MockTest.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YZ.Utility;

namespace LYY.MockTest.Entity.ViewModel
{
    public class QusetionFilter : QueryFilter
    {
        public string document_name { get; set; }
        public int? mode { get; set; }
        public string content { get; set; }

        public int? usertag { get; set; }
    }

    public class QusetionView : t_exam_question
    {
        public string UsertagDesc { get; set; }
        //public string UsertagDesc
        //{
        //    get
        //    {
        //        if (user_tag.HasValue)
        //        {
        //            return user_tag.GetDescription();
        //        }
        //        return "";
        //    }
        //    set { }
        //}

        public string modeStr
        {
            get
            {
                if (mode.HasValue)
                {
                    return mode.GetDescription();
                }
                return "";
            }
            set { }
        }
        public string document_name { get; set; }
        public List<AnswerView> AnswerList { get; set; }
        public int qusetionId { get; set; }

        public string img_url_str
        {
            get
            {
                if (!string.IsNullOrEmpty(img_url))
                {
                    return "http://sc.cmccsi.com.cn/api/file/" + img_url.Replace("/upload/", "");
                }
                return "";
            }
            set { }
        }

    }

    public class AnswerView : t_exam_answer
    {
        public string Is_RightStr
        {
            get
            {
                if (is_right.HasValue)
                {
                    return is_right.GetDescription();
                }
                return "";
            }
            set { }
        }

        public string img_url_str
        {
            get
            {
                if (!string.IsNullOrEmpty(img_url))
                {
                    return "http://sc.cmccsi.com.cn/api/file/" + img_url.Replace("/upload/", "");
                }
                return "";
            }
            set { }
        }

    }

    public class DocumentValid
    {
        /// <summary>
        /// 文档id
        /// </summary>
        public int? id { get; set; }
        /// <summary>
        /// 文档标题
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 问题id
        /// </summary>
        public int? QuestionId { get; set; }

        public UserTagEnums? user_tag { get; set; }


    }
}
