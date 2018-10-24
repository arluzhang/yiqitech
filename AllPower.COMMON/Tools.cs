using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Net;
using System.Runtime.Remoting.Contexts;
using System.Web.UI;


namespace AllPower.Common
{
    /// <summary>
    /// 前台常用方法
    /// </summary>
    public class Tools
    {
        #region 根据sql语句查询数据，返回DataTable
        public static DataTable GetDataSet(string sql)
        {
            DataTable dt = new DataTable();
            dt = AllPower.Common.SQLHelper.ExecuteDataSet(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, sql).Tables[0];
            return dt;
        }
        #endregion
        #region 根据参数数组，执行存储过程，返回DataTable主要用于分页
        public static DataTable GetDataSetByParam(SqlParameter[] param)
        {
            DataTable dt = new DataTable();
            dt = AllPower.Common.SQLHelper.ExecuteDataSet(SQLHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, "proc_SplitPager", param).Tables[1];
            return dt;
        }
        #endregion

        #region 返回指定格式的日期
        public static string returnChinaTimeByStyle(DateTime dt, string style)
        {
            int day = dt.Day;
            string days = "";

            int month = dt.Month;
            string months = "";

            if (month < 10)
            {
                months = "0" + month;
            }
            else
            {
                months = month + "";
            }

            if (day < 10)
            {
                days = "0" + day;
            }
            else
            {
                days = day + "";
            }
            return dt.Year + style + months + style + days;
        }

        public static string returnChinaTime(DateTime dt)
        {
            int day = dt.Day;
            string days = "";

            int month = dt.Month;
            string months = "";

            if (month < 10)
            {
                months = "0" + month;
            }
            else
            {
                months = month + "";
            }

            if (day < 10)
            {
                days = "0" + day;
            }
            else
            {
                days = day + "";
            }
            return dt.Year + "年" + months + "月" + days + "日";
        }

        public static string returnShortChinaTime(DateTime dt, string style)
        {
            int day = dt.Day;
            string days = "";

            int month = dt.Month;
            string months = "";

            if (month < 10)
            {
                months = "0" + month;
            }
            else
            {
                months = month + "";
            }

            if (day < 10)
            {
                days = "0" + day;
            }
            else
            {
                days = day + "";
            }
            return months + style + days;
        }
        #endregion

