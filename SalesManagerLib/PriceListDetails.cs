using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagerLib
{
    public class PriceListDetails
    {
        #region Private Properties
        private int _PriceListDetailId;
        private int _PriceListId;
        private int _ProductId;
        private string _ProductName;
        private byte _StatusId;
        private short _UnitId;
        private double _Price;
        private int _CrUserId;
        private int _UpdateUserId;
        private DateTime _CrDateTime;
        private DBAccess db;

        #endregion

        #region Public Properties

        //-----------------------------------------------------------------
        public PriceListDetails()
        {
            db = new DBAccess(LibConstants.CONNECTION_STRING);
        }
        //-----------------------------------------------------------------        
        public PriceListDetails(string constr)
        {
            db = (constr.Length > 0) ? new DBAccess(constr) : new DBAccess();
        }
        //-----------------------------------------------------------------
        ~PriceListDetails()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }

        //-----------------------------------------------------------------    
        public int PriceListDetailId
        {
            get
            {
                return _PriceListDetailId;
            }
            set
            {
                _PriceListDetailId = value;
            }
        }

        public int PriceListId
        {
            get
            {
                return _PriceListId;
            }
            set
            {
                _PriceListId = value;
            }
        }
        public int ProductId
        {
            get
            {
                return _ProductId;
            }
            set
            {
                _ProductId = value;
            }
        }

        public string ProductName
        {
            get
            {
                return _ProductName;
            }
            set
            {
                _ProductName = value;
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

        public short UnitId
        {
            get
            {
                return _UnitId;
            }
            set
            {
                _UnitId = value;
            }
        }
        public double Price
        {
            get
            {
                return _Price;
            }
            set
            {
                _Price = value;
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
        private List<PriceListDetails> Init(SqlCommand cmd)
        {
            SqlConnection con = db.GetConnection();
            cmd.Connection = con;
            List<PriceListDetails> l_PriceListDetails = new List<PriceListDetails>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    PriceListDetails m_PriceListDetails = new PriceListDetails();
                    m_PriceListDetails.PriceListDetailId = smartReader.GetInt32("PriceListDetailId");
                    m_PriceListDetails.PriceListId = smartReader.GetInt32("PriceListId");
                    m_PriceListDetails.ProductId = smartReader.GetInt32("ProductId");
                    m_PriceListDetails.ProductName = smartReader.GetString("ProductName");
                    m_PriceListDetails.UnitId = smartReader.GetInt16("UnitId");
                    m_PriceListDetails.Price = smartReader.GetFloat("Price");
                    m_PriceListDetails.StatusId = smartReader.GetByte("StatusId");
                    m_PriceListDetails.CrUserId = smartReader.GetInt32("CrUserId");
                    m_PriceListDetails.UpdateUserId = smartReader.GetInt32("UpdateUserId");
                    m_PriceListDetails.CrDateTime = smartReader.GetDateTime("CrDateTime");
                    l_PriceListDetails.Add(m_PriceListDetails);
                }
                reader.Close();
                return l_PriceListDetails;
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
                SqlCommand cmd = new SqlCommand("PriceListDetails_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@PriceListId", this.PriceListId));
                cmd.Parameters.Add(new SqlParameter("@ProductId", this.ProductId));
                cmd.Parameters.Add(new SqlParameter("@UnitId", this.UnitId));
                cmd.Parameters.Add(new SqlParameter("@Price", this.Price));
                cmd.Parameters.Add(new SqlParameter("@CrUserId", this.CrUserId));
                cmd.Parameters.Add(new SqlParameter("@UpdateUserId", this.UpdateUserId));
                cmd.Parameters.Add(new SqlParameter("@PriceListDetailId", this.PriceListDetailId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.PriceListDetailId = int.Parse(cmd.Parameters["@PriceListDetail"].Value.ToString());
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
                SqlCommand cmd = new SqlCommand("PriceListDetails_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@PriceListDetailId", this.PriceListDetailId));
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
        public PriceListDetails Get()
        {
            PriceListDetails retVal = new PriceListDetails();
            int RowCount = 0;
            string DateFrom = "";
            string DateTo = "";
            string OrderBy = "";
            int PageSize = 1;
            int PageNumber = 0;
            try
            {

                List<PriceListDetails> list = GetPage(DateFrom, DateTo, OrderBy, PageSize, PageNumber, ref RowCount);
                if (list.Count > 0) retVal = list[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }


        //-------------------------------------------------------------- 

        public List<PriceListDetails> GetPage(string DateFrom, string DateTo, string OrderBy, int PageSize, int PageNumber, ref int RowCount)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("PriceListDetails_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@PriceListDetailId", this.PriceListDetailId));
                cmd.Parameters.Add(new SqlParameter("@PriceListId", this.PriceListId));
                cmd.Parameters.Add(new SqlParameter("@ProductId", this.ProductId));
                cmd.Parameters.Add(new SqlParameter("@ProductName", this.ProductName));
                cmd.Parameters.Add(new SqlParameter("@StatusId", this.StatusId));
                cmd.Parameters.Add(new SqlParameter("@UnitId", this.UnitId));
                cmd.Parameters.Add(new SqlParameter("@Price", this.Price));
                cmd.Parameters.Add(new SqlParameter("@CrUserId", this.CrUserId));
                cmd.Parameters.Add(new SqlParameter("@UpdateUserId", this.UpdateUserId));
                if (!string.IsNullOrEmpty(DateFrom)) cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                if (!string.IsNullOrEmpty(DateTo)) cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                cmd.Parameters.Add(new SqlParameter("@PageSize", PageSize));
                cmd.Parameters.Add(new SqlParameter("@PageNumber", PageNumber));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                List<PriceListDetails> list = Init(cmd);
                RowCount = int.Parse(cmd.Parameters["@RowCount"].Value.ToString());
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //--------------------------------------------------------------

        public void Insert_Auto()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("PriceListDetails_Insert_Auto");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@PriceListId", this.PriceListId));
                cmd.Parameters.Add(new SqlParameter("@CrUserId", this.CrUserId));
                db.ExecuteSQL(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdatePrice()
        {
            try
            {
                SqlCommand sqlCommand = new SqlCommand("PriceListDetails_Update_Price") { CommandType = CommandType.StoredProcedure };
                sqlCommand.Parameters.Add(new SqlParameter("@Price", this.Price));
                sqlCommand.Parameters.Add(new SqlParameter("@PriceListDetailId", this.PriceListDetailId));
                this.db.ExecuteSQL(sqlCommand);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
