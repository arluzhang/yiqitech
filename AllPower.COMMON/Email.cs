using System;
using System.Collections.Generic;
using System.Text;
using myjmail;
#region 版权注释
/*================================================================
    Copyright (C) 2010 华强北在线

    作者:     周武
    创建时间： 2010年4月14日

    功能描述： 邮件发送

 
// 更新日期        更新人      更新原因/内容
--===============================================================*/
#endregion
namespace AllPower.Common
{
    public class Email
    {
        /// <summary>
        /// 邮箱用户名

        /// </summary>
        private string strUserName;
        /// <summary>
        /// 邮箱用户密码
        /// </summary>
        private string strPassWord;
        /// <summary>
        /// 发件人

        /// </summary>
        private string strFrom;
        /// <summary>
        /// 邮箱主题
        /// </summary>
        private string strSubject;
        /// <summary>
        /// 邮箱正文
        /// </summary>
        private string strBody;
        /// <summary>
        /// 收件人 多个以,隔开
        /// </summary>
        private string strTo;
        /// <summary>
        /// 邮箱smtp
        /// </summary>
        private string strServerSmtp;
        /// <summary>
        /// 页面编码格式
        /// </summary>
        private string strCharset = "gb2312";
        /// <summary>
        /// 页面Encoding
        /// </summary>
        private string strEncoding = "base64";
        /// <summary>
        /// 页面Encoding
        /// </summary>
        public string StrEncoding
        {
            get { return strEncoding; }
            set { strEncoding = value; }
        }
        /// <summary>
        /// 页面编码格式
        /// </summary>
        public string StrCharset
        {
            get { return strCharset; }
            set { strCharset = value; }
        }
        /// <summary>
        /// 收件人 多个以,隔开
        /// </summary>
        public string StrTo
        {
            get { return strTo; }
            set { strTo = value; }
        }
        /// <summary>
        /// 邮箱正文
        /// </summary>
        public string StrBody
        {
            get { return strBody; }
            set { strBody = value; }
        }
        /// <summary>
        /// 邮箱主题
        /// </summary>
        public string StrSubject
        {
            get { return strSubject; }
            set { strSubject = value; }
        }
        /// <summary>
        /// 发件人

        /// </summary>
        public string StrFrom
        {
            get { return strFrom; }
            set { strFrom = value; }
        }
        /// <summary>
        /// 邮箱用户密码
        /// </summary>
        public string StrPassWord
        {
            get { return strPassWord; }
            set { strPassWord = value; }
        }
        /// <summary>
        /// 邮箱用户名

        /// </summary>
        public string StrUserName
        {
            get { return strUserName; }
            set { strUserName = value; }
        }
        /// <summary>
        /// 邮箱smtp
        /// </summary>
        public string StrServerSmtp
        {
            get { return strServerSmtp; }
            set { strServerSmtp = value; }
        }


        /// <summary>
        /// 电子邮件发送

        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="strfrom">发件人邮件</param>
        /// <param name="pwd">密码</param>
        /// <param name="subject">邮件主题</param>
        /// <param name="strbody">邮件正文</param>
        /// <param name="strto">收件人邮件</param>
        /// <param name="strserver">邮件服务器</param>
        /// <returns>发送成功为true,失败为false</returns>
        public bool SendEmail(string username, string strfrom, string pwd, string subject, string strbody, string strto, string strserver)
        {
            strUserName = username;
            strFrom = strfrom;
            strPassWord = pwd;
            strSubject = subject;
            strBody = strbody;
            strTo = strto;
            strServerSmtp = strserver;
            return SendEmail();
        }
        /// <summary>
        /// 邮件发送

        /// </summary>
        /// <returns></returns>
        public bool SendEmail()
        {
            try
            {
                MessageClass jmail = new MessageClass();
                jmail.Silent = true;
                jmail.ContentType = "text/html";
                jmail.FromName = strUserName;
                jmail.Silent = true;
                jmail.Logging = true;
                jmail.Charset = "GB2312";
                jmail.From = strFrom;
                jmail.MailServerUserName = strFrom;
                jmail.MailServerPassWord = strPassWord;
                jmail.Priority = 5;
                jmail.Subject = strSubject;
                jmail.HTMLBody = strBody;
                //jmail.Charset = strCharset;
                jmail.Encoding = StrEncoding;
                jmail.ISOEncodeHeaders = false;
                jmail.AddRecipient(strTo, "", "");
                jmail.Send(strServerSmtp, false);
                jmail.ClearAttachments();
                jmail.ClearRecipients();
                jmail.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw;
                //return false;
            }
        }
    }
}
