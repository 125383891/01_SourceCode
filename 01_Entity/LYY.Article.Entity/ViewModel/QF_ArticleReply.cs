using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YZ.Utility;

namespace LYY.Article.Entity
{
    public class QF_ArticleReply : QueryFilter
    {
        /// <summary>
        /// 推文Id
        /// </summary>
        public int ArticleId { get; set; }
    }
}
