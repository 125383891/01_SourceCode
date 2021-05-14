using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYY.InfoManage.Entity.Enums
{
    public class InfoManageEnum
    {
       
    }


    public enum BulletinType
    {

        [Description("行业资讯")]
        Info = 1,
        [Description("考试发布")]
        Release = 2,
        [Description("题库更新")]
        Update = 3
    }


}
