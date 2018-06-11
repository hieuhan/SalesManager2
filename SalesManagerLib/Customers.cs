using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagerLib
{
    public class Customers
    {
        #region Private Properties
        private int _CustomerId;
        private string _FullName;
        private string _Mobile;
        private string _Email;
        private string _Address;
        private byte _GenderId;
        private short _CustomerGroupId;
        private string _Note;
        private byte _StatusId;
        private double _DebitBalance;
        private double _PaymentLimit;
        private DateTime _DateOfBirth;
        private DateTime _LastTradingDay;
        private int _DisplayOrder;
        private DateTime _CrDateTime;
        private DBAccess db;

        #endregion

        #region Public Properties

        //-----------------------------------------------------------------
        public Customers()
        {
            db = new DBAccess(LibConstants.CONNECTION_STRING);
        }
        //-----------------------------------------------------------------        
        public Customers(string constr)
        {
            db = (constr.Length > 0) ? new DBAccess(constr) : new DBAccess();
        }
        //-----------------------------------------------------------------
        ~Customers()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }

        //-----------------------------------------------------------------    
        public int CustomerId
        {
            get
            {
                return _CustomerId;
            }
            set
            {
                _CustomerId = value;
            }
        }

        public string FullName
        {
            get
            {
                return _FullName;
            }
            set
            {
                _FullName = value;
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
        public byte GenderId
        {
            get
            {
                return _GenderId;
            }
            set
            {
                _GenderId = value;
            }
        }
        public short CustomerGroupId
        {
            get
            {
                return _CustomerGroupId;
            }
            set
            {
                _CustomerGroupId = value;
            }
        }
        public string Note
        {
            get
            {
                return _Note;
            }
            set
            {
                _Note = value;
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
        public double PaymentLimit
        {
            get
            {
                return _PaymentLimit;
            }
            set
            {
                _PaymentLimit = value;
            }
        }
        public DateTime DateOfBirth
        {
            get
            {
                return _DateOfBirth;
            }
            set
            {
                _DateOfBirth = value;
            }
        }
        public DateTime LastTradingDay
        {
            get
            {
                return _LastTradingDay;
            }
            set
            {
                _LastTradingDay = value;
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
        private List<Customers> Init(SqlCommand cmd)
        {
            SqlConnection con = db.GetConnection();
            cmd.Connection = con;
            List<Customers> l_Customers = new List<Customers>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    Customers m_Customers = new Customers();
                    m_Customers.CustomerId = smartReader.GetInt32("CustomerId");
                    m_Customers.FullName = smartReader.GetString("FullName");
                    m_Customers.Mobile = smartReader.GetString("Mobile");
                    m_Customers.Email = smartReader.GetString("Email");
                    m_Customers.Address = smartReader.GetString("Address");
                    m_Customers.GenderId = smartReader.GetByte("GenderId");
                    m_Customers.CustomerGroupId = smartReader.GetInt16("CustomerGroupId");
                    m_Customers.Note = smartReader.GetString("Note");
                    m_Customers.StatusId = smartReader.GetByte("StatusId");
                    m_Customers.DebitBalance = smartReader.GetFloat("DebitBalance");
                    m_Customers.PaymentLimit = smartReader.GetFloat("PaymentLimit");
                    m_Customers.DateOfBirth = smartReader.GetDateTime("DateOfBirth");
                    m_Customers.LastTradingDay = smartReader.GetDateTime("LastTradingDay");
                    m_Customers.DisplayOrder = smartReader.GetInt32("DisplayOrder");
                    m_Customers.CrDateTime = smartReader.GetDateTime("CrDateTime");
                    l_Customers.Add(m_Customers);
                }
                reader.Close();
                return l_Customers;
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
                RetVal = InsertOrUpdate(Replicated, ActUserId, ref SysMessageId);
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
                RetVal = InsertOrUpdate(Replicated, ActUserId, ref SysMessageId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-----------------------------------------------------------
        public byte InsertOrUpdate(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("Customers_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                //cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@FullName", this.FullName));
                cmd.Parameters.Add(new SqlParameter("@Mobile", this.Mobile));
                cmd.Parameters.Add(new SqlParameter("@Email", this.Email));
                cmd.Parameters.Add(new SqlParameter("@Address", this.Address));
                cmd.Parameters.Add(new SqlParameter("@GenderId", this.GenderId));
                cmd.Parameters.Add(new SqlParameter("@CustomerGroupId", this.CustomerGroupId));
                cmd.Parameters.Add(new SqlParameter("@Note", this.Note));
                cmd.Parameters.Add(new SqlParameter("@StatusId", this.StatusId));
                cmd.Parameters.Add(new SqlParameter("@DebitBalance", this.DebitBalance));
                cmd.Parameters.Add(new SqlParameter("@PaymentLimit", this.PaymentLimit));
                cmd.Parameters.Add(DateOfBirth == DateTime.MinValue
                    ? new SqlParameter("@DateOfBirth", DBNull.Value)
                    : new SqlParameter("@DateOfBirth", this.DateOfBirth));
                cmd.Parameters.Add(LastTradingDay == DateTime.MinValue
                    ? new SqlParameter("@LastTradingDay", DBNull.Value)
                    : new SqlParameter("@LastTradingDay", this.LastTradingDay));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", this.CustomerId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.CustomerId = int.Parse(cmd.Parameters["@CustomerId"].Value.ToString());
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
        public byte Delete(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("Customers_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                //cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", this.CustomerId));
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
        public Customers Get()
        {
            Customers retVal = new Customers();
            int RowCount = 0;
            string DateFrom = "";
            string DateTo = "";
            string OrderBy = "";
            int PageSize = 1;
            int PageNumber = 0;
            try
            {

                List<Customers> list = GetPage(DateFrom, DateTo, OrderBy, PageSize, PageNumber, ref RowCount);
                if (list.Count > 0) retVal = list[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }


        //-------------------------------------------------------------- 

        public List<Customers> GetPage(string DateFrom, string DateTo, string OrderBy, int PageSize, int PageNumber, ref int RowCount)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Customers_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@CustomerId", this.CustomerId));
                cmd.Parameters.Add(new SqlParameter("@FullName", this.FullName));
                cmd.Parameters.Add(new SqlParameter("@Mobile", this.Mobile));
                cmd.Parameters.Add(new SqlParameter("@Email", this.Email));
                cmd.Parameters.Add(new SqlParameter("@Address", this.Address));
                cmd.Parameters.Add(new SqlParameter("@GenderId", this.GenderId));
                cmd.Parameters.Add(new SqlParameter("@CustomerGroupId", this.CustomerGroupId));
                cmd.Parameters.Add(new SqlParameter("@Note", this.Note));
                cmd.Parameters.Add(new SqlParameter("@StatusId", this.StatusId));
                cmd.Parameters.Add(new SqlParameter("@DebitBalance", this.DebitBalance));
                cmd.Parameters.Add(new SqlParameter("@PaymentLimit", this.PaymentLimit));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add(new SqlParameter("@DateFrom", DateFrom));
                cmd.Parameters.Add(new SqlParameter("@DateTo", DateTo));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                cmd.Parameters.Add(new SqlParameter("@PageSize", PageSize));
                cmd.Parameters.Add(new SqlParameter("@PageNumber", PageNumber));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                List<Customers> list = Init(cmd);
                RowCount = int.Parse(cmd.Parameters["@RowCount"].Value.ToString());
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //--------------------------------------------------------------
        public static string Static_GetDisplayString(int CustomerId)
        {
            string RetVal = "";
            Customers m_Customers = new Customers();
            m_Customers.CustomerId = CustomerId;
            m_Customers = m_Customers.Get();
            RetVal = m_Customers.FullName;
            return RetVal;
        }

        public void UpdateDisplayOrder()
        {
            try
            {
                SqlCommand sqlCommand = new SqlCommand("Customers_Update_DisplayOrder") { CommandType = CommandType.StoredProcedure };
                sqlCommand.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                sqlCommand.Parameters.Add(new SqlParameter("@CustomerId", this.CustomerId));
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
