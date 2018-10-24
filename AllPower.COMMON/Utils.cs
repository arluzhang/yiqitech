#region 程序集引用

using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Data;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.XPath;
using System.Security.Cryptography;
using System.IO;
using System.Net;
using System.Web.Hosting;

#endregion

#region 版权注释
/*================================================================
    Copyright (C) 2010 华强北在线

    作者:     袁纯林 吴岸标 周武 严辉
    创建时间： 2010年3月10日

    功能描述： 通用方法集

 
// 更新日期        更新人      更新原因/内容
// 2010-3-11       周武        加入cookie操作
// 2010-5-12       严辉        替换 HTML 标签
// 2010-5-25       朱存群      加入前台服务端验证、字符过滤和MD5加密
// 2010-5-27       何建龙      加入读文件夹/文件列表的读取、删除方法

--===============================================================*/
#endregion


namespace AllPower.Common
{
    public static partial class Utils
    {
        #region 获得当前页面客户端的IP ycl@360hqb.com
        /// <summary>
        /// 获得当前页面客户端的IP
        /// </summary>
        /// <returns>当前页面客户端的IP</returns>
        public static string GetIP()
        {
            string result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            if (string.IsNullOrEmpty(result))
                result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (string.IsNullOrEmpty(result))
                result = HttpContext.Current.Request.UserHostAddress;

            if (string.IsNullOrEmpty(result) || !Utils.IsIP(result))
                return "127.0.0.1";

            return result;
        }
        #endregion

        #region 判断是否为ip ycl@360hqb.com
        /// <summary>
        /// 是否为ip
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsIP(string ip)
        {
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }
        #endregion

        #region 将object类型值转换成long值，失败返回预设的缺省值

        /// <summary>
        /// 将object类型值转换成long值，失败返回预设的缺省值

        /// </summary>
        /// <param name="originalValue"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static long ParseLong(object originalValue, long defaultValue)
        {
            long targetValue;

            try
            {
                targetValue = long.Parse(originalValue.ToString().Trim());
            }
            catch
            {
                targetValue = defaultValue;
            }

            return targetValue;
        }
        #endregion

        #region 将object类型值转换成int值，失败返回预设的缺省值

        /// <summary>
        /// 将object类型值转换成long值，失败返回预设的缺省值

        /// </summary>
        /// <param name="originalValue"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int ParseInt(object originalValue, int defaultValue)
        {
            int targetValue;

            try
            {
                targetValue = int.Parse(originalValue.ToString().Trim());
            }
            catch
            {
                targetValue = defaultValue;
            }

            return targetValue;
        }
        #endregion

        #region 将object类型值转换成float值，失败返回预设的缺省值

        /// <summary>
        /// 将object类型值转换成long值，失败返回预设的缺省值

        /// </summary>
        /// <param name="originalValue"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static float ParseFloat(object originalValue, float defaultValue)
        {
            float targetValue;

            try
            {
                targetValue = float.Parse(originalValue.ToString().Trim());
            }
            catch
            {
                targetValue = defaultValue;
            }

            return targetValue;
        }
        #endregion

        #region 将int转成bool 1转true 其它转false

        /// <summary>
        /// 将int转成bool 1转true 其它转false
        /// </summary>
        /// <param name="intValue"></param>
        /// <returns></returns>
        public static bool ParseBool(int intValue)
        {
            bool targetValue;
            if (intValue == 1)
            {
                targetValue = true;
            }
            else
            {
                targetValue = false;
            }
            return targetValue;
        }
        /// <summary>
        /// 将int转成bool 1转true 或者"True"转True 其它转false
        /// </summary>
        /// <param name="intValue"></param>
        /// <returns></returns>
        public static bool ParseBool(string intValue)
        {
            bool targetValue;
            if (intValue == "1" || intValue == "True")
            {
                targetValue = true;
            }
            else
            {
                targetValue = false;
            }
            return targetValue;
        }

        /// <summary>
        /// 将object转成bool 1转true 其它转false
        /// </summary>
        /// <param name="objValue"></param>
        /// <returns></returns>
        public static bool ParseBool(object objValue)
        {
            bool targetValue;

            if (objValue.ToString() == "1" || objValue.ToString().ToLower() == "true")
            {
                targetValue = true;
            }
            else
            {
                targetValue = false;
            }

            return targetValue;
        }

        public static string BoolToIntString(bool targetValue)
        {
            return targetValue ? "1" : "0";
        }

        /// <summary>
        /// 将Bool转成int true转1 其它转0
        /// </summary>
        /// <param name="objValue"></param>
        /// <returns></returns>
        public static int ParseBoolToInt(object objValue)
        {
            return ParseBoolToInt(objValue.ToString());
        }

        /// <summary>
        /// 将Bool转成int true转1 其它转0
        /// </summary>
        /// <param name="objValue"></param>
        /// <returns></returns>
        public static int ParseBoolToInt(string objValue)
        {
            int targetValue;

            if (objValue == "True")
            {
                targetValue = 1;
            }
            else
            {
                targetValue = 0;
            }

            return targetValue;
        }



        #endregion

        #region xml操作类 
        /// <summary>
        /// 读取xml 转DataSet
        /// </summary>
        /// <param name="strPath">xml路径</param>
        /// <returns></returns>
        public static DataSet GetXmlDataSet(string strPath)
        {
            DataSet dsXml = new DataSet();
            dsXml.ReadXml(GetPath(strPath));
            return dsXml;
        }

        /// <summary>
        /// 查询xml文件属性单个节点

