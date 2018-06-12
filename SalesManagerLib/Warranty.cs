using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagerLib
{
    public class Warranty
    {
        #region Private Properties
        private short _WarrantyId;
        private string _WarrantyName;
        private string _WarrantyDesc;
        private short _DisplayOrder;
        private DBAccess db;

        #endregion

        #region Public Properties

        //-----------------------------------------------------------------
        public Warranty()
        {
            db = new DBAccess(LibConstants.CONNECTION_STRING);
        }
        //-----------------------------------------------------------------        
        public Warranty(string constr)
        {
            db = (constr.Length > 0) ? new DBAccess(constr) : new DBAccess();
        }
        //-----------------------------------------------------------------
        ~Warranty()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }

        //-----------------------------------------------------------------    
        public short WarrantyId
        {
            get
            {
                return _WarrantyId;
            }
            set
            {
                _WarrantyId = value;
            }
        }

        public string WarrantyName
        {
            get
            {
                return _WarrantyName;
            }
            set
            {
                _WarrantyName = value;
            }
        }
        public string WarrantyDesc
        {
            get
            {
                return _WarrantyDesc;
            }
            set
            {
                _WarrantyDesc = value;
            }
        }
        public short DisplayOrder
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
        private List<Warranty> Init(SqlCommand cmd)
        {
            SqlConnection con = db.GetConnection();
            cmd.Connection = con;
            List<Warranty> l_Warranty = new List<Warranty>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    Warranty m_Warranty = new Warranty();
                    m_Warranty.WarrantyId = smartReader.GetInt16("WarrantyId");
                    m_Warranty.WarrantyName = smartReader.GetString("WarrantyName");
                    m_Warranty.WarrantyDesc = smartReader.GetString("WarrantyDesc");
                    m_Warranty.DisplayOrder = smartReader.GetInt16("DisplayOrder");
                    l_Warranty.Add(m_Warranty);
                }
                reader.Close();
                return l_Warranty;
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
                SqlCommand cmd = new SqlCommand("Warranty_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@WarrantyName", this.WarrantyName));
                cmd.Parameters.Add(new SqlParameter("@WarrantyDesc", this.WarrantyDesc));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add(new SqlParameter("@WarrantyId", this.WarrantyId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.WarrantyId = short.Parse(cmd.Parameters["@WarrantyId"].Value.ToString());
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
                SqlCommand cmd = new SqlCommand("Warranty_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@WarrantyId", this.WarrantyId));
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
        public Warranty Get()
        {
            Warranty retVal = new Warranty();
            int RowCount = 0;
            string OrderBy = "";
            int PageSize = 1;
            int PageNumber = 0;
            try
            {

                List<Warranty> list = GetPage(OrderBy, PageSize, PageNumber, ref RowCount);
                if (list.Count > 0) retVal = list[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }


        //-------------------------------------------------------------- 

        public List<Warranty> GetPage(string OrderBy, int PageSize, int PageNumber, ref int RowCount)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Warranty_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@WarrantyId", this.WarrantyId));
                cmd.Parameters.Add(new SqlParameter("@WarrantyName", this.WarrantyName));
                cmd.Parameters.Add(new SqlParameter("@WarrantyDesc", this.WarrantyDesc));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                cmd.Parameters.Add(new SqlParameter("@PageSize", PageSize));
                cmd.Parameters.Add(new SqlParameter("@PageNumber", PageNumber));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                List<Warranty> list = Init(cmd);
                RowCount = int.Parse(cmd.Parameters["@RowCount"].Value.ToString());
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //--------------------------------------------------------------
        public List<Warranty> GetList()
        {
            List<Warranty> RetVal = new List<Warranty>();
            try
            {
                string sql = "SELECT * FROM Warranty";
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
        public static string Static_GetDisplayString(short WarrantyId)
        {
            string RetVal = "";
            Warranty m_Warranty = new Warranty();
            m_Warranty.WarrantyId = WarrantyId;
            m_Warranty = m_Warranty.Get();
            RetVal = m_Warranty.WarrantyName;
            return RetVal;
        }

        public void UpdateDisplayOrder()
        {
            try
            {
                SqlCommand sqlCommand = new SqlCommand("Warranty_Update_DisplayOrder") { CommandType = CommandType.StoredProcedure };
                sqlCommand.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                sqlCommand.Parameters.Add(new SqlParameter("@WarrantyId", this.WarrantyId));
                this.db.ExecuteSQL(sqlCommand);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //-------------------------------------------------------------- 
        public static Warranty Static_Get(short warrantyId, List<Warranty> listWarranty)
        {
            var retVal = listWarranty.Find(x => x.WarrantyId == warrantyId) ?? new Warranty();
            return retVal;
        }
        #endregion
    }
}
