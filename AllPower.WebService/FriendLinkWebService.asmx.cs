﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using KingTop.Common;
using KingTop.BLL.LinkManage;
using System.Xml;
using System.IO;
using System.Text;
using KingTop.Model;
using System.Runtime.Serialization.Formatters.Binary;

#region 版权注释
/*================================================================
    Copyright (C) 2010 华强北在线
    作者:      何伟
    创建时间： 2010年10月22日
    功能描述： 友情链接接口 
// 更新日期        更新人      更新原因/内容
--===============================================================*/
#endregion

namespace KingTop.WebService
{
    /// <summary>
    /// FriendLinkWebService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://www.360hqb.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    [System.Web.Script.Services.ScriptService]
    public class FriendLinkWebService : System.Web.Services.WebService
    {
        #region 获取指定的链接列表

        /// <summary>
        /// 获取指定的友情链接列表
        /// </summary>
        /// <param name="linkType">类型</param>
        /// <param name="className">分类名</param>
        /// <param name="showNum">显示的条数</param>
        /// <param name="siteID">站点的ID</param>
        /// <param name="IsCommend">是否推荐的</param>
        /// <returns>链接的列表</returns>
        [WebMethod]
        public XmlDocument GetFriendLinkList(int linkType, string className, int showNum, int siteID, int IsCommend)
        {
            #region 业务操作类

            FriendLink bllFriendLink = new FriendLink();                            //业务操作类
            FriendLinkClass bllLinkClass = new FriendLinkClass();                   //分类的操作类
            KingTop.Model.SelectParams sp = new KingTop.Model.SelectParams();       //查询的参数
            #endregion

            DataTable dt = null;                                                    //返回的信息列表
            sp.I1 = showNum;                                                        //显示的条数
            sp.I2 = IsCommend;                                                      //是否推荐
            sp.I3 = 1;                                                              //审核的
            sp.S1 = linkType.ToString();                                            //链接的类型

            DataTable dtClass = bllLinkClass.GetList("GETBYNAME", Utils.getOneParams(className));
            if (dtClass.Rows.Count > 0)
            {
                sp.S2 = dtClass.Rows[0]["ID"].ToString();
            }
            sp.S3 = siteID.ToString();                                              //站点的ID
            dt = bllFriendLink.GetList("SELECTBYCONFIG", sp);
            string res = DataTableToXMl(dt);

            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(new System.IO.MemoryStream(System.Text.Encoding.GetEncoding("UTF-8").GetBytes(res)));
            return xmldoc;
        }

        /// <summary>
        /// DataTable转成xml文件
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <returns>xml内容文件字符串</returns>
        public string DataTableToXMl(DataTable dt)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<FriendLinkList>");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sb.AppendLine("<FriendLink>");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    sb.AppendLine("<" + dt.Columns[j].ColumnName + ">" + dt.Rows[i][j].ToString() + "</" + dt.Columns[j].ColumnName + ">");
                }
                sb.AppendLine("</FriendLink>");
            }
            sb.AppendLine("</FriendLinkList>");
            return sb.ToString();
        }
        #endregion
    }
}
