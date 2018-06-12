using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagerLib
{
    public class ProductGroups
    {
        #region Private Properties
        private short _ProductGroupId;
        private string _ProductGroupName;
        private string _ProductGroupDesc;
        private short _DisplayOrder;
        private DBAccess db;

        #endregion

        #region Public Properties

        //-----------------------------------------------------------------
        public ProductGroups()
        {
            db = new DBAccess(LibConstants.CONNECTION_STRING);
        }
        //-----------------------------------------------------------------        
        public ProductGroups(string constr)
        {
            db = (constr.Length > 0) ? new DBAccess(constr) : new DBAccess();
        }
        //-----------------------------------------------------------------
        ~ProductGroups()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }

        //-----------------------------------------------------------------    
        public short ProductGroupId
        {
            get
            {
                return _ProductGroupId;
            }
            set
            {
                _ProductGroupId = value;
            }
        }

        public string ProductGroupName
        {
            get
            {
                return _ProductGroupName;
            }
            set
            {
                _ProductGroupName = value;
            }
        }
        public string ProductGroupDesc
        {
            get
            {
                return _ProductGroupDesc;
            }
            set
            {
                _ProductGroupDesc = value;
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
        private List<ProductGroups> Init(SqlCommand cmd)
        {
            SqlConnection con = db.GetConnection();
            cmd.Connection = con;
            List<ProductGroups> l_ProductGroups = new List<ProductGroups>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    ProductGroups m_ProductGroups = new ProductGroups();
                    m_ProductGroups.ProductGroupId = smartReader.GetInt16("ProductGroupId");
                    m_ProductGroups.ProductGroupName = smartReader.GetString("ProductGroupName");
                    m_ProductGroups.ProductGroupDesc = smartReader.GetString("ProductGroupDesc");
                    m_ProductGroups.DisplayOrder = smartReader.GetInt16("DisplayOrder");
                    l_ProductGroups.Add(m_ProductGroups);
                }
                reader.Close();
                return l_ProductGroups;
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
                SqlCommand cmd = new SqlCommand("ProductGroups_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ProductGroupName", this.ProductGroupName));
                cmd.Parameters.Add(new SqlParameter("@ProductGroupDesc", this.ProductGroupDesc));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add(new SqlParameter("@ProductGroupId", this.ProductGroupId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.ProductGroupId = short.Parse(cmd.Parameters["@ProductGroupId"].Value.ToString());
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
                SqlCommand cmd = new SqlCommand("ProductGroups_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ProductGroupId", this.ProductGroupId));
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
        public ProductGroups Get()
        {
            ProductGroups retVal = new ProductGroups();
            int RowCount = 0;
            string OrderBy = "";
            int PageSize = 1;
            int PageNumber = 0;
            try
            {

                List<ProductGroups> list = GetPage(OrderBy, PageSize, PageNumber, ref RowCount);
                if (list.Count > 0) retVal = list[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }


        //-------------------------------------------------------------- 

        public List<ProductGroups> GetPage(string OrderBy, int PageSize, int PageNumber, ref int RowCount)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("ProductGroups_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@ProductGroupId", this.ProductGroupId));
                cmd.Parameters.Add(new SqlParameter("@ProductGroupName", this.ProductGroupName));
                cmd.Parameters.Add(new SqlParameter("@ProductGroupDesc", this.ProductGroupDesc));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                cmd.Parameters.Add(new SqlParameter("@PageSize", PageSize));
                cmd.Parameters.Add(new SqlParameter("@PageNumber", PageNumber));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                List<ProductGroups> list = Init(cmd);
                RowCount = int.Parse(cmd.Parameters["@RowCount"].Value.ToString());
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //--------------------------------------------------------------
        public List<ProductGroups> GetList()
        {
            List<ProductGroups> RetVal = new List<ProductGroups>();
            try
            {
                string sql = "SELECT * FROM ProductGroups";
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
        public static string Static_GetDisplayString(short ProductGroupId)
        {
            string RetVal = "";
            ProductGroups m_ProductGroups = new ProductGroups();
            m_ProductGroups.ProductGroupId = ProductGroupId;
            m_ProductGroups = m_ProductGroups.Get();
            RetVal = m_ProductGroups.ProductGroupName;
            return RetVal;
        }

        public void UpdateDisplayOrder()
        {
            try
            {
                SqlCommand sqlCommand = new SqlCommand("ProductGroups_Update_DisplayOrder") { CommandType = CommandType.StoredProcedure };
                sqlCommand.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                sqlCommand.Parameters.Add(new SqlParameter("@ProductGroupId", this.ProductGroupId));
                this.db.ExecuteSQL(sqlCommand);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //-------------------------------------------------------------- 
        public static ProductGroups Static_Get(short productGroupId, List<ProductGroups> listProductGroups)
        {
            var retVal = listProductGroups.Find(x => x.ProductGroupId == productGroupId) ?? new ProductGroups();
            return retVal;
        }

        #endregion
    }
}
