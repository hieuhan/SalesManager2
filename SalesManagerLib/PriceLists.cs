using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagerLib
{
    public class PriceLists
    {
        #region Private Properties
        private int _PriceList;
        private string _PriceListName;
        private string _PriceListDesc;
        private byte _PriceListTypeId;
        private byte _IsDetail;
        private byte _StatusId;
        private int _DisplayOrder;
        private int _CrUserId;
        private int _UpdateUserId;
        private DateTime _CrDateTime;
        private DBAccess db;

        #endregion

        #region Public Properties

        //-----------------------------------------------------------------
        public PriceLists()
        {
            db = new DBAccess(LibConstants.CONNECTION_STRING);
        }
        //-----------------------------------------------------------------        
        public PriceLists(string constr)
        {
            db = (constr.Length > 0) ? new DBAccess(constr) : new DBAccess();
        }
        //-----------------------------------------------------------------
        ~PriceLists()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }

        //-----------------------------------------------------------------    
        public int PriceList
        {
            get
            {
                return _PriceList;
            }
            set
            {
                _PriceList = value;
            }
        }

        public string PriceListName
        {
            get
            {
                return _PriceListName;
            }
            set
            {
                _PriceListName = value;
            }
        }
        public string PriceListDesc
        {
            get
            {
                return _PriceListDesc;
            }
            set
            {
                _PriceListDesc = value;
            }
        }
        public byte PriceListTypeId
        {
            get
            {
                return _PriceListTypeId;
            }
            set
            {
                _PriceListTypeId = value;
            }
        }
        public byte IsDetail
        {
            get
            {
                return _IsDetail;
            }
            set
            {
                _IsDetail = value;
            }
        }
        public byte StatusId
        {
            get
            {
                return _StatusId;
            }
            set
            {
                _StatusId = value;
            }
        }
        public int DisplayOrder
        {
            get
            {
                return _DisplayOrder;
            }
            set
            {
                _DisplayOrder = value;
            }
        }
        public int CrUserId
        {
            get
            {
                return _CrUserId;
            }
            set
            {
                _CrUserId = value;
            }
        }
        public int UpdateUserId
        {
            get
            {
                return _UpdateUserId;
            }
            set
            {
                _UpdateUserId = value;
            }
        }
        public DateTime CrDateTime
        {
            get
            {
                return _CrDateTime;
            }
            set
            {
                _CrDateTime = value;
            }
        }



        #endregion
        //-----------------------------------------------------------
        #region Method
        private List<PriceLists> Init(SqlCommand cmd)
        {
            SqlConnection con = db.GetConnection();
            cmd.Connection = con;
            List<PriceLists> l_PriceLists = new List<PriceLists>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    PriceLists m_PriceLists = new PriceLists();
                    m_PriceLists.PriceList = smartReader.GetInt32("PriceList");
                    m_PriceLists.PriceListName = smartReader.GetString("PriceListName");
                    m_PriceLists.PriceListDesc = smartReader.GetString("PriceListDesc");
                    m_PriceLists.PriceListTypeId = smartReader.GetByte("PriceListTypeId");
                    m_PriceLists.IsDetail = smartReader.GetByte("IsDetail");
                    m_PriceLists.StatusId = smartReader.GetByte("StatusId");
                    m_PriceLists.DisplayOrder = smartReader.GetInt32("DisplayOrder");
                    m_PriceLists.CrUserId = smartReader.GetInt32("CrUserId");
                    m_PriceLists.UpdateUserId = smartReader.GetInt32("UpdateUserId");
                    m_PriceLists.CrDateTime = smartReader.GetDateTime("CrDateTime");
                    l_PriceLists.Add(m_PriceLists);
                }
                reader.Close();
                return l_PriceLists;
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
                SqlCommand cmd = new SqlCommand("PriceLists_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@PriceListName", this.PriceListName));
                cmd.Parameters.Add(new SqlParameter("@PriceListDesc", this.PriceListDesc));
                cmd.Parameters.Add(new SqlParameter("@PriceListTypeId", this.PriceListTypeId));
                cmd.Parameters.Add(new SqlParameter("@IsDetail", this.IsDetail));
                cmd.Parameters.Add(new SqlParameter("@StatusId", this.StatusId));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add(new SqlParameter("@CrUserId", this.CrUserId));
                cmd.Parameters.Add(new SqlParameter("@UpdateUserId", this.UpdateUserId));
                cmd.Parameters.Add(new SqlParameter("@PriceList", this.PriceList)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.PriceList = int.Parse(cmd.Parameters["@PriceList"].Value.ToString());
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
                SqlCommand cmd = new SqlCommand("PriceLists_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@PriceList", this.PriceList));
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
        public PriceLists Get()
        {
            PriceLists retVal = new PriceLists();
            int RowCount = 0;
            string DateFrom = "";
            string DateTo = "";
            string OrderBy = "";
            int PageSize = 1;
            int PageNumber = 0;
            try
            {

                List<PriceLists> list = GetPage(DateFrom, DateTo, OrderBy, PageSize, PageNumber, ref RowCount);
                if (list.Count > 0) retVal = list[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }


        //-------------------------------------------------------------- 

        public List<PriceLists> GetPage(string DateFrom, string DateTo, string OrderBy, int PageSize, int PageNumber, ref int RowCount)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("PriceLists_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@PriceList", this.PriceList));
                cmd.Parameters.Add(new SqlParameter("@PriceListName", this.PriceListName));
                cmd.Parameters.Add(new SqlParameter("@PriceListDesc", this.PriceListDesc));
                cmd.Parameters.Add(new SqlParameter("@PriceListTypeId", this.PriceListTypeId));
                cmd.Parameters.Add(new SqlParameter("@IsDetail", this.IsDetail));
                cmd.Parameters.Add(new SqlParameter("@StatusId", this.StatusId));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add(new SqlParameter("@CrUserId", this.CrUserId));
                cmd.Parameters.Add(new SqlParameter("@UpdateUserId", this.UpdateUserId));
                if (!string.IsNullOrEmpty(DateFrom)) cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                if (!string.IsNullOrEmpty(DateTo)) cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                cmd.Parameters.Add(new SqlParameter("@PageSize", PageSize));
                cmd.Parameters.Add(new SqlParameter("@PageNumber", PageNumber));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                List<PriceLists> list = Init(cmd);
                RowCount = int.Parse(cmd.Parameters["@RowCount"].Value.ToString());
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //--------------------------------------------------------------
        public static string Static_GetDisplayString(int PriceList)
        {
            string RetVal = "";
            PriceLists m_PriceLists = new PriceLists();
            m_PriceLists.PriceList = PriceList;
            m_PriceLists = m_PriceLists.Get();
            RetVal = m_PriceLists.PriceListName;
            return RetVal;
        }

        #endregion
    }
}