        #region 日期操作
        /// <summary>
        /// 返回标准日期格式string
        /// </summary>
        public static string GetDate()
        {
            return DateTime.Now.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 返回指定日期格式
        /// </summary>
        public static string GetDate(string datetimestr, string replacestr)
        {
            if (datetimestr == null)
                return replacestr;

            if (datetimestr.Equals(""))
                return replacestr;

            try
            {
                datetimestr = Convert.ToDateTime(datetimestr).ToString("yyyy-MM-dd").Replace("1900-01-01", replacestr);
            }
            catch
            {
                return replacestr;
            }
            return datetimestr;
        }


        /// <summary>
        /// 返回标准时间格式string
        /// </summary>
        public static string GetTime()
        {
            return DateTime.Now.ToString("HH:mm:ss");
        }

        /// <summary>
        /// 返回标准时间格式string
        /// </summary>
        public static string GetDateTime()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 返回相对于当前时间的相对天数
        /// </summary>
        public static string GetDateTime(int relativeday)
        {
            return DateTime.Now.AddDays(relativeday).ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 返回标准时间格式string
        /// </summary>
        public static string GetDateTimeF()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fffffff");
        }

        /// <summary>
        /// 返回标准时间 
        /// </sumary>
        public static string GetStandardDateTime(string fDateTime, string formatStr)
        {
            if (fDateTime == "0000-0-0 0:00:00")
                return fDateTime;
            DateTime time = new DateTime(1900, 1, 1, 0, 0, 0, 0);
            if (DateTime.TryParse(fDateTime, out time))
                return time.ToString(formatStr);
            else
                return "";
        }

        /// <summary>
        /// 返回标准时间 yyyy-MM-dd HH:mm:ss
        /// </sumary>
        public static string GetStandardDateTime(string fDateTime)
        {
            return GetStandardDateTime(fDateTime, "yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 返回标准时间 yyyy-MM-dd HH
        /// </sumary>
        public static string GetStandardDateTime1(string fDateTime)
        {
            return GetStandardDateTime(fDateTime, "yyyy-MM-dd");
        }

        /// <summary>
        /// 返回标准时间 yyyy-MM-dd HH
        /// </sumary>
        public static string GetStandardDateTime2(string fDateTime)
        {
            return GetStandardDateTime(fDateTime, "MM/dd/yyyy").Replace("-", "/");
        }

        /// <summary>
        /// 返回标准时间 yyyy-MM-dd HH
        /// </sumary>
        public static string GetStandardDateTime3(string fDateTime)
        {
            return GetStandardDateTime(fDateTime, "yyyy年MM月dd日");
        }
        #endregion

        #region 清除查询字符串的危险字符
        /// <summary>
        /// 清除查询字符串的危险字符
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static string CheckSql(string sql)
        {
            string reSql = "";
            if (sql == null)
            {
                return reSql;
            }
            else
            {
                reSql = sql;
                reSql = reSql.ToLower().Replace("\"", "&quot;");
                reSql = reSql.ToLower().Replace("<", "&lt;");
                reSql = reSql.ToLower().Replace(">", "&gt;");
                reSql = reSql.Replace("script", "&#115;cript");
                reSql = reSql.Replace("SCRIPT", "&#083;CRIPT");
                reSql = reSql.Replace("Script", "&#083;cript");
                reSql = reSql.Replace("script", "&#083;cript");
                reSql = reSql.Replace("object", "&#111;bject");
                reSql = reSql.Replace("OBJECT", "&#079;BJECT");
                reSql = reSql.Replace("Object", "&#079;bject");
                reSql = reSql.Replace("object", "&#079;bject");
                reSql = reSql.Replace("applet", "&#097;pplet");
                reSql = reSql.Replace("APPLET", "&#065;PPLET");
                reSql = reSql.Replace("Applet", "&#065;pplet");
                reSql = reSql.Replace("applet", "&#065;pplet");
                reSql = reSql.ToLower().Replace("[", "&#091;");
                reSql = reSql.ToLower().Replace("]", "&#093;");
                reSql = reSql.ToLower().Replace("=", "&#061;");
                reSql = reSql.ToLower().Replace("'", "''");
                reSql = reSql.ToLower().Replace("select", "&#115;elect");
                reSql = reSql.ToLower().Replace("execute", "&#101xecute");
                reSql = reSql.ToLower().Replace("exec", "&#101xec");
                reSql = reSql.ToLower().Replace("join", "&#106;oin");
                reSql = reSql.ToLower().Replace("union", "&#117;nion");
                reSql = reSql.ToLower().Replace("where", "&#119;here");
                reSql = reSql.ToLower().Replace("insert", "&#105;nsert");
                reSql = reSql.ToLower().Replace("delete", "&#100;elete");
                reSql = reSql.ToLower().Replace("update", "&#117;pdate");
                reSql = reSql.ToLower().Replace("like", "&#108;ike");
                reSql = reSql.ToLower().Replace("drop", "&#100;rop");
                reSql = reSql.ToLower().Replace("create", "&#99;reate");
                reSql = reSql.ToLower().Replace("rename", "&#114;ename");
                reSql = reSql.ToLower().Replace("count", "co&#117;nt");
                reSql = reSql.ToLower().Replace("chr", "c&#104;r");
                reSql = reSql.ToLower().Replace("mid", "m&#105;d");
                reSql = reSql.ToLower().Replace("truncate", "trunc&#097;te");
                reSql = reSql.ToLower().Replace("nchar", "nch&#097;r");
                reSql = reSql.ToLower().Replace("char", "ch&#097;r");
                reSql = reSql.ToLower().Replace("alter", "&#97;lter");
                reSql = reSql.ToLower().Replace("cast", "&#99;ast");
                reSql = reSql.ToLower().Replace("exists", "e&#120;ists");
                reSql = reSql.ToLower().Replace("\n", "<br>");
                return reSql;
            }
        }
        #endregion

        #region 得到字符串的长度

        /// <summary>
        /// 得到字符串的长度
        /// 创建者：袁纯林   时间：2008-3-4
        /// 修改者：         时间： 
        /// </summary>
        /// <param name="strData"></param>
        /// <returns></returns>
        public static int GetStrLen(string strData)
        {
            System.Text.Encoding encoder5 = System.Text.Encoding.GetEncoding("GB2312");
            return encoder5.GetByteCount(strData);
        }
        #endregion

        #region 切割字符串

        /// <summary>
        /// 切割字符串
        /// 创建者：袁纯林   时间：2008-3-4
        /// 修改者：         时间： 
        /// </summary>
        /// <param name="s">需要切割的字符串</param>
        /// <param name="i">需要切割的字符串长度</param>
        /// <param name="smore">切割后字符串后面添加的字符串</param>
        /// <returns></returns>
        public static string SubStr(string s, int i, string smore)
        {
            int intResult = 0;
            int j = 0;
            string s1 = s;
            if (GetStrLen(s) > i)
            {
                foreach (char Char in s)
                {
                    if (intResult < i)
                    {
                        j++;
                        if ((int)Char > 127)
                            intResult = intResult + 2;
                        else
                            intResult++;
                    }
                    else
                        break;
                }
                s1 = s.Substring(0, j);
            }
            else
            {
                return s1;
            }
            return s1 + smore;
        }
        public static string CutStr(string s, int i, string smore)
        {
            if (s.Length > i)
                return s.Substring(0, i) + smore;
            else
                return s;
        }
        #endregion


        #region 去除html标签
        ///   <summary>
        ///   去除HTML标记
        ///   </summary>
        ///   <param   name="NoHTML">包括HTML的源码   </param>
        ///   <returns>已经去除后的文字</returns>
        public static string NoHTML(string Htmlstring)
        {
            //删除脚本 
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除HTML 
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);

            Htmlstring.Replace("<", "");
            Htmlstring.Replace(">", "");
            Htmlstring.Replace("\r\n", "");
            Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();
            return Htmlstring;
        }
        #endregion



        #region 分页
        public static DataSet GetSplitPage(int PageIndex, int PageSize, string strSql, string OrderByField)
        {
            SqlParameter[] prams = new SqlParameter[]{
                new SqlParameter("@Id",""),
                new SqlParameter("@Table",""),             
                new SqlParameter("@Where",""),
                new SqlParameter("@Cou","*"),
                new SqlParameter("@NewPageIndex",PageIndex),
                new SqlParameter("@PageSize",PageSize), 
                new SqlParameter("@order",OrderByField),                
                new  SqlParameter("@isSql",1),
                new  SqlParameter("@strSql",strSql)
            };

            DataSet ds = SQLHelper.ExecuteDataSet(SQLHelper.ConnectionStringLocalTransaction,
                      CommandType.StoredProcedure, "proc_Pager", prams);

            return ds;

        }
        #endregion

        #region DataSet ds(SqlStr)
        /// <summary>
        /// return SQLHelper.ExecuteDataSet
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public static DataSet ds(string strSql)
        {
            DataSet ds = SQLHelper.ExecuteDataSet(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql);
            if (ds != null)
            {
                if (ds.Tables.Count < 1) return null;
                if (ds.Tables[0].Rows.Count < 1) return null;
            }
            return ds;
        }
        #endregion

        #region 根据网址得到HTML源代码

        /// <summary>
        /// 根据网址得到HTML源代码

        /// </summary>
        /// <param name="strUrl">网址</param>
        /// <returns></returns>
        public static string GetUrlHTML(string strUrl)
        {
            return GetUrlHTML(strUrl, "utf-8");
        }

        /// <summary>
        /// 根据网址得到HTML源代码

        /// </summary>
        /// <param name="strUrl">网址</param>
        /// <param name="strEncode">编码</param>
        /// <returns></returns>
        public static string GetUrlHTML(string strUrl, string strEncode)
        {
            WebClient web = new WebClient();
            web.Encoding = Encoding.GetEncoding(strEncode);
            return web.DownloadString(strUrl);
        }
        #endregion

        #region 判断字符串是否数字组成，不是则返回0
        public static int Cint(string str)
        {
            if (str == null || str == "")
                return 0;
            string sChar = "";
            string sNum = "0123456789";
            for (int i = 0; i < str.Length; i++)
            {
                sChar = str.Substring(i, 1);
                if (sNum.IndexOf(sChar) == -1)
                {
                    return 0;
                }
            }
            return Convert.ToInt32(str);
        }
        #endregion

        #region 判断字符串是否数字组成，不是则返回0
        public static double Cdouble(string str)
        {
            if (str == null || str == "")
                return 0;
            string sChar = "";
            string sNum = "0123456789";
            for (int i = 0; i < str.Length; i++)
            {
                sChar = str.Substring(i, 1);
                if (sNum.IndexOf(sChar) == -1)
                {
                    return 0;
                }
            }
            return Convert.ToDouble(str);
        }
        #endregion

        #region 新闻内容分页
        /// <summary>
        /// 新闻内容分页
        /// </summary>
        /// <param name="content">新闻内容</param>
        /// <param name="extension">扩展名(aspx,html..)</param>
        /// <param name="url">index.aspx?</param>
        /// <returns></returns>
        public static string NewsContentPager(string content, string extension, string url)
        {
            string p = "\\[NextPage]";
            if (content.IndexOf("[NextPage]") != -1)
            {
                string page = HttpContext.Current.Request.QueryString["page"];
                string[] arrContent = Regex.Split(content, p, RegexOptions.IgnoreCase);
                int pageSize = arrContent.Length;

                if (String.IsNullOrEmpty(page))
                    page = "0";

                if (int.Parse(page) >= pageSize)
                {
                    HttpContext.Current.Response.StatusCode = 404;
                    HttpContext.Current.Response.End();
                }

                //生成分页页码
                StringBuilder sb = new StringBuilder();
                sb.Append(arrContent[int.Parse(page)].ToString());
                sb.Append("<br/><div id=\"newspager\" style=\"margin-top:10px; margin:0px auto; text-align:center;width:600px;\">");

                if (int.Parse(page) > 0)

                    sb.AppendFormat("<a style=\"color:black;\" href=\"" + url + "&page={0}\">上一页</a>&nbsp;&nbsp;&nbsp;&nbsp;", int.Parse(page) - 1);
                for (int i = 0; i < pageSize; i++)
                {
                    if (i == int.Parse(page))
                        sb.AppendFormat("<span><strong>{0}</strong></span>&nbsp;&nbsp;&nbsp;&nbsp;", i + 1);
                    else
                        sb.AppendFormat("<a style=\"color:black;\" href=\"" + url + "&page={0}\">{1}</a>&nbsp;&nbsp;&nbsp;&nbsp;", i, i + 1);
                }

                if (int.Parse(page) < pageSize - 1)

                    sb.AppendFormat("<a  style=\"color:black;\" href=\"" + url + "&page={0}\">下一页</a>", int.Parse(page) + 1);

                sb.Append("</div>");

                return sb.ToString();
            }

            return content;

        }
        #endregion


        #region 获取网站搜索的关键字和描述
        public static string GetKeyWords(string nodecode, string Fieldname)
        {
            string keywords = string.Empty;
            DataTable dt = Tools.GetDataSet("select " + Fieldname + " from K_SysmoduleNode where NodeCode='" + nodecode + "' and WebSiteID=1 and IsDel=0");
            if (dt.Rows.Count > 0)
            {
                keywords = dt.Rows[0]["" + Fieldname + ""].ToString();
            }
            return keywords;
        }
        #endregion

        #region 获取当前栏目名称
        /// <summary>
        /// 获取当前栏目名称
        /// </summary>
        /// <param name="nodecode">当前栏目编号</param>
        /// <param name="siteId">当前站点编号</param>
        /// <returns></returns>
        public static string GetCurrentNodeName(string nodecode, int websiteId)
        {
            string nodename = string.Empty;
            DataTable dt = Tools.GetDataSet("select NodeName from K_sysmodulenode where NodeCode='" + nodecode + "' and WebSiteID=" + websiteId + " and IsDel=0");
            if (dt != null && dt.Rows.Count > 0)
            {
                nodename = dt.Rows[0]["NodeName"].ToString();
            }
            return nodename;
        }
        #endregion
        #region 获取当前栏目名称
        /// <summary>
        /// 获取当前栏目名称
        /// </summary>
        /// <param name="nodecode">当前栏目编号</param>
        /// <param name="siteId">当前站点编号</param>
        /// <returns></returns>
        public static string GetTypeName(string TypeID)
        {
            string nodename = string.Empty;
            DataTable dt = Tools.GetDataSet("select TypeName from K_Types where typeid='" + TypeID + "'");
            if (dt != null && dt.Rows.Count > 0)
            {
                nodename = dt.Rows[0]["TypeName"].ToString();
            }
            return nodename;
        }
        #endregion

        #region 获取K_SinglePage单页内容
        public static string GetSinglePageContent(string nodecode)
        {
            string content = string.Empty;
            DataTable dt = Tools.GetDataSet("select Content from K_singlepage where NodeCode='" + nodecode + "'");
            if (dt != null && dt.Rows.Count > 0)
            {
                content = dt.Rows[0]["Content"].ToString();
            }
            return content;
        }
        #endregion

        #region 修改新闻浏览次数
        public static void UpdateNewsHits(string Id)
        {
            string sql = "update K_U_Article set Hits=Hits+1 where ID='" + Id + "'";
            SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, null);
        }

        public static void UpdateNewsHits(string Id, string tablename)
        {
            string sql = "update " + tablename + " set Hits=Hits+1 where ID='" + Id + "'";
            SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, null);
        }
        #endregion

