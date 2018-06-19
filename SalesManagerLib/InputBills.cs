using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagerLib
{
    public class InputBills
    {
        #region Private Properties
        private int _InputBillId;
        private int _SupplierId;
        private double _DebitBalance;
        private byte _WarehouseId;
        private byte _PaymentTypeId;
        private double _Pay;
        private double _Total;
        private string _Notes;
        private byte _BillStatusId;
        private DateTime _CrDateTime;
        private DBAccess db;

        #endregion

        #region Public Properties

        //-----------------------------------------------------------------
        public InputBills()
        {
            db = new DBAccess(LibConstants.CONNECTION_STRING);
        }
        //-----------------------------------------------------------------        
        public InputBills(string constr)
        {
            db = (constr.Length > 0) ? new DBAccess(constr) : new DBAccess();
        }
        //-----------------------------------------------------------------
        ~InputBills()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }

        //-----------------------------------------------------------------    
        public int InputBillId
        {
            get
            {
                return _InputBillId;
            }
            set
            {
                _InputBillId = value;
            }
        }
        public int SupplierId
        {
            get
            {
                return _SupplierId;
            }
            set
            {
                _SupplierId = value;
            }
        }
        public double DebitBalance
        {
            get
            {
                return _DebitBalance;
            }
            set
            {
                _DebitBalance = value;
            }
        }
        public byte WarehouseId
        {
            get
            {
                return _WarehouseId;
            }
            set
            {
                _WarehouseId = value;
            }
        }
        public byte PaymentTypeId
        {
            get
            {
                return _PaymentTypeId;
            }
            set
            {
                _PaymentTypeId = value;
            }
        }
        public double Pay
        {
            get
            {
                return _Pay;
            }
            set
            {
                _Pay = value;
            }
        }
        public double Total
        {
            get
            {
                return _Total;
            }
            set
            {
                _Total = value;
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
        private List<InputBills> Init(SqlCommand cmd)
        {
            SqlConnection con = db.GetConnection();
            cmd.Connection = con;
            List<InputBills> l_InputBills = new List<InputBills>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    InputBills m_InputBills = new InputBills();
                    m_InputBills.InputBillId = smartReader.GetInt32("InputBillId");
                    m_InputBills.SupplierId = smartReader.GetInt32("SupplierId");
                    m_InputBills.DebitBalance = smartReader.GetFloat("DebitBalance");
                    m_InputBills.WarehouseId = smartReader.GetByte("WarehouseId");
                    m_InputBills.PaymentTypeId = smartReader.GetByte("PaymentTypeId");
                    m_InputBills.Pay = smartReader.GetFloat("Pay");
                    m_InputBills.Total = smartReader.GetFloat("Total");
                    m_InputBills.Notes = smartReader.GetString("Notes");
                    m_InputBills.BillStatusId = smartReader.GetByte("BillStatusId");
                    m_InputBills.CrDateTime = smartReader.GetDateTime("CrDateTime");
                    l_InputBills.Add(m_InputBills);
                }
                reader.Close();
                return l_InputBills;
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
                SqlCommand cmd = new SqlCommand("InputBills_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SupplierId", this.SupplierId));
                cmd.Parameters.Add(new SqlParameter("@DebitBalance", this.DebitBalance));
                cmd.Parameters.Add(new SqlParameter("@WarehouseId", this.WarehouseId));
                cmd.Parameters.Add(new SqlParameter("@PaymentTypeId", this.PaymentTypeId));
                cmd.Parameters.Add(new SqlParameter("@Pay", this.Pay));
                cmd.Parameters.Add(new SqlParameter("@Total", this.Total));
                cmd.Parameters.Add(new SqlParameter("@Notes", this.Notes));
                cmd.Parameters.Add(new SqlParameter("@BillStatusId", this.BillStatusId));
                cmd.Parameters.Add(new SqlParameter("@InputBillId", this.InputBillId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.InputBillId = int.Parse(cmd.Parameters["@InputBillId"].Value.ToString());
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
                SqlCommand cmd = new SqlCommand("InputBills_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@InputBillId", this.InputBillId));
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
        public InputBills Get()
        {
            InputBills retVal = new InputBills();
            int RowCount = 0;
            string DateFrom = "";
            string DateTo = "";
            string OrderBy = "";
            int PageSize = 1;
            int PageNumber = 0;
            try
            {

                List<InputBills> list = GetPage(DateFrom, DateTo, OrderBy, PageSize, PageNumber, ref RowCount);
                if (list.Count > 0) retVal = list[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }


        //-------------------------------------------------------------- 

        public List<InputBills> GetPage(string DateFrom, string DateTo, string OrderBy, int PageSize, int PageNumber, ref int RowCount)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("InputBills_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@InputBillId", this.InputBillId));
                cmd.Parameters.Add(new SqlParameter("@SupplierId", this.SupplierId));
                cmd.Parameters.Add(new SqlParameter("@DebitBalance", this.DebitBalance));
                cmd.Parameters.Add(new SqlParameter("@WarehouseId", this.WarehouseId));
                cmd.Parameters.Add(new SqlParameter("@PaymentTypeId", this.PaymentTypeId));
                cmd.Parameters.Add(new SqlParameter("@Pay", this.Pay));
                cmd.Parameters.Add(new SqlParameter("@Total", this.Total));
                cmd.Parameters.Add(new SqlParameter("@Notes", this.Notes));
                cmd.Parameters.Add(new SqlParameter("@BillStatusId", this.BillStatusId));
                if (!string.IsNullOrEmpty(DateFrom)) cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                if (!string.IsNullOrEmpty(DateTo)) cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                cmd.Parameters.Add(new SqlParameter("@PageSize", PageSize));
                cmd.Parameters.Add(new SqlParameter("@PageNumber", PageNumber));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                List<InputBills> list = Init(cmd);
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
