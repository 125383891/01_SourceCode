using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYY.MockTest.Entity.Entity
{
    public class t_document
    {
        public int id { get; set; }

        public int? folder_id { get; set; }
        public DateTime? create_time { get; set; }
        public int is_deleted { get; set; }

        public string name { get; set; }

        public string type { get; set; }

        public string url { get; set; }

        public int? size { get; set; }

        public int? view_count { get; set; }

        public string words { get; set; }

        public int? order_num { get; set; }
    }
}
