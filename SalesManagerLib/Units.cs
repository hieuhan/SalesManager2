using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagerLib
{
    public class Units
    {
        #region Private Properties
        private short _UnitId;
        private string _UnitName;
        private string _UnitDesc;
        private DateTime _CrDateTime;
        private DBAccess db;

        #endregion

        #region Public Properties

        //-----------------------------------------------------------------
        public Units()
        {
            db = new DBAccess(LibConstants.CONNECTION_STRING);
        }
        //-----------------------------------------------------------------        
        public Units(string constr)
        {
            db = (constr.Length > 0) ? new DBAccess(constr) : new DBAccess();
        }
        //-----------------------------------------------------------------
        ~Units()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }

        //-----------------------------------------------------------------    
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

        public string UnitName
        {
            get
            {
                return _UnitName;
            }
            set
            {
                _UnitName = value;
            }
        }
        public string UnitDesc
        {
            get
            {
                return _UnitDesc;
            }
            set
            {
                _UnitDesc = value;
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
        private List<Units> Init(SqlCommand cmd)
        {
            SqlConnection con = db.GetConnection();
            cmd.Connection = con;
            List<Units> l_Units = new List<Units>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    Units m_Units = new Units();
                    m_Units.UnitId = smartReader.GetInt16("UnitId");
                    m_Units.UnitName = smartReader.GetString("UnitName");
                    m_Units.UnitDesc = smartReader.GetString("UnitDesc");
                    m_Units.CrDateTime = smartReader.GetDateTime("CrDateTime");
                    l_Units.Add(m_Units);
                }
                reader.Close();
                return l_Units;
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
                SqlCommand cmd = new SqlCommand("Units_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@UnitName", this.UnitName));
                cmd.Parameters.Add(new SqlParameter("@UnitDesc", this.UnitDesc));
                cmd.Parameters.Add(new SqlParameter("@UnitId", this.UnitId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.UnitId = short.Parse(cmd.Parameters["@UnitId"].Value.ToString());
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
                SqlCommand cmd = new SqlCommand("Units_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@UnitId", this.UnitId));
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
        public Units Get()
        {
            Units retVal = new Units();
            int RowCount = 0;
            string DateFrom = "";
            string DateTo = "";
            string OrderBy = "";
            int PageSize = 1;
            int PageNumber = 0;
            try
            {

                List<Units> list = GetPage(DateFrom, DateTo, OrderBy, PageSize, PageNumber, ref RowCount);
                if (list.Count > 0) retVal = list[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }


        //-------------------------------------------------------------- 

        public List<Units> GetPage(string DateFrom, string DateTo, string OrderBy, int PageSize, int PageNumber, ref int RowCount)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Units_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@UnitId", this.UnitId));
                cmd.Parameters.Add(new SqlParameter("@UnitName", this.UnitName));
                cmd.Parameters.Add(new SqlParameter("@UnitDesc", this.UnitDesc));
                cmd.Parameters.Add(new SqlParameter("@DateFrom", DateFrom));
                cmd.Parameters.Add(new SqlParameter("@DateTo", DateTo));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                cmd.Parameters.Add(new SqlParameter("@PageSize", PageSize));
                cmd.Parameters.Add(new SqlParameter("@PageNumber", PageNumber));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                List<Units> list = Init(cmd);
                RowCount = int.Parse(cmd.Parameters["@RowCount"].Value.ToString());
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //--------------------------------------------------------------
        public static string Static_GetDisplayString(short UnitId)
        {
            string RetVal = "";
            Units m_Units = new Units();
            m_Units.UnitId = UnitId;
            m_Units = m_Units.Get();
            RetVal = m_Units.UnitName;
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public List<Units> GetList()
        {
            List<Units> RetVal = new List<Units>();
            try
            {
                string sql = "SELECT * FROM Units";
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
        public static Units Static_Get(int UnitId, List<Units> l_Units)
        {
            Units RetVal;
            RetVal = l_Units.Find(x => x.UnitId == UnitId);
            if (RetVal == null)
                RetVal = new Units();
            return RetVal;
        }
        #endregion
    }
}
