using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YZ.Utility;

namespace LYY.MockTest.Entity.ViewModel
{
    public class TeamPointsVO
    {
        public string name { get; set; }
        public string tag_name { get; set; }
        public decimal? score_average { get; set; }

        public decimal? score_year { get; set; }
    }
    public class TeamPointsFilter : QueryFilter
    {
        public int UserTag { get; set; }
        public string statisticsYear { get; set; }
        public int statisticsMode { get; set; }

    }
}
