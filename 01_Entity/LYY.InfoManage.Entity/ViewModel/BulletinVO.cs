using LYY.InfoManage.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YZ.Utility;

namespace LYY.InfoManage.Entity.ViewModel
{
    public class BulletinVO : t_exam_bulletin
    {
        public int bulleid { get; set; }
        public string TypeDesc
        {
            get
            {
                if (type.HasValue)
                {
                    return type.GetDescription();
                }
                return "";
            }
            set { }
        }


        public string CreateTimeDesc
        {
            get
            {
                if (create_time.HasValue)
                {
                    return create_time.Value.ToString("yyyy-MM-dd");
                }
                return "";
            }
            set { }
        }

        public string UserName { get; set; }
    }

    public class BulletinFilter : QueryFilter
    {
        public DateTime? CreateTimeStart { get; set; }
        public DateTime? CreateTimeEnd { get; set; }
    }
}
