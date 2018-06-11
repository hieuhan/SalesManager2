using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagerLib
{
    public class Products
    {
        #region Private Properties
        private int _ProductId;
        private string _ProductName;
        private string _ImagePath;
        private int _ManufacturerId;
        private short _UnitId;
        private short _ProductGroupId;
        private short _ProductTypeId;
        private short _OriginId;
        private short _WarrantyId;
        private byte _StatusId;
        private string _ProductContent;
        private int _DisplayOrder;
        private int _CrUserId;
        private int _UpdateUserId;
        private DateTime _CrDateTime;
        private DBAccess db;

        #endregion

        #region Public Properties

        //-----------------------------------------------------------------
        public Products()
        {
            db = new DBAccess(LibConstants.CONNECTION_STRING);
        }
        //-----------------------------------------------------------------        
        public Products(string constr)
        {
            db = (constr.Length > 0) ? new DBAccess(constr) : new DBAccess();
        }
        //-----------------------------------------------------------------
        ~Products()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }

        //-----------------------------------------------------------------    
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
        public string ImagePath
        {
            get
            {
                return _ImagePath;
            }
            set
            {
                _ImagePath = value;
            }
        }
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
        public short OriginId
        {
            get
            {
                return _OriginId;
            }
            set
            {
                _OriginId = value;
            }
        }
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
        public string ProductContent
        {
            get
            {
                return _ProductContent;
            }
            set
            {
                _ProductContent = value;
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
        private List<Products> Init(SqlCommand cmd)
        {
            SqlConnection con = db.GetConnection();
            cmd.Connection = con;
            List<Products> l_Products = new List<Products>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    Products m_Products = new Products();
                    m_Products.ProductId = smartReader.GetInt32("ProductId");
                    m_Products.ProductName = smartReader.GetString("ProductName");
                    m_Products.ImagePath = smartReader.GetString("ImagePath");
                    m_Products.ManufacturerId = smartReader.GetInt32("ManufacturerId");
                    m_Products.UnitId = smartReader.GetInt16("UnitId");
                    m_Products.ProductGroupId = smartReader.GetInt16("ProductGroupId");
                    m_Products.ProductTypeId = smartReader.GetInt16("ProductTypeId");
                    m_Products.OriginId = smartReader.GetInt16("OriginId");
                    m_Products.WarrantyId = smartReader.GetInt16("WarrantyId");
                    m_Products.StatusId = smartReader.GetByte("StatusId");
                    m_Products.ProductContent = smartReader.GetString("ProductContent");
                    m_Products.DisplayOrder = smartReader.GetInt32("DisplayOrder");
                    m_Products.CrUserId = smartReader.GetInt32("CrUserId");
                    m_Products.UpdateUserId = smartReader.GetInt32("UpdateUserId");
                    m_Products.CrDateTime = smartReader.GetDateTime("CrDateTime");
                    l_Products.Add(m_Products);
                }
                reader.Close();
                return l_Products;
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
                SqlCommand cmd = new SqlCommand("Products_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ProductName", this.ProductName));
                cmd.Parameters.Add(new SqlParameter("@ImagePath", this.ImagePath));
                cmd.Parameters.Add(new SqlParameter("@ManufacturerId", this.ManufacturerId));
                cmd.Parameters.Add(new SqlParameter("@UnitId", this.UnitId));
                cmd.Parameters.Add(new SqlParameter("@ProductGroupId", this.ProductGroupId));
                cmd.Parameters.Add(new SqlParameter("@ProductTypeId", this.ProductTypeId));
                cmd.Parameters.Add(new SqlParameter("@OriginId", this.OriginId));
                cmd.Parameters.Add(new SqlParameter("@WarrantyId", this.WarrantyId));
                cmd.Parameters.Add(new SqlParameter("@StatusId", this.StatusId));
                cmd.Parameters.Add(new SqlParameter("@ProductContent", this.ProductContent));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add(new SqlParameter("@CrUserId", this.CrUserId));
                cmd.Parameters.Add(new SqlParameter("@UpdateUserId", this.UpdateUserId));
                cmd.Parameters.Add(new SqlParameter("@ProductId", this.ProductId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.ProductId = int.Parse(cmd.Parameters["@ProductId"].Value.ToString());
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
                SqlCommand cmd = new SqlCommand("Products_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ProductId", this.ProductId));
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
        public Products Get()
        {
            Products retVal = new Products();
            int RowCount = 0;
            string DateFrom = "";
            string DateTo = "";
            string OrderBy = "";
            int PageSize = 1;
            int PageNumber = 0;
            try
            {

                List<Products> list = GetPage(DateFrom, DateTo, OrderBy, PageSize, PageNumber, ref RowCount);
                if (list.Count > 0) retVal = list[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }


        //-------------------------------------------------------------- 

        public List<Products> GetPage(string DateFrom, string DateTo, string OrderBy, int PageSize, int PageNumber, ref int RowCount)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Products_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@ProductId", this.ProductId));
                cmd.Parameters.Add(new SqlParameter("@ProductName", this.ProductName));
                cmd.Parameters.Add(new SqlParameter("@ImagePath", this.ImagePath));
                cmd.Parameters.Add(new SqlParameter("@ManufacturerId", this.ManufacturerId));
                cmd.Parameters.Add(new SqlParameter("@UnitId", this.UnitId));
                cmd.Parameters.Add(new SqlParameter("@ProductGroupId", this.ProductGroupId));
                cmd.Parameters.Add(new SqlParameter("@ProductTypeId", this.ProductTypeId));
                cmd.Parameters.Add(new SqlParameter("@OriginId", this.OriginId));
                cmd.Parameters.Add(new SqlParameter("@WarrantyId", this.WarrantyId));
                cmd.Parameters.Add(new SqlParameter("@StatusId", this.StatusId));
                cmd.Parameters.Add(new SqlParameter("@ProductContent", this.ProductContent));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add(new SqlParameter("@CrUserId", this.CrUserId));
                cmd.Parameters.Add(new SqlParameter("@UpdateUserId", this.UpdateUserId));
                if (!string.IsNullOrEmpty(DateFrom)) cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                if (!string.IsNullOrEmpty(DateTo)) cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                cmd.Parameters.Add(new SqlParameter("@PageSize", PageSize));
                cmd.Parameters.Add(new SqlParameter("@PageNumber", PageNumber));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                List<Products> list = Init(cmd);
                RowCount = int.Parse(cmd.Parameters["@RowCount"].Value.ToString());
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //--------------------------------------------------------------
        public static string Static_GetDisplayString(int ProductId)
        {
            string RetVal = "";
            Products m_Products = new Products();
            m_Products.ProductId = ProductId;
            m_Products = m_Products.Get();
            RetVal = m_Products.ProductName;
            return RetVal;
        }

        #endregion
    }
}
