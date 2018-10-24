using System;
using System.Collections.Generic;
using System.Text;
#region 版权注释
/*================================================================
    Copyright (C) 2010 华强北在线
 
// 更新日期        更新人      更新原因/内容
//2010-9-14        胡志瑶      分页标签的标签名开头为：{HQB_SPLIT_
--===============================================================*/
#endregion
namespace AllPower.Common
{
    public class TemplateTools
    {
        /// <summary>
        /// 提取 标签中的名称 如：{HQB_标签名称} 提取后为：标签名称
        /// </summary>
        /// <param name="lable"></param>
        /// <returns></returns>
        public static string GetLableName(string lable, int isSystem)
        {
            if (lable.IndexOf("{HQB_") < 0 || lable.IndexOf("}") < 0)
                return "";
            switch (isSystem)
            {
                case 0:  //静态标签
                    lable = lable.Substring(lable.IndexOf("{HQB_STATIC_") + 12);
                    break;
                case 1:  //系统标签
                    lable = lable.Substring(lable.IndexOf("{HQB_SYSTEM_") + 12);
                    break;
                case 2:  //分页标签
                    lable = lable.Substring(lable.IndexOf("{HQB_SPLIT_") + 11);
                    break;
                case 10: //自由标签
                    lable = lable.Substring(lable.IndexOf("{HQB_FREE_") + 10);
                    break;
                default:
                    lable = lable.Substring(lable.IndexOf("{HQB_") + 5);
                    break;
            }
            return lable.Substring(0, lable.LastIndexOf("}"));
        }
    }
}
