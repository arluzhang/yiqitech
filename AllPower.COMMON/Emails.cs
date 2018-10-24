using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Threading;
using System.Configuration;
using System.Data;
//using AllPower.Config;
using System.Web;
using myjmail;

namespace AllPower.COMMON
{
    /// <summary>
    /// 主要用于邮件发送
    /// </summary>
    public class Emails
    {
        ///// <summary>
        ///// 邮件群体发送 
        ///// </summary>
        ///// <param name="MailName">收件人集合(每个Email之间以，隔开)请注意各位要判断Email地址是不能为空,记住要做判断</param>
        ///// <param name="body">发送内容</param>
        ///// <param name="Title">邮件标题</param>
        /////  <param name="AlertList">Email为空的协助人</param>
        /////   
        //public static string EmailOutPut(ArrayList MailName, string body, string emailTitle, string EmaliserverName, string EmailName, string EmailPass)
        //{
        //    string EmailMessage = "";
        //    try
        //    {
        //        jmail.Message Jmail = new jmail.Message();

        //        string FromEmail = EmailName;//你的email  
        //        Jmail.Silent = true;
        //        //Jmail创建的日志，前提loging属性设置为true
        //        Jmail.Logging = true;
        //        //字符集，缺省为"US-ASCII"
        //        Jmail.Charset = "GB2312";
        //        for (int i = 1; i <= MailName.Count; i++)
        //        {
        //            //添加收件人 
        //            Jmail.AddRecipient(MailName[i - 1].ToString(), "", "");
        //            //Jmail.AddRecipient("yuanzhigang_t24@163.com", "", "");
        //            Jmail.From = EmailName;
        //            //Jmail.From = "yuanzhi_gang@126.com";
        //            //发件人邮件用户名
        //            Jmail.MailServerUserName = EmailName.Split('@')[0];
        //            //Jmail.MailServerUserName = "yuanzhi_gang";
        //            //发件人邮件密码
        //            //Jmail.MailServerPassWord = "5584162yuanzhi";
        //            Jmail.MailServerPassWord = EmailPass;//FromEmail邮箱的登陆密码
        //            //设置邮件标题
        //            Jmail.Subject = emailTitle;
        //            //邮件内容
        //            Jmail.Body = body;
        //            Jmail.Priority = 1;//‘邮件的优先级，可以范围从1到5。越大的优先级约高，比如，5最高，1最低,一般设置为3   
        //            //Jmail发送的方法
        //            //EmaliserverName 如：smtp.126.com

        //            bool Validate = Jmail.Send(EmaliserverName, false);
        //            Jmail.ClearAttachments();
        //            Jmail.ClearRecipients();
        //            if (i % 4 == 0)
        //            {
        //                Thread.Sleep(30000);
        //            }
        //            if (Validate == false)
        //            {
        //                EmailMessage = "发送失败！请检查您的邮件地址是否正确";
        //                break;
        //            }
        //            else
        //            {
        //                EmailMessage = "发送成功";
        //            }
        //        }
        //        Jmail.Close();
        //        return EmailMessage;
        //    }
        //    catch
        //    {
        //        return "邮件服务器有问题";
        //    }
        //}

        //public static string SendMail(ArrayList MailName, ArrayList body, string emailTitle, string EmaliserverName, string EmailName, string EmailPass)
        //{
        //    string EmailMessage = "";
        //    try
        //    {
        //        jmail.Message Jmail = new jmail.Message();

