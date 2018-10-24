#region 程序集引用
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using System.Data;
#endregion

#region 版权注释
/*================================================================
    Copyright (C) 2010 华强北在线
    作者:     袁纯林 吴岸标 周武
    创建时间： 2010年3月10日
    功能描述： 控件操作通用方法
 
// 更新日期        更新人      更新原因/内容
//
--===============================================================*/
#endregion


namespace AllPower.Common
{
    public class ControlUtils
    {
        /// <summary>
        /// 按值设定单选按钮列表的选定项
        /// </summary>
        /// <param name="radl"></param>
        /// <param name="objValue"></param>
        public static void RadlSetValue(RadioButtonList radl, object objValue)
        {
            string radlValue;

            radlValue = objValue.ToString();
            radl.ClearSelection();

            foreach (ListItem item in radl.Items)
            {
                if (item.Value == radlValue)
                {
                    item.Selected = true;
                    return;
                }
            }
        }

        /// <summary>
        /// 按显示文本设定单选按钮列表的选定项
        /// </summary>
        /// <param name="radl"></param>
        /// <param name="objText"></param>
        public static void RadlSetText(RadioButtonList radl, object objText)
        {
            string radlText;

            radlText = (string)objText;
            radl.ClearSelection();

            foreach (ListItem item in radl.Items)
            {
                if (item.Text == radlText)
                {
                    item.Selected = true;
                    return;
                }
            }
        }

        /// <summary>
        /// CheckBoxList快速绑定
        /// </summary>
        /// <param name="ddlControls">cbl控件</param>
        /// <param name="dt">DataTable表</param>
        /// <param name="strName">要绑定的Text列</param>
        /// <param name="strValue">要绑定的Value列</param>
        public static void CheckBoxListBind(System.Web.UI.WebControls.CheckBoxList cblControls, DataTable dt, string strName, string strValue)
        {
            CheckBoxListBind(cblControls, dt, strName, strValue, true);
        }
        /// <summary>
        /// CheckBoxList快速绑定
        /// </summary>
        /// <param name="ddlControls">cbl控件</param>
        /// <param name="dt">DataTable表</param>
        /// <param name="strName">要绑定的Text列</param>
        /// <param name="strValue">要绑定的Value列</param>
        public static void CheckBoxListBind(System.Web.UI.WebControls.CheckBoxList ddlControls, DataTable dt, string strName, string strValue, bool isDisplay)
        {
            ddlControls.DataSource = dt;
            ddlControls.DataTextField = strName;
            ddlControls.DataValueField = strValue;
            ddlControls.DataBind();
            if (isDisplay)
            {
                dt.Dispose();
            }
        }


          /// <summary>
        /// dropDown快速绑定
        /// </summary>
        /// <param name="ddlControls">ddl控件</param>
        /// <param name="dt">DataTable表</param>
        /// <param name="strName">要绑定的Text列</param>
        /// <param name="strValue">要绑定的Value列</param>
        public static void DropDownDataBind(System.Web.UI.WebControls.DropDownList ddlControls, DataTable dt, string strName, string strValue)
        {
            DropDownDataBind(ddlControls, dt, strName, strValue,true);
        }
        /// <summary>
        /// dropDown快速绑定
        /// </summary>
        /// <param name="ddlControls">ddl控件</param>
        /// <param name="dt">DataTable表</param>
        /// <param name="strName">要绑定的Text列</param>
        /// <param name="strValue">要绑定的Value列</param>
        public static void DropDownDataBind(System.Web.UI.WebControls.DropDownList ddlControls, DataTable dt, string strName, string strValue,bool isDisplay)
        {
            ddlControls.DataSource = dt;
            ddlControls.DataTextField = strName;
            ddlControls.DataValueField = strValue;
            ddlControls.DataBind();
            if (isDisplay)
            {
                dt.Dispose();
            }
        }


        /// <summary>
        /// DropDownList 绑定数据为 OptionsDictionary
        /// </summary>
        /// <param name="ddlControls">DropDownList控件</param>
        /// <param name="findOption">实例化 OptionsDictionary对象，要传入的字符串</param>
        public static void DropDownDataBind(System.Web.UI.WebControls.DropDownList ddlControls, string findOption)
        {
            OptionsDictionary odLinkType = new OptionsDictionary(findOption);

            foreach (KeyValuePair<string, string> kvp in odLinkType.getDictionary2)
            {
                ddlControls.Items.Add(new ListItem(kvp.Value, kvp.Key.ToString()));
            }
        }

           /// <summary>
        /// RadioButtonLis快速绑定
        /// </summary>
        /// <param name="rbtlControls">RadioButtonLis控件</param>
        /// <param name="dt">DataTable表</param>
        /// <param name="strName">要绑定的Text列</param>
        /// <param name="strValue">要绑定的Value列</param>
        public static void RadioButtonListDataBind(System.Web.UI.WebControls.RadioButtonList rbtlControls, DataTable dt, string strName, string strValue)
        {
            RadioButtonListDataBind(rbtlControls, dt, strName, strValue, true);
        }
        /// <summary>
        /// RadioButtonLis快速绑定
        /// </summary>
        /// <param name="rbtlControls">RadioButtonLis控件</param>
        /// <param name="dt">DataTable表</param>
        /// <param name="strName">要绑定的Text列</param>
        /// <param name="strValue">要绑定的Value列</param>
        public static void RadioButtonListDataBind(System.Web.UI.WebControls.RadioButtonList rbtlControls, DataTable dt, string strName, string strValue,bool isDisplay)
        {
            rbtlControls.DataSource = dt;
            rbtlControls.DataTextField = strName;
            rbtlControls.DataValueField = strValue;
            rbtlControls.DataBind();
            if (isDisplay)
            {
                dt.Dispose();
            }
        }

