using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace AllPower.Common
{
    /// <summary>
    /// ��ҳʵ����
    /// </summary>
    public class Pager
    {
        #region ��������
        protected int _recordCount = 0;		//��¼��
        protected string _tableName = "";		//���ݱ�����
        protected string _sqlStr = "";		//���ݱ�����
        protected string _condition = "";		//��ѯ����
        protected int _pageIndex = 0;		//��ǰҳ����
        protected int _pageSize = 0;			//ÿҳ��С
        protected string _pkField = "*";		//�����ֶ�
        protected string _orderDesc = "";		//���������� ��:order by id desc,publishdate desc
        protected string _orderAsc = "";		//���������� ��:order by id asc,publishdate asc
        protected string _searField = "*";      //��ѯ�ֶ�
        #endregion

        #region ����
        /// <summary>
        /// Return ��¼��
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
        /// ����ҳ����
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
        /// ����ÿҳ��ʾ��¼��
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
        /// ���ñ�����
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
        /// ������Ҫִ�е�SQL���
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
        /// ���ò�ѯ����
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
        /// ���ñ������ֶ�
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
        /// ���ý��� ��:order by id desc,publishdate desc
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
        /// �������� ��:order by id asc,publishdate asc
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
        /// ��ѯ�ֶ� �ֶ���Ӧ����������ֶ�
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
        /// ��ҳ��ȡ�����б�
        /// </summary>
        /// <param name="tblName">����</param>
        /// <param name="strGetFields">�ֶ�</param>
        /// <param name="strOrder">����</param>
        /// <param name="strWhere">��ѯ����</param>
        /// <param name="pageIndex">ҳ��</param>
        /// <param name="pageSize">ÿҳ��¼����</param>
        /// <param name="recordCount">�ܹ���¼����</param>
        /// <param name="doCount">�Ƿ�ͳ�Ƽ�¼����0Ϊ��ͳ�ƣ���0Ϊͳ��</param>
        /// <returns></returns>
        public DataSet GetPageList(string tblName, string strGetFields, string strOrder, string strWhere, int pageIndex, int pageSize, out int recordCount, int doCount)
        {
            /*
            @tblName   varchar(255),            -- �����磺'xtest'
            @strGetFields varchar(1000) = '*',  -- ��Ҫ���ص����磺'xname,xdemo'
            @strOrder varchar(255)='',          -- ������ֶ����磺'order by id desc'
            @strWhere  varchar(1500) = '',      -- ��ѯ����(ע��:��Ҫ��where)��:'xname like ''%222name%''' 
            @pageIndex  int = 1,                -- ҳ���磺2
            @pageSize   int = 20,               -- ÿҳ��¼���磺20
            @recordCount int output,            -- ��¼����
            @doCount bit=0                      -- ��0��ͳ��,Ϊ0��ͳ��(ͳ�ƻ�Ӱ��Ч��)
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
