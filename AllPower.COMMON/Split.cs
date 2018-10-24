#region 引用程序集
using System;
using System.Web;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Configuration;
using System.Reflection;
using System.IO;
#endregion

#region 版权注释
/*===========================================================================
// Copyright (C) 2010 华强北在线 
// 作者：吴岸标// 创建日期：2010-12-8
// 功能描述：商品显示分页// 更新日期        更新人      更新原因/内容
//
===========================================================================*/
#endregion

namespace AllPower.Common
{
    #region 前几页 中间几页 未尾几页的间断显示方式
    /// <summary>
    /// 前几页 中间几页 未尾几页的显示方式
    /// </summary>
    public class Split
    {
        #region 变量成员
        public static string TemplateHtmlCode = "<li><a href=\"{$FirstPageUrl}\"><span>首页</span></a><a href=\"{$PrevPageUrl}\"><span>上一页</span></a>[HQB.Loop]<a href=\"{$PageUrl}\"><span>{$PageIndex}</span></a>[$$$$]<a href=\"{$CurrentPageUrl}\" style=\"color:#ee0000;\"><span>{$CurrentPageIndex}</span></a>[/HQB.Loop]<a href=\"{$NextPageUrl}\"><span>下一页</span></a><a href=\"{$LastPageUrl}\"><span>末页</span></a></li>";
        public static string AjaxTemplateHtmlCode = "<li><a onclick=\"split({$FirstPageUrl})\"><span>首页</span></a><a onclick=\"split({$PrevPageUrl})\"><span>上一页</span></a>[HQB.Loop]<a onclick=\"split({$PageUrl})\"><span>{$PageIndex}</span></a>[$$$$]<a onclick=\"split({$CurrentPageUrl})\" style=\"color:#ee0000;\"><span>{$CurrentPageIndex}</span></a>[/HQB.Loop]<a onclick=\"split({$NextPageUrl})\"><span>下一页</span></a><a onclick=\"split({$LastPageUrl})\"><span>末页</span></a></li>";
        #endregion