        //        string FromEmail = EmailName;//你的email  
        //        Jmail.Silent = true;
        //        //Jmail创建的日志，前提loging属性设置为true
        //        Jmail.Logging = true;
        //        //字符集，缺省为"US-ASCII"
        //        Jmail.Charset = "GB2312";
        //        for (int i = 1; i <= MailName.Count; i++)
        //        {
        //            //添加收件人 
        //            Jmail.AddRecipient(MailName[i - 1].ToString(), "", "");
        //            //Jmail.AddRecipient("yuanzhigang_t24@163.com", "", "");
        //            Jmail.From = EmailName;
        //            //Jmail.From = "yuanzhi_gang@126.com";
        //            //发件人邮件用户名
        //            Jmail.MailServerUserName = EmailName.Split('@')[0];
        //            //Jmail.MailServerUserName = "yuanzhi_gang";
        //            //发件人邮件密码
        //            //Jmail.MailServerPassWord = "5584162yuanzhi";
        //            Jmail.MailServerPassWord = EmailPass;//FromEmail邮箱的登陆密码
        //            //设置邮件标题
        //            Jmail.Subject = emailTitle;
        //            //邮件内容
        //            Jmail.Body = body[i - 1].ToString();
        //            Jmail.Priority = 1;//‘邮件的优先级，可以范围从1到5。越大的优先级约高，比如，5最高，1最低,一般设置为3   
        //            //Jmail发送的方法

