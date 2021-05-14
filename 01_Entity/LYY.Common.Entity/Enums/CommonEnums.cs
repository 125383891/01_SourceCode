using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYY.Common.Entity.Enums
{
    public class CommonEnums
    {
        public static Dictionary<object, string> EnumListDic<T>(string keyDefault, string valueDefault = "")
        {
            Dictionary<object, string> dicEnum = new Dictionary<object, string>();
            Type enumType = typeof(T);
            if (!enumType.IsEnum)
            {
                return dicEnum;
            }
            if (!string.IsNullOrEmpty(keyDefault)) //判断是否添加默认选项
            {
                dicEnum.Add(keyDefault, valueDefault);
            }
            string[] fieldstrs = Enum.GetNames(enumType); //获取枚举字段数组
            foreach (var item in fieldstrs)
            {
                string description = string.Empty;
                var field = enumType.GetField(item);
                object[] arr = field.GetCustomAttributes(typeof(DescriptionAttribute), true); //获取属性字段数组
                if (arr != null && arr.Length > 0)
                {
                    description = ((DescriptionAttribute)arr[0]).Description;   //属性描述
                }
                else
                {
                    description = item;  //描述不存在取字段名称
                }
                dicEnum.Add((int)Enum.Parse(enumType, item), description);  //不用枚举的value值作为字典key值的原因从枚举例子能看出来，其实这边应该判断他的值不存在，默认取字段名称
            }
            return dicEnum;
        }
    }

    public enum UserTagEnums
    {
        [Description("考试-评审专家")]
        UserTag9 = 9,

        [Description("考试-采购人员")]
        UserTag10 = 10,

        [Description("考试-代理人员")]
        UserTag11 = 11
    }
}
