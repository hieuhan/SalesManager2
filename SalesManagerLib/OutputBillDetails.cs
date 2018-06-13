using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagerLib
{
    public class OutputBillDetails
    {
        #region Private Properties
        private int _OutputBillDetailInt;
        private int _OutputBillId;
        private int _ProductId;
        private string _ProductName;
        private int _Quantity;
        private double _Price;
        private string _Notes;
        private byte _BillStatusId;
        private DateTime _CrDateTime;
        private DBAccess db;

        #endregion

        #region Public Properties

        //-----------------------------------------------------------------
        public OutputBillDetails()
        {
            db = new DBAccess(LibConstants.CONNECTION_STRING);
        }
        //-----------------------------------------------------------------        
        public OutputBillDetails(string constr)
        {
            db = (constr.Length > 0) ? new DBAccess(constr) : new DBAccess();
        }
        //-----------------------------------------------------------------
        ~OutputBillDetails()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }

        //-----------------------------------------------------------------    
        public int OutputBillDetailInt
        {
            get
            {
                return _OutputBillDetailInt;
            }
            set
            {
                _OutputBillDetailInt = value;
            }
        }

        public int OutputBillId
        {
            get
            {
                return _OutputBillId;
            }
            set
            {
                _OutputBillId = value;
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
        public int Quantity
        {
            get
            {
                return _Quantity;
            }
            set
            {
                _Quantity = value;
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
        public string Notes
        {
            get
            {
                return _Notes;
            }
            set
            {
                _Notes = value;
            }
        }
        public byte BillStatusId
        {
            get
            {
                return _BillStatusId;
            }
            set
            {
                _BillStatusId = value;
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
        private List<OutputBillDetails> Init(SqlCommand cmd)
        {
            SqlConnection con = db.GetConnection();
            cmd.Connection = con;
            List<OutputBillDetails> l_OutputBillDetails = new List<OutputBillDetails>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    OutputBillDetails m_OutputBillDetails = new OutputBillDetails();
                    m_OutputBillDetails.OutputBillDetailInt = smartReader.GetInt32("OutputBillDetailInt");
                    m_OutputBillDetails.OutputBillId = smartReader.GetInt32("OutputBillId");
                    m_OutputBillDetails.ProductId = smartReader.GetInt32("ProductId");
                    m_OutputBillDetails.ProductName = smartReader.GetString("ProductName");
                    m_OutputBillDetails.Quantity = smartReader.GetInt32("Quantity");
                    m_OutputBillDetails.Price = smartReader.GetFloat("Price");
                    m_OutputBillDetails.Notes = smartReader.GetString("Notes");
                    m_OutputBillDetails.BillStatusId = smartReader.GetByte("BillStatusId");
                    m_OutputBillDetails.CrDateTime = smartReader.GetDateTime("CrDateTime");
                    l_OutputBillDetails.Add(m_OutputBillDetails);
                }
                reader.Close();
                return l_OutputBillDetails;
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
                SqlCommand cmd = new SqlCommand("OutputBillDetails_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@OutputBillId", this.OutputBillId));
                cmd.Parameters.Add(new SqlParameter("@ProductId", this.ProductId));
                cmd.Parameters.Add(new SqlParameter("@ProductName", this.ProductName));
                cmd.Parameters.Add(new SqlParameter("@Quantity", this.Quantity));
                cmd.Parameters.Add(new SqlParameter("@Price", this.Price));
                cmd.Parameters.Add(new SqlParameter("@Notes", this.Notes));
                cmd.Parameters.Add(new SqlParameter("@BillStatusId", this.BillStatusId));
                cmd.Parameters.Add(new SqlParameter("@OutputBillDetailInt", this.OutputBillDetailInt)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.OutputBillDetailInt = int.Parse(cmd.Parameters["@OutputBillDetailInt"].Value.ToString());
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
                SqlCommand cmd = new SqlCommand("OutputBillDetails_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@OutputBillDetailInt", this.OutputBillDetailInt));
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
        public OutputBillDetails Get()
        {
            OutputBillDetails retVal = new OutputBillDetails();
            int RowCount = 0;
            string DateFrom = "";
            string DateTo = "";
            string OrderBy = "";
            int PageSize = 1;
            int PageNumber = 0;
            try
            {

                List<OutputBillDetails> list = GetPage(DateFrom, DateTo, OrderBy, PageSize, PageNumber, ref RowCount);
                if (list.Count > 0) retVal = list[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }


        //-------------------------------------------------------------- 

        public List<OutputBillDetails> GetPage(string DateFrom, string DateTo, string OrderBy, int PageSize, int PageNumber, ref int RowCount)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("OutputBillDetails_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@OutputBillDetailInt", this.OutputBillDetailInt));
                cmd.Parameters.Add(new SqlParameter("@OutputBillId", this.OutputBillId));
                cmd.Parameters.Add(new SqlParameter("@ProductId", this.ProductId));
                cmd.Parameters.Add(new SqlParameter("@ProductName", this.ProductName));
                cmd.Parameters.Add(new SqlParameter("@Quantity", this.Quantity));
                cmd.Parameters.Add(new SqlParameter("@Price", this.Price));
                cmd.Parameters.Add(new SqlParameter("@Notes", this.Notes));
                cmd.Parameters.Add(new SqlParameter("@BillStatusId", this.BillStatusId));
                if (!string.IsNullOrEmpty(DateFrom)) cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                if (!string.IsNullOrEmpty(DateTo)) cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                cmd.Parameters.Add(new SqlParameter("@PageSize", PageSize));
                cmd.Parameters.Add(new SqlParameter("@PageNumber", PageNumber));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                List<OutputBillDetails> list = Init(cmd);
                RowCount = int.Parse(cmd.Parameters["@RowCount"].Value.ToString());
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //--------------------------------------------------------------

        #endregion
    }
}
