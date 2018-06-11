using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagerLib
{
    public class Manufacturers
    {
        #region Private Properties
        private int _ManufacturerId;
        private string _ManufacturerName;
        private string _ManufacturerDesc;
        private byte _StatusId;
        private int _DisplayOrder;
        private DBAccess db;

        #endregion

        #region Public Properties

        //-----------------------------------------------------------------
        public Manufacturers()
        {
            db = new DBAccess(LibConstants.CONNECTION_STRING);
        }
        //-----------------------------------------------------------------        
        public Manufacturers(string constr)
        {
            db = (constr.Length > 0) ? new DBAccess(constr) : new DBAccess();
        }
        //-----------------------------------------------------------------
        ~Manufacturers()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }

        //-----------------------------------------------------------------    
        public int ManufacturerId
        {
            get
            {
                return _ManufacturerId;
            }
            set
            {
                _ManufacturerId = value;
            }
        }

        public string ManufacturerName
        {
            get
            {
                return _ManufacturerName;
            }
            set
            {
                _ManufacturerName = value;
            }
        }
        public string ManufacturerDesc
        {
            get
            {
                return _ManufacturerDesc;
            }
            set
            {
                _ManufacturerDesc = value;
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



        #endregion
        //-----------------------------------------------------------
        #region Method
        private List<Manufacturers> Init(SqlCommand cmd)
        {
            SqlConnection con = db.GetConnection();
            cmd.Connection = con;
            List<Manufacturers> l_Manufacturers = new List<Manufacturers>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    Manufacturers m_Manufacturers = new Manufacturers();
                    m_Manufacturers.ManufacturerId = smartReader.GetInt32("ManufacturerId");
                    m_Manufacturers.ManufacturerName = smartReader.GetString("ManufacturerName");
                    m_Manufacturers.ManufacturerDesc = smartReader.GetString("ManufacturerDesc");
                    m_Manufacturers.StatusId = smartReader.GetByte("StatusId");
                    m_Manufacturers.DisplayOrder = smartReader.GetInt32("DisplayOrder");
                    l_Manufacturers.Add(m_Manufacturers);
                }
                reader.Close();
                return l_Manufacturers;
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
                SqlCommand cmd = new SqlCommand("Manufacturers_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                //cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@ManufacturerName", this.ManufacturerName));
                cmd.Parameters.Add(new SqlParameter("@ManufacturerDesc", this.ManufacturerDesc));
                cmd.Parameters.Add(new SqlParameter("@StatusId", this.StatusId));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add(new SqlParameter("@ManufacturerId", this.ManufacturerId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.ManufacturerId = int.Parse(cmd.Parameters["@ManufacturerId"].Value.ToString());
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
                SqlCommand cmd = new SqlCommand("Manufacturers_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                //cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@ManufacturerId", this.ManufacturerId));
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
        public Manufacturers Get()
        {
            Manufacturers retVal = new Manufacturers();
            int RowCount = 0;
            string DateFrom = "";
            string DateTo = "";
            string OrderBy = "";
            int PageSize = 1;
            int PageNumber = 0;
            try
            {

                List<Manufacturers> list = GetPage(DateFrom, DateTo, OrderBy, PageSize, PageNumber, ref RowCount);
                if (list.Count > 0) retVal = list[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }


        //-------------------------------------------------------------- 

        public List<Manufacturers> GetPage(string DateFrom, string DateTo, string OrderBy, int PageSize, int PageNumber, ref int RowCount)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Manufacturers_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@ManufacturerId", this.ManufacturerId));
                cmd.Parameters.Add(new SqlParameter("@ManufacturerName", this.ManufacturerName));
                cmd.Parameters.Add(new SqlParameter("@ManufacturerDesc", this.ManufacturerDesc));
                cmd.Parameters.Add(new SqlParameter("@StatusId", this.StatusId));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add(new SqlParameter("@DateFrom", DateFrom));
                cmd.Parameters.Add(new SqlParameter("@DateTo", DateTo));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                cmd.Parameters.Add(new SqlParameter("@PageSize", PageSize));
                cmd.Parameters.Add(new SqlParameter("@PageNumber", PageNumber));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                List<Manufacturers> list = Init(cmd);
                RowCount = int.Parse(cmd.Parameters["@RowCount"].Value.ToString());
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //--------------------------------------------------------------
        public static string Static_GetDisplayString(int ManufacturerId)
        {
            string RetVal = "";
            Manufacturers m_Manufacturers = new Manufacturers();
            m_Manufacturers.ManufacturerId = ManufacturerId;
            m_Manufacturers = m_Manufacturers.Get();
            RetVal = m_Manufacturers.ManufacturerName;
            return RetVal;
        }

        #endregion
    }
}
