using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace AllPower.Common
{
    #region 版权注释
    /*================================================================
    Copyright (C) 2010 华强北在线
    作者:     陈顺
    创建时间： 2010年3月24日
    功能描述： 安全助手类
    ===============================================================*/
    #endregion
    public class SecurityHelper
    {
        /// <summary>
        /// 对str字符串进行MD5加密
        /// </summary>
        /// <param name="str">要MD5的字符串</param>
        /// <returns></returns>
        public static string MD5(string str)
        {
            if (str == null) str = "";
            byte[] bt = UTF8Encoding.UTF8.GetBytes(str);
            MD5CryptoServiceProvider objMD5 = new MD5CryptoServiceProvider();
            byte[] output = objMD5.ComputeHash(bt);
            return BitConverter.ToString(output);
        }
    }
}
