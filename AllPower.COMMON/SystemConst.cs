using System;
using System.Collections.Generic;
using System.Text;

namespace AllPower.Common
{
    #region 版权注释
    /*================================================================
    Copyright (C) 2010 华强北在线
    作者:     陈顺
    创建时间： 2010年3月24日
    功能描述： 系统常量
    ===============================================================*/
    #endregion

    public class SystemConst
    {
        #region Session、Cookies定义
        /// <summary>
        /// 用户登录时，在客户端的COOKIES的名称
        /// </summary>
        public static readonly string SYSTEM_COOKIES = "HQB_COOKIES";
        /// <summary>
        /// 用户登录成功时，Session中存放用户信息的名称
        /// </summary>
        public static readonly string SYSTEM_SESSION_LOGININFO = "USER_INFO";  
        /// <summary>
        /// 用户登录成功时，Session中存放用户信息的名称
        /// </summary>
        public static readonly string HQB360 = "LoginAccount";

        /// <summary>
        /// 多图片上传时临时使用的cookie
        /// </summary>
        public static readonly string UPLOADTEMPIMG = "hqbTempImg";

        /// <summary>
        /// 用户登录成功时，Session中存放用户商户信息的名称
        /// </summary>
        public static readonly string BUSINESSINFO = "Business";

        /// <summary>
        /// 前台页面使用总COOKIE
        /// </summary>
        public static readonly string COOKIE_PAGE = "360HQB";

        /// <summary>
        /// 前台会员登录名,保存于Cookies中
        /// </summary>
        public static readonly string COOKIE_USERNAME = "UserName";

        /// <summary>
        /// 前台订单收货地址cookie
        /// </summary>
        public static readonly string ADDRESSCOOKIE = "ReceiveAddress";

        /// <summary>
        /// 购物车cookie
        /// </summary>
        public static readonly string CARTCOOKIE = "ShopCartCookie";

        /// <summary>
        /// 生成订单的订单号
        /// </summary>
        public static readonly string ORDERCOOKIE = "NewOrderID";
        #endregion

        #region Web.config文件中配置定义
        /// <summary>
        /// 在Web.config文件中配置的超级用户的用户名的Settings中的key的名称
        /// </summary>
        public static readonly string CONFIG_SUPERUSER = "SuperUser";
        /// <summary>
        /// 在Web.config文件中配置的超级用户的密码的Settings中的key的名称
        /// </summary>
        public static readonly string CONFIG_SUPERUSER_PASSWORD = "SuperUserPassword";
        /// <summary>
        /// 在Web.config文件中配置的分页时每页显示多少条记录
        /// </summary>
        public static readonly string CONFIG_PAGEING_COUNTOFPAGE = "RecCountOfPage";
        /// <summary>
        /// 验证码session
        /// </summary>
        public static readonly string SESSION_VALIDATECODE = "checkcode";
        /// <summary>
        /// 评论投票COOKIE
        /// </summary>
        public static readonly string COOKIE_COMMENT_Vote = "hqbCommentVote";

        /// <summary>
        /// 投票cookie
        /// </summary>
        public static readonly string COOKIE__Vote = "hqbVote";

        /// <summary>
        ///问卷调查cookie
        /// </summary>
        public static readonly string COOKIE__SURVEY = "hqbSURVEY";

        /// <summary>
        /// 会员投票cookie
        /// </summary>
        public static readonly string COOKIE_User_Vote = "hqbUserVote";

        public static readonly string VIEWSTATE_KEY = "hqbSearch"; //ViewState key         

        /// <summary>
        /// 最大保存cookie子集
        /// </summary>
        public static readonly int intMaxCookiePageCount = 10;

        /// <summary>
        /// 客户端后台cookie
        /// </summary>
        public static readonly string COOKIES_KEY = "hqbzx";

        /// <summary>
        /// 语言包cookie key
        /// </summary>
        public static readonly string COOKIES_LANG_KEY = "lang";

        /// <summary>
        /// 网站分页保存cookie key
        /// </summary>
        public static readonly string COOKIES_PAGE_KEY = "hqbzxpage";


        public static readonly string COOKIES_PWD_KEY = "emailpwd";
        /// <summary>
        /// 禁止上传的文件类型
        /// </summary>
        public static readonly string NOT_ALLOWED_UPLOAD_TYPE = "asa|asax|ascx|ashx|asmx|asp|aspx|axd|cdx|cer|config|cs|csproj|idc|licx|rem|resources|resx|shtm|shtml|soap|stm|vb|vbproj|vsdisco|webinfo|js";

        
        #endregion      
    }
}
