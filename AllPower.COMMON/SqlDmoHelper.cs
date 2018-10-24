using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.IO;
using SQLDMO;
using System.Collections.Generic ;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace AllPower.Common
{
    /// <summary>
    /// SQLDMO辅助类
    /// </summary>
    /// <remarks>
    /// 使用前添加 "Microsoft SQLDMO Object Library" COM 引用。
    /// </remarks>
    public class SqlDmoHelper
    {
        #region DatabaseInfo

        /// <summary>
        /// 数据库信息
        /// </summary>
        public struct DatabaseInfo
        {
            public string Name;
            public string Owner;
            public string PrimaryFilePath;
            public string CreateDate;
            public int Size;
            public float SpaceAvailable;
            public string PrimaryName;
            public string PrimaryFilename;
            public int PrimarySize;
            public int PrimaryMaxSize;
            public string LogName;
            public string LogFilename;
            public int LogSize;
            public int LogMaxSize;

            public override string ToString()
            {
                string s = "Name:{0}\r\n" +
                  "Owner:{1}\r\n" +
                  "PrimaryFilePath:{2}\r\n" +
                  "CreateDate:{3}\r\n" +
                  "Size:{4}MB\r\n" +
                  "SpaceAvailable:{5}MB\r\n" +
                  "PrimaryName:{6}\r\n" +
                  "PrimaryFilename:{7}\r\n" +
                  "PrimarySize:{8}MB\r\n" +
                  "PrimaryMaxSize:{9}MB\r\n" +
                  "LogName:{10}\r\n" +
                  "LogFilename:{11}\r\n" +
                  "LogSize:{12}MB\r\n" +
                  "LogMaxSize:{13}MB";

                return string.Format(s, Name, Owner, PrimaryFilePath, CreateDate, Size,
                  SpaceAvailable, PrimaryName, PrimaryFilename, PrimarySize,
                  PrimaryMaxSize, LogName, LogFilename, LogSize, LogMaxSize);
            }
        }

        #endregion

        private SQLDMO.SQLServer sqlServer;
        private string server;
        private string login;
        private string password;
        private string database;
        private string cmsdbname;

        public string CmsDbName
        {
            set { cmsdbname = value; }
            get { return cmsdbname; }
        }


        public SqlDmoHelper(string server,string login,string password,string database)
        {
            if (string.IsNullOrEmpty(server))
            {
                string connStr = AllPower.Common.SQLHelper.ConnectionStringLocalTransaction;
                string[] arrConn = connStr.Split(';');
                for (int i = 0; i < arrConn.Length; i++)
                {
                    string[] itemArr = arrConn[i].Split('=');
                    if (itemArr[0].ToLower() == "server")
                        server = itemArr[1];
                    else if (itemArr[0].ToLower() == "database")
                        database = itemArr[1];
                    else if (itemArr[0].ToLower() == "uid")
                        login = itemArr[1];
                    else if (itemArr[0].ToLower() == "pwd")
                        password = itemArr[1];
                }
            }
            //if (string.IsNullOrEmpty(server) || string.IsNullOrEmpty(database) || string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            //{
            //    return;
            //}
            this.CmsDbName = database;
            this.server = server;
            this.login = login;
            this.password = password;
            this.database = database;

            //sqlServer = new SQLServer2Class();
            //sqlServer = new SQLDMO.SQLServer();
            try
            {
                sqlServer = new SQLServerClass();
                sqlServer.Connect(server, login, password);
            }
            catch
            {
                sqlServer = null;
            }
        }

        public void Close()
        {
            sqlServer.Close();
        }

        #region Property

        /// <summary>
        /// 获取主要版本号
        /// </summary>
        public string Version
        {
            get
            {
                return string.Format("{0}.{1}", sqlServer.VersionMajor, sqlServer.VersionMinor);
            }
        }

        /// <summary>
        /// 获取详细版本信息
        /// </summary>
        public string VersionString
        {
            get
            {
                return sqlServer.VersionString;
            }
        }

        /// <summary>
        /// 获取服务器时间
        /// </summary>
        public string ServerTime
        {
            get
            {
                return sqlServer.ServerTime;
            }
        }

        /// <summary>
        /// 获取系统服务名称 sqlserver2下可用
        /// </summary>
        //public string ServiceName
        //{
        //    get
        //    {
        //        return sqlServer.ServiceName;
        //    }
        //}

        /// <summary>
        /// 获取或设置系统服务是否自动启动
        /// </summary>
        public bool AutostartServer
        {
            get
            {
                return sqlServer.Registry.AutostartServer;
            }
            set
            {
                sqlServer.Registry.AutostartServer = value;
            }
        }

        /// <summary>
        /// 获取字符集设置
        /// </summary>
        public string CharacterSet
        {
            get
            {
                return sqlServer.Registry.CharacterSet;
            }
        }

        /// <summary>
        /// 获取服务器物理内存大小(MB)
        /// </summary>
        public int PhysicalMemory
        {
            get
            {
                return sqlServer.Registry.PhysicalMemory;
            }
        }

        /// <summary>
        /// 获取服务器处理器(CPU)数量
        /// </summary>
        public int NumberOfProcessors
        {
            get
            {
                return sqlServer.Registry.NumberOfProcessors;
            }
        }

        #endregion

        #region Public Method

        /// <summary>
        /// 获取网络内所有可用的服务器
        /// </summary>
        /// <returns></returns>
        public static string[] ListAvailableSQLServers()
        {
            NameList servers = new ApplicationClass().ListAvailableSQLServers();
            if (servers.Count <= 0) return new string[0];

            ArrayList list = new ArrayList(servers.Count);
            foreach (object o in servers) list.Add(o);
            return (string[])list.ToArray(typeof(string));
        }

        /// <summary>
        /// 断开数据库所有连接
        /// </summary>
        /// <param name="dbName"></param>
        public void KillAllProcess(string dbName)
        {
            QueryResults qr = sqlServer.EnumProcesses(-1);

            // 获取SPID和DBNAME字段列序号
            int iColPIDNum = -1;
            int iColDbName = -1;
            for (int i = 1; i <= qr.Columns; i++)
            {
                string strName = qr.get_ColumnName(i);

                if (strName.ToUpper().Trim() == "SPID")
                    iColPIDNum = i;
                else if (strName.ToUpper().Trim() == "DBNAME")
                    iColDbName = i;

                if (iColPIDNum != -1 && iColDbName != -1)
                    break;
            }

            // 将指定数据库的连接全部断开
            for (int i = 1; i <= qr.Rows; i++)
            {
                int lPID = qr.GetColumnLong(i, iColPIDNum);
                string strDBName = qr.GetColumnString(i, iColDbName);

                if (string.Compare(strDBName, "test", true) == 0)
                    sqlServer.KillProcess(lPID);
            }
        }

        /// <summary>
        /// 获取数据库信息
        /// </summary>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public DatabaseInfo GetDatabaseInfo(string dbName)
        {
            DatabaseInfo info = new DatabaseInfo();
            Database db = GetDatabase(dbName);
            if (db == null)
            {
                return info;
                //throw new Exception("Database not exists!");
            }

            

            info.Name = db.Name;
            info.Owner = db.Owner;
            info.PrimaryFilePath = db.PrimaryFilePath;
            info.CreateDate = db.CreateDate;
            info.Size = db.Size;
            try
            {
                info.SpaceAvailable = db.SpaceAvailableInMB;
            }
            catch { }

            DBFile primary = db.FileGroups.Item("PRIMARY").DBFiles.Item(1);
            info.PrimaryName = primary.Name;
            info.PrimaryFilename = primary.PhysicalName.Trim();
            info.PrimarySize = primary.Size;
            info.PrimaryMaxSize = primary.MaximumSize;

            _LogFile log = db.TransactionLog.LogFiles.Item(1);
            info.LogName = log.Name;
            info.LogFilename = log.PhysicalName.Trim();
            info.LogSize = log.Size;
            info.LogMaxSize = log.MaximumSize;

            return info;
        }

        /// <summary>
        /// 分离数据库
        /// </summary>
        /// <param name="dbName"></param>
        /// <remarks>
        /// 分离前最好调用KillAllProcess关闭所有连接，否则分离可能失败。
        /// </remarks>
        public void DetachDB(string dbName)
        {
            sqlServer.DetachDB(dbName, true);
        }

        /// <summary>
        /// 附加数据库
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="dbFile"></param>
        /// <example>
        /// <code>
        /// SqlDmoHelper dmo = new SqlDmoHelper("(local)", "sa", "sa");
        /// dmo.AttachDB("test", @"d:\temp\database\test_data.mdf");
        /// </code>
        /// </example>
        public void AttachDB(string dbName, string dbFile)
        {
            sqlServer.AttachDB(dbName, dbFile);
        }

        /// <summary>
        /// 删除数据库(文件也将被删除)
        /// </summary>
        /// <param name="dbName"></param>
        public void KillDB(string dbName)
        {
            sqlServer.KillDatabase(dbName);
        }

        /// <summary>
        /// 创建数据库
        /// </summary>
        /// <param name="dbName">数据库名称</param>
        /// <param name="path">数据文件保存路径</param>
        /// <param name="primaryFilename">数据库文件名(不含路径)</param>
        /// <param name="logFilename">日志文件名(不含路径)</param>
        /// <example>
        /// <code>
        /// SqlDmoHelper dmo = new SqlDmoHelper("(local)", "sa", "sa");
        /// dmo.CreateDB("test1", @"d:\temp\database", "abc.mdf", "abc1.ldf");
        /// </code>
        /// </example>
        public void CreateDB(string dbName, string path, string primaryFilename, string logFilename)
        {
            // 创建数据库文件
            DBFile dbFile = new DBFileClass();
            dbFile.Name = dbName + "_Data";
            dbFile.PhysicalName = Path.Combine(path, primaryFilename);
            dbFile.PrimaryFile = true;
            //dbFile.Size = 2; // 设置初始化大小(MB)
            //dbFile.FileGrowthType = SQLDMO_GROWTH_TYPE.SQLDMOGrowth_MB; // 设置文件增长方式
            //dbFile.FileGrowth=1; // 设置增长幅度

            // 创建日志文件
            _LogFile logFile = new LogFileClass();
            logFile.Name = dbName + "_Log";
            logFile.PhysicalName = Path.Combine(path, logFilename);
            //logFile.Size = 3;
            //logFile.FileGrowthType=SQLDMO_GROWTH_TYPE.SQLDMOGrowth_MB;
            //logFile.FileGrowth=1;

            // 创建数据库
            Database db = new DatabaseClass();
            db.Name = dbName;
            db.FileGroups.Item("PRIMARY").DBFiles.Add(dbFile);
            db.TransactionLog.LogFiles.Add(logFile);

            // 建立数据库联接，并添加数据库到服务器
            sqlServer.Databases.Add(db);
        }

        /// <summary>
        /// 备份数据库
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="bakFile"></param>
        /// <param name="bakSetName"></param>
        /// <param name="bakDescription"></param>
        /// <example>
        /// <code>
        /// SqlDmoHelper dmo = new SqlDmoHelper("(local)", "sa", "sa");
        /// dmo.BackupDB("test", @"d:\temp\database\test.bak", "手动备份1", "备份说明...");
        /// </code>
        /// </example>
        public bool BackupDB(string dbName, string bakFile, string bakSetName, string bakDescription)
        {
            try
            {
                Backup oBackup = new BackupClass();
                oBackup.Action = SQLDMO_BACKUP_TYPE.SQLDMOBackup_Database;
                oBackup.Database = dbName;
                oBackup.Files = bakFile;
                oBackup.BackupSetName = bakSetName;
                //oBackup.BackupSetDescription = bakDescription;
                oBackup.Initialize = true;
                oBackup.SQLBackup(sqlServer);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 恢复数据库
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="bakFile"></param>
        /// <remarks>
        /// 恢复前最好调用KillAllProcess关闭所有连接，否则恢复可能失败。
        /// </remarks>
        /// <example>
        /// <code>
        /// SqlDmoHelper dmo = new SqlDmoHelper("(local)", "sa", "sa");
        /// dmo.RestoreDB("test", @"d:\temp\database\test.bak");
        /// </code>
        /// </example>
        public bool RestoreDB(string dbName, string bakFile)
        {
            try
            {
                Restore oRestore = new RestoreClass();
                oRestore.Action = SQLDMO_RESTORE_TYPE.SQLDMORestore_Database;
                oRestore.Database = dbName;
                oRestore.Files = bakFile;
                oRestore.FileNumber = 1;
                oRestore.ReplaceDatabase = true;
                oRestore.SQLRestore(sqlServer);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 收缩数据库
        /// </summary>
        /// <param name="dbName"></param>
        public void ShrinkDB(string dbName)
        {
            Database db = GetDatabase(dbName);
            if (db == null) throw new Exception("Database not exists!");

            db.Shrink(0, SQLDMO_SHRINK_TYPE.SQLDMOShrink_Default);
        }

        /// <summary>
        /// 获取所有的数据库名
        /// </summary>
        /// <returns></returns>
        public string[] ListAllDatabase()
        {
            ArrayList list = new ArrayList();
            foreach (Database d in sqlServer.Databases)
            {
                list.Add(d.Name);
            }

            if (list.Count == 0)
                return new string[0];
            else
                return (string[])list.ToArray(typeof(string));
        }

        /// <summary>
        /// 获取所有登录名
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// 管理工具 "安全性->登录"
        /// </remarks>
        public string[] ListAllLogins()
        {
            ArrayList list = new ArrayList();
            foreach (Login d in sqlServer.Logins)
            {
                list.Add(d.Name);
            }

            if (list.Count == 0)
                return new string[0];
            else
                return (string[])list.ToArray(typeof(string));
        }

        /// <summary>
        /// 获取全部数据表名称
        /// </summary>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public string[] ListAllTables(string dbName)
        {
            Database db = GetDatabase(dbName);
            if (db == null) throw new Exception("Database not exists!");

            ArrayList list = new ArrayList();
            foreach (Table t in db.Tables)
            {
                list.Add(t.Name);
            }

            if (list.Count == 0)
                return new string[0];
            else
                return (string[])list.ToArray(typeof(string));
        }

        /// <summary>
        /// 获取全部存储过程名称
        /// </summary>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public string[] ListAllStoredProcedure(string dbName)
        {
            Database db = GetDatabase(dbName);
            if (db == null) throw new Exception("Database not exists!");

            ArrayList list = new ArrayList();
            foreach (StoredProcedure sp in db.StoredProcedures)
            {
                list.Add(sp.Name);
            }

            if (list.Count == 0)
                return new string[0];
            else
                return (string[])list.ToArray(typeof(string));
        }

        /// <summary>
        /// 获取数据库对象
        /// </summary>
        /// <param name="dbName"></param>
        /// <returns></returns>
        /// <remarks>
        /// 可以通过数据库对象获取数据库内表、存储过程、触发器、数据类型等信息。
        /// </remarks>
        /// <example>
        /// 显示数据库中所有表及其结构
        /// <code>
        /// SqlDmoHelper dmo = new SqlDmoHelper("(local)", "sa", "sa");
        /// SQLDMO.Database db = dmo.GetDatabase("test");
        /// foreach(SQLDMO.Table t in db.Tables)
        /// {
        ///    Console.WriteLine("Table:{0}", t.Name);
        ///    for (int i = 1; i &lt;= t.Columns.Count; i++) // SQLDMO所有索引序号从1开始
        ///    {
        ///      SQLDMO._Column col = t.Columns.Item(i);
        ///      Console.WriteLine(" Column:{0} DataType:{1}", col.Name, col.Datatype);
        ///    }
        ///
        ///    Console.WriteLine("---------------");
        /// }
        /// </code>
        /// </example>
        public Database GetDatabase(string dbName)
        {
            if (sqlServer != null)
            {
                foreach (Database d in sqlServer.Databases)
                {
                    if (string.Compare(d.Name, dbName, true) == 0)
                        return d;
                }
            }

            return null;
        }

        //生成表脚本
        public string CreateTableSql(string dbName,object tableName)
        {
            string s=string.Empty ;
            try
            {
                SQLDMO._Database mydb = GetDatabase(dbName);
                SQLDMO._Table myTable = mydb.Tables.Item(tableName, "dbo");
                s = myTable.Script(SQLDMO.SQLDMO_SCRIPT_TYPE.SQLDMOScript_Default, null, null, SQLDMO.SQLDMO_SCRIPT2_TYPE.SQLDMOScript2_Default);
                s = s.Replace("[nvarchar] (0)", "[nvarchar] (max)");
                s = s.Replace("[varchar] (-1)", "[varchar] (max)");
                //去掉GO
                int lastPosNum = s.LastIndexOf("GO");
                if(lastPosNum>0)
                    s = s.Substring(0, lastPosNum);
            }
            catch
            {
            }
            return s;
        }

        //生成Insert语句，调用proc_insert 存储过程
        public string CreateInsertSql(string tableName)
        {
            StringBuilder inserSql = new StringBuilder ();
            SqlParameter[] prams = new SqlParameter[]{
                    new SqlParameter("tableName",tableName),
                };
            string connStr = "server=" + server + ";database=" + database + ";uid=" + login + ";pwd=" + password;
            DataSet ds = SQLHelper.ExecuteDataSet(connStr,
                      CommandType.StoredProcedure, "proc_insert", prams);
            DataTable dt;
            dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                inserSql.Append(dt.Rows[0][0].ToString());
                inserSql.Append("\r\n");
            }
            dt = ds.Tables[1];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                inserSql.Append(dt.Rows[i][0].ToString());
                inserSql.Append("\r\n");
            }
            dt = ds.Tables[2];
            if (dt.Rows.Count > 0)
            {
                inserSql.Append(dt.Rows[0][0].ToString());
                inserSql.Append("\r\n");
            }
            return inserSql.ToString();
        }
        #endregion
    }
}
