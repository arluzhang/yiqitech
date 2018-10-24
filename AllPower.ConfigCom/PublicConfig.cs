using System;
using System.Data.Common;
using System.Text.RegularExpressions;
using AllPower.Common;

namespace AllPower.Config
{
    /// <summary>
	/// 系统基本信息设置类
	/// </summary>
    public class PublicConfig
    {
        private static object lockHelper = new object();

        //private static System.Timers.Timer publicConfigTimer = new System.Timers.Timer(150000000);

        private static PublicConfigInfo m_configinfo;

        /// <summary>
        /// 静态构造函数初始化相应实例和定时器
        /// </summary>
        static PublicConfig()
        {
            m_configinfo = PublicConfigFileManager.LoadConfig();

            //publicConfigTimer.AutoReset = true;
            //publicConfigTimer.Enabled = true;
            //publicConfigTimer.Elapsed += new System.Timers.ElapsedEventHandler(Timer_Elapsed);
            //publicConfigTimer.Start();
        }

        private static void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            ResetConfig();
        }


        /// <summary>
        /// 重设配置类实例
        /// </summary>
        public static void ResetConfig()
        {
            m_configinfo = PublicConfigFileManager.LoadConfig();
        }

        public static PublicConfigInfo GetConfig()
        {
            if (m_configinfo == null)
            {
                ResetConfig();
            }
            return m_configinfo;
        }


        /// <summary>
        /// 获得设置项信息
        /// </summary>
        /// <returns>设置项</returns>
        public static bool SetIpDenyAccess(string denyipaccess)
        {
            bool result;

            lock (lockHelper)
            {
                try
                {
                    PublicConfigInfo configInfo = PublicConfig.GetConfig();
                    //configInfo.Ipdenyaccess = configInfo.Ipdenyaccess + "\n" + denyipaccess;
                    PublicConfig.Serialiaze(configInfo, System.Web.HttpContext.Current.Server.MapPath("~/config/public.config"));
                    result = true;
                }
                catch
                {
                    return false;
                }

            }
            return result;

        }


        /// <summary>
        /// 保存配置类实例
        /// </summary>
        /// <param name="publicconfiginfo"></param>
        /// <returns></returns>
        public static bool SaveConfig(PublicConfigInfo publicconfiginfo)
        {
            PublicConfigFileManager gcf = new PublicConfigFileManager();
            PublicConfigFileManager.ConfigInfo = publicconfiginfo;
            return gcf.SaveConfig("");
        }



        #region Helper

        /// <summary>
        /// 序列化配置信息为XML
        /// </summary>
        /// <param name="configinfo">配置信息</param>
        /// <param name="configFilePath">配置文件完整路径</param>
        public static PublicConfigInfo Serialiaze(PublicConfigInfo configinfo, string configFilePath)
        {
            lock (lockHelper)
            {
                SerializationHelper.Save(configinfo, configFilePath);
            }
            return configinfo;
        }


        public static PublicConfigInfo Deserialize(string configFilePath)
        {
            return (PublicConfigInfo)SerializationHelper.Load(typeof(PublicConfigInfo), configFilePath);
        }

        #endregion
    }
}