        /// </summary>
        /// <param name="strPath">xml路径</param>
        /// <param name="strXPath">XPath字符串</param>
        /// <returns></returns>
        public static XmlNode XmlSelectSingleNode(string strPath, string strXPath)
        {
            XmlNode xmlnode = null;
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(GetPath(strPath));
            xmlnode = xmldoc.SelectSingleNode(strXPath);
            return xmlnode;
        }

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="attribute">属性名，非空时返回该属性值，否则返回串联值</param>
        /// <returns>string</returns>
        public static string XmlRead(string path, string node, string attribute)
        {
            string value = "";
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                XmlNode xn = doc.SelectSingleNode(node);
                value = (attribute.Equals("") ? xn.InnerText : xn.Attributes[attribute].Value);
            }
            catch { }
            return value;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="element">元素名，非空时插入新元素，否则在该元素中插入属性</param>
        /// <param name="attribute">属性名，非空时插入该元素属性值，否则插入元素值</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static void XmlInsert(string path, string node, string element, string attribute, string value)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                XmlNode xn = doc.SelectSingleNode(node);
                if (element.Equals(""))
                {
                    if (!attribute.Equals(""))
                    {
                        XmlElement xe = (XmlElement)xn;
                        xe.SetAttribute(attribute, value);
                    }
                }
                else
                {
                    XmlElement xe = doc.CreateElement(element);
                    if (attribute.Equals(""))
                        xe.InnerText = value;
                    else
                        xe.SetAttribute(attribute, value);
                    xn.AppendChild(xe);
                }
                doc.Save(path);
            }
            catch { }
        }

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="attribute">属性名，非空时修改该节点属性值，否则修改节点值</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static void XmlUpdate(string path, string node, string attribute, string value)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                XmlNode xn = doc.SelectSingleNode(node);
                XmlElement xe = (XmlElement)xn;
                if (attribute.Equals(""))
                    xe.InnerText = value;
                else
                    xe.SetAttribute(attribute, value);
                doc.Save(path);
            }
            catch { }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="attribute">属性名，非空时删除该节点属性值，否则删除节点值</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static void XmlDelete(string path, string node, string attribute)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                XmlNode xn = doc.SelectSingleNode(node);
                XmlElement xe = (XmlElement)xn;
                if (attribute.Equals(""))
                    xn.ParentNode.RemoveChild(xn);
                else
                    xe.RemoveAttribute(attribute);
                doc.Save(path);
            }
            catch { }
        }

        #endregion

        #region  解析性别
        public static string ParseGender(object originalValue, string defaultValue)
        {
            string parseValue;

            parseValue = "";

            switch (originalValue.ToString().Trim())
            {
                case "1":
                    parseValue = "男";
                    break;
                case "0":
                    parseValue = "女";
                    break;
                case "-1":
                    parseValue = "保密";
                    break;
                default:
                    parseValue = defaultValue;
                    break;
            }

            return parseValue;

        }
        #endregion

        #region 解析状态
        //状态图标
        public static string ParseState(object orginalValue, string defaultValue)
        {
            string parseValue;

            parseValue = "";

            switch (orginalValue.ToString().Trim())
            {
                case "1":
                case "True":
                    parseValue = GetResourcesValue("Common", "ON");
                    break;
                case "0":
                case "False":
                    parseValue = GetResourcesValue("Common", "OFF");
                    break;
                default:
                    parseValue = defaultValue;
                    break;
            }

            return parseValue;

        }
        //状态名称 gavin by 2010-07-14
        public static string ParseStateTitle(object orginalValue, string defaultValue)
        {
            string parseValue;

            parseValue = "";

            switch (orginalValue.ToString().Trim())
            {
                case "1":
                case "True":
                    parseValue = GetResourcesValue("Common", "OnTitle");
                    break;
                case "0":
                case "False":
                    parseValue = GetResourcesValue("Common", "OffTitle");
                    break;
                default:
                    parseValue = defaultValue;
                    break;
            }

            return parseValue;

        }
        #endregion

        #region 解析推荐状态

        public static string ParseIsRecommend(object orginalValue, string defaultValue)
        {
            string parseValue;

            parseValue = "";

            switch (orginalValue.ToString().Trim())
            {
                case "1":
                case "True":
                    parseValue = GetResourcesValue("Common", "IsRecommendOn");
                    break;
                case "0":
                case "False":
                    parseValue = GetResourcesValue("Common", "IsRecommendOff");
                    break;
                default:
                    parseValue = defaultValue;
                    break;
            }

            return parseValue;

        }
        #endregion

        #region 解析置顶状态

        public static string ParseIsTop(object orginalValue, string defaultValue)
        {
            string parseValue;

            parseValue = "";

            switch (orginalValue.ToString().Trim())
            {
                case "1":
                case "True":
                    parseValue = GetResourcesValue("Common", "IsTopOn");
                    break;
                case "0":
                case "False":
                    parseValue = GetResourcesValue("Common", "IsTopOff");
                    break;
                default:
                    parseValue = defaultValue;
                    break;
            }

            return parseValue;

        }
        #endregion

        #region 解析是或否

        public static string ParseIsOrNot(object orginalValue, string defaultValue)
        {
            string parseValue;

            parseValue = "";

            switch (orginalValue.ToString().Trim())
            {
                case "1":
                case "True":
                    parseValue = "是";
                    break;
                case "0":
                case "False":
                    parseValue = "否";
                    break;
                default:
                    parseValue = defaultValue;
                    break;
            }

            return parseValue;

        }
        #endregion

        #region 解析状态

        public static string ParseModelFieldState(object orginalValue, string defaultValue)
        {
            string parseValue = "";

            switch (orginalValue.ToString().Trim())
            {
                case "True":
                case "1":
                    parseValue = "√";
                    break;
                case "False":
                case "0":
                    parseValue = "";
                    break;
                default:
                    parseValue = defaultValue;
                    break;
            }

            return parseValue;

        }

        public static string ParseModelFieldState2(object orginalValue, string defaultValue)
        {
            string parseValue = "";

            switch (ParseInt(orginalValue, 0))
            {
                case 1:
                    parseValue = "√";
                    break;
                case 0:
                    parseValue = "";
                    break;
                default:
                    parseValue = defaultValue;
                    break;
            }

            return parseValue;

        }
        #endregion

        #region 

        #region 将数据表转换成JSON类型串

        /// <summary>
        /// 将数据表转换成JSON类型串

        /// </summary>
        /// <param name="dt">要转换的数据表</param>
        /// <returns></returns>
        public static StringBuilder DataTableToJson(System.Data.DataTable dt)
        {
            return DataTableToJson(dt, true);
        }


        /// <summary>
        /// 将数据表转换成JSON类型串

        /// </summary>
        /// <param name="dt">要转换的数据表</param>
        /// <param name="dispose">数据表转换结束后是否dispose掉</param>
        /// <returns></returns>
        public static StringBuilder DataTableToJson(System.Data.DataTable dt, bool dt_dispose)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("[\r\n");

            //数据表字段名和类型数组

            string[] dt_field = new string[dt.Columns.Count];
            int i = 0;
            string formatStr = "{{";
            string fieldtype = "";
            foreach (System.Data.DataColumn dc in dt.Columns)
            {
                dt_field[i] = dc.Caption.ToLower().Trim();
                formatStr += "'" + dc.Caption.ToLower().Trim() + "':";
                fieldtype = dc.DataType.ToString().Trim().ToLower();
                if (fieldtype.IndexOf("int") > 0 || fieldtype.IndexOf("deci") > 0 ||
                    fieldtype.IndexOf("floa") > 0 || fieldtype.IndexOf("doub") > 0 ||
                    fieldtype.IndexOf("bool") > 0)
                {
                    formatStr += "{" + i + "}";
                }
                else
                {
                    formatStr += "'{" + i + "}'";
                }
                formatStr += ",";
                i++;
            }

            if (formatStr.EndsWith(","))
                formatStr = formatStr.Substring(0, formatStr.Length - 1);//去掉尾部","号


            formatStr += "}},";

            i = 0;
            object[] objectArray = new object[dt_field.Length];
            foreach (System.Data.DataRow dr in dt.Rows)
            {

                foreach (string fieldname in dt_field)
                {   //对 \ , ' 符号进行转换 
                    objectArray[i] = dr[dt_field[i]].ToString().Trim().Replace("\\", "\\\\").Replace("'", "\\'");
                    switch (objectArray[i].ToString())
                    {
                        case "True":
                            {
                                objectArray[i] = "true"; break;
                            }
                        case "False":
                            {
                                objectArray[i] = "false"; break;
                            }
                        default: break;
                    }
                    i++;
                }
                i = 0;
                stringBuilder.Append(string.Format(formatStr, objectArray));
            }
            if (stringBuilder.ToString().EndsWith(","))
                stringBuilder.Remove(stringBuilder.Length - 1, 1);//去掉尾部","号


            if (dt_dispose)
                dt.Dispose();

            return stringBuilder.Append("\r\n];");
        }
        #endregion

        #region 分割字符串

        /// <summary>
        /// 分割字符串

        /// </summary>
        public static string[] strSplit(string strContent, string strSplit)
        {
            if (!Utils.StrIsNullOrEmpty(strContent))
            {
                if (strContent.IndexOf(strSplit) < 0)
                    return new string[] { strContent };

                return Regex.Split(strContent, Regex.Escape(strSplit), RegexOptions.IgnoreCase);
            }
            else
                return new string[0] { };
        }

        /// <summary>
        /// 字段串是否为Null或为""(空)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool StrIsNullOrEmpty(string str)
        {
            if (str == null || str.Trim() == string.Empty)
                return true;

            return false;
        }


        /// <summary>
        /// 分割字符串

        /// </summary>
        /// <returns></returns>
        public static string[] strSplit(string strContent, string strValue, int count)
        {
            string[] result = new string[count];
            string[] splited = strSplit(strContent, strValue);

            for (int i = 0; i < count; i++)
            {
                if (i < splited.Length)
                    result[i] = splited[i];
                else
                    result[i] = string.Empty;
            }

            return result;
        }
        #endregion

        #region 获取站点根目录URL
        /// <summary>
        /// 获取站点根目录URL
        /// </summary>
        /// <returns></returns>
        public static string GetRootUrl(string forumPath)
        {
            int port = HttpContext.Current.Request.Url.Port;
            return string.Format("{0}://{1}{2}{3}",
                                 HttpContext.Current.Request.Url.Scheme,
                                 HttpContext.Current.Request.Url.Host.ToString(),
                                 (port == 80 || port == 0) ? "" : ":" + port,
                                 forumPath);
        }
        #endregion

        #region  bool值转int  true 为1  false为0
        /// <summary>
        /// bool值转int  true 为1  false为0
        /// </summary>
        /// <param name="strValue">bool值</param>
        /// <returns>转变后的值</returns>
        public static int BoolToInt(string strValue)
        {
            return strValue == "True" ? 1 : 0;
        }

        /// <summary>
        /// bool值转int  true 为1  false为0
        /// </summary>
        /// <param name="strValue">bool值</param>
        /// <returns>转变后的值</returns>
        public static int BoolToInt(bool strValue)
        {
            return strValue ? 1 : 0;
        }
        #endregion

        #region cookie操作
        /// <summary>
        /// 写cookie值

        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        public static void WriteCookie(string strName, string strValue)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie.Value = strValue;
            cookie.Path = "/";
            HttpContext.Current.Response.AppendCookie(cookie);
        }


        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="key">key</param>
        /// <param name="strValue">值</param>
        public static void WriteCookie(string strName, string key, string strValue)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie[key] = strValue;
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        /// 写cookie值

        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        /// <param name="expires">过期时间(分钟)</param>
        public static void WriteCookie(string strName, string strValue, int expires)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie.Value = strValue;
            cookie.Expires = DateTime.Now.AddMinutes(expires);
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        /// 读cookie值

        /// </summary>
        /// <param name="strName">名称</param>
        /// <returns>cookie值</returns>
        public static string GetCookie(string strName)
        {
            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[strName] != null)
                return HttpContext.Current.Request.Cookies[strName].Value.ToString();

            return "";
        }

        /// <summary>
        /// 读cookie值

        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="key">key</param>
        /// <returns>cookie值</returns>
        public static string GetCookie(string strName, string key)
        {
            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[strName] != null && HttpContext.Current.Request.Cookies[strName][key] != null)
                return HttpContext.Current.Request.Cookies[strName][key].ToString();

            return "";
        }
        #endregion

        #region 获取url全部参数
        /// <summary>
        /// 获取url全部参数
        /// </summary>
        /// <returns></returns>
        public static string GetUrlParams()
        {
            string strUrl = HttpContext.Current.Request.Url.OriginalString;
            int index = strUrl.IndexOf("?");
            if (index != -1)
            {
                return strUrl.Substring(index + 1);
            }
            else
            {
                return "";
            }
        }
        #endregion

        #region html/url 编码/解码

        /// <summary>
        /// 返回 HTML 字符串的编码结果
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>编码结果</returns>
        public static string HtmlEncode(string str)
        {
            return HttpUtility.HtmlEncode(str);
        }

        /// <summary>
        /// 返回 HTML 字符串的解码结果
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>解码结果</returns>
        public static string HtmlDecode(string str)
        {
            return HttpUtility.HtmlDecode(str);
        }

        /// <summary>
        /// 返回 URL 字符串的编码结果
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>编码结果</returns>
        public static string UrlEncode(string str)
        {
            return HttpUtility.UrlEncode(str);
        }

        /// <summary>
        /// 返回 URL 字符串的编码结果
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>解码结果</returns>
        public static string UrlDecode(string str)
        {
            return HttpUtility.UrlDecode(str);
        }
        #endregion

        #region 页面跳转
        /// <summary>
        /// 页面跳转
        /// </summary>
        /// <param name="strUrl">要跳转的地址</param>
        public static void UrlRedirect(string strUrl)
        {
            HttpContext.Current.Response.Redirect(strUrl);
        }

        /// <summary>
        /// 页面跳转
        /// </summary>
        /// <param name="typeClass">执行的类</param>
        /// <param name="strUrl">要跳转的地址</param>
        public static void UrlRedirect(System.Web.UI.Page typeClass, string strUrl)
        {
            UrlRedirect(typeClass, strUrl, "", 1);
        }

        /// <summary>
        /// 页面跳转
        /// </summary>
        /// <param name="typeClass">执行的类</param>
        /// <param name="strUrl">要跳转的地址</param>
        /// <param name="strMessage">要弹出的消息</param>
        public static void UrlRedirect(System.Web.UI.Page typeClass, string strUrl, string strMessage)
        {
            UrlRedirect(typeClass, strUrl, strMessage, 2);
        }

        /// <summary>
        /// 页面跳转
        /// </summary>
        /// <param name="typeClass">执行的类</param>
        /// <param name="strUrl">要跳转的地址</param>
        /// <param name="strMessage">要弹出的消息</param>
        /// <param name="strType">操作的类型 1为直接跳转 2为alert 3 为confrim 4为close</param>
        public static void UrlRedirect(System.Web.UI.Page typeClass, string strUrl, string strMessage, int iType)
        {
            StringBuilder sb = new StringBuilder(36);
            strMessage = strMessage.Replace("'", "''");
            sb.Append("<script>");
            switch (iType)
            {
                case 2:
                    sb.Append("alert('" + strMessage + "');");
                    break;
            }
            if (iType != 4 && iType != 5)
            {
                sb.Append("location.href='" + strUrl + "'");
            }
            sb.Append("</script>");
            typeClass.RegisterStartupScript("", sb.ToString());
        }

        /// <summary>
        /// 弹出对话框

        /// </summary>
        /// <param name="typeClass">执行的类</param>
        /// <param name="strMessage">要执行的javascript代码</param>
        public static void AlertJavaScript(System.Web.UI.Page typeClass, string strMessage)
        {
            strMessage = strMessage.Replace("'", "\'");
            RunJavaScript(typeClass, "alert('" + strMessage + "');");
        }


        /// <summary>
        /// 执行客户端脚本

        /// </summary>
        /// <param name="typeClass">执行的类</param>
        /// <param name="strUrl">要执行的javascript代码</param>
        public static void RunJavaScript(System.Web.UI.Page typeClass, string strMessage)
        {
            typeClass.RegisterStartupScript("", "<script>$(function(){" + strMessage + "});</script>");
        }
        #endregion

        #region 返回上一个页面的地址
        /// <summary>
        /// 返回上一个页面的地址
        /// </summary>
        /// <returns>上一个页面的地址</returns>
        public static string GetUrlReferrer()
        {
            string retVal = null;

            try
            {
                retVal = HttpContext.Current.Request.UrlReferrer.ToString();
            }
            catch { }

            if (retVal == null)
                return "";

            return retVal;
        }
        #endregion

        #region 设置访问过的分页列表
        /// <summary>
        /// 设置访问过的分页列表(有数量限制)
        /// </summary>
        /// <param name="strCookieName">cookie名称</param>
        /// <param name="strCookieKey">cookie key</param>
        /// <param name="strCookieValue">cookie 值</param>
        /// <param name="intMax">最大保存限制</param>
        public static void SetVisiteList(string strCookieName, string strCookieKey, string strCookieValue, int intMax)
        {
            string pageKey = System.Web.HttpContext.Current.Request.ServerVariables["SCRIPT_NAME"].ToLower();
            int num = pageKey.LastIndexOf("list");
            if (num > 0)
            {
                string[] arrpage = pageKey.Substring(0, num).Split('/');
                pageKey = arrpage[arrpage.Length - 1];
            }
            else
            {
                pageKey = "";
            }

            strCookieKey = strCookieKey + Utils.ReqUrlParameter("NodeCode") + pageKey;
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strCookieName];

            if (cookie != null)
            {
                if (cookie[strCookieKey] == null)  //如果当前cookie子集没有保存,则新增一个
                {
                    int iCount = cookie.Values.Count; //当前保存的cookie子集合数
                    if (iCount >= intMax) //如果子集合数超过最大保存限制,则删除最早添加的一个子集
                    {
                        cookie.Values.Remove(cookie.Values.GetKey(iCount - 1));
                    }
                    cookie.Values.Add(strCookieKey, strCookieValue);
                }
                else  //否则 直接改变当前子集的值
                {
                    cookie[strCookieKey] = strCookieValue;
                }
            }
            else
            {
                cookie = new HttpCookie(strCookieName);
                cookie.Values.Add(strCookieKey, strCookieValue);
            }
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        #endregion

        #region 添加参数
        /// <summary>
        /// 添加单个字符型参数 By 何伟 2010-9-18 update
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static AllPower.Model.SelectParams getOneParams(string strValue)
        {
            AllPower.Model.SelectParams param = new AllPower.Model.SelectParams();
            param.S1 = strValue;
            return param;
        }

        /// <summary>
        /// 添加单个数字型参数 By 何伟 2010-9-18 update
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static AllPower.Model.SelectParams getOneNumParams(int num)
        {
            AllPower.Model.SelectParams param = new AllPower.Model.SelectParams();
            param.I1 = num;
            return param;
        }

        /// <summary>
        /// 添加两个参数
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static AllPower.Model.SelectParams getTwoParams(string strOneValue, string strTwoValue)
        {
            AllPower.Model.SelectParams param = new AllPower.Model.SelectParams();
            param.S1 = strOneValue;
            param.S2 = strTwoValue;
            return param;
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
        #endregion

        #region 字符串数组操作

        /// <summary>
        /// 判断指定字符串在指定字符串数组中的位置

        /// </summary>
        /// <param name="strSearch">字符串</param>
        /// <param name="stringArray">字符串数组</param>
        /// <param name="caseInsensetive">是否不区分大小写, true为不区分, false为区分</param>
        /// <returns>字符串在指定字符串数组中的位置, 如不存在则返回-1</returns>
        public static int GetInArrayID(string strSearch, string[] stringArray, bool caseInsensetive)
        {
            for (int i = 0; i < stringArray.Length; i++)
            {
                if (caseInsensetive)
                {
                    if (strSearch.ToLower() == stringArray[i].ToLower())
                        return i;
                }
                else if (strSearch == stringArray[i])
                    return i;
            }
            return -1;
        }


        /// <summary>
        /// 判断指定字符串在指定字符串数组中的位置

        /// </summary>
        /// <param name="strSearch">字符串</param>
        /// <param name="stringArray">字符串数组</param>
        /// <returns>字符串在指定字符串数组中的位置, 如不存在则返回-1</returns>		
        public static int GetInArrayID(string strSearch, string[] stringArray)
        {
            return GetInArrayID(strSearch, stringArray, true);
        }

        /// <summary>
        /// 判断指定字符串是否属于指定字符串数组中的一个元素

        /// </summary>
        /// <param name="strSearch">字符串</param>
        /// <param name="stringArray">字符串数组</param>
        /// <param name="caseInsensetive">是否不区分大小写, true为不区分, false为区分</param>
        /// <returns>判断结果</returns>
        public static bool InArray(string strSearch, string[] stringArray, bool caseInsensetive)
        {
            return GetInArrayID(strSearch, stringArray, caseInsensetive) >= 0;
        }

        /// <summary>
        /// 判断指定字符串是否属于指定字符串数组中的一个元素

        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="stringarray">字符串数组</param>
        /// <returns>判断结果</returns>
        public static bool InArray(string str, string[] stringarray)
        {
            return InArray(str, stringarray, false);
        }

        /// <summary>
        /// 判断指定字符串是否属于指定字符串数组中的一个元素

        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="stringarray">内部以逗号分割单词的字符串</param>
        /// <returns>判断结果</returns>
        public static bool InArray(string str, string stringarray)
        {
            return InArray(str, strSplit(stringarray, ","), false);
        }

        /// <summary>
        /// 判断指定字符串是否属于指定字符串数组中的一个元素

        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="stringarray">内部以逗号分割单词的字符串</param>
        /// <param name="strsplit">分割字符串</param>
        /// <returns>判断结果</returns>
        public static bool InArray(string str, string stringarray, string strsplit)
        {
            return InArray(str, strSplit(stringarray, strsplit), false);
        }

        /// <summary>
        /// 判断指定字符串是否属于指定字符串数组中的一个元素

        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="stringarray">内部以逗号分割单词的字符串</param>
        /// <param name="strsplit">分割字符串</param>
        /// <param name="caseInsensetive">是否不区分大小写, true为不区分, false为区分</param>
        /// <returns>判断结果</returns>
        public static bool InArray(string str, string stringarray, string strsplit, bool caseInsensetive)
        {
            return InArray(str, strSplit(stringarray, strsplit), caseInsensetive);
        }

        /// <summary>
        /// 获取资源文件内容
        /// </summary>
        /// <param name="strFileName">资源文件名</param>
        /// <param name="strKey">要获取的字符名</param>
        /// <returns></returns>
        public static string GetResourcesValue(string strFileName, string strKey)
        {
            System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Resources." + strFileName, global::System.Reflection.Assembly.Load("App_GlobalResources"));
            return temp.GetString(strKey);
        }
        #endregion

        #region 获取参数
        /// <summary>
        /// 获取URL中参数值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string ReqUrlParameter(string key)
        {
            string urlParamValue = HttpContext.Current.Request.QueryString[key];

            if (urlParamValue == null)
            {
                urlParamValue = "";
            }

            return urlParamValue;
        }

        /// <summary>
        /// 获取from值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string ReqFromParameter(string key)
        {
            return ReqFromParameter(key, -1);
        }

        /// <summary>
        /// 获取指定name名的单个值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="intType"></param>
        /// <returns></returns>
        public static string ReqFromParameter(string key, int intType)
        {
            string urlParamValue = null;
            if (intType == -1)
            {
                urlParamValue = HttpContext.Current.Request.Form[key];
            }
            else
            {
                urlParamValue = HttpContext.Current.Request.Form.GetValues(key)[intType];
            }

            if (urlParamValue == null)
            {
                urlParamValue = "";
            }

            return urlParamValue;
        }

        #endregion

        #region 其它操作
        /// <summary>
        /// 返回虚拟路径
        /// </summary>
        /// <param name="strPath">实际路径</param>
        /// <returns></returns>
        public static string GetPath(string strPath)
        {
            return HttpContext.Current.Server.MapPath(strPath);
        }



        /// <summary>
        /// 过滤sql字段关键字,如果存在 则加上[]
        /// </summary>
        /// <param name="strColumnName">sql字段名</param>
        /// <returns></returns>
        public static string GetFilterKeyword(string strColumnName)
        {
            return "[" + strColumnName + "]";
        }

        /// <summary>
        /// sql条件拼接
        /// </summary>
        /// <param name="strKey">key</param>
        /// <param name="strValue">value</param>
        /// <param name="strType">连接符号 可为=,<>,>,<</param>
        /// <param name="strParamType">参数类型,可为 str,int等</param>
        /// <param name="sbSqlWhere">原始条件</param>
        public static void GetWhereAppend(string strKey, string strValue, string strValue2, string strType, AllPower.Model.SqlParamType strParamType, ref StringBuilder sbSqlWhere)
        {
            sbSqlWhere.Append(" AND [" + strKey + "] ");
            sbSqlWhere.Append(strType);
            if (strType.ToUpper() == "LIKE") //如果是like
            {
                sbSqlWhere.Append("'%" + strValue.Replace("'", "''") + "%'");
            }
            else if (strType.ToUpper() == "Between") //如果是Between
            {
                switch (strParamType) //参数类型
                {
                    case AllPower.Model.SqlParamType.Str:  //字符串则要加单引号

                        sbSqlWhere.Append("  between '" + strValue.Replace("'", "''") + "' and '" + strValue2.Replace("'", "''") + "'");
                        break;
                    case AllPower.Model.SqlParamType.Int:
                        sbSqlWhere.Append("  between " + strValue.Replace("'", "''") + " and " + strValue2.Replace("'", "''"));
                        break;
                    default:
                        sbSqlWhere.Append("  between " + strValue.Replace("'", "''") + " and " + strValue2.Replace("'", "''"));
                        break;
                }
            }
            else
            {
                switch (strParamType) //参数类型
                {
                    case AllPower.Model.SqlParamType.Str:  //字符串则要加单引号

                        sbSqlWhere.Append(" '" + strValue.Replace("'", "''") + "'");
                        break;
                    case AllPower.Model.SqlParamType.Int:
                        sbSqlWhere.Append(" " + strValue.Replace("'", "''"));
                        break;
                    default:
                        sbSqlWhere.Append(" " + strValue.Replace("'", "''"));
                        break;
                }
            }

        }

        /// <summary>
        /// sql条件拼接
        /// </summary>
        /// <param name="strKey">key</param>
        /// <param name="strValue">value</param>
        /// <param name="strType">连接符号 可为=,<>,>,<</param>
        /// <param name="strParamType">参数类型,可为 str,int等</param>
        /// <param name="sbSqlWhere">原始条件</param>
        public static void GetWhereAppend(string strKey, string strValue, string strType, AllPower.Model.SqlParamType strParamType, ref StringBuilder sbSqlWhere)
        {
            GetWhereAppend(strKey, strValue, "", strType, strParamType, ref sbSqlWhere);
        }
        #endregion

        #region 解析系统类型
        public static string ParseModelType(object orginalValue)
        {
            string parseValue = "";

            switch (orginalValue.ToString().Trim())
            {
                case "1":
                    parseValue = GetResourcesValue("Model", "Customize");
                    break;
                case "0":
                    parseValue = GetResourcesValue("Model", "ModelListFiledSys");
                    break;
                default:
                    parseValue = GetResourcesValue("Model", "Customize");
                    break;
            }

            return parseValue;

        }
        #endregion

        #region 泛型集合Dictionary转SqlParams
        /// <summary>
        /// 泛型集合Dictionary转SqlParams
        /// </summary>
        /// <param name="dctWhere"></param>
        /// <returns></returns>
        public static System.Data.SqlClient.SqlParameter[] DictToSqlParams(Dictionary<string, string> dctWhere)
        {
            List<System.Data.SqlClient.SqlParameter> list = new List<System.Data.SqlClient.SqlParameter>();
            foreach (KeyValuePair<string, string> kvp in dctWhere)
            {
                list.Add(new System.Data.SqlClient.SqlParameter("@" + kvp.Key, kvp.Value));
            }
            return list.ToArray();
        }
        #endregion

        #region 将字符串转时间

        /// <summary>
        /// 字符串转时间
        /// </summary>
        /// <param name="strDateTime"></param>
        /// <returns></returns>
        public static DateTime ParseDateTime(string strDateTime)
        {
            if (strDateTime.Trim() == "")
            {
                return DateTime.Now;
            }
            try
            {
                return Convert.ToDateTime(strDateTime);
            }
            catch (Exception ex)
            {
                return DateTime.Now;
            }
        }
        #endregion

        #region IP地址转整数

        /// <summary>
        /// 地址转整数

        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public static uint IPToInt(string ipAddress)
        {
            string disjunctiveStr = ".,: ";
            char[] delimiter = disjunctiveStr.ToCharArray();
            string[] startIP = null;
            for (int i = 1; i <= 5; i++)
            {
                startIP = ipAddress.Split(delimiter, i);
            }
            string a1 = startIP[0].ToString();
            string a2 = startIP[1].ToString();
            string a3 = startIP[2].ToString();
            string a4 = startIP[3].ToString();
            uint U1 = uint.Parse(a1);
            uint U2 = uint.Parse(a2);
            uint U3 = uint.Parse(a3);
            uint U4 = uint.Parse(a4);

            uint U = U1 << 24;
            U += U2 << 16;
            U += U3 << 8;
            U += U4;
            return U;
        }
        #endregion

        #region sql关键字过滤

        /// <summary>
        /// sql关键字过滤

        /// </summary>
        /// <param name="str">传入的文字</param>
        /// <returns>过滤后的文字</returns>
        public static string SqlReplace(string str)
        {
            if (string.IsNullOrEmpty(str))
                return "";
            str = str.ToLower();
            if (str.Trim() != "")
            {
                string SqlStr = "exec|insert|select|delete|update|count|chr|mid|master|truncate|char|declare|drop|table|create|create|*|iframe|script|";
                SqlStr += "exec|insert|delete|update|count(|count|chr|mid(|mid|master|truncate|char|char(|declare|'";
                string[] anySqlStr = SqlStr.Split('|');
                foreach (string ss in anySqlStr)
                {
                    if (str.IndexOf(ss) >= 0)
                    {
                        str = str.Replace(ss, "");
                    }
                }
            }
            return str;
        }
        #endregion

        #region 字符串截取


        #region 截取允许的最大的字符串子串

        /// <summary>
        /// 说明：截取允许的最大的字符串子串      
        /// </summary>
        /// <param name="str">要截取的字符串</param>
        /// <param name="len">保留的长度</param>
        /// <returns></returns>
        public static string MaxLengthSubString(string str, int len)
        {
            string temp = str;

            if (GetStringLength(str) <= len)
            {
                return str;
            }
            else
            {
                //先大胆截断一截

                if (str.Length > len)
                {
                    temp = str.Substring(0, len);
                }

                while (GetStringLength(temp) > len && temp.Length > 0)
                {
                    temp = temp.Substring(0, temp.Length - 1);
                }

                return temp;
            }
        }
        #endregion

        #region 截取允许的最大的字符串子串

        /// <summary>
        /// 说明：截取允许的最大的字符串子串       
        /// </summary>
        /// <param name="str">要截取的字符串</param>
        /// <param name="len">保留的长度</param>
        /// <param name="postStr">如果超过允许的长度，字符串添加的后缀</param>
        /// <returns></returns>
        public static string GetSubString(string str, int len, string postStr)
        {
            if (!string.IsNullOrEmpty(str))
            {
                string subStr = MaxLengthSubString(str, len);
                int subStrLen = GetStringLength(subStr);
                int postStrLen = GetStringLength(postStr);
                if (subStr.Length == str.Length)
                {
                    return str;
                }
                else
                {
                    if (subStrLen > postStrLen)
                    {
                        return MaxLengthSubString(subStr, len - postStrLen) + postStr;
                    }
                    else
                    {
                        return subStr;
                    }
                }
            }
            else
            {
                return "";
            }
        }
        #endregion

        #region 获取字符串长度

        /// <summary>
        /// 说明：获取字符串长度       
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int GetStringLength(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                return Regex.Replace(str, "[\u4e00-\u9fa5]", "zz", RegexOptions.IgnoreCase).Length;
            }
            else
            {
                return 0;
            }
        }
        #endregion

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


        #endregion

        #region 严辉

        #region 取定长字符串
        /// <summary>
        /// 取定长字符串
        /// </summary>
        /// <param name="content">字符串内容</param>
        /// <param name="length">要取的字符串长度</param>
        /// <param name="isPoints">是否加 ...</param>
        /// <returns>取定长字符串</returns>
        public static string GetShortString(string content, int length, bool isPoints)
        {
            if (isPoints)
            {
                return GetSubString(content, length, "...");
            }
            else
            {
                return GetSubString(content, length, "");
            }
        }

        /// <summary>
        /// 取定长字符串
        /// </summary>
        /// <param name="content">字符串内容</param>
        /// <param name="length">要取的字符串长度</param>
        /// <param name="isPoints">是否加 ...</param>
        /// <returns>取定长字符串</returns>
        public static string GetShortString(string content, int length, string strSqlit)
        {
            Encoding encoding = Encoding.GetEncoding("GB2312");
            // int strLength = encoding.GetBytes(content).Length;
            int strLength = content.Length;
            string result = "";
            if (strLength < length)
            {
                return content;
            }
            else
            {
                return content.Substring(0, length) + strSqlit;
            }

        }
        #endregion

        #region 去掉html内容中的html标签
        /// <summary>
        /// 去掉html内容中的html标签
        /// </summary>
        /// <param name="content">html内容</param>
        /// <returns>去掉标签的内容</returns>
        public static string DropHtmlTag(string content)
        {
            //去掉<tagname>和</tagname>
            // return DropIgnoreCase(content, "<[/]{0,1}" + tagName + "[^\\>]*\\>");
            return DropIgnoreCase(content, "<.+?>");

        }
        #endregion

        #region 删除字符串中指定的内容,不区分大小写
        /// <summary>
        /// 删除字符串中指定的内容,不区分大小写
        /// </summary>
        /// <param name="src">要修改的字符串</param>
        /// <param name="pattern">要删除的正则表达式模式</param>
        /// <returns>已删除指定内容的字符串</returns>
        public static string DropIgnoreCase(string src, string pattern)
        {
            return Replace(src, pattern, "", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Multiline);
        }
        #endregion

        #region  正则替换字符串

        /// <summary>
        ///  正则替换字符串

        /// </summary>
        /// <param name="src">要修改的字符串</param>
        /// <param name="pattern">要匹配的正则表达式模式</param>
        /// <param name="replacement">替换字符串</param>
        /// <param name="options">匹配模式</param>
        /// <returns>已修改的字符串</returns>
        public static string Replace(string src, string pattern, string replacement, RegexOptions options)
        {
            if (!string.IsNullOrEmpty(src))
            {
                Regex regex = new Regex(pattern, options | RegexOptions.Compiled);

                return regex.Replace(src, replacement);
            }
            return "";
        }
        #endregion


        /// <summary>
        /// 弹出对话框

        /// </summary>
        /// <param name="typeClass">执行的类</param>
        /// <param name="strMessage">要执行的javascript代码</param>
        /// <param name="alertTitle">弹出框标题</param>
        public static void AlertMessage(System.Web.UI.Page typeClass, string strMessage, string alertTitle)
        {
            RunJavaScript(typeClass, "alert({msg:'" + strMessage + "',title:'" + alertTitle + "'})");
        }

        /// <summary>
        /// 弹出对话框

        /// </summary>
        /// <param name="typeClass">执行的类</param>
        /// <param name="strMessage">要执行的javascript代码</param>
        public static void AlertMessage(System.Web.UI.Page typeClass, string strMessage)
        {
            RunJavaScript(typeClass, "alert({msg:'" + strMessage + "',title:'提示消息'})");
        }

        /// <summary>
        /// 提示框内容处理，将'替换成\',\r\n替换成<br>

        /// </summary>
        /// <param name="strMessage">提示框内容</param>
        public static string AlertMessage(string strMessage)
        {
            if (string.IsNullOrEmpty(strMessage))
                return "";

            return strMessage.Replace("\r\n", "<br>").Replace("'", "\\'");
        }
        #endregion

        #region 肖丹
        //把文本框的回车变为<br/>
        public static string ParseToHtml(string content)
        {
            content = content.Replace("\n", "<br>");
            content = content.Replace("\n\r", "<br>");
            content = content.Replace("\r", "<br>");
            return content;
        }
        //把html中的<br>变为文本框的回车
        public static string ParseToText(string content)
        {
            content = content.Replace("<br>", "\n");
            content = content.Replace("<br>", "\n\r");
            content = content.Replace("<br>", "\r");
            return content;
        }
        #endregion

        #region 朱存群


        /// <summary>
        /// 获取真正的MD5加密
        /// </summary>
        /// <param name="encypStr"></param>
        /// <returns></returns>
        public static string getMD5(string encypStr)
        {
            string retStr;
            MD5CryptoServiceProvider m5 = new MD5CryptoServiceProvider();

            //创建md5对象
            byte[] inputBye;
            byte[] outputBye;

            //使用GB2312编码方式把字符串转化为字节数组．
            inputBye = Encoding.GetEncoding("GB2312").GetBytes(encypStr);

            outputBye = m5.ComputeHash(inputBye);

            retStr = System.BitConverter.ToString(outputBye);
            retStr = retStr.Replace("-", "");
            return retStr;
        }

        /// <summary>
        /// 定制字符MD5加密长度16位小写或32位大写

        /// </summary>
        /// <param name="str">要进行MD5加密的字符串</param>
        /// <param name="code">截取长度</param>
        /// <returns>返回字符串</returns>
        public static string getMD5(string str, int code)
        {
            if (code == 16) //16位加密
            {
                return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").ToLower().Substring(8, 16);
            }
            else if (code == 32) //32位加密
            {
                return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5");
            }
            return "0";
        }


        /// <summary>
        /// 替换html中的特殊字符
        /// </summary>
        /// <param name="theString">需要进行替换的文本。</param>
        /// <returns>替换完的文本。</returns>
        public static string KHtmlEncode(string theString)
        {
            theString = theString.Replace(">", "&gt;");
            theString = theString.Replace("<", "&lt;");
            theString = theString.Replace("  ", " &nbsp;");
            return theString;
        }

        /// <summary>
        /// 恢复html中的特殊字符
        /// </summary>
        /// <param name="theString">需要恢复的文本。</param>
        /// <returns>恢复好的文本。</returns>
        public static string KHtmlDiscode(string theString)
        {
            theString = theString.Replace("&gt;", ">");
            theString = theString.Replace("&lt;", "<");
            theString = theString.Replace("&nbsp;", " ");
            theString = theString.Replace("&nbsp;", "  ");
            theString = theString.Replace("&quot;", "\"");
            theString = theString.Replace("&#39;", "\'");
            theString = theString.Replace("<br/> ", "\n");
            return theString;

        }

        #region 验证输入类型


        /// <summary>
        /// 是否数字字符串

        /// </summary>
        public static bool IsNumber(string inputData)
        {
            Regex RegNumber = new Regex("^[-]?[0-9]+$");
            try
            {
                Match m = RegNumber.Match(inputData);
                return m.Success;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 检测是否有中文字符
        /// </summary>
        public static bool IsHasCHZN(string inputData)
        {
            Regex RegCHZN = new Regex("[\u4e00-\u9fa5]");
            try
            {
                Match m = RegCHZN.Match(inputData);
                return m.Success;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 是否是邮件地址
        /// </summary>
        public static bool IsEmail(string inputData)
        {
            Regex RegEmail = new Regex(@"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$");        //email地址 
            try
            {
                Match m = RegEmail.Match(inputData);
                return m.Success;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 是否是浮点数
        /// </summary>
        public static bool IsDecimal(string inputData)
        {
            Regex RegDecimal = new Regex("^[0-9]+[.]?[0-9]+$");
            try
            {
                Match m = RegDecimal.Match(inputData);
                return m.Success;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 是否是浮点数 可带正负号

        /// </summary>
        public static bool IsDecimalSign(string inputData)
        {
            Regex RegDecimalSign = new Regex("^[+-]?[0-9]+[.]?[0-9]+$"); //等价于^[+-]?\d+[.]?\d+$
            try
            {
                Match m = RegDecimalSign.Match(inputData);
                return m.Success;
            }
            catch
            {
                return false;
            }
        }
        ///<summary>
        ///是否指定的合法字符（以字母开头，长度在6-18之间）只能输入由数字和 26 个英文字母组成的字符串："^[A-Za-z0-9]+$"
        /// </summary>
        public static bool IsLegitSign(string inputData)
        {
            Regex RegLegitSign = new Regex("^[a-zA-Z]w{5,17}$");
            try
            {
                Match m = RegLegitSign.Match(inputData);
                return m.Success;
            }
            catch
            {
                return false;
            }
        }
        ///<summary>
        ///是否只能输入由数字和 26 个英文字母组成的字符串： 
        /// </summary>
        public static bool IsM26MSign(string inputData)
        {
            Regex RegM26MSign = new Regex("^[A-Za-z0-9]+$");
            try
            {
                Match m = RegM26MSign.Match(inputData);
                return m.Success;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 是否是身份证
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static bool IsIDCard(string inputData)
        {
            Regex RegIDCardSign = new Regex("d{15}|d{}18$");
            try
            {
                Match m = RegIDCardSign.Match(inputData);
                return m.Success;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 是否是电话号码 
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static bool IsTelSign(string inputData)
        {
            Regex RegTelSign = new Regex("^((d{3,4})|(d{3,4}-)?d{7,8}$");
            try
            {
                Match m = RegTelSign.Match(inputData);
                return m.Success;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 过滤非法字符
        /// </summary>
        /// <param name="inputStr"></param>
        /// <returns></returns>
        public static string cutBadStr(string inputStr)
        {
            if (string.IsNullOrEmpty(inputStr))
                return "";

            //inputStr = inputStr.Replace(",", "");
            inputStr = inputStr.Replace("<", "");
            inputStr = inputStr.Replace(">", "");
            inputStr = inputStr.Replace("%", "");
            inputStr = inputStr.Replace("^", "");
            inputStr = inputStr.Replace("*", "");
            inputStr = inputStr.Replace("`", "");
            //inputStr = inputStr.Replace(" ", "");
            //inputStr = inputStr.Replace("~", "");
            inputStr = inputStr.Replace("'", "");
            return inputStr;
        }
        #endregion

        #endregion

        #region 文件夹及目录路径的操作 By 何伟 2010-09-08

        /// <summary>
        /// 验证一个输入的值是否为数字
        /// </summary>
        /// <param name="checkNumber">输入的值</param>
        /// <returns>是否</returns>
        public static bool ValidateNumber(string checkNumber)
        {
            bool isCheck = true;

            if (string.IsNullOrEmpty(checkNumber))
            {
                isCheck = false;
            }
            else
            {
                char[] charNumber = checkNumber.ToCharArray();

                for (int i = 0; i < charNumber.Length; i++)
                {
                    if (!Char.IsNumber(charNumber[i]))
                    {
                        isCheck = false;
                        break;
                    }
                }
            }
            return isCheck;
        }

        /// <summary>
        /// 获取文件夹列表
        /// </summary>
        /// <param name="dirPath"></param>
        /// <returns></returns>
        public static DirectoryInfo[] GetDirectoryList(string dirPath)
        {
            DirectoryInfo[] dirList = null;

            if (Directory.Exists(dirPath))
            {
                dirList = new DirectoryInfo(dirPath).GetDirectories();
            }
            return dirList;
        }

        /// <summary>
        /// 判断目录是否在指定的父目录下面

        /// </summary>
        /// <param name="parentDirectory">父目录相对路径</param>
        /// <param name="childDirectory">子目录相对路径</param>
        /// <returns></returns>
        public static bool IsChildDirectory(string parentDirectory, string childDirectory, string virtualPath)
        {
            bool isChildDir = false;
            if (string.IsNullOrEmpty(parentDirectory) || string.IsNullOrEmpty(childDirectory))
                return isChildDir;
            if (childDirectory.IndexOf(":") == -1)
            {
                childDirectory = HttpContext.Current.Server.MapPath(virtualPath + childDirectory);
            }
            if (parentDirectory.IndexOf(":") == -1)
            {
                parentDirectory = HttpContext.Current.Server.MapPath(virtualPath + parentDirectory);
            }
            if (Directory.Exists(parentDirectory) && Directory.Exists(childDirectory))
            {
                if (childDirectory.ToLower().IndexOf(parentDirectory.ToLower()) >= 0 && childDirectory.Length > parentDirectory.Length)
                {
                    isChildDir = true;
                }
            }
            return isChildDir;
        }
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="filePath"></param>
        public static void FileDelete(string filePath)
        {
            if (filePath.IndexOf(":") == -1)
            {
                filePath = HttpContext.Current.Server.MapPath(filePath);
            }
            if (File.Exists(filePath))
            {
                try
                {
                    File.Delete(filePath);
                }
                catch
                { }
            }
        }
        /// <summary>
        /// 判断文件夹是否为空

        /// </summary>
        /// <param name="directoryPath"></param>
        /// <returns></returns>
        public static bool IsEmptyDirectory(string directoryPath)
        {
            bool isEmpty = false;
            if (Directory.Exists(directoryPath))
            {
                string[] arrFiles = Directory.GetFiles(directoryPath);
                DirectoryInfo[] arrDirList = GetDirectoryList(directoryPath);
                if (arrFiles.Length == 0 && arrDirList.Length == 0)
                {
                    return isEmpty = true;
                }
            }
            return isEmpty;
        }
        /// <summary>
        /// 删除文件夹及文件夹下面的目录、文件

        /// </summary>
        /// <param name="dirPath"></param>
        public static void DirectoryDelete(string dirPath)
        {
            if (dirPath.IndexOf(":") == -1)
            {
                dirPath = HttpContext.Current.Server.MapPath(dirPath);
            }
            if (Directory.Exists(dirPath))
            {
                try
                {
                    Directory.Delete(dirPath, true);
                }
                catch { }
            }
        }

        //根据虚拟路径获取绝对路径
        public static string ConvertSpecifiedPathToRelativePath(string specifiedPath)
        {
            string pathRooted = HostingEnvironment.MapPath("~/");

            if (!Path.IsPathRooted(specifiedPath) || specifiedPath.IndexOf(pathRooted) == -1)
            {
                return specifiedPath;
            }

            if (pathRooted.Substring(pathRooted.Length - 1, 1) == "\\")
            {
                specifiedPath = specifiedPath.Replace(pathRooted, "~/");
            }
            else
            {
                specifiedPath = specifiedPath.Replace(pathRooted, "~");
            }

            string relativePath = specifiedPath.Replace("\\", "/");
            return relativePath;
        }

        /// <summary>
        /// 计算一个目录的大小      By 何伟 2010-09-08
        /// </summary>
        /// <param name="di">指定目录</param>
        /// <param name="includeSubDir">是否包含子目录</param>
        /// <returns>目录文件的大小</returns>
        public static long CalculateDirSize(DirectoryInfo di, bool includeSubDir)
        {
            long totalSize = 0;

            // 检查所有（直接）包含的文件
            FileInfo[] files = di.GetFiles();
            foreach (FileInfo file in files)
            {
                totalSize += file.Length;
            }

            // 检查所有子目录，如果includeSubDir参数为true
            if (includeSubDir)
            {
                DirectoryInfo[] dirs = di.GetDirectories();
                foreach (DirectoryInfo dir in dirs)
                {
                    totalSize += CalculateDirSize(dir, includeSubDir);
                }
            }
            return totalSize;
        }

        /// <summary>
        /// 过滤 Script
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string FilterScript(string str)
        {
            Regex reg = new System.Text.RegularExpressions.Regex(@"<script[\s\S]+</script *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            return reg.Replace(str, "");
        }

        /// <summary>
        /// 过滤 Href
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string FilterHref(string str)
        {
            Regex reg = new System.Text.RegularExpressions.Regex(@" href *= *[\s\S]*script *:", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            return reg.Replace(str, "");
        }

        /// <summary>
        /// 过滤 Iframe
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string FilterIframe(string str)
        {
            Regex reg = new System.Text.RegularExpressions.Regex(@"<iframe[\s\S]+</iframe *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            return reg.Replace(str, "");
        }

        /// <summary>
        /// 过滤其它控件的on...事件
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string FilterOn(string str)
        {
            Regex reg = new System.Text.RegularExpressions.Regex(@" no[\s\S]*=", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            return reg.Replace(str, "");
        }

        /// <summary>
        /// 过滤 frameset
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string FilterFrameset(string str)
        {
            Regex reg = new System.Text.RegularExpressions.Regex(@"<frameset[\s\S]+</frameset *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            return reg.Replace(str, "");
        }

        /// <summary>
        /// 过滤 Image
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string FilterImage(string str)
        {
            Regex reg = new System.Text.RegularExpressions.Regex(@"\<img[^\>]+\>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            return reg.Replace(str, "");
        }

        /// <summary>
        /// 过滤除图片外的其他HTML标签
        /// </summary>
        /// <param name="str"></param>
        public static string FilterComment(string str)
        {
            str = FilterFrameset(str);
            str = FilterHref(str);
            str = FilterIframe(str);
            str = FilterScript(str);
            str = FilterOn(str);
            return str;
        }

        #endregion

        #region 卜向阳

        /// <summary>
        ///创建静态页面

        /// </summary>
        /// <param name="filename">文件绝对路径</param>
        /// <param name="content">文件内容</param>
        public static bool AddHtml(string filename, string str_Templet, string content)
        {
            string temporary = HttpContext.Current.Server.MapPath(str_Templet);
            //当文件夹不存在时创建一个

            if (!Directory.Exists(temporary))
            {
                Directory.CreateDirectory(temporary);
            }

            string filepath = temporary + "\\" + filename;
            bool Result = WriteFile(filepath, content);
            return Result;
        }
        /// <summary>
        /// 文件写入
        /// </summary>
        /// <param name="path">写入文件的路径</param>
        /// <param name="content">写入文件的内容</param>
        /// <returns>返回一个Boolean值,表示文件是否写入成功</returns>
        public static Boolean WriteFile(string path, string content)
        {
            Encoding code = Encoding.GetEncoding("utf-8");//字符编码
            FileInfo file = new FileInfo(path);
            if (file.Exists)
            {
                file.Delete();
            }
            StreamWriter writer = null;
            try
            {
                writer = new StreamWriter(path, false, code);
                writer.Write(content);

                writer.Flush();
                return true;

            }
            catch
            {
                return false;
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                    writer.Dispose();
                }
            }
        }
        /// <summary>
        /// 返回根目录路径

        /// </summary>
        /// <param name="type">文件路径类型</param>
        /// <param name="path">文件夹地址</param>
        /// <param name="filename">文件名</param>
        /// <returns></returns>
        public static string GetPath(string type, string path, string filename)
        {
            string finallyPath = "";
            string temporary = "";//设置临时路径

            if (!string.IsNullOrEmpty(filename))//检查处理文件名
            {
                string[] array = filename.Split('.');
                if (array.Length == 1)
                {
                    filename += ".html";
                }
            }

            if (type == "Absolutely")//绝对路径
            {
                temporary = HttpContext.Current.Server.MapPath(path);
                finallyPath = temporary + "\\" + filename;

            }
            else if (type == "Relatively")//相对路径
            {
                temporary = System.IO.Path.Combine(path, filename);
                temporary = temporary.Replace("//", "/");
                finallyPath = temporary;
            }

            return finallyPath;
        }

        /// <summary>
        /// 显示文件内容
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns></returns>
        public static string showFileContet(string path)
        {
            string str_content = "";
            if (File.Exists(path))
            {
                try
                {
                    StreamReader Fso = new StreamReader(path);
                    str_content = Fso.ReadToEnd();
                    Fso.Close();
                    Fso.Dispose();
                }
                catch (IOException e)
                {
                    throw new IOException(e.ToString());
                }
            }
            else
            {
                throw new Exception("找不到相应的文件!");
            }
            return str_content;
        }
        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="fileContent">文件内容</param>
        /// <returns></returns>
        public static bool saveHtml(string path, string fileContent)
        {
            bool result = false;
            if (File.Exists(path))
            {
                try
                {
                    StreamWriter Fso = new StreamWriter(path, false, Encoding.UTF8);
                    Fso.WriteLine(fileContent);
                    Fso.Close();
                    Fso.Dispose();
                }
                catch (IOException e)
                {
                    throw new IOException(e.ToString());
                }
                result = true;
            }
            else
            {
                throw new Exception("文件已经被删除!");
            }
            return result;
        }
        /// <summary>
        /// 替换数据库中空格换行符等
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ReplaceDbMark(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                str = str.Replace("> ", "&gt; ");
                str = str.Replace(" < ", "&lt; ");
                str = str.Replace("\r\n\r\n", "<BR>");
                while (str.IndexOf("\n") != -1)
                {
                    str = str.Substring(0, str.IndexOf("\n")) + "<br>" + str.Substring(str.IndexOf("\n") + 1);
                }
                while (str.IndexOf(" ") != -1)
                {
                    str = str.Substring(0, str.IndexOf(" ")) + "&nbsp;" + str.Substring(str.IndexOf(" ") + 1);
                }
            }
            return str;
        }

        /// <summary>
        /// 检测是否整数型数据
        /// </summary>
        /// <param name="Num">待检查数据</param>
        /// <returns></returns>
        public static bool IsInt(string Input)
        {
            if (Input == null)
            {
                return false;
            }
            else
            {
                return IsInteger(Input, true);
            }
        }
        /// <summary>
        /// 是否全是正整数
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        public static bool IsInteger(string Input, bool Plus)
        {
            if (Input == null)
            {
                return false;
            }
            else
            {
                string pattern = "^-?[0-9]+$";
                if (Plus)
                    pattern = "^[0-9]+$";
                if (Regex.Match(Input, pattern, RegexOptions.Compiled).Success)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        #endregion

        #region 刘俊

        /// <summary>
        /// 自定义验证方法

        /// </summary>
        /// <param name="str">要验证的字符串</param>
        /// <param name="pattern">正则表达式语句</param>
        /// <returns></returns>
        public static bool UserValidate(string str, string pattern)
        {
            try
            {
                if (str != null)
                {
                    return Regex.IsMatch(str, pattern);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 验证整数 
        /// </summary>
        /// <param name="str">要验证的字符串</param>
        /// <returns></returns>
        public static bool IsInteger(string str)
        {
            string pattern = "^(-|)\\d+$";
            return UserValidate(str, pattern);
        }


        /// <summary>
        /// 转换数字，不能转换返回0
        /// </summary>
        /// <param name="str">需要转换的字串</param>
        /// <returns></returns>
        public static int ConvertInt(string str)
        {
            if (IsInteger(str))
                return Convert.ToInt32(str);
            else
                return 0;
        }
        /// <summary>
        /// 返回从URL获取到的访问页面的名字(不带后缀.aspx)
        /// </summary>
        /// <param name="URL">URL地址如：http://www.360hqb.com/login.aspx 则返回(login)</param>
        /// <returns></returns>
        public static string GetRequestName(string URL)
        {
            return URL.Substring(URL.LastIndexOf('/') + 1, URL.LastIndexOf('.') - URL.LastIndexOf('/') - 1);
        }
        #endregion

        #region 去掉字符串中的单引号/双引号/中括号/大括号
        public static string ReplaceQuotes(string str)
        {
            Regex reg = new Regex(@"['""\[\]}{]");
            return reg.Replace(str, " ");
        }
        #endregion

        #region 文件下载
        /// <summary>
        /// 文件下载
        /// </summary>
        /// <param name="_Request">Request对象</param>
        /// <param name="_Response">Response对象</param>
        /// <param name="_fileName">下载文件名</param>
        /// <param name="_fullPath">文件路径</param>
        /// <param name="_speed"></param>
        /// <returns></returns>
        public static bool ResponseFile(HttpRequest _Request, HttpResponse _Response, string _fileName, string _fullPath, long _speed)
        {
            try
            {
                FileStream myFile = new FileStream(_fullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                BinaryReader br = new BinaryReader(myFile);
                try
                {
                    _Response.AddHeader("Accept-Ranges", "bytes");
                    _Response.Buffer = false;
                    long fileLength = myFile.Length;
                    long startBytes = 0;

                    double pack = 10240; //10K bytes
                    //int sleep = 200;   //每秒5次   即5*10K bytes每秒
                    int sleep = (int)Math.Floor(1000 * pack / _speed) + 1;
                    if (_Request.Headers["Range"] != null)
                    {
                        _Response.StatusCode = 206;
                        string[] range = _Request.Headers["Range"].Split(new char[] { '=', '-' });
                        startBytes = Convert.ToInt64(range[1]);
                    }
                    _Response.AddHeader("Content-Length", (fileLength - startBytes).ToString());
                    if (startBytes != 0)
                    {
                        //Response.AddHeader("Content-Range", string.Format(" bytes {0}-{1}/{2}", startBytes, fileLength-1, fileLength));
                    }
                    _Response.AddHeader("Connection", "Keep-Alive");
                    _Response.ContentType = "application/octet-stream";
                    _Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(_fileName, System.Text.Encoding.UTF8));

                    br.BaseStream.Seek(startBytes, SeekOrigin.Begin);
                    int maxCount = (int)Math.Floor((fileLength - startBytes) / pack) + 1;

                    for (int i = 0; i < maxCount; i++)
                    {
                        if (_Response.IsClientConnected)
                        {
                            _Response.BinaryWrite(br.ReadBytes(int.Parse(pack.ToString())));
                            System.Threading.Thread.Sleep(sleep);
                        }
                        else
                        {
                            i = maxCount;
                        }
                    }
                }
                catch
                {
                    return false;
                }
                finally
                {
                    br.Close();

                    myFile.Close();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
        #endregion

        #region 转换值，例如：1,2,3转换成'1','2','3'
        /// <summary>
        /// 转换值，例如：1,2,3转换成'1','2','3'
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static string ConvertString(string ids)
        {
            StringBuilder str = new StringBuilder();
            foreach (string id in ids.Split(','))
            {
                str.Append("'" + id + "',");
            }

            return str.ToString().Length > 0 ? str.ToString().TrimEnd(',') : str.ToString();
        }
        #endregion

        #region 验证客户信息
        public static int CheckClientInfo()
        {
            string adminPath = HttpContext.Current.Server.MapPath("/sysadmin/Configuraion/SiteInfoManage.config");
            string xmlPath = HttpContext.Current.Server.MapPath("/sysadmin/Configuraion/SystemInfo.config");
            int get;
            object[] args = new object[6];
            args[0] = HttpContext.Current.Request.Url.Host;
            args[1] = GetIP();
            args[2] = Utils.XmlRead(adminPath, "SiteInfoManage/ClientName", "");
            args[3] = Utils.XmlRead(xmlPath, "SystemInfo/SystemType", "");
            args[4] = Utils.XmlRead(xmlPath, "SystemInfo/SystemVer", "");
            string license = Utils.XmlRead(adminPath, "SiteInfoManage/License", "");
            try
            {
                Guid guidlicense = new Guid(license);
                args[5] = guidlicense;
                get = ParseInt(WebServiceHelper.ClientRecordWebService("CheckClient", args), -99);
            }
            catch
            {
                get = -99;
            }
            return get;

            //string license = Utils.XmlRead(adminPath, "SiteInfoManage/License", "");
            //string[] args1 = new string[1];
            //args1[0] = license;
            //string re = WebServiceHelper.ClientRecordWebService(url, "CheckLicense", args1).ToString();

            //return 0;

        }
        #endregion
    }
}