        /// <summary>
        /// RadioButtonLis 绑定数据为 OptionsDictionary
        /// </summary>
        /// <param name="rbtlControls">RadioButtonLis控件</param>
        /// <param name="findOption">实例化 OptionsDictionary对象，要传入的字符串</param>
        public static void RadioButtonListDataBind(System.Web.UI.WebControls.RadioButtonList rbtlControls,string findOption)
        {
            OptionsDictionary odLinkType = new OptionsDictionary(findOption);

            foreach (KeyValuePair<string, string> kvp in odLinkType.getDictionary2)
            {
                rbtlControls.Items.Add(new ListItem(kvp.Value, kvp.Key.ToString()));
            }
        }

        /// <summary>
        /// 获取复选列表的值  用,号隔开
        /// </summary>
        /// <param name="cblControls"></param>
        /// <returns></returns>
        public static string GetCheckBoxListSelectValue(System.Web.UI.WebControls.CheckBoxList cblControls)
        {
            return GetCheckBoxListSelectValue(cblControls, ",");
        }

        /// <summary>
        /// 获取复选列表的值  用,号隔开
        /// </summary>
        /// <param name="cblControls"></param>
        /// <returns></returns>
        public static string GetCheckBoxListSelectValue(System.Web.UI.WebControls.CheckBoxList cblControls, string strParam)
        {
            StringBuilder sb = new StringBuilder(16);
            ListItem litem = null;
            for (int i = 0; i < cblControls.Items.Count; i++)
            {
                litem = cblControls.Items[i];
                if (litem.Selected)
                { 
                    if (sb.Length > 0)
                    {
                        sb.Append(strParam);
                    }
                    sb.Append(litem.Value);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 获取多选列表的值  用,号隔开
        /// </summary>
        /// <param name="cblControls"></param>
        /// <returns></returns>
        public static string GetListBoxSelectValue(System.Web.UI.WebControls.ListBox cblControls)
        {
            return GetListBoxSelectValue(cblControls, ",");
        }

        /// <summary>
        /// 获取多选列表的值  用,号隔开
        /// </summary>
        /// <param name="cblControls"></param>
        /// <returns></returns>
        public static string GetListBoxSelectValue(System.Web.UI.WebControls.ListBox cblControls, string strParam)
        {
            StringBuilder sb = new StringBuilder(16);
            ListItem litem = null;
            for (int i = 0; i < cblControls.Items.Count; i++)
            {
                litem = cblControls.Items[i];
                if (litem.Selected)
                {
                    if (sb.Length > 0)
                    {
                        sb.Append(strParam);
                    }
                    sb.Append(litem.Value);
                }
            }
            return sb.ToString();
        }

         /// <summary>
        /// 设置复选框列表的值 用,号隔开
        /// </summary>
        /// <param name="cblControls"></param>
        /// <param name="strValue"></param>
        public static void SetGetCheckBoxListSelectValue(System.Web.UI.WebControls.CheckBoxList cblControls, string strValue)
        {
            SetGetCheckBoxListSelectValue(cblControls, strValue, ","); 
        }

        /// <summary>
        /// 设置复选框列表的值 用,号隔开
        /// </summary>
        /// <param name="cblControls"></param>
        /// <param name="strValue"></param>
        public static void SetGetCheckBoxListSelectValue(System.Web.UI.WebControls.CheckBoxList cblControls, string strValue, string strParam)
        {
            //string[] strValues = Utils.strSplit(strValue, strParam);
            //ListItem lItem = null;
            //for(int i=0;i<cblControls.Items.Count;i++)  //循环控件项
            //{
            //    lItem = cblControls.Items[i];
            //    for(int j=0;j<strValue.Length;j++)
            //    {
            //        if (lItem.Value == strValues[j])  //如果当前控件项和要绑定的值相等 则绑定 退出此次小循环
            //        {
            //            lItem.Selected = true;
            //            break;
            //        }
            //    }
            //}

            string[] arrStrValue;

            arrStrValue = Utils.strSplit(strValue, strParam);

            foreach (string strItem in arrStrValue)
            {
                foreach (ListItem item in cblControls.Items)
                {
                    if (item.Value == strItem)
                    {
                        item.Selected = true;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 设置 下拉列表的值 用,号隔开
        /// </summary>
        /// <param name="cblControls"></param>
        /// <param name="strValue"></param>
        public static void SetGetListBoxSelectValue(System.Web.UI.WebControls.ListBox cblControls, string strValue)
        {
            SetGetListBoxSelectValue(cblControls, strValue, ",");
        }

        /// <summary>
        /// 设置 下拉列表的值 用,号隔开
        /// </summary>
        /// <param name="cblControls"></param>
        /// <param name="strValue"></param>
        public static void SetGetListBoxSelectValue(System.Web.UI.WebControls.ListBox cblControls, string strValue, string strParam)
        {
            //string[] strValues = Utils.strSplit(strValue, strParam);
            //ListItem lItem = null;
            //for(int i=0;i<cblControls.Items.Count;i++)  //循环控件项
            //{
            //    lItem = cblControls.Items[i];
            //    for(int j=0;j<strValue.Length;j++)
            //    {
            //        if (lItem.Value == strValues[j])  //如果当前控件项和要绑定的值相等 则绑定 退出此次小循环
            //        {
            //            lItem.Selected = true;
            //            break;
            //        }
            //    }
            //}

            string[] arrStrValue;

            arrStrValue = Utils.strSplit(strValue, strParam);

            foreach (string strItem in arrStrValue)
            {
                foreach (ListItem item in cblControls.Items)
                {
                    if (item.Value == strItem)
                    {
                        item.Selected = true;
                        break;
                    }
                }
            }
        }
    }
}
