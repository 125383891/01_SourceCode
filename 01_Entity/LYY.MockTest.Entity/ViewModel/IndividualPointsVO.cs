using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YZ.Utility;

namespace LYY.MockTest.Entity.ViewModel
{
    public class IndividualPointsVO
    {
        public string name { get; set; }
        public string tag_name { get; set; }
        public decimal? score_total { get; set; }

        public decimal? score_year { get; set; }
        public decimal? exam_accuracy { get; set; }
        public decimal? exam_accuracyEx
        {
            get
            {
                return exam_accuracy * 100;
            }
            set { }
        }
    }

    public class IndividualPointsFilter : QueryFilter
    {
        public int UserTag { get; set; }
        public string statisticsYear { get; set; }
        public int statisticsMode { get; set; }
    }
}
