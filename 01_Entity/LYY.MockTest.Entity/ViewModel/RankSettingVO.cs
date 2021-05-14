using LYY.MockTest.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YZ.Utility;

namespace LYY.MockTest.Entity.ViewModel
{
    public class RankSettingVO : t_exam_rank_setting
    {
        public int rankid { get; set; }
        public int? accuracy_requireDesc
        {
            get
            {
                if (accuracy_require.HasValue)
                {
                    return (int)(accuracy_require.Value * 100);
                }
                return null;
            }
            set { }
        }
    }

    public class RankSettingFilter : QueryFilter
    {

    }

}
