using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

#region 版权注释
/*================================================================
    Copyright (C) 2010 华强北在线
    作者:     袁纯林 吴岸标 周武
    创建时间： 2010年3月18日
    功能描述： 文件读取或者写入操作
--===============================================================*/
#endregion
namespace AllPower.Common
{
    public class FileReadOrWrite
    {
        private bool isServerPath = true;

        /// <summary>
        /// 是否虚拟路径
        /// </summary>
        public bool IsServerPath
        {
            get { return isServerPath; }
            set { isServerPath = value; }
        }
        /// <summary>
        /// 文件名

        /// </summary>
        public string FileName { set; get; }

        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath { get; set; }

        private Encoding fileEncding = Encoding.GetEncoding("UTF-8");

        public Encoding FileEncding
        {
            get { return fileEncding; }
            set { fileEncding = value; }
        }

        private bool fileAppend = false;

        public bool FileAppend
        {
            get { return fileAppend; }
            set { fileAppend = value; }
        }

        /// <summary>
        /// 文件读取
        /// </summary>
        /// <returns></returns>
        public string FileRead()
        {
            StreamReader srRead = null;
            try
            {
                if (IsServerPath)
                {
                    srRead = new StreamReader(Utils.GetPath(FilePath), System.Text.Encoding.Default);  //读取文件
                }
                else
                {
                    srRead = new StreamReader(FilePath, System.Text.Encoding.Default);  //读取文件
                }
                string strValue = srRead.ReadToEnd();
                return strValue;
            }
            catch (Exception ex)
            {
                return "";
            }
            finally
            {
                if (srRead != null)
                {
                    srRead.Close();
                    srRead.Dispose();
                }
            }
        }

        /// <summary>
        /// 文件读取
        /// </summary>
        /// <param name="strPath">文件路径</param>
        /// <returns></returns>
        public string FileRead(string strPath)
        {
            FilePath = strPath;
            return FileRead();
        }

        /// <summary>
        /// 文件读取
        /// </summary>
        /// <param name="strPath">文件路径</param>
        /// <returns></returns>
        public string FileRead(string strPath, Encoding encoding)
        {
            FilePath = strPath;
            fileEncding = encoding;
            return FileRead();
        }

        /// <summary>
        /// 文件写入
        /// </summary>
        /// <param name="strValue">要写入的内容</param>
        /// <returns></returns>
        public bool FileWrite(string strValue)
        {
            StreamWriter srWrite = null;
            try
            {
                if (IsServerPath)
                {
                    srWrite = new StreamWriter(Utils.GetPath(FilePath), fileAppend, fileEncding);
                }
                else
                {
                    srWrite = new StreamWriter(FilePath, fileAppend, fileEncding);
                }
                srWrite.Write(strValue);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                if (srWrite != null)
                {
                    srWrite.Close();
                    srWrite.Dispose();
                }
            }
        }

        /// <summary>
        /// 文件写入
        /// </summary>
        /// <param name="strValue">要写入的内容</param>
        /// <returns></returns>
        public bool FileWrite(string strValue, string strPath)
        {
            FilePath = strPath;
            return FileWrite(strValue);
        }

        /// <summary>
        /// 文件写入
        /// </summary>
        /// <param name="strValue">要写入的内容</param>
        /// <returns></returns>
        public bool FileWrite(string strValue, string strPath, Encoding encoding)
        {
            fileEncding = encoding;
            FilePath = strPath;
            return FileWrite(strValue);
        }

        /// <summary>
        /// 文件写入
        /// </summary>
        /// <param name="strValue">要写入的内容</param>
        /// <returns></returns>
        public bool FileWrite(string strValue, string strPath, Encoding encoding, bool append)
        {
            fileAppend = append;
            fileEncding = encoding;
            FilePath = strPath;
            return FileWrite(strValue);
        }

        /// <summary>
        /// 判断文件是否存在 
        /// </summary>
        /// <param name="strPath">路径</param>
        /// <param name="strType">类型  0为文件,1为目录</param>
        /// <returns></returns>
        public bool FileExists(string strPath, string strType)
        {
            FilePath = strPath;
            return FileExists(strType);
        }

        /// <summary>
        /// 判断文件是否存在
        /// </summary>
        /// <param name="strType">类型  0为文件,1为目录</param>
        /// <returns></returns>
        public bool FileExists(string strType)
        {
            if (strType == "0") //类型 0为文件,1为目录 
            {
                if (IsServerPath)
                {
                    return File.Exists(Utils.GetPath(FilePath));
                }
                else
                {
                    return File.Exists(FilePath);
                }

            }
            else //目录 
            {
                if (IsServerPath)
                {
                    return Directory.Exists(Utils.GetPath(FilePath));
                }
                else
                {
                    return Directory.Exists(FilePath);
                }
            }
        }

        /// <summary>
        /// 获取文件类

        /// </summary>
        /// <param name="strPath">文件路径</param>
        /// <returns></returns>
        public FileInfo GetFileInfo(string strPath)
        {
            FilePath = strPath;
            return GetFileInfo();
        }

        /// <summary>
        /// 获取文件类

        /// </summary>
        /// <returns></returns>
        public FileInfo GetFileInfo()
        {
            if (IsServerPath)
            {
                return new FileInfo(Utils.GetPath(FilePath));
            }
            else
            {
                return new FileInfo(FilePath);
            }
        }
    }
}
