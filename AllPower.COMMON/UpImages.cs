using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.IO;
using System.Drawing;
using System.Collections;
using System.Web.SessionState;

/// <summary>
/// UpImages 的摘要说明
/// </summary>
public class UpImages
{
    /// <summary>
    /// 上传按比例缩放的图片
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    public static string MakeThumbnail(FileUpload file, int width, int height)
    {
        //得到上传文件的大小
        int filesize = file.PostedFile.ContentLength;

        //把文件大小转换成二进制数组
        byte[] arrFile = new byte[filesize];
        if (filesize > 0 && filesize < 1048576)
        {
            string[] getFileType = file.FileName.Split('.');
            //得到文件名称部分
            string imageName = getFileType[0].ToString();
            //得到文件后缀部分
            string suffix = getFileType[1].ToString().ToLower();

            string imagePath = "";
            if (suffix == "jpg" || suffix == "gif" || suffix == "bmp" || suffix == "jpeg")
            {
                Byte[] oFileByte = new byte[filesize];
                System.IO.Stream oStream = file.PostedFile.InputStream;
                System.Drawing.Image oImage = System.Drawing.Image.FromStream(oStream);

                int oWidth = oImage.Width;//原图宽度
                int oHeight = oImage.Height;//原图高度

                int tWidth = width;//设置缩略图初始宽度
                int tHeight = height;//设置缩略图初始高度

                //按比例计算出缩略图的宽度和高度
                if (oWidth >= oHeight)
                {
                    tHeight = (int)Math.Floor(Convert.ToDouble(oHeight) * (Convert.ToDouble(tWidth) / Convert.ToDouble(oWidth)));
                }
                else
                {
                    tWidth = (int)Math.Floor(Convert.ToDouble(oWidth) * (Convert.ToDouble(tHeight) / Convert.ToDouble(oHeight)));
                }

                //生成缩略原图
                Bitmap tImage = new Bitmap(tWidth, tHeight);
                Graphics g = Graphics.FromImage(tImage);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;//设置高质量插值法
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;//设置高质量,低速度呈现平滑程度
                g.Clear(Color.Transparent);//清空画布并以透明背景色填充
                g.DrawImage(oImage, new Rectangle(0, 0, tWidth, tHeight), new Rectangle(0, 0, oWidth, oHeight), GraphicsUnit.Pixel);

                //生成以当前时间为缩略图的名称
                string text = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();

                //string oFullName = System.Web.HttpContext.Current.Server.MapPath("~/upvenueimage/" + text + "." + suffix); //保存原图的物理路径
                string tFullName = System.Web.HttpContext.Current.Server.MapPath("/UploadFiles/main/Images/2011/9/" + text + "." + suffix); //保存缩略图的物理路径

                try
                {
                    //oImage.Save(oFullName, System.Drawing.Imaging.ImageFormat.Jpeg);//上传原图
                    tImage.Save(tFullName, System.Drawing.Imaging.ImageFormat.Jpeg);//上传缩略图

                    imagePath = "2011/9/" + text + "." + suffix;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    oImage.Dispose();
                    g.Dispose();
                    tImage.Dispose();
                }
            }
            else
            {
                imagePath = "error";
            }
            return imagePath;
        }
        return null;
    }
}