        //            bool Validate = Jmail.Send("smtp." + EmaliserverName + ".com", false);
        //            Jmail.ClearAttachments();
        //            Jmail.ClearRecipients();
        //            if (i % 4 == 0)
        //            {
        //                Thread.Sleep(30000);
        //            }
        //            if (Validate == false)
        //            {
        //                EmailMessage = "发送失败！请检查您的邮件地址是否正确";
        //                break;
        //            }
        //            else
        //            {
        //                EmailMessage = "发送成功";
        //            }
        //        }
        //        Jmail.Close();
        //        return EmailMessage;
        //    }
        //    catch
        //    {
        //        return "邮件服务器有问题";
        //    }
        //}
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="emailTitle">邮件标题</param>
        /// <param name="emailContent">邮件内容</param>
        /// <param name="emailname">收件人</param>
        /// <returns></returns>
        public static int SendMail(string emailTitle, string emailContent, string emailname)
        {
            //PostConfig M_PostConfig = new PostConfig();

            int state = 0;
            //try
            //{
            //    //this.lblTitle.Text = "发送邮件控件";

            //    string FilePath = "/main/config/Post.config";
            //    string PhyFilePath = HttpContext.Current.Server.MapPath(FilePath);
            //    M_PostConfig = Post.GetConfig(PhyFilePath);
                
            //    //获取配置文件信息
            //    if (M_PostConfig.Email != "")
            //    {
            //        MessageClass Jmail = new MessageClass();
            //        DateTime t = DateTime.Now;
            //        String Subject = emailTitle;
            //        String body = emailContent;
            //        string FromEmail = M_PostConfig.Email;//你的email
            //        String ToEmail = emailname;//对方的email
            //        String AddAttachment = "";
            //        //Silent属性：如果设置为true,JMail不会抛出例外错误. JMail. Send( () 会根据操作结果返回true或false
            //        Jmail.Silent = true;
            //        //Jmail创建的日志，前提loging属性设置为true
            //        Jmail.Logging = true;
            //        //字符集，缺省为"US-ASCII"
            //        Jmail.Charset = "GB2312";
            //        //信件的contentype. 缺省是"text/plain"） : 字符串如果你以HTML格式发送邮件, 改为"text/html"即可。
            //        Jmail.ContentType = "text/html";
            //        ToEmail = ToEmail.Replace("\n", "").Replace(" ", "");
            //        string[] str = ToEmail.Split(',');


            //        for (int i = 0; i < str.Length; i++)
            //        {
            //            if (str[i] != "")
            //            {


            //                if (str[i].IndexOf("hotmail.com") > 0)
            //                {
            //                    Jmail.Charset = "UTF-8";
            //                }
            //                else
            //                {
            //                    Jmail.Charset = "GB2312";
            //                }

            //                //添加收件人
            //                Jmail.AddRecipient(str[i], "", "");

            //                Jmail.From = FromEmail;
            //                //发件人邮件用户名
            //                Jmail.MailServerUserName = FromEmail;
            //                //发件人邮件密码
            //                Jmail.MailServerPassWord = AllPower.Common.DesSecurity.DesDecrypt(M_PostConfig.Password, "emailpwd");//FromEmail邮箱的登陆密码
            //                //设置邮件标题
            //                Jmail.Subject = Subject;
            //                //  邮件添加附件,(多附件的话，可以再加一条Jmail.AddAttachment( "c:\\test.jpg",true,null);)就可以搞定了。［注］：加了附件，讲把上面的Jmail.ContentType="text/html";删掉。否则会在邮件里出现乱码。
            //                if (AddAttachment.Length > 0)
            //                {
            //                    Jmail.AddAttachment(AddAttachment, true, null);
            //                }

            //                //邮件内容
            //                Jmail.Body = body + t.ToString();
            //                //加密文件
            //                //Jmail.PGPEncrypt = true;
            //                //Jmail发送的方法 dtconfig.Rows[0]["EmailServerName"].ToString()
            //                if (Jmail.Send(M_PostConfig.SmtpServer, false))
            //                {
            //                    state = 1;
            //                    //mailserver.com邮件服务器 
            //                    //Response.Write("<script>alert('发送成功！！')</script>");
            //                }
            //                else
            //                {
            //                    state = 2;
            //                    //  Response.Write("<script>alert('发送失败，发件人邮箱的等级是否超过16级！！')</script>");
            //                    //this.lblMessage.Text = "发送失败，发件人邮箱的等级是否超过16级！！";
            //                }
            //                Jmail.ClearAttachments();
            //                Jmail.ClearRecipients();
            //            }

            //        }
            //        Jmail.Close();
            //    }
            //    else
            //    {
            //        MessageClass Jmail = new MessageClass();
            //        DateTime t = DateTime.Now;
            //        String Subject = emailTitle;
            //        String body = emailContent;
            //        string FromEmail = "hahabozhai@163.com";//你的email
            //        String ToEmail = emailname;//对方的email
            //        String AddAttachment = "";
            //        //Silent属性：如果设置为true,JMail不会抛出例外错误. JMail. Send( () 会根据操作结果返回true或false
            //        Jmail.Silent = true;
            //        //Jmail创建的日志，前提loging属性设置为true
            //        Jmail.Logging = true;
            //        //字符集，缺省为"US-ASCII"
            //        Jmail.Charset = "GB2312";
            //        //信件的contentype. 缺省是"text/plain"） : 字符串如果你以HTML格式发送邮件, 改为"text/html"即可。
            //        Jmail.ContentType = "text/html";
            //        ToEmail = ToEmail.Replace("\n", "").Replace(" ", "");
            //        string[] str = ToEmail.Split(',');


            //        for (int i = 0; i < str.Length; i++)
            //        {
            //            if (str[i].IndexOf("hotmail.com") > 0)
            //            {
            //                Jmail.Charset = "UTF-8";
            //            }
            //            else
            //            {
            //                Jmail.Charset = "GB2312";
            //            }


            //            //添加收件人
            //            Jmail.AddRecipient(str[i], "", "");

            //            Jmail.From = FromEmail;
            //            //发件人邮件用户名
            //            Jmail.MailServerUserName = FromEmail;
            //            //发件人邮件密码
            //            Jmail.MailServerPassWord = "20030403024";//FromEmail邮箱的登陆密码
            //            //设置邮件标题
            //            Jmail.Subject = Subject;
            //            //  邮件添加附件,(多附件的话，可以再加一条Jmail.AddAttachment( "c:\\test.jpg",true,null);)就可以搞定了。［注］：加了附件，讲把上面的Jmail.ContentType="text/html";删掉。否则会在邮件里出现乱码。
            //            if (AddAttachment.Length > 0)
            //            {
            //                Jmail.AddAttachment(AddAttachment, true, null);
            //            }

            //            //邮件内容
            //            Jmail.Body = body + t.ToString();
            //            //加密文件
            //            //Jmail.PGPEncrypt = true;
            //            //Jmail发送的方法
            //            if (Jmail.Send("smtp.163.com", false))
            //            {
            //                state = 1;
            //                //mailserver.com邮件服务器 
            //                //Response.Write("<script>alert('发送成功！！')</script>");
            //            }
            //            else
            //            {
            //                state = 0;
            //                //  Response.Write("<script>alert('发送失败，发件人邮箱的等级是否超过16级！！')</script>");
            //                //this.lblMessage.Text = "发送失败，发件人邮箱的等级是否超过16级！！";
            //            }
            //            Jmail.ClearAttachments();
            //            Jmail.ClearRecipients();

            //        }
            //        Jmail.Close();
            //    }
                
            //}
            //catch (Exception ex)
            //{
            //    state = 0;
            //}

            return state;
        }

