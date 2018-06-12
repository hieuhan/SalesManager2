using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagerLib
{
    public class ProductTypes
    {
        #region Private Properties
        private short _ProductTypeId;
        private string _ProductTypeName;
        private string _ProductTypeDesc;
        private short _DisplayOrder;
        private DBAccess db;

        #endregion

        #region Public Properties

        //-----------------------------------------------------------------
        public ProductTypes()
        {
            db = new DBAccess(LibConstants.CONNECTION_STRING);
        }
        //-----------------------------------------------------------------        
        public ProductTypes(string constr)
        {
            db = (constr.Length > 0) ? new DBAccess(constr) : new DBAccess();
        }
        //-----------------------------------------------------------------
        ~ProductTypes()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }

        //-----------------------------------------------------------------    
        public short ProductTypeId
        {
            get
            {
                return _ProductTypeId;
            }
            set
            {
                _ProductTypeId = value;
            }
        }

        public string ProductTypeName
        {
            get
            {
                return _ProductTypeName;
            }
            set
            {
                _ProductTypeName = value;
            }
        }
        public string ProductTypeDesc
        {
            get
            {
                return _ProductTypeDesc;
            }
            set
            {
                _ProductTypeDesc = value;
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
        private List<ProductTypes> Init(SqlCommand cmd)
        {
            SqlConnection con = db.GetConnection();
            cmd.Connection = con;
            List<ProductTypes> l_ProductTypes = new List<ProductTypes>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    ProductTypes m_ProductTypes = new ProductTypes();
                    m_ProductTypes.ProductTypeId = smartReader.GetInt16("ProductTypeId");
                    m_ProductTypes.ProductTypeName = smartReader.GetString("ProductTypeName");
                    m_ProductTypes.ProductTypeDesc = smartReader.GetString("ProductTypeDesc");
                    m_ProductTypes.DisplayOrder = smartReader.GetInt16("DisplayOrder");
                    l_ProductTypes.Add(m_ProductTypes);
                }
                reader.Close();
                return l_ProductTypes;
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
                SqlCommand cmd = new SqlCommand("ProductTypes_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ProductTypeName", this.ProductTypeName));
                cmd.Parameters.Add(new SqlParameter("@ProductTypeDesc", this.ProductTypeDesc));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add(new SqlParameter("@ProductTypeId", this.ProductTypeId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.ProductTypeId = short.Parse(cmd.Parameters["@ProductTypeId"].Value.ToString());
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
                SqlCommand cmd = new SqlCommand("ProductTypes_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ProductTypeId", this.ProductTypeId));
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
        public ProductTypes Get()
        {
            ProductTypes retVal = new ProductTypes();
            int RowCount = 0;
            string OrderBy = "";
            int PageSize = 1;
            int PageNumber = 0;
            try
            {

                List<ProductTypes> list = GetPage(OrderBy, PageSize, PageNumber, ref RowCount);
                if (list.Count > 0) retVal = list[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }


        //-------------------------------------------------------------- 

        public List<ProductTypes> GetPage(string OrderBy, int PageSize, int PageNumber, ref int RowCount)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("ProductTypes_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@ProductTypeId", this.ProductTypeId));
                cmd.Parameters.Add(new SqlParameter("@ProductTypeName", this.ProductTypeName));
                cmd.Parameters.Add(new SqlParameter("@ProductTypeDesc", this.ProductTypeDesc));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                cmd.Parameters.Add(new SqlParameter("@PageSize", PageSize));
                cmd.Parameters.Add(new SqlParameter("@PageNumber", PageNumber));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                List<ProductTypes> list = Init(cmd);
                RowCount = int.Parse(cmd.Parameters["@RowCount"].Value.ToString());
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //--------------------------------------------------------------
        public List<ProductTypes> GetList()
        {
            List<ProductTypes> RetVal = new List<ProductTypes>();
            try
            {
                string sql = "SELECT * FROM ProductTypes";
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
        public static string Static_GetDisplayString(short ProductTypeId)
        {
            string RetVal = "";
            ProductTypes m_ProductTypes = new ProductTypes();
            m_ProductTypes.ProductTypeId = ProductTypeId;
            m_ProductTypes = m_ProductTypes.Get();
            RetVal = m_ProductTypes.ProductTypeName;
            return RetVal;
        }

        public void UpdateDisplayOrder()
        {
            try
            {
                SqlCommand sqlCommand = new SqlCommand("ProductTypes_Update_DisplayOrder") { CommandType = CommandType.StoredProcedure };
                sqlCommand.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                sqlCommand.Parameters.Add(new SqlParameter("@ProductTypeId", this.ProductTypeId));
                this.db.ExecuteSQL(sqlCommand);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //-------------------------------------------------------------- 
        public static ProductTypes Static_Get(short productTypeId, List<ProductTypes> listProductTypes)
        {
            var retVal = listProductTypes.Find(x => x.ProductTypeId == productTypeId) ?? new ProductTypes();
            return retVal;
        }

        #endregion
    }
}
