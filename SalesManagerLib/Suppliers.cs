using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagerLib
{
    public class Suppliers
    {
        #region Private Properties
        private int _SupplierId;
        private string _SupplierName;
        private string _Address;
        private string _Mobile;
        private string _Email;
        private string _Contact;
        private double _DebitBalance;
        private string _Note;
        private int _DisplayOrder;
        private byte _ReviewStatusId;
        private DateTime _CrDateTime;
        private DBAccess db;

        #endregion

        #region Public Properties

        //-----------------------------------------------------------------
        public Suppliers()
        {
            db = new DBAccess(LibConstants.CONNECTION_STRING);
        }
        //-----------------------------------------------------------------        
        public Suppliers(string constr)
        {
            db = (constr.Length > 0) ? new DBAccess(constr) : new DBAccess();
        }
        //-----------------------------------------------------------------
        ~Suppliers()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }

        //-----------------------------------------------------------------    
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

        public string SupplierName
        {
            get
            {
                return _SupplierName;
            }
            set
            {
                _SupplierName = value;
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
        public string Contact
        {
            get
            {
                return _Contact;
            }
            set
            {
                _Contact = value;
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
        public byte ReviewStatusId
        {
            get
            {
                return _ReviewStatusId;
            }
            set
            {
                _ReviewStatusId = value;
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
        private List<Suppliers> Init(SqlCommand cmd)
        {
            SqlConnection con = db.GetConnection();
            cmd.Connection = con;
            List<Suppliers> l_Suppliers = new List<Suppliers>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    Suppliers m_Suppliers = new Suppliers();
                    m_Suppliers.SupplierId = smartReader.GetInt32("SupplierId");
                    m_Suppliers.SupplierName = smartReader.GetString("SupplierName");
                    m_Suppliers.Address = smartReader.GetString("Address");
                    m_Suppliers.Mobile = smartReader.GetString("Mobile");
                    m_Suppliers.Email = smartReader.GetString("Email");
                    m_Suppliers.Contact = smartReader.GetString("Contact");
                    m_Suppliers.DebitBalance = smartReader.GetDouble("DebitBalance");
                    m_Suppliers.Note = smartReader.GetString("Note");
                    m_Suppliers.DisplayOrder = smartReader.GetInt32("DisplayOrder");
                    m_Suppliers.ReviewStatusId = smartReader.GetByte("ReviewStatusId");
                    m_Suppliers.CrDateTime = smartReader.GetDateTime("CrDateTime");
                    l_Suppliers.Add(m_Suppliers);
                }
                reader.Close();
                return l_Suppliers;
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
                SqlCommand cmd = new SqlCommand("Suppliers_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SupplierName", this.SupplierName));
                cmd.Parameters.Add(new SqlParameter("@Address", this.Address));
                cmd.Parameters.Add(new SqlParameter("@Mobile", this.Mobile));
                cmd.Parameters.Add(new SqlParameter("@Email", this.Email));
                cmd.Parameters.Add(new SqlParameter("@Contact", this.Contact));
                cmd.Parameters.Add(new SqlParameter("@DebitBalance", this.DebitBalance));
                cmd.Parameters.Add(new SqlParameter("@Note", this.Note));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", this.ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@SupplierId", this.SupplierId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.SupplierId = int.Parse(cmd.Parameters["@SupplierId"].Value.ToString());
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
                SqlCommand cmd = new SqlCommand("Suppliers_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SupplierId", this.SupplierId));
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
        public Suppliers Get()
        {
            Suppliers retVal = new Suppliers();
            int RowCount = 0;
            string DateFrom = "";
            string DateTo = "";
            string OrderBy = "";
            int PageSize = 1;
            int PageNumber = 0;
            try
            {

                List<Suppliers> list = GetPage(DateFrom, DateTo, OrderBy, PageSize, PageNumber, ref RowCount);
                if (list.Count > 0) retVal = list[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }


        //-------------------------------------------------------------- 

        public List<Suppliers> GetPage(string DateFrom, string DateTo, string OrderBy, int PageSize, int PageNumber, ref int RowCount)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Suppliers_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@SupplierId", this.SupplierId));
                cmd.Parameters.Add(new SqlParameter("@SupplierName", this.SupplierName));
                cmd.Parameters.Add(new SqlParameter("@Address", this.Address));
                cmd.Parameters.Add(new SqlParameter("@Mobile", this.Mobile));
                cmd.Parameters.Add(new SqlParameter("@Email", this.Email));
                cmd.Parameters.Add(new SqlParameter("@Contact", this.Contact));
                cmd.Parameters.Add(new SqlParameter("@DebitBalance", this.DebitBalance));
                cmd.Parameters.Add(new SqlParameter("@Note", this.Note));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", this.ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@DateFrom", DateFrom));
                cmd.Parameters.Add(new SqlParameter("@DateTo", DateTo));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                cmd.Parameters.Add(new SqlParameter("@PageSize", PageSize));
                cmd.Parameters.Add(new SqlParameter("@PageNumber", PageNumber));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                List<Suppliers> list = Init(cmd);
                RowCount = int.Parse(cmd.Parameters["@RowCount"].Value.ToString());
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //--------------------------------------------------------------
        public static string Static_GetDisplayString(int SupplierId)
        {
            string RetVal = "";
            Suppliers m_Suppliers = new Suppliers();
            m_Suppliers.SupplierId = SupplierId;
            m_Suppliers = m_Suppliers.Get();
            RetVal = m_Suppliers.SupplierName;
            return RetVal;
        }

        #endregion
    }
}