        public static int SendMail(string emailTitle, string emailContent, string emailname,string filename)
        {
            //PostConfig M_PostConfig = new PostConfig();

            int state = 0;
            //try
            //{
            //    //this.lblTitle.Text = "发送邮件控件";

            //    string FilePath = "/main/config/Post.config";
            //    string PhyFilePath = HttpContext.Current.Server.MapPath(FilePath);
            //    M_PostConfig = Post.GetConfig(PhyFilePath);

            //    //获取配置文件信息
            //    if (M_PostConfig.Email != "")
            //    {
            //        MessageClass Jmail = new MessageClass();
            //        DateTime t = DateTime.Now;
            //        String Subject = emailTitle;
            //        String body = emailContent;
            //        string FromEmail = M_PostConfig.Email;//你的email
            //        String ToEmail = emailname;//对方的email
            //        String AddAttachment = filename;
            //        //Silent属性：如果设置为true,JMail不会抛出例外错误. JMail. Send( () 会根据操作结果返回true或false
            //        Jmail.Silent = true;
            //        //Jmail创建的日志，前提loging属性设置为true
            //        Jmail.Logging = true;
            //        //字符集，缺省为"US-ASCII"
            //        Jmail.Charset = "GB2312";
            //        //信件的contentype. 缺省是"text/plain"） : 字符串如果你以HTML格式发送邮件, 改为"text/html"即可。
            //      //  Jmail.ContentType = "text/html";
            //        ToEmail = ToEmail.Replace("\n", "").Replace(" ", "");
            //        string[] str = ToEmail.Split(',');


            //        for (int i = 0; i < str.Length; i++)
            //        {
            //            if (str[i] != "")
            //            {


            //                if (str[i].IndexOf("hotmail.com") > 0)
            //                {
            //                    Jmail.Charset = "UTF-8";
            //                }
            //                else
            //                {
            //                    Jmail.Charset = "GB2312";
            //                }

            //                //添加收件人
            //                Jmail.AddRecipient(str[i], "", "");

            //                Jmail.From = FromEmail;
            //                //发件人邮件用户名
            //                Jmail.MailServerUserName = FromEmail;
            //                //发件人邮件密码
            //                Jmail.MailServerPassWord = AllPower.Common.DesSecurity.DesDecrypt(M_PostConfig.Password, "emailpwd");//FromEmail邮箱的登陆密码
            //                //设置邮件标题
            //                Jmail.Subject = Subject;
            //               // Jmail.AddAttachment(filename, true, null);
            //                //  邮件添加附件,(多附件的话，可以再加一条Jmail.AddAttachment( "c:\\test.jpg",true,null);)就可以搞定了。［注］：加了附件，讲把上面的Jmail.ContentType="text/html";删掉。否则会在邮件里出现乱码。
            //                if (AddAttachment.Length > 0)
            //                {
            //                    Jmail.AddAttachment(AddAttachment, true, null);
            //                }

            //                //邮件内容
            //                Jmail.Body = body + t.ToString();
            //                //加密文件
            //                //Jmail.PGPEncrypt = true;
            //                //Jmail发送的方法 dtconfig.Rows[0]["EmailServerName"].ToString()
            //                if (Jmail.Send(M_PostConfig.SmtpServer, false))
            //                {
            //                    state = 1;
            //                    //mailserver.com邮件服务器 
            //                    //Response.Write("<script>alert('发送成功！！')</script>");
            //                }
            //                else
            //                {
            //                    state = 2;
            //                    //  Response.Write("<script>alert('发送失败，发件人邮箱的等级是否超过16级！！')</script>");
            //                    //this.lblMessage.Text = "发送失败，发件人邮箱的等级是否超过16级！！";
            //                }
            //                Jmail.ClearAttachments();
            //                Jmail.ClearRecipients();
            //            }

            //        }
            //        Jmail.Close();
            //    }
            //    else
            //    {
            //        MessageClass Jmail = new MessageClass();
            //        DateTime t = DateTime.Now;
            //        String Subject = emailTitle;
            //        String body = emailContent;
            //        string FromEmail = "hahabozhai@163.com";//你的email
            //        String ToEmail = emailname;//对方的email
            //        String AddAttachment = filename;
            //        //Silent属性：如果设置为true,JMail不会抛出例外错误. JMail. Send( () 会根据操作结果返回true或false
            //        Jmail.Silent = true;
            //        //Jmail创建的日志，前提loging属性设置为true
            //        Jmail.Logging = true;
            //        //字符集，缺省为"US-ASCII"
            //        Jmail.Charset = "GB2312";
            //        //信件的contentype. 缺省是"text/plain"） : 字符串如果你以HTML格式发送邮件, 改为"text/html"即可。
            //      //  Jmail.ContentType = "text/html";
            //        ToEmail = ToEmail.Replace("\n", "").Replace(" ", "");
            //        string[] str = ToEmail.Split(',');


            //        for (int i = 0; i < str.Length; i++)
            //        {
            //            if (str[i].IndexOf("hotmail.com") > 0)
            //            {
            //                Jmail.Charset = "UTF-8";
            //            }
            //            else
            //            {
            //                Jmail.Charset = "GB2312";
            //            }


            //            //添加收件人
            //            Jmail.AddRecipient(str[i], "", "");

            //            Jmail.From = FromEmail;
            //            //发件人邮件用户名
            //            Jmail.MailServerUserName = FromEmail;
            //            //发件人邮件密码
            //            Jmail.MailServerPassWord = "20030403024";//FromEmail邮箱的登陆密码
            //            //设置邮件标题
            //            Jmail.Subject = Subject;
            //            //  邮件添加附件,(多附件的话，可以再加一条Jmail.AddAttachment( "c:\\test.jpg",true,null);)就可以搞定了。［注］：加了附件，讲把上面的Jmail.ContentType="text/html";删掉。否则会在邮件里出现乱码。
            //            if (AddAttachment.Length > 0)
            //            {
            //                Jmail.AddAttachment(AddAttachment, true, null);
            //            }

            //            //邮件内容
            //            Jmail.Body = body + t.ToString();
            //            //加密文件
            //            //Jmail.PGPEncrypt = true;
            //            //Jmail发送的方法
            //            if (Jmail.Send("smtp.163.com", false))
            //            {
            //                state = 1;
            //                //mailserver.com邮件服务器 
            //                //Response.Write("<script>alert('发送成功！！')</script>");
            //            }
            //            else
            //            {
            //                state = 0;
            //                //  Response.Write("<script>alert('发送失败，发件人邮箱的等级是否超过16级！！')</script>");
            //                //this.lblMessage.Text = "发送失败，发件人邮箱的等级是否超过16级！！";
            //            }
            //            Jmail.ClearAttachments();
            //            Jmail.ClearRecipients();

            //        }
            //        Jmail.Close();
            //    }

            //}
            //catch (Exception ex)
            //{
            //    state = 0;
            //}

            return state;
        }

        
    }
}