        #region 获取分页HTML代码
        /// <summary>
        /// 获取分页HTML代码
        /// </summary>
        /// <param name="pageName">列表处理url如list.aspx,当isFuncType=true时为null</param>
        /// <param name="template">分页显示模板</param>
        /// <param name="midCount">分页中间显示的页码数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="rsCount">总记录数</param>
        /// <param name="isFuncType">是否为函数分页方式</param>
        /// <returns></returns>
        public static string GetHtmlCode(string pageName, string template, int midCount, int pageIndex, int pageSize, int rsCount, bool isFuncType)
        {
            string itemTemp;            // 分页链接及样式
            string currentPageTemp;     // 当前分页链接及样式
            string splitHtmlCode;       // 分页代码
            StringBuilder sbItemHtmlCode; // 所有分页面码链接
            string tempLink;            // 临时变量，保存循环中的当前分页链接
            string[] arrTemp;           // 临时变量 
            Regex reg;
            Match match;
            int midAmount;
            string pageUrl;
            int pageCount;

            itemTemp = string.Empty;
            tempLink = string.Empty;
            currentPageTemp = string.Empty;
            midAmount = midCount;

            reg = new Regex(@"\[HQB\.Loop\s*(count\s*=\s*[""]\d+[""])?\s*\](?<1>.+?)\[/HQB\.Loop\]", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            match = reg.Match(template);
            splitHtmlCode = template;

            if (!isFuncType)
            {
                pageUrl = pageName;

                if (!string.IsNullOrEmpty(HttpContext.Current.Request.Url.Query))
                {
                    pageUrl += Regex.Replace(HttpContext.Current.Request.Url.Query, @"&pg=\d+|pg=\d+&|pg=\d+", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                    pageUrl += "&";
                }
                else
                {
                    pageUrl += "?";
                }
            }
            else
            {
                pageUrl = "1";
            }

            if (rsCount < pageSize)
            {
                pageCount = 1;
            }
            else
            {
                if (rsCount % pageSize == 0)
                {
                    pageCount = rsCount / pageSize;
                }
                else
                {
                    pageCount = rsCount / pageSize + 1;
                }
            }

            if (midAmount == 0)
            {
                midAmount = 8;
            }

            splitHtmlCode = SetLabeslValue(splitHtmlCode, pageIndex, pageCount, pageSize, rsCount, pageUrl,isFuncType);

            if (match.Success)      // 分页循环标签
            {
                itemTemp = match.Groups[1].Value;
                splitHtmlCode = splitHtmlCode.Replace(match.Value, "{[#SplitItem#]}");

                if (itemTemp.Contains("[$$$$]"))    // 存在当前分页链接样式
                {
                    arrTemp = itemTemp.Split(new string[] { "[$$$$]" }, StringSplitOptions.None);

                    if (arrTemp.Length > 1)
                    {
                        itemTemp = arrTemp[0];
                        currentPageTemp = arrTemp[1];
                    }
                    else
                    {
                        itemTemp = arrTemp[0];
                        currentPageTemp = arrTemp[0];
                    }
                }

                if (pageCount > midAmount + 6)
                {
                    if (pageIndex > midAmount / 2 + 2)
                    {
                        if (pageIndex > (pageCount - (midAmount / 2) - 2))
                        {
                            sbItemHtmlCode = GetIntermittentHtmlCode(pageUrl, itemTemp, currentPageTemp, pageIndex, pageCount - midAmount, pageCount, true, false, pageCount,isFuncType);
                        }
                        else
                        {
                            sbItemHtmlCode = GetIntermittentHtmlCode(pageUrl, itemTemp, currentPageTemp, pageIndex, pageIndex - midAmount / 2, pageIndex + midAmount / 2, true, true, pageCount,isFuncType);
                        }
                    }
                    else
                    {
                        sbItemHtmlCode = GetIntermittentHtmlCode(pageUrl, itemTemp, currentPageTemp, pageIndex, 3, midAmount + 2, false, true, pageCount,isFuncType);
                    }

                }
                else
                {
                    sbItemHtmlCode = GetIntermittentHtmlCode(pageUrl, itemTemp, currentPageTemp, pageIndex, 3, pageCount, false, false, pageCount,isFuncType);
                }


                splitHtmlCode = splitHtmlCode.Replace("{[#SplitItem#]}", sbItemHtmlCode.ToString());
            }

            splitHtmlCode = splitHtmlCode.Replace("{$CurrentPageIndex}", pageIndex.ToString());



            return splitHtmlCode;
        }
        #endregion

        #region 中间分页链接
        private static StringBuilder GetIntermittentHtmlCode(string pageUrl, string itemTemp, string currentPageTemp, int currentPageIndex, int startIndex, int endIndex, bool isStart, bool isEnd, int pageCount, bool isFuncType)
        {
            StringBuilder sbItemHtmlCode;
            string tempLink;
            string firstPageUrl;

            sbItemHtmlCode = new StringBuilder();

            if (!isFuncType)
            {
                if (pageUrl.Substring(pageUrl.Length - 1, 1) == "&" || pageUrl.Substring(pageUrl.Length - 1, 1) == "?")
                {
                    firstPageUrl = pageUrl.Substring(0, pageUrl.Length - 1);
                }
                else
                {
                    firstPageUrl = pageUrl;
                }
            }
            else
            {
                firstPageUrl = "1";
            }

            if (currentPageIndex != 1)
            {
                tempLink = itemTemp.Replace("{$PageUrl}", firstPageUrl);  // 第一页
                tempLink = tempLink.Replace("{$PageIndex}", "1");
            }
            else  // 当前页为第一页
            {
                tempLink = currentPageTemp.Replace("{$CurrentPageUrl}", firstPageUrl);  // 第一页
                tempLink = tempLink.Replace("{$CurrentPageIndex}", "1");
            }

            sbItemHtmlCode.Append(tempLink);

            if (pageCount > 1)
            {
                if (currentPageIndex != 2)
                {
                    if (isFuncType)
                    {
                        tempLink = itemTemp.Replace("{$PageUrl}", "2");  // 第二页
                    }
                    else
                    {
                        tempLink = itemTemp.Replace("{$PageUrl}", pageUrl + "pg=2");  // 第二页
                    }

                    tempLink = tempLink.Replace("{$PageIndex}", "2");
                }
                else  // 当前页为第二页
                {
                    if (isFuncType)
                    {
                        tempLink = currentPageTemp.Replace("{$CurrentPageUrl}", "2");  // 第二页
                    }
                    else
                    {
                        tempLink = currentPageTemp.Replace("{$CurrentPageUrl}", pageUrl + "pg=2");  // 第二页
                    }

                    tempLink = tempLink.Replace("{$CurrentPageIndex}", "2");
                }

                sbItemHtmlCode.Append(tempLink);
            }

            if (isStart)
            {
                sbItemHtmlCode.Append("...");
            }

            for (int k = startIndex; k <= endIndex; k++)
            {
                if (k == currentPageIndex)
                {
                    if (isFuncType)
                    {
                        tempLink = currentPageTemp.Replace("{$CurrentPageUrl}", k.ToString());
                    }
                    else
                    {
                        tempLink = currentPageTemp.Replace("{$CurrentPageUrl}", pageUrl + "pg=" + k.ToString());
                    }

                    tempLink = tempLink.Replace("{$CurrentPageIndex}", k.ToString());
                }
                else
                {
                    if (isFuncType)
                    {
                        tempLink = itemTemp.Replace("{$PageUrl}",k.ToString());
                    }
                    else
                    {
                        tempLink = itemTemp.Replace("{$PageUrl}", pageUrl + "pg=" + k.ToString());
                    }

                    tempLink = tempLink.Replace("{$PageIndex}", k.ToString());
                }

                sbItemHtmlCode.Append(tempLink);
            }

            if (isEnd)
            {
                sbItemHtmlCode.Append("...");

                if (isFuncType)
                {
                    tempLink = itemTemp.Replace("{$PageUrl}", (pageCount - 1).ToString());  // 尾二页
                }
                else
                {
                    tempLink = itemTemp.Replace("{$PageUrl}", pageUrl + "pg=" + (pageCount - 1).ToString());  // 尾二页
                }

                tempLink = tempLink.Replace("{$PageIndex}", (pageCount - 1).ToString());
                sbItemHtmlCode.Append(tempLink);

                if (isFuncType)
                {
                    tempLink = itemTemp.Replace("{$PageUrl}", pageCount.ToString());  // 尾页
                }
                else
                {
                    tempLink = itemTemp.Replace("{$PageUrl}", pageUrl + "pg=" + pageCount.ToString());  // 尾页
                }

                tempLink = tempLink.Replace("{$PageIndex}", pageCount.ToString());
                sbItemHtmlCode.Append(tempLink);
            }

            return sbItemHtmlCode;
        }
        #endregion

        #region 替换分页标签中的变量
        private static string SetLabeslValue(string content, int currentPageIndex, int totalPage, int pageSize, int totalRS, string pageUrl, bool isFuncType)
        {
            string resultContent;
            string firstPageUrl;

            resultContent = content;

            if (!isFuncType)
            {
                pageUrl = pageUrl.Trim();

                if (pageUrl.Substring(pageUrl.Length - 1, 1) == "&" || pageUrl.Substring(pageUrl.Length - 1, 1) == "?")
                {
                    firstPageUrl = pageUrl.Substring(0, pageUrl.Length - 1);
                }
                else
                {
                    firstPageUrl = pageUrl;
                }
            }
            else
            {
                firstPageUrl = "1";
            }

            resultContent = resultContent.Replace("{$TotalPage}", totalPage.ToString());                                    // 总页数
            resultContent = resultContent.Replace("{$FirstPageUrl}", firstPageUrl);                                          // 首页地址
            resultContent = resultContent.Replace("{$RSTotal}", totalRS.ToString());                                        // 记录总数
            resultContent = resultContent.Replace("{$PageSize}", pageSize.ToString());                                      // 分页大小

            if (currentPageIndex > 2)   // 上一页地址
            {
                if (isFuncType)
                {
                    resultContent = resultContent.Replace("{$PrevPageUrl}", (currentPageIndex - 1).ToString());
                }
                else
                {
                    resultContent = resultContent.Replace("{$PrevPageUrl}", pageUrl + "pg=" + (currentPageIndex - 1).ToString());
                }
            }
            else
            {
                resultContent = resultContent.Replace("{$PrevPageUrl}", firstPageUrl);
            }

            if (totalPage - 1 > currentPageIndex)  // 下一页地址
            {
                if (isFuncType)
                {
                    resultContent = resultContent.Replace("{$NextPageUrl}", (currentPageIndex + 1).ToString());
                }
                else
                {
                    resultContent = resultContent.Replace("{$NextPageUrl}", pageUrl + "pg=" + (currentPageIndex + 1).ToString());
                }
            }
            else
            {
                if (totalPage != 1)
                {
                    if (isFuncType)
                    {
                        resultContent = resultContent.Replace("{$NextPageUrl}", totalPage.ToString());
                    }
                    else
                    {
                        resultContent = resultContent.Replace("{$NextPageUrl}", pageUrl + "pg=" + totalPage.ToString());
                    }
                }
                else
                {
                    resultContent = resultContent.Replace("{$NextPageUrl}", firstPageUrl);
                }
            }

            if (totalPage != 1) // 最后一页地址
            {
                if (isFuncType)
                {
                    resultContent = resultContent.Replace("{$LastPageUrl}", totalPage.ToString());
                }
                else
                {
                    resultContent = resultContent.Replace("{$LastPageUrl}", pageUrl + "pg=" + totalPage.ToString());
                }
            }
            else
            {
                resultContent = resultContent.Replace("{$LastPageUrl}", firstPageUrl);
            }

            return resultContent;
        }
        #endregion
    }
    #endregion
}
