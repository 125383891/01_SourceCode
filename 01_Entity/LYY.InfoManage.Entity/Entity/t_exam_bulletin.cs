using LYY.InfoManage.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYY.InfoManage.Entity.Entity
{
    public class t_exam_bulletin
    {
        public int id { get; set; }
        public DateTime? create_time { get; set; }

        public BulletinType? type { get; set; }
        public string title { get; set; }
        public string content { get; set; }

        public string user_id { get; set; }

        public int is_delete { get; set; }


    }
}