        #region 根据新闻ID查询新闻标题
        public static string GetNewsTitle(string Id)
        {
            string newstitle = string.Empty;
            DataTable dt = Tools.GetDataSet("select Title,UpdateDate from K_U_Article where ID='" + Id + "'");
            if (dt.Rows.Count > 0)
            {
                newstitle = dt.Rows[0]["Title"].ToString() + "[ " + Tools.returnShortChinaTime(Convert.ToDateTime(dt.Rows[0]["UpdateDate"]), "-") + " ]";
            }
            return newstitle;
        }

        public static string GetTitle(string Id, string tablename)
        {
            string newstitle = string.Empty;
            DataTable dt = Tools.GetDataSet("select Title,UpdateDate from " + tablename + " where ID='" + Id + "'");
            if (dt.Rows.Count > 0)
            {
                newstitle = dt.Rows[0]["Title"].ToString() + "[ " + Tools.returnShortChinaTime(Convert.ToDateTime(dt.Rows[0]["UpdateDate"]), "-") + " ]";
            }
            return newstitle;
        }
        #endregion

        #region 写Cookie到客户机器

        /// <summary>
        /// 写Cookie到客户机器

        /// </summary>
        /// <param name="CookieName">Cookie名称</param>
        /// <param name="MemberId">会员登陆ID</param>
        /// <param name="VCode">验证码</param>
        /// <param name="TimeToExpire">到期时间点</param>
        /// <returns>写操作是否成功标识</returns>
        public static bool WriteCookie(string CookieName, string UserName)
        {
            try
            {
                HttpCookie cookie = new HttpCookie(CookieName);//定义cookie对象以及名为Info的项
                DateTime dt = DateTime.Now;

                //cookie.Value = triDes.Encrypt(MemberId+"#"+MemberPwd);
                cookie.Value = UserName;
                cookie.Expires = dt.AddHours(1);
                if (HttpContext.Current.Request.Cookies[CookieName] != null)
                {
                    HttpContext.Current.Response.Cookies.Remove(CookieName);
                    HttpContext.Current.Response.Cookies.Add(cookie);
                }
                else
                {

                    HttpContext.Current.Response.AppendCookie(cookie);//确定写入cookie中

                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        #endregion

        /// <summary>
        /// 清除指定Cookie
        /// </summary>
        /// <param name="cookiename">cookiename</param>
        public static void ClearCookie(string cookiename)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookiename];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddYears(-3);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }

        #region 蔡志伟加的
        #region 字符串的处理方法（能把Content属性中的图片过滤）
        /// <summary>
        /// 字符串的处理方法（能把Content属性中的图片过滤）
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static string GetSubHtml(string htmlString)
        {
            return Regex.Replace(htmlString, @"<[\s\S]*?>", "", RegexOptions.IgnoreCase);
        }

        public static string FormatStr(string str, int length)
        {
            return (SubStr(GetSubHtml(str).Replace("&nbsp;", ""), length, "..."));
        }
        #endregion

        #region 字符串根据长度分割  20110906新增
        /// <summary>
        /// 字符串根据长度分割
        /// </summary>
        /// <param name="str">分割的字符串</param>
        /// <param name="strLength">分割字符串的长度</param>
        /// <returns>以长度分割用|分开的字符串</returns>
        public static string StringApart(string str, int strLength)
        {
            string strs = string.Empty;
            int strlg = str.Length / strLength;

            if (str.Length % strLength > 0)
            {
                strlg += 1;
            }

            for (int i = 0; i < strlg; i++)
            {
                if (i + 1 == strlg)
                {
                    strs += str.Substring(i * strLength, str.Length - (i * strLength));
                }
                else
                {
                    strs += str.Substring(i * strLength, strLength) + "|";
                }
            }

            return strs;
        }

        #endregion



        #region  根据指定的 NodeCode ， CType 存储过程名称 ，查询指定的 K_SinglePage 的内容模块信息，String2是新增的参数
        /// <summary>
        /// 根据指定的 NodeCode ， CType 存储过程名称 ，查询指定的 proc_K_SinglePageSel 的新闻信息，String2是新增的参数
        /// </summary>
        /// <param name="NodeCode">栏目对应的NodeCode</param>
        /// <param name="CType">K_U_KJNewsSel 对应的存储过程名称</param>
        /// <param name="String2">参数 @S2</param>
        /// <param name="Int1">参数 @I2</param>
        /// <returns>返回目标的 DataTable</returns>
        
        //public static DataTable ReturnSinglePageDataTable(string NodeCode, string CType, string String2, string Int1)
        //{
        //    AllPower.BLL.Single.SinglePage bllSinglePage = new AllPower.BLL.Single.SinglePage();

        //    AllPower.Model.SelectParams param = new AllPower.Model.SelectParams();


        //    param.S1 = NodeCode;
        //    if (!string.IsNullOrEmpty(String2))
        //    {
        //        param.S2 = String2;
        //    }
        //    if (!string.IsNullOrEmpty(Int1))
        //    {
        //        param.I1 = Convert.ToInt32(Int1);
        //    }


        //    return bllSinglePage.GetList(CType, param);
        //}

        #endregion

        #region  根据指定的 NodeCode ，返回 Content 字段内容
        /// <summary>
        /// 根据指定的 NodeCode ，返回 Content 字段内容
        /// </summary>
        /// <param name="NodeCode">栏目对应的NodeCode</param>
        /// <param name="CType">K_U_KJNewsSel 对应的存储 ONE</param>
        /// <returns>返回目标的 Content值</returns>
        //public static string ReturnContent(string NodeCode)
        //{
        //    string ReturnString = string.Empty;
        //    AllPower.BLL.Single.SinglePage bllSinglePage = new AllPower.BLL.Single.SinglePage();

        //    AllPower.Model.SelectParams param = new AllPower.Model.SelectParams();


        //    param.S2 = NodeCode;
        //    param.S1 = "1";

        //    using (DataTable dt = bllSinglePage.GetList("ONE", param))
        //    {
        //        if (dt.Rows.Count > 0)
        //        {
        //            ReturnString = dt.Rows[0]["Content"].ToString();
        //        }
        //        else
        //        {
        //            ReturnString = "";
        //        }
        //    }
        //    return ReturnString;
        //}

        #endregion

        #region  根据指定的 NodeCode ， CType 存储过程名称 ，查询指定的 K_SysModuleNode 的栏目信息，String2是新增的参数
        /// <summary>
        /// 根据指定的 NodeCode ， CType 存储过程名称 ，查询指定的 proc_K_SysModuleNodeSel 的栏目信息，String2是新增的参数
        /// </summary>
        /// <param name="NodeCode">栏目对应的NodeCode</param>
        /// <param name="CType">proc_K_SysModuleNodeSel 对应的存储过程名称</param>
        /// <param name="String2">参数 @S2</param>
        /// <param name="Int1">参数 @I2</param>
        /// <returns>返回目标的 DataTable</returns>
        //public static DataTable ReturnSysModuleNodeDataTable(string NodeCode, string CType, string String2, string Int1)
        //{
        //    AllPower.BLL.SysManage.ModuleNode bllModuleNode = new AllPower.BLL.SysManage.ModuleNode();

        //    AllPower.Model.SelectParams param = new AllPower.Model.SelectParams();

        //    param.S1 = NodeCode;
        //    if (!string.IsNullOrEmpty(String2))
        //    {
        //        param.S2 = String2;
        //    }
        //    if (!string.IsNullOrEmpty(Int1))
        //    {
        //        param.I1 = Convert.ToInt32(Int1);
        //    }

        //    return bllModuleNode.GetList(CType, param);
        //}

        #endregion

        #region  根据指定的 NodeCode ， CType 存储过程名称 ，查询指定的 K_SysModuleNode 的栏目信息，String2是新增的参数
        /// <summary>
        /// 根据指定的 NodeCode ， CType 存储过程名称 ，查询指定的 proc_K_SysModuleNodeSel 的栏目信息，String2是新增的参数
        /// </summary>
        /// <param name="NodeCode">栏目对应的NodeCode</param>
        /// <param name="Field">对应的栏目的字段</param>
        /// <returns>" "表示无法找到栏目名称，否则返回栏目名称</returns>
        /// 
        //public static string ReturnSysModuleNodeField(string NodeCode, string Field)
        //{
        //    AllPower.BLL.SysManage.ModuleNode bllModuleNode = new AllPower.BLL.SysManage.ModuleNode();

        //    AllPower.Model.SelectParams param = new AllPower.Model.SelectParams();

        //    param.S1 = NodeCode;

        //    using (DataTable dt = bllModuleNode.GetList("ONEBYNODECODE", param))
        //    {
        //        if (dt.Rows.Count > 0)
        //        {
        //            return dt.Rows[0][Field].ToString();
        //        }
        //        else
        //        {
        //            return "";
        //        }
        //    }

        //}

        #endregion

        #region  根据指定的 NodeCode ， CType 存储过程名称 ，查询指定的 K_Advertisement 的广告信息，String2是新增的参数
        /// <summary>
        /// 根据指定的 NodeCode ， CType 存储过程名称 ，查询指定的 proc_K_AdvertisementSel 的广告信息，String2是新增的参数
        /// </summary>
        /// <param name="NodeCode">栏目对应的NodeCode</param>
        /// <param name="CType">proc_K_AdvertisementSel 对应的存储过程名称</param>
        /// <param name="String2">参数 @S2</param>
        /// <param name="Int1">参数 @I2</param>
        /// <returns>返回目标的 DataTable</returns>
        //public static DataTable ReturnAdvertisementDataTable(string NodeCode, string CType, string String2, string Int1)
        //{
        //    AllPower.BLL.Content.Advertisement bllAdvertisement = new AllPower.BLL.Content.Advertisement();

        //    AllPower.Model.SelectParams param = new AllPower.Model.SelectParams();

        //    param.S1 = NodeCode;
        //    if (!string.IsNullOrEmpty(String2))
        //    {
        //        param.S2 = String2;
        //    }
        //    if (!string.IsNullOrEmpty(Int1))
        //    {
        //        param.I1 = Convert.ToInt32(Int1);
        //    }

        //    return bllAdvertisement.GetList(CType, param);

        //}

        #endregion

        #region  根据指定的 NodeCode ， CType 存储过程名称 ，查询指定的 K_U_KJNews 的内容模块信息，String2是新增的参数
        /// <summary>
        /// 根据指定的 NodeCode ， CType 存储过程名称 ，查询指定的 K_U_KJNews 的新闻信息，String2是新增的参数
        /// </summary>
        /// <param name="NodeCode">栏目对应的NodeCode</param>
        /// <param name="CType">K_U_KJNewsSel 对应的存储过程名称</param>
        /// <param name="String2">参数 @S2</param>
        /// <param name="Int1">参数 @I2</param>
        /// <returns>返回目标的 DataTable</returns>
        //public static DataTable ReturnNewsDataTable(string NodeCode, string CType, string String2, string Int1)
        //{


        //    AllPower.Model.SelectParams param = new AllPower.Model.SelectParams();


        //    param.S1 = NodeCode;
        //    if (!string.IsNullOrEmpty(String2))
        //    {
        //        param.S2 = String2;
        //    }
        //    if (!string.IsNullOrEmpty(Int1))
        //    {
        //        param.I1 = Convert.ToInt32(Int1);
        //    }

        //    SqlParameter[] prams = new SqlParameter[]{
        //            new SqlParameter("tranType",CType),
        //            new SqlParameter("I1",param.I1),
        //            new SqlParameter("I2",param.I2),
        //            new SqlParameter("S1",param.S1),
        //            new SqlParameter("S2",param.S2)
        //        };

        //    //proc_K_SinglePageSel
        //    return SQLHelper.ExecuteDataSet(SQLHelper.ConnectionStringLocalTransaction,
        //              CommandType.StoredProcedure, "K_U_KJNewsSel", prams).Tables[0];
        //}

        #endregion


        #region  根据指定的 NodeCode ， CType 存储过程名称 ，查询指定的 K_FriendLink 的栏目信息，String2是新增的参数
        /// <summary>
        /// 根据指定的 NodeCode ， CType 存储过程名称 ，查询指定的 proc_K_FriendLinkSel 的栏目信息，String2是新增的参数
        /// </summary>
        /// <param name="NodeCode">栏目对应的NodeCode</param>
        /// <param name="Field">对应的栏目的字段</param>
        /// <returns>" "表示无法找到栏目名称，否则返回栏目名称</returns>

        //public static DataTable ReturnFriendLinkField(string NodeCode, string CType, string String2, string Int1)
        //{
        //    AllPower.BLL.LinkManage.FriendLink bllFriendLink = new AllPower.BLL.LinkManage.FriendLink();
        //    AllPower.Model.SelectParams param = new AllPower.Model.SelectParams();

        //    param.S1 = NodeCode;
        //    param.S1 = NodeCode;
        //    if (!string.IsNullOrEmpty(String2))
        //    {
        //        param.S2 = String2;
        //    }
        //    if (!string.IsNullOrEmpty(Int1))
        //    {
        //        param.I1 = Convert.ToInt32(Int1);
        //    }

        //    return bllFriendLink.GetList(CType, param);
        //}


        #endregion

        #region  ymPrompt 弹出框提示
        /// <summary>
        /// 弹出框提示
        /// </summary>
        /// <param name="pages"> 当前页面，一般直接 this.Page</param>
        /// <param name="MessageType">ymPrompt的提示类型,默认值为errorInfo   例如： ymPrompt.errorInfo  则 MessageType 为 errorInfo  成功 为succeedInfo  失败为 errorInfo 消息为 ymPrompt.alert 询问 为ymPrompt.confirmInfo</param>
        /// <param name="MessageContent">ymPrompt的message的值 例如：message:'非法操作！'  则 MessageContent 为 非法操作！</param>
        /// <param name="MessageTitle"> ymPrompt的提示信息的值 例如：title:'提示信息'  则 MessageTitle 为 提示信息</param>
        /// <param name="LocationHref"> ymPrompt的提示信息后跳转的页面</param>
        /// <param name="AssemblingString"> 自定义ymPrompt的参数 格式为 message:'修改成功！',title:'提示信息',handler:function() {location.href='/Logins.aspx';</param>

        /// <returns></returns>
        public static void ReturnYmPrompt(Page pages, string MessageType, string MessageContent, string MessageTitle, string LocationHref, string AssemblingString)
        {
            if (string.IsNullOrEmpty(AssemblingString))
            {
                pages.ClientScript.RegisterStartupScript(pages.GetType(), "message", "<script language='javascript' defer>ymPrompt." + (MessageType != "" ? MessageType : "errorInfo") + "({" + (MessageContent != "" ? "message:'" + MessageContent + "'," : "") + (MessageTitle != "" ? "title:'" + MessageTitle + "'," : "") + (LocationHref != "" ? "handler:function() {location.href='" + LocationHref + "';}" : "") + "});</script>");
            }
            else
            {
                pages.ClientScript.RegisterStartupScript(pages.GetType(), "message", "<script language='javascript' defer>ymPrompt." + (MessageType != "" ? MessageType : "errorInfo") + "({" + AssemblingString + "}});</script>");
            }

        }

        #endregion
        #endregion
    }
}
