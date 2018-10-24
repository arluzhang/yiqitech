using System;
using System.Collections.Generic;
using System.Text;
#region 版权注释
/*================================================================
    Copyright (C) 2010 华强北在线
    作者:    周武
    创建时间： 2010年6月4日
    功能描述： 前台调用弹出层
 
// 更新日期        更新人      更新原因/内容
--===============================================================*/
#endregion
namespace AllPower.Common
{
    public class FrontDialog
    {
        private static string strTitle = "系统消息";

        /// <summary>
        /// 对话框标题
        /// </summary>
        public static string StrTitle
        {
            get { return FrontDialog.strTitle; }
            set { FrontDialog.strTitle = value; }
        }
        private static string strContent;

        /// <summary>
        /// 对话框内容
        /// </summary>
        public static string StrContent
        {
            get { return FrontDialog.strContent; }
            set { FrontDialog.strContent = value; }
        }
        private static string strType = "3";
        
        /// <summary>
        /// 对话框图片类型(1为警告,2为错误,3为成功)
        /// </summary>
        public static string StrType
        {
            get { return FrontDialog.strType; }
            set { FrontDialog.strType = value; }
        }
        private static string strFunction;

        /// <summary>
        /// 回调函数,默认返回调用结果
        /// </summary>
        public static string StrFunction
        {
            get { return FrontDialog.strFunction; }
            set { FrontDialog.strFunction = value; }
        }
        private static System.Web.UI.Page page;

        /// <summary>
        /// 调用页面
        /// </summary>
        public static System.Web.UI.Page Page
        {
            get { return page; }
            set { page = value; }
        }

        private static string strUrl; //跳转地址

        /// <summary>
        /// 跳转地址
        /// </summary>
        public static string StrUrl
        {
            get { return FrontDialog.strUrl; }
            set { FrontDialog.strUrl = value; }
        }

        private static string strBack; // 回退页数

        /// <summary>
        /// 回退页数
        /// </summary>
        public static string StrBack
        {
            get { return FrontDialog.strBack; }
            set { FrontDialog.strBack = value; }
        }

        /// <summary>
        /// 弹出对话框
        /// </summary>
        /// <param name="_page">调用页面</param>
        /// <param name="strContent">内容</param>       
        public static string Alter(System.Web.UI.Page _page, string strContent)
        {
            Page = _page;
            StrContent = strContent;
            strUrl = "";
            return Alter();
        }
        /// <summary>
        /// 弹出对话框
        /// </summary>
        /// <param name="_page">调用页面</param>
        /// <param name="strContent">内容</param>
        /// <param name="strUrl">跳转地址</param>  
        public static string Alter(System.Web.UI.Page _page,string strContent,string strUrl)
        {
            Page = _page;
            StrContent = strContent;
            StrUrl = strUrl;
            return Alter();
        }

        /// <summary>
        /// 弹出对话框
        /// </summary>
        /// <param name="strContent">内容</param>
        /// <param name="strUrl">跳转地址</param>
        /// <param name="strType">对话框图片类型(1为警告,2为错误,3为成功)</param>
        public static string Alter(System.Web.UI.Page _page, string strContent, string strUrl, string strType)
        {
            Page = _page;
            StrContent = strContent;
            StrUrl = strUrl;
            StrType = strType;
            return Alter();
        }

        /// <summary>
        /// 弹出对话框
        /// </summary>
        /// <param name="strContent">内容</param>
        /// <param name="strUrl">跳转地址</param>
        /// <param name="strType">对话框图片类型(1为警告,2为错误,3为成功)</param>
        /// <param name="strTitle">标题</param>
        public static string Alter(System.Web.UI.Page _page, string strContent, string strUrl, string strType, string strTitle)
        {
            Page = _page;
            StrContent = strContent;
            StrUrl = strUrl;
            StrType = strType;
            StrTitle = strTitle;
            return Alter();
        }

        /// <summary>
        /// 弹出对话框
        /// </summary>
        /// <param name="_page">调用页面</param>
        /// <param name="strContent">内容</param>
        /// <param name="strUrl">跳转地址</param>  
        public static string Alter(System.Web.UI.Page _page, string strContent, int iBack)
        {
            Page = _page;
            StrContent = strContent;
            StrBack = iBack.ToString();
            StrType = "1";
            strUrl = "";
            return Alter();
        }

        /// <summary>
        /// 弹出对话框
        /// </summary>
        /// <param name="strContent">内容</param>
        /// <param name="strUrl">跳转地址</param>
        /// <param name="strType">对话框图片类型(1为警告,2为错误,3为成功)</param>
        public static string Alter(System.Web.UI.Page _page, string strContent, int iBack, string strType)
        {
            Page = _page;
            StrContent = strContent;
            StrBack = iBack.ToString();
            StrType = strType;
            strUrl = "";
            return Alter();
        }

        /// <summary>
        /// 弹出对话框
        /// </summary>
        public static string Alter()
        {
            StringBuilder sbMessage = new StringBuilder(64);
            sbMessage.Append("pAlert(" + strType + ",'" + strTitle + "','" + strContent + "'");
            if (!string.IsNullOrEmpty(strFunction)) //如果有回调方法
            {
                sbMessage.Append(",function(Boolval){" + strFunction + "(Boolval)}");
            }
            else
            {
                if (!string.IsNullOrEmpty(strUrl))  //是否有跳转地址
                {
                    sbMessage.Append(",function(Boolval){if(Boolval) location.href='" + strUrl + "';}");
                }
                else if (!string.IsNullOrEmpty(strBack) && strBack!="0")    //是否有回退页面
                {
                    sbMessage.Append(",function(Boolval){if(Boolval) window.history.go(" + strBack + ");}");
                }
                else
                {
                    sbMessage.Append(",function(Boolval){}");
                }
            }
            sbMessage.Append(");");
            page.RegisterStartupScript("alert", "<script type='text/javascript'>"+sbMessage.ToString()+"</script>");
            return "<script type='text/javascript'>" + sbMessage.ToString() + "</script>";
           // System.Web.HttpContext.Current.Response.End();
        }

    }
}
