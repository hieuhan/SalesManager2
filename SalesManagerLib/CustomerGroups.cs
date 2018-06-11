using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagerLib
{
    public class CustomerGroups
    {
        private short _CustomerGroupId;
        private string _CustomerGroupName;
        private string _CustomerGroupDesc;
        private int _CrUserId;
        private DateTime _CrDateTime;
        private DBAccess db;
        //-----------------------------------------------------------------
        public CustomerGroups()
        {
            db = new DBAccess(LibConstants.CONNECTION_STRING);
        }
        //-----------------------------------------------------------------        
        public CustomerGroups(string constr)
        {
            db = new DBAccess((constr == "") ? LibConstants.CONNECTION_STRING : constr);
        }
        //-----------------------------------------------------------------
        ~CustomerGroups()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public short CustomerGroupId
        {
            get { return _CustomerGroupId; }
            set { _CustomerGroupId = value; }
        }
        //-----------------------------------------------------------------
        public string CustomerGroupName
        {
            get { return _CustomerGroupName; }
            set { _CustomerGroupName = value; }
        }
        //-----------------------------------------------------------------
        public string CustomerGroupDesc
        {
            get { return _CustomerGroupDesc; }
            set { _CustomerGroupDesc = value; }
        }
        //-----------------------------------------------------------------
        public int CrUserId
        {
            get { return _CrUserId; }
            set { _CrUserId = value; }
        }
        //-----------------------------------------------------------------
        public DateTime CrDateTime
        {
            get { return _CrDateTime; }
            set { _CrDateTime = value; }
        }
        //-----------------------------------------------------------------

        private List<CustomerGroups> Init(SqlCommand cmd)
        {
            SqlConnection con = db.GetConnection();
            cmd.Connection = con;
            List<CustomerGroups> l_CustomerGroups = new List<CustomerGroups>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    CustomerGroups m_CustomerGroups = new CustomerGroups(db.ConnectionString);
                    m_CustomerGroups.CustomerGroupId = smartReader.GetInt16("CustomerGroupId");
                    m_CustomerGroups.CustomerGroupName = smartReader.GetString("CustomerGroupName");
                    m_CustomerGroups.CustomerGroupDesc = smartReader.GetString("CustomerGroupDesc");
                    m_CustomerGroups.CrUserId = smartReader.GetInt32("CrUserId");
                    m_CustomerGroups.CrDateTime = smartReader.GetDateTime("CrDateTime");

                    l_CustomerGroups.Add(m_CustomerGroups);
                }
                reader.Close();
                return l_CustomerGroups;
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
            finally
            {
                db.closeConnection(con);
            }
        }
        //-----------------------------------------------------------
        public byte Insert(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("CustomerGroups_Insert");
                cmd.CommandType = CommandType.StoredProcedure; ;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@CustomerGroupName", this.CustomerGroupName));
                cmd.Parameters.Add(new SqlParameter("@CustomerGroupDesc", this.CustomerGroupDesc));
                cmd.Parameters.Add("@CustomerGroupId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.CustomerGroupId = Convert.ToInt16((cmd.Parameters["@CustomerGroupId"].Value == null) ? 0 : (cmd.Parameters["@CustomerGroupId"].Value));
                SysMessageId = Convert.ToInt16((cmd.Parameters["@SysMessageId"].Value == null) ? "0" : cmd.Parameters["@SysMessageId"].Value);
                RetVal = Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value == null) ? "0" : cmd.Parameters["@SysMessageTypeId"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------
        public byte Update(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("CustomerGroups_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@CustomerGroupName", this.CustomerGroupName));
                cmd.Parameters.Add(new SqlParameter("@CustomerGroupDesc", this.CustomerGroupDesc));
                cmd.Parameters.Add(new SqlParameter("@CustomerGroupId", this.CustomerGroupId));
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                SysMessageId = Convert.ToInt16((cmd.Parameters["@SysMessageId"].Value == null) ? "0" : cmd.Parameters["@SysMessageId"].Value);
                RetVal = Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value == null) ? "0" : cmd.Parameters["@SysMessageTypeId"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-----------------------------------------------------------
        public byte InsertOrUpdate(byte Replicated, int ActUserId, ref int SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("CustomerGroups_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@CustomerGroupName", this.CustomerGroupName));
                cmd.Parameters.Add(new SqlParameter("@CustomerGroupDesc", this.CustomerGroupDesc));
                cmd.Parameters.Add(new SqlParameter("@CustomerGroupId", this.CustomerGroupId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.CustomerGroupId = short.Parse(cmd.Parameters["@CustomerGroupId"].Value.ToString());
                SysMessageId = Convert.ToInt32((cmd.Parameters["@SysMessageId"].Value == null) ? "0" : cmd.Parameters["@SysMessageId"].Value);
                RetVal = Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value == null) ? "0" : cmd.Parameters["@SysMessageTypeId"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------            
        public byte Delete(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("CustomerGroups_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@CustomerGroupId", this.CustomerGroupId));
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                SysMessageId = Convert.ToInt16((cmd.Parameters["@SysMessageId"].Value == null) ? "0" : cmd.Parameters["@SysMessageId"].Value);
                RetVal = Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value == null) ? "0" : cmd.Parameters["@SysMessageTypeId"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public CustomerGroups Get()
        {
            CustomerGroups retVal = new CustomerGroups();
            int RowCount = 0;
            string DateFrom = "";
            string DateTo = "";
            string OrderBy = "";
            int PageSize = 1;
            int PageNumber = 0;
            try
            {

                List<CustomerGroups> list = GetPage(DateFrom, DateTo, OrderBy, PageSize, PageNumber, ref RowCount);
                if (list.Count > 0) retVal = list[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }
        //-------------------------------------------------------------- 

        public List<CustomerGroups> GetPage(string DateFrom, string DateTo, string OrderBy, int PageSize, int PageNumber, ref int RowCount)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("CustomerGroups_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@CustomerGroupId", this.CustomerGroupId));
                cmd.Parameters.Add(new SqlParameter("@CustomerGroupName", this.CustomerGroupName));
                cmd.Parameters.Add(new SqlParameter("@CustomerGroupDesc", this.CustomerGroupDesc));
                cmd.Parameters.Add(new SqlParameter("@CrUserId", this.CrUserId));
                cmd.Parameters.Add(new SqlParameter("@DateFrom", DateFrom));
                cmd.Parameters.Add(new SqlParameter("@DateTo", DateTo));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                cmd.Parameters.Add(new SqlParameter("@PageSize", PageSize));
                cmd.Parameters.Add(new SqlParameter("@PageNumber", PageNumber));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                List<CustomerGroups> list = Init(cmd);
                RowCount = int.Parse(cmd.Parameters["@RowCount"].Value.ToString());
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //-------------------------------------------------------------- 
        public List<CustomerGroups> GetList()
        {
            List<CustomerGroups> RetVal = new List<CustomerGroups>();
            try
            {
                string sql = "SELECT * FROM CustomerGroups";
                SqlCommand cmd = new SqlCommand(sql);
                RetVal = Init(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------
        public static List<CustomerGroups> Static_GetList(string ConStr)
        {
            List<CustomerGroups> RetVal = new List<CustomerGroups>();
            try
            {
                CustomerGroups m_CustomerGroups = new CustomerGroups(ConStr);
                RetVal = m_CustomerGroups.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<CustomerGroups> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------   
        public static List<CustomerGroups> Static_GetListAll(string ConStr, string TextOptionAll = "...")
        {
            CustomerGroups m_CustomerGroups = new CustomerGroups(ConStr);
            List<CustomerGroups> RetVal = m_CustomerGroups.GetList();
            TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
            if (TextOptionAll.Length > 0)
            {
                m_CustomerGroups.CustomerGroupName = TextOptionAll;
                m_CustomerGroups.CustomerGroupDesc = TextOptionAll;
                RetVal.Insert(0, m_CustomerGroups);
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<CustomerGroups> Static_GetListAll(string TextOptionAll = "...")
        {
            return Static_GetListAll("", TextOptionAll);
        }
        //--------------------------------------------------------------    
        public List<CustomerGroups> GetListOrderBy(string OrderBy)
        {
            List<CustomerGroups> RetVal = new List<CustomerGroups>();
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                string sql = "SELECT * FROM CustomerGroups ";
                if (OrderBy.Length > 0)
                {
                    sql += "ORDER BY " + OrderBy;
                }
                SqlCommand cmd = new SqlCommand(sql);
                RetVal = Init(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<CustomerGroups> Static_GetListOrderBy(string ConStr, string OrderBy)
        {
            CustomerGroups m_CustomerGroups = new CustomerGroups(ConStr);
            return m_CustomerGroups.GetListOrderBy(OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<CustomerGroups> Static_GetListOrderBy(string OrderBy)
        {
            return Static_GetListOrderBy("", OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<CustomerGroups> Static_GetListAllOrderBy(string ConStr, string OrderBy, string TextOptionAll = "...")
        {
            List<CustomerGroups> RetVal = new List<CustomerGroups>();
            CustomerGroups m_CustomerGroups = new CustomerGroups(ConStr);
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                if (OrderBy.Length > 0)
                {
                    RetVal = m_CustomerGroups.GetListOrderBy(OrderBy);
                }
                TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
                if (TextOptionAll.Length > 0)
                {
                    m_CustomerGroups.CustomerGroupName = TextOptionAll;
                    m_CustomerGroups.CustomerGroupDesc = TextOptionAll;
                    RetVal.Insert(0, m_CustomerGroups);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<CustomerGroups> Static_GetListAllOrderBy(string OrderBy, string TextOptionAll = "...")
        {
            return Static_GetListAllOrderBy("", OrderBy, TextOptionAll);
        }
        //--------------------------------------------------------------  
        public List<CustomerGroups> GetListByCustomerGroupId(short CustomerGroupId)
        {
            List<CustomerGroups> RetVal = new List<CustomerGroups>();
            try
            {
                if (CustomerGroupId > 0)
                {
                    string sql = "SELECT * FROM CustomerGroups WHERE (CustomerGroupId=" + CustomerGroupId.ToString() + ")";
                    SqlCommand cmd = new SqlCommand(sql);
                    RetVal = Init(cmd);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }

        //-------------------------------------------------------------- 
        public CustomerGroups Get(short CustomerGroupId)
        {
            CustomerGroups RetVal = new CustomerGroups(db.ConnectionString);
            try
            {
                List<CustomerGroups> list = GetListByCustomerGroupId(CustomerGroupId);
                if (list.Count > 0)
                {
                    RetVal = (CustomerGroups)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        //public CustomerGroups Get()
        //{
        //    return Get(this.CustomerGroupId);
        //}
        //-------------------------------------------------------------- 
        public static CustomerGroups Static_Get(short CustomerGroupId)
        {
            return new CustomerGroups().Get(CustomerGroupId);
        }
        //-----------------------------------------------------------------------------
        public static CustomerGroups Static_Get(short CustomerGroupId, List<CustomerGroups> lList)
        {
            CustomerGroups RetVal = new CustomerGroups();
            if (CustomerGroupId > 0 && lList.Count > 0)
            {
                RetVal = lList.Find(i => i.CustomerGroupId == CustomerGroupId);
                if (RetVal == null) RetVal = new CustomerGroups();
            }
            return RetVal;
        }
        //--------------------------------------------------------------
    }
}
