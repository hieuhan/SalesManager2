using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagerLib
{
    public class Stores
    {
        #region Private Properties
        private byte _StoreId;
        private string _StoreName;
        private string _StoreDesc;
        private string _Address;
        private string _Mobile;
        private string _Email;
        private string _Website;
        private DBAccess db;

        #endregion

        #region Public Properties

        //-----------------------------------------------------------------
        public Stores()
        {
            db = new DBAccess(LibConstants.CONNECTION_STRING);
        }
        //-----------------------------------------------------------------        
        public Stores(string constr)
        {
            db = (constr.Length > 0) ? new DBAccess(constr) : new DBAccess();
        }
        //-----------------------------------------------------------------
        ~Stores()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }

        //-----------------------------------------------------------------    
        public byte StoreId
        {
            get
            {
                return _StoreId;
            }
            set
            {
                _StoreId = value;
            }
        }

        public string StoreName
        {
            get
            {
                return _StoreName;
            }
            set
            {
                _StoreName = value;
            }
        }
        public string StoreDesc
        {
            get
            {
                return _StoreDesc;
            }
            set
            {
                _StoreDesc = value;
            }
        }
        public string Address
        {
            get
            {
                return _Address;
            }
            set
            {
                _Address = value;
            }
        }
        public string Mobile
        {
            get
            {
                return _Mobile;
            }
            set
            {
                _Mobile = value;
            }
        }
        public string Email
        {
            get
            {
                return _Email;
            }
            set
            {
                _Email = value;
            }
        }
        public string Website
        {
            get
            {
                return _Website;
            }
            set
            {
                _Website = value;
            }
        }



        #endregion
        //-----------------------------------------------------------
        #region Method
        private List<Stores> Init(SqlCommand cmd)
        {
            SqlConnection con = db.GetConnection();
            cmd.Connection = con;
            List<Stores> l_Stores = new List<Stores>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    Stores m_Stores = new Stores();
                    m_Stores.StoreId = smartReader.GetByte("StoreId");
                    m_Stores.StoreName = smartReader.GetString("StoreName");
                    m_Stores.StoreDesc = smartReader.GetString("StoreDesc");
                    m_Stores.Address = smartReader.GetString("Address");
                    m_Stores.Mobile = smartReader.GetString("Mobile");
                    m_Stores.Email = smartReader.GetString("Email");
                    m_Stores.Website = smartReader.GetString("Website");
                    l_Stores.Add(m_Stores);
                }
                reader.Close();
                return l_Stores;
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
        public byte Insert(ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                RetVal = InsertOrUpdate(ref SysMessageId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------
        public byte Update(ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                RetVal = InsertOrUpdate(ref SysMessageId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-----------------------------------------------------------
        public byte InsertOrUpdate(ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("Stores_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@StoreName", this.StoreName));
                cmd.Parameters.Add(new SqlParameter("@StoreDesc", this.StoreDesc));
                cmd.Parameters.Add(new SqlParameter("@Address", this.Address));
                cmd.Parameters.Add(new SqlParameter("@Mobile", this.Mobile));
                cmd.Parameters.Add(new SqlParameter("@Email", this.Email));
                cmd.Parameters.Add(new SqlParameter("@Website", this.Website));
                cmd.Parameters.Add(new SqlParameter("@StoreId", this.StoreId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.StoreId = byte.Parse(cmd.Parameters["@StoreId"].Value.ToString());
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
        public byte Delete(ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("Stores_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@StoreId", this.StoreId));
                cmd.Parameters.Add("@SysMessageId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                SysMessageId = short.Parse(cmd.Parameters["@SysMessageId"].Value.ToString());
                RetVal = Byte.Parse(cmd.Parameters["@SysMessageTypeId"].Value.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public Stores Get()
        {
            Stores retVal = new Stores();
            int RowCount = 0;
            string DateFrom = "";
            string DateTo = "";
            string OrderBy = "";
            int PageSize = 1;
            int PageNumber = 0;
            try
            {
                List<Stores> list = GetPage(DateFrom, DateTo, OrderBy, PageSize, PageNumber, ref RowCount);
                if (list.Count > 0) retVal = list[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }


        //-------------------------------------------------------------- 

        public List<Stores> GetPage(string DateFrom, string DateTo, string OrderBy, int PageSize, int PageNumber, ref int RowCount)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Stores_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@StoreId", this.StoreId));
                cmd.Parameters.Add(new SqlParameter("@StoreName", this.StoreName));
                cmd.Parameters.Add(new SqlParameter("@StoreDesc", this.StoreDesc));
                cmd.Parameters.Add(new SqlParameter("@Address", this.Address));
                cmd.Parameters.Add(new SqlParameter("@Mobile", this.Mobile));
                cmd.Parameters.Add(new SqlParameter("@Email", this.Email));
                cmd.Parameters.Add(new SqlParameter("@Website", this.Website));
                if (!string.IsNullOrEmpty(DateFrom)) cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                if (!string.IsNullOrEmpty(DateTo)) cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                cmd.Parameters.Add(new SqlParameter("@PageSize", PageSize));
                cmd.Parameters.Add(new SqlParameter("@PageNumber", PageNumber));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                List<Stores> list = Init(cmd);
                RowCount = int.Parse(cmd.Parameters["@RowCount"].Value.ToString());
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //--------------------------------------------------------------
        public static string Static_GetDisplayString(byte StoreId)
        {
            string RetVal = "";
            Stores m_Stores = new Stores();
            m_Stores.StoreId = StoreId;
            m_Stores = m_Stores.Get();
            RetVal = m_Stores.StoreName;
            return RetVal;
        }

        #endregion
    }
}
