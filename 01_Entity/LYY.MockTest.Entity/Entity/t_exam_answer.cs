using LYY.MockTest.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYY.MockTest.Entity.Entity
{
    public class t_exam_answer
    {
        public int id { get; set; }
        public int question_id { get; set; }
        public string label { get; set; }
        public string content { get; set; }
        public string img_url { get; set; }
        public Answer_Is_Right? is_right { get; set; }
    }
}
