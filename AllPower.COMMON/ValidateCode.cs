using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
#region 版权注释
/*================================================================
    Copyright (C) 2010 华强北在线

    作者:     周武
    创建时间： 2010年4月12日

    功能描述： 验证码

 
// 更新日期        更新人      更新原因/内容
--===============================================================*/
#endregion
namespace AllPower.Common
{
    public class ValidateCode
    {        
        /// <summary>
        /// 验证码生成

        /// </summary>
        public static void Create()
        {
            ValidateCode code = new ValidateCode();
            code.CreateCheckCodeImage(code.GenerateCheckCode(4));
        }
        private string GenerateCheckCode(int iCount)
        {
            int number;
            char code;
            string checkCode = String.Empty;
            System.Random random = new Random();
            for (int i = 0; i < iCount; i++)
            {
                number = random.Next();

                code = (char)('0' + (char)(number % 10));
                //if(number % 2 == 0)
                //    code = (char)('0' + (char)(number % 10));
                //else
                //    code = (char)('a' + (char)(number % 26));

                checkCode += code.ToString();
            }

            System.Web.HttpContext.Current.Response.Cookies.Add(new HttpCookie("CheckCode", checkCode));
            System.Web.HttpContext.Current.Session[SystemConst.SESSION_VALIDATECODE] = checkCode;
            return checkCode;
        }

        private void CreateCheckCodeImage(string checkCode)
        {
            if(checkCode == null || checkCode.Trim() == String.Empty)
                return;
            System.Drawing.Bitmap image = new System.Drawing.Bitmap((int)Math.Ceiling((checkCode.Length * 12.5)), 22);
            Graphics g = Graphics.FromImage(image);
            try
            {
                //生成随机生成器

                Random random = new Random();

                //清空图片背景色

                g.Clear(Color.White);

                ////画图片的背景噪音线

                //for(int i=0; i<25; i++)
                //{
                //    int x1 = random.Next(image.Width);
                //    int x2 = random.Next(image.Width);
                //    int y1 = random.Next(image.Height);
                //    int y2 = random.Next(image.Height);

                //    g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
                //}

                Font font = new System.Drawing.Font("Arial", 12, (System.Drawing.FontStyle.Regular));
                System.Drawing.Drawing2D.LinearGradientBrush brush = new System.Drawing.Drawing2D.LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Blue, Color.Blue, 1.2f, true);
                g.DrawString(checkCode, font, brush, 2, 2);

                ////画图片的前景噪音点

                //for(int i=0; i<100; i++)
                //{
                //    int x = random.Next(image.Width);
                //    int y = random.Next(image.Height);

                //    image.SetPixel(x, y, Color.FromArgb(random.Next()));
                //}

                //画图片的边框线

                g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);

                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                System.Web.HttpContext.Current.Response.ClearContent();
                System.Web.HttpContext.Current.Response.ContentType = "image/Gif";
                System.Web.HttpContext.Current.Response.BinaryWrite(ms.ToArray());
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }
    }
}
