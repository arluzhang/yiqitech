using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace AllPower.Common
{
    /// <summary>
    /// 分页实体类
    /// </summary>
    public class Pager
    {
        #region 参数定义
        protected int _recordCount = 0;		//记录数
        protected string _tableName = "";		//数据表名称
        protected string _sqlStr = "";		//数据表名称
        protected string _condition = "";		//查询条件
        protected int _pageIndex = 0;		//当前页索引
        protected int _pageSize = 0;			//每页大小
        protected string _pkField = "*";		//索引字段
        protected string _orderDesc = "";		//（降序）排序 如:order by id desc,publishdate desc
        protected string _orderAsc = "";		//（升序）排序 如:order by id asc,publishdate asc
        protected string _searField = "*";      //查询字段
        #endregion

        #region 属性
        /// <summary>
        /// Return 记录数
        /// </summary>
        public int RecordCount
        {
            get
            {
                return _recordCount;
            }
            set
            {
                if (_recordCount != value)
                    _recordCount = value;
            }
        }
        /// <summary>
        /// 设置页索引
        /// </summary>
        public int PageIndex
        {
            get
            {
                return _pageIndex;
            }
            set
            {
                if (_pageIndex != value)
                    _pageIndex = value;
            }
        }
        /// <summary>
        /// 设置每页显示记录数
        /// </summary>
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                if (_pageSize != value)
                    _pageSize = value;
            }
        }
        /// <summary>
        /// 设置表名称
        /// </summary>
        public string TableName
        {
            get
            {
                return _tableName;
            }
            set
            {
                if (_tableName != value)
                    _tableName = value;
            }
        }

        /// <summary>
        /// 设置需要执行的SQL语句
        /// </summary>
        public string SqlStr
        {
            get
            {
                return _sqlStr;
            }
            set
            {
                if (_sqlStr != value)
                    _sqlStr = value;
            }
        }

        /// <summary>
        /// 设置查询条件
        /// </summary>

        public string Condition
        {
            get
            {
                return _condition;
            }
            set
            {
                if (_condition != value)
                    _condition = value;
            }
        }

        /// <summary>
        /// 设置表索引字段
        /// </summary>

        public string PKField
        {
            get
            {
                return _pkField;
            }
            set
            {
                if (_pkField != value)
                    _pkField = value;
            }
        }

        /// <summary>
        /// 设置降序 如:order by id desc,publishdate desc
        /// </summary>
        public string OrderDesc
        {
            get
            {
                return _orderDesc;
            }
            set
            {
                if (_orderDesc != value)
                    _orderDesc = value;
            }
        }

        /// <summary>
        /// 设置升序 如:order by id asc,publishdate asc
        /// </summary>

        public string OrderAsc
        {
            get
            {
                return _orderAsc;
            }
            set
            {
                if (_orderAsc != value)
                    _orderAsc = value;
            }
        }

        /// <summary>
        /// 查询字段 字段中应包含排序的字段
        /// </summary>

        public string SearField
        {
            get { return _searField; }
            set { if (_searField != value)_searField = value; }
        }
        #endregion

        #region Method
        public Pager()
        {
        }
        #endregion

        public DataSet GetPageData()
        {
            SqlConnection cn = new SqlConnection(SQLHelper.ConnectionStringLocalTransaction);
            SqlCommand cm = new SqlCommand("PublicSplitPage_sp", cn);
            cm.CommandType = CommandType.StoredProcedure;
            cm.Connection.Open();
            cm.CommandTimeout = 500;
            SqlParameter[] param = new SqlParameter[]{
                new SqlParameter("@TableName",this.TableName),
                new SqlParameter("@SqlStr",this.SqlStr),
                new SqlParameter("@Condition",this.Condition),
                new SqlParameter("@PageIndex",this.PageIndex),
                new SqlParameter("@PageSize",this.PageSize),
                new SqlParameter("@PKField",this.PKField),
                new SqlParameter("@OrderDesc",this.OrderDesc),
                new SqlParameter("@OrderAsc",this.OrderAsc),
                new SqlParameter("@SearField",this.SearField),
                new SqlParameter("@RecordCount",SqlDbType.Int,4)
            };
            for (int i = 0; i < param.Length; i++)
            {
                cm.Parameters.Add(param[i]);
            }
            cm.Parameters["@RecordCount"].Direction = ParameterDirection.Output;
            cm.Parameters["@RecordCount"].Value = 0;

            DataSet ds = new DataSet();

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cm;

            try
            {
                if (this.SqlStr.Trim() != "")
                {
                    int StartRecord = (this.PageIndex - 1) * this.PageSize;
                    da.Fill(ds, StartRecord, PageSize, "tmpTable");
                }
                else
                    da.Fill(ds);
            }
            catch(Exception ex)
            { }

            _recordCount = Convert.ToInt32(cm.Parameters["@RecordCount"].Value);
            cm.Dispose();
            cn.Close();
            return ds;
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="tblName">表名</param>
        /// <param name="strGetFields">字段</param>
        /// <param name="strOrder">排序</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="pageIndex">页次</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="recordCount">总共记录条数</param>
        /// <param name="doCount">是否统计记录总数0为不统计，非0为统计</param>
        /// <returns></returns>
        public DataSet GetPageList(string tblName, string strGetFields, string strOrder, string strWhere, int pageIndex, int pageSize, out int recordCount, int doCount)
        {
            /*
            @tblName   varchar(255),            -- 表名如：'xtest'
            @strGetFields varchar(1000) = '*',  -- 需要返回的列如：'xname,xdemo'
            @strOrder varchar(255)='',          -- 排序的字段名如：'order by id desc'
            @strWhere  varchar(1500) = '',      -- 查询条件(注意:不要加where)如:'xname like ''%222name%''' 
            @pageIndex  int = 1,                -- 页码如：2
            @pageSize   int = 20,               -- 每页记录数如：20
            @recordCount int output,            -- 记录总数
            @doCount bit=0                      -- 非0则统计,为0则不统计(统计会影响效率)
             */
            SqlParameter ReInfo = new SqlParameter("@recordCount", SqlDbType.Int, 4);
            ReInfo.Value = 0;
            ReInfo.Direction = ParameterDirection.Output;

            SqlParameter[] parameters = 
            {
                new SqlParameter("@tblName", tblName),
                new SqlParameter("@strGetFields", strGetFields),
                new SqlParameter("@strOrder", strOrder),
                new SqlParameter("@strWhere", strWhere),
                new SqlParameter("@pageIndex", pageIndex),
                new SqlParameter("@pageSize", pageSize),
                new SqlParameter("@doCount", doCount),
                ReInfo
            };

            DataSet ds = SQLHelper.ExecuteDataSet(SQLHelper.ConnectionStringLocalTransaction,
                CommandType.StoredProcedure,
                "KingTopShop_Pager2005",
                parameters);


            if (ds != null)
            {
                recordCount = int.Parse(ReInfo.Value.ToString());
            }
            else
            {
                recordCount = 0;
            }
            return ds;
        }



    }
}
