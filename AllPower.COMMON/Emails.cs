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
    /// ��Ҫ�����ʼ�����
    /// </summary>
    public class Emails
    {
        ///// <summary>
        ///// �ʼ�Ⱥ�巢�� 
        ///// </summary>
        ///// <param name="MailName">�ռ��˼���(ÿ��Email֮���ԣ�����)��ע���λҪ�ж�Email��ַ�ǲ���Ϊ��,��סҪ���ж�</param>
        ///// <param name="body">��������</param>
        ///// <param name="Title">�ʼ�����</param>
        /////  <param name="AlertList">EmailΪ�յ�Э����</param>
        /////   
        //public static string EmailOutPut(ArrayList MailName, string body, string emailTitle, string EmaliserverName, string EmailName, string EmailPass)
        //{
        //    string EmailMessage = "";
        //    try
        //    {
        //        jmail.Message Jmail = new jmail.Message();

        //        string FromEmail = EmailName;//���email  
        //        Jmail.Silent = true;
        //        //Jmail��������־��ǰ��loging��������Ϊtrue
        //        Jmail.Logging = true;
        //        //�ַ�����ȱʡΪ"US-ASCII"
        //        Jmail.Charset = "GB2312";
        //        for (int i = 1; i <= MailName.Count; i++)
        //        {
        //            //����ռ��� 
        //            Jmail.AddRecipient(MailName[i - 1].ToString(), "", "");
        //            //Jmail.AddRecipient("yuanzhigang_t24@163.com", "", "");
        //            Jmail.From = EmailName;
        //            //Jmail.From = "yuanzhi_gang@126.com";
        //            //�������ʼ��û���
        //            Jmail.MailServerUserName = EmailName.Split('@')[0];
        //            //Jmail.MailServerUserName = "yuanzhi_gang";
        //            //�������ʼ�����
        //            //Jmail.MailServerPassWord = "5584162yuanzhi";
        //            Jmail.MailServerPassWord = EmailPass;//FromEmail����ĵ�½����
        //            //�����ʼ�����
        //            Jmail.Subject = emailTitle;
        //            //�ʼ�����
        //            Jmail.Body = body;
        //            Jmail.Priority = 1;//���ʼ������ȼ������Է�Χ��1��5��Խ������ȼ�Լ�ߣ����磬5��ߣ�1���,һ������Ϊ3   
        //            //Jmail���͵ķ���
        //            //EmaliserverName �磺smtp.126.com

        //            bool Validate = Jmail.Send(EmaliserverName, false);
        //            Jmail.ClearAttachments();
        //            Jmail.ClearRecipients();
        //            if (i % 4 == 0)
        //            {
        //                Thread.Sleep(30000);
        //            }
        //            if (Validate == false)
        //            {
        //                EmailMessage = "����ʧ�ܣ����������ʼ���ַ�Ƿ���ȷ";
        //                break;
        //            }
        //            else
        //            {
        //                EmailMessage = "���ͳɹ�";
        //            }
        //        }
        //        Jmail.Close();
        //        return EmailMessage;
        //    }
        //    catch
        //    {
        //        return "�ʼ�������������";
        //    }
        //}

        //public static string SendMail(ArrayList MailName, ArrayList body, string emailTitle, string EmaliserverName, string EmailName, string EmailPass)
        //{
        //    string EmailMessage = "";
        //    try
        //    {
        //        jmail.Message Jmail = new jmail.Message();

        //        string FromEmail = EmailName;//���email  
        //        Jmail.Silent = true;
        //        //Jmail��������־��ǰ��loging��������Ϊtrue
        //        Jmail.Logging = true;
        //        //�ַ�����ȱʡΪ"US-ASCII"
        //        Jmail.Charset = "GB2312";
        //        for (int i = 1; i <= MailName.Count; i++)
        //        {
        //            //����ռ��� 
        //            Jmail.AddRecipient(MailName[i - 1].ToString(), "", "");
        //            //Jmail.AddRecipient("yuanzhigang_t24@163.com", "", "");
        //            Jmail.From = EmailName;
        //            //Jmail.From = "yuanzhi_gang@126.com";
        //            //�������ʼ��û���
        //            Jmail.MailServerUserName = EmailName.Split('@')[0];
        //            //Jmail.MailServerUserName = "yuanzhi_gang";
        //            //�������ʼ�����
        //            //Jmail.MailServerPassWord = "5584162yuanzhi";
        //            Jmail.MailServerPassWord = EmailPass;//FromEmail����ĵ�½����
        //            //�����ʼ�����
        //            Jmail.Subject = emailTitle;
        //            //�ʼ�����
        //            Jmail.Body = body[i - 1].ToString();
        //            Jmail.Priority = 1;//���ʼ������ȼ������Է�Χ��1��5��Խ������ȼ�Լ�ߣ����磬5��ߣ�1���,һ������Ϊ3   
        //            //Jmail���͵ķ���

        //            bool Validate = Jmail.Send("smtp." + EmaliserverName + ".com", false);
        //            Jmail.ClearAttachments();
        //            Jmail.ClearRecipients();
        //            if (i % 4 == 0)
        //            {
        //                Thread.Sleep(30000);
        //            }
        //            if (Validate == false)
        //            {
        //                EmailMessage = "����ʧ�ܣ����������ʼ���ַ�Ƿ���ȷ";
        //                break;
        //            }
        //            else
        //            {
        //                EmailMessage = "���ͳɹ�";
        //            }
        //        }
        //        Jmail.Close();
        //        return EmailMessage;
        //    }
        //    catch
        //    {
        //        return "�ʼ�������������";
        //    }
        //}
        /// <summary>
        /// �����ʼ�
        /// </summary>
        /// <param name="emailTitle">�ʼ�����</param>
        /// <param name="emailContent">�ʼ�����</param>
        /// <param name="emailname">�ռ���</param>
        /// <returns></returns>
        public static int SendMail(string emailTitle, string emailContent, string emailname)
        {
            //PostConfig M_PostConfig = new PostConfig();

            int state = 0;
            //try
            //{
            //    //this.lblTitle.Text = "�����ʼ��ؼ�";

            //    string FilePath = "/main/config/Post.config";
            //    string PhyFilePath = HttpContext.Current.Server.MapPath(FilePath);
            //    M_PostConfig = Post.GetConfig(PhyFilePath);
                
            //    //��ȡ�����ļ���Ϣ
            //    if (M_PostConfig.Email != "")
            //    {
            //        MessageClass Jmail = new MessageClass();
            //        DateTime t = DateTime.Now;
            //        String Subject = emailTitle;
            //        String body = emailContent;
            //        string FromEmail = M_PostConfig.Email;//���email
            //        String ToEmail = emailname;//�Է���email
            //        String AddAttachment = "";
            //        //Silent���ԣ��������Ϊtrue,JMail�����׳��������. JMail. Send( () ����ݲ����������true��false
            //        Jmail.Silent = true;
            //        //Jmail��������־��ǰ��loging��������Ϊtrue
            //        Jmail.Logging = true;
            //        //�ַ�����ȱʡΪ"US-ASCII"
            //        Jmail.Charset = "GB2312";
            //        //�ż���contentype. ȱʡ��"text/plain"�� : �ַ����������HTML��ʽ�����ʼ�, ��Ϊ"text/html"���ɡ�
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

            //                //����ռ���
            //                Jmail.AddRecipient(str[i], "", "");

            //                Jmail.From = FromEmail;
            //                //�������ʼ��û���
            //                Jmail.MailServerUserName = FromEmail;
            //                //�������ʼ�����
            //                Jmail.MailServerPassWord = AllPower.Common.DesSecurity.DesDecrypt(M_PostConfig.Password, "emailpwd");//FromEmail����ĵ�½����
            //                //�����ʼ�����
            //                Jmail.Subject = Subject;
            //                //  �ʼ���Ӹ���,(�฽���Ļ��������ټ�һ��Jmail.AddAttachment( "c:\\test.jpg",true,null);)�Ϳ��Ը㶨�ˡ���ע�ݣ����˸��������������Jmail.ContentType="text/html";ɾ������������ʼ���������롣
            //                if (AddAttachment.Length > 0)
            //                {
            //                    Jmail.AddAttachment(AddAttachment, true, null);
            //                }

            //                //�ʼ�����
            //                Jmail.Body = body + t.ToString();
            //                //�����ļ�
            //                //Jmail.PGPEncrypt = true;
            //                //Jmail���͵ķ��� dtconfig.Rows[0]["EmailServerName"].ToString()
            //                if (Jmail.Send(M_PostConfig.SmtpServer, false))
            //                {
            //                    state = 1;
            //                    //mailserver.com�ʼ������� 
            //                    //Response.Write("<script>alert('���ͳɹ�����')</script>");
            //                }
            //                else
            //                {
            //                    state = 2;
            //                    //  Response.Write("<script>alert('����ʧ�ܣ�����������ĵȼ��Ƿ񳬹�16������')</script>");
            //                    //this.lblMessage.Text = "����ʧ�ܣ�����������ĵȼ��Ƿ񳬹�16������";
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
            //        string FromEmail = "hahabozhai@163.com";//���email
            //        String ToEmail = emailname;//�Է���email
            //        String AddAttachment = "";
            //        //Silent���ԣ��������Ϊtrue,JMail�����׳��������. JMail. Send( () ����ݲ����������true��false
            //        Jmail.Silent = true;
            //        //Jmail��������־��ǰ��loging��������Ϊtrue
            //        Jmail.Logging = true;
            //        //�ַ�����ȱʡΪ"US-ASCII"
            //        Jmail.Charset = "GB2312";
            //        //�ż���contentype. ȱʡ��"text/plain"�� : �ַ����������HTML��ʽ�����ʼ�, ��Ϊ"text/html"���ɡ�
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


            //            //����ռ���
            //            Jmail.AddRecipient(str[i], "", "");

            //            Jmail.From = FromEmail;
            //            //�������ʼ��û���
            //            Jmail.MailServerUserName = FromEmail;
            //            //�������ʼ�����
            //            Jmail.MailServerPassWord = "20030403024";//FromEmail����ĵ�½����
            //            //�����ʼ�����
            //            Jmail.Subject = Subject;
            //            //  �ʼ���Ӹ���,(�฽���Ļ��������ټ�һ��Jmail.AddAttachment( "c:\\test.jpg",true,null);)�Ϳ��Ը㶨�ˡ���ע�ݣ����˸��������������Jmail.ContentType="text/html";ɾ������������ʼ���������롣
            //            if (AddAttachment.Length > 0)
            //            {
            //                Jmail.AddAttachment(AddAttachment, true, null);
            //            }

            //            //�ʼ�����
            //            Jmail.Body = body + t.ToString();
            //            //�����ļ�
            //            //Jmail.PGPEncrypt = true;
            //            //Jmail���͵ķ���
            //            if (Jmail.Send("smtp.163.com", false))
            //            {
            //                state = 1;
            //                //mailserver.com�ʼ������� 
            //                //Response.Write("<script>alert('���ͳɹ�����')</script>");
            //            }
            //            else
            //            {
            //                state = 0;
            //                //  Response.Write("<script>alert('����ʧ�ܣ�����������ĵȼ��Ƿ񳬹�16������')</script>");
            //                //this.lblMessage.Text = "����ʧ�ܣ�����������ĵȼ��Ƿ񳬹�16������";
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
            //    //this.lblTitle.Text = "�����ʼ��ؼ�";

            //    string FilePath = "/main/config/Post.config";
            //    string PhyFilePath = HttpContext.Current.Server.MapPath(FilePath);
            //    M_PostConfig = Post.GetConfig(PhyFilePath);

            //    //��ȡ�����ļ���Ϣ
            //    if (M_PostConfig.Email != "")
            //    {
            //        MessageClass Jmail = new MessageClass();
            //        DateTime t = DateTime.Now;
            //        String Subject = emailTitle;
            //        String body = emailContent;
            //        string FromEmail = M_PostConfig.Email;//���email
            //        String ToEmail = emailname;//�Է���email
            //        String AddAttachment = filename;
            //        //Silent���ԣ��������Ϊtrue,JMail�����׳��������. JMail. Send( () ����ݲ����������true��false
            //        Jmail.Silent = true;
            //        //Jmail��������־��ǰ��loging��������Ϊtrue
            //        Jmail.Logging = true;
            //        //�ַ�����ȱʡΪ"US-ASCII"
            //        Jmail.Charset = "GB2312";
            //        //�ż���contentype. ȱʡ��"text/plain"�� : �ַ����������HTML��ʽ�����ʼ�, ��Ϊ"text/html"���ɡ�
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

            //                //����ռ���
            //                Jmail.AddRecipient(str[i], "", "");

            //                Jmail.From = FromEmail;
            //                //�������ʼ��û���
            //                Jmail.MailServerUserName = FromEmail;
            //                //�������ʼ�����
            //                Jmail.MailServerPassWord = AllPower.Common.DesSecurity.DesDecrypt(M_PostConfig.Password, "emailpwd");//FromEmail����ĵ�½����
            //                //�����ʼ�����
            //                Jmail.Subject = Subject;
            //               // Jmail.AddAttachment(filename, true, null);
            //                //  �ʼ���Ӹ���,(�฽���Ļ��������ټ�һ��Jmail.AddAttachment( "c:\\test.jpg",true,null);)�Ϳ��Ը㶨�ˡ���ע�ݣ����˸��������������Jmail.ContentType="text/html";ɾ������������ʼ���������롣
            //                if (AddAttachment.Length > 0)
            //                {
            //                    Jmail.AddAttachment(AddAttachment, true, null);
            //                }

            //                //�ʼ�����
            //                Jmail.Body = body + t.ToString();
            //                //�����ļ�
            //                //Jmail.PGPEncrypt = true;
            //                //Jmail���͵ķ��� dtconfig.Rows[0]["EmailServerName"].ToString()
            //                if (Jmail.Send(M_PostConfig.SmtpServer, false))
            //                {
            //                    state = 1;
            //                    //mailserver.com�ʼ������� 
            //                    //Response.Write("<script>alert('���ͳɹ�����')</script>");
            //                }
            //                else
            //                {
            //                    state = 2;
            //                    //  Response.Write("<script>alert('����ʧ�ܣ�����������ĵȼ��Ƿ񳬹�16������')</script>");
            //                    //this.lblMessage.Text = "����ʧ�ܣ�����������ĵȼ��Ƿ񳬹�16������";
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
            //        string FromEmail = "hahabozhai@163.com";//���email
            //        String ToEmail = emailname;//�Է���email
            //        String AddAttachment = filename;
            //        //Silent���ԣ��������Ϊtrue,JMail�����׳��������. JMail. Send( () ����ݲ����������true��false
            //        Jmail.Silent = true;
            //        //Jmail��������־��ǰ��loging��������Ϊtrue
            //        Jmail.Logging = true;
            //        //�ַ�����ȱʡΪ"US-ASCII"
            //        Jmail.Charset = "GB2312";
            //        //�ż���contentype. ȱʡ��"text/plain"�� : �ַ����������HTML��ʽ�����ʼ�, ��Ϊ"text/html"���ɡ�
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


            //            //����ռ���
            //            Jmail.AddRecipient(str[i], "", "");

            //            Jmail.From = FromEmail;
            //            //�������ʼ��û���
            //            Jmail.MailServerUserName = FromEmail;
            //            //�������ʼ�����
            //            Jmail.MailServerPassWord = "20030403024";//FromEmail����ĵ�½����
            //            //�����ʼ�����
            //            Jmail.Subject = Subject;
            //            //  �ʼ���Ӹ���,(�฽���Ļ��������ټ�һ��Jmail.AddAttachment( "c:\\test.jpg",true,null);)�Ϳ��Ը㶨�ˡ���ע�ݣ����˸��������������Jmail.ContentType="text/html";ɾ������������ʼ���������롣
            //            if (AddAttachment.Length > 0)
            //            {
            //                Jmail.AddAttachment(AddAttachment, true, null);
            //            }

            //            //�ʼ�����
            //            Jmail.Body = body + t.ToString();
            //            //�����ļ�
            //            //Jmail.PGPEncrypt = true;
            //            //Jmail���͵ķ���
            //            if (Jmail.Send("smtp.163.com", false))
            //            {
            //                state = 1;
            //                //mailserver.com�ʼ������� 
            //                //Response.Write("<script>alert('���ͳɹ�����')</script>");
            //            }
            //            else
            //            {
            //                state = 0;
            //                //  Response.Write("<script>alert('����ʧ�ܣ�����������ĵȼ��Ƿ񳬹�16������')</script>");
            //                //this.lblMessage.Text = "����ʧ�ܣ�����������ĵȼ��Ƿ񳬹�16������";
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
