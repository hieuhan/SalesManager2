using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SalesManagerLib
{
    public class ReviewStatus
    {
        private byte _ReviewStatusId;
        private string _ReviewStatusName;
        private string _ReviewStatusDesc;
        private byte _DisplayOrder;
        private DBAccess db;
        //-----------------------------------------------------------------
        public ReviewStatus()
        {
            db = new DBAccess(LibConstants.CONNECTION_STRING);
        }
        //-----------------------------------------------------------------        
        public ReviewStatus(string constr)
        {
            db = new DBAccess((constr == "") ? LibConstants.CONNECTION_STRING : constr);
        }
        //-----------------------------------------------------------------
        ~ReviewStatus()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public byte ReviewStatusId
        {
            get { return _ReviewStatusId; }
            set { _ReviewStatusId = value; }
        }
        //-----------------------------------------------------------------
        public string ReviewStatusName
        {
            get { return _ReviewStatusName; }
            set { _ReviewStatusName = value; }
        }
        //-----------------------------------------------------------------
        public string ReviewStatusDesc
        {
            get { return _ReviewStatusDesc; }
            set { _ReviewStatusDesc = value; }
        }
        //-----------------------------------------------------------------
        public byte DisplayOrder
        {
            get { return _DisplayOrder; }
            set { _DisplayOrder = value; }
        }
        //-----------------------------------------------------------------

        private List<ReviewStatus> Init(SqlCommand cmd)
        {
            SqlConnection con = db.GetConnection();
            cmd.Connection = con;
            List<ReviewStatus> l_ReviewStatus = new List<ReviewStatus>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    ReviewStatus m_ReviewStatus = new ReviewStatus(db.ConnectionString);
                    m_ReviewStatus.ReviewStatusId = smartReader.GetByte("ReviewStatusId");
                    m_ReviewStatus.ReviewStatusName = smartReader.GetString("ReviewStatusName");
                    m_ReviewStatus.ReviewStatusDesc = smartReader.GetString("ReviewStatusDesc");
                    m_ReviewStatus.DisplayOrder = smartReader.GetByte("DisplayOrder");

                    l_ReviewStatus.Add(m_ReviewStatus);
                }
                reader.Close();
                return l_ReviewStatus;
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
                SqlCommand cmd = new SqlCommand("ReviewStatus_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusName", this.ReviewStatusName));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusDesc", this.ReviewStatusDesc));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add("@ReviewStatusId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.ReviewStatusId = Convert.ToByte((cmd.Parameters["@ReviewStatusId"].Value == null) ? 0 : (cmd.Parameters["@ReviewStatusId"].Value));
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
        public byte Update(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("ReviewStatus_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusName", this.ReviewStatusName));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusDesc", this.ReviewStatusDesc));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", this.ReviewStatusId));
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
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
                SqlCommand cmd = new SqlCommand("ReviewStatus_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", this.ReviewStatusId));
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
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
        public List<ReviewStatus> GetList()
        {
            List<ReviewStatus> RetVal = new List<ReviewStatus>();
            try
            {
                string sql = "SELECT * FROM ReviewStatus";
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
        public static List<ReviewStatus> Static_GetList(string ConStr)
        {
            List<ReviewStatus> RetVal = new List<ReviewStatus>();
            try
            {
                ReviewStatus m_ReviewStatus = new ReviewStatus(ConStr);
                RetVal = m_ReviewStatus.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<ReviewStatus> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------   
        public static List<ReviewStatus> Static_GetListAll(string ConStr, string TextOptionAll = "...")
        {
            ReviewStatus m_ReviewStatus = new ReviewStatus(ConStr);
            List<ReviewStatus> RetVal = m_ReviewStatus.GetList();
            TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
            if (TextOptionAll.Length > 0)
            {
                m_ReviewStatus.ReviewStatusName = TextOptionAll;
                m_ReviewStatus.ReviewStatusDesc = TextOptionAll;
                RetVal.Insert(0, m_ReviewStatus);
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<ReviewStatus> Static_GetListAll(string TextOptionAll = "...")
        {
            return Static_GetListAll("", TextOptionAll);
        }
        //--------------------------------------------------------------    
        public List<ReviewStatus> GetListOrderBy(string OrderBy)
        {
            List<ReviewStatus> RetVal = new List<ReviewStatus>();
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                string sql = "SELECT * FROM ReviewStatus ";
                if (OrderBy.Length > 0)
                {
                    sql += "ORDER BY " + OrderBy;
                }
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
        public static List<ReviewStatus> Static_GetListOrderBy(string ConStr, string OrderBy)
        {
            ReviewStatus m_ReviewStatus = new ReviewStatus(ConStr);
            return m_ReviewStatus.GetListOrderBy(OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<ReviewStatus> Static_GetListOrderBy(string OrderBy)
        {
            return Static_GetListOrderBy("", OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<ReviewStatus> Static_GetListAllOrderBy(string ConStr, string OrderBy, string TextOptionAll = "...")
        {
            List<ReviewStatus> RetVal = new List<ReviewStatus>();
            ReviewStatus m_ReviewStatus = new ReviewStatus(ConStr);
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                if (OrderBy.Length > 0)
                {
                    RetVal = m_ReviewStatus.GetListOrderBy(OrderBy);
                }
                TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
                if (TextOptionAll.Length > 0)
                {
                    m_ReviewStatus.ReviewStatusName = TextOptionAll;
                    m_ReviewStatus.ReviewStatusDesc = TextOptionAll;
                    RetVal.Insert(0, m_ReviewStatus);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<ReviewStatus> Static_GetListAllOrderBy(string OrderBy, string TextOptionAll = "...")
        {
            return Static_GetListAllOrderBy("", OrderBy, TextOptionAll);
        }
        //--------------------------------------------------------------  
        public List<ReviewStatus> GetListByReviewStatusId(byte ReviewStatusId)
        {
            List<ReviewStatus> RetVal = new List<ReviewStatus>();
            try
            {
                if (ReviewStatusId > 0)
                {
                    string sql = "SELECT * FROM ReviewStatus WHERE (ReviewStatusId=" + ReviewStatusId.ToString() + ")";
                    SqlCommand cmd = new SqlCommand(sql);
                    RetVal = Init(cmd);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }

        //-------------------------------------------------------------- 
        public ReviewStatus Get(byte ReviewStatusId)
        {
            ReviewStatus RetVal = new ReviewStatus(db.ConnectionString);
            try
            {
                List<ReviewStatus> list = GetListByReviewStatusId(ReviewStatusId);
                if (list.Count > 0)
                {
                    RetVal = (ReviewStatus)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public ReviewStatus Get()
        {
            return Get(this.ReviewStatusId);
        }
        //-------------------------------------------------------------- 
        public static ReviewStatus Static_Get(byte ReviewStatusId)
        {
            ReviewStatus RetVal = new ReviewStatus();
            return RetVal.Get(ReviewStatusId);
        }
        //-----------------------------------------------------------------------------
        public static ReviewStatus Static_Get(int ReviewStatusId, List<ReviewStatus> lList)
        {
            ReviewStatus RetVal = new ReviewStatus();
            if (ReviewStatusId > 0 && lList.Count > 0)
            {
                RetVal = lList.Find(i => i.ReviewStatusId == ReviewStatusId);
                if (RetVal == null) RetVal = new ReviewStatus();
            }
            return RetVal;
        }
        //--------------------------------------------------------------
        public static string Static_GetDescByCulture(byte ReviewStatusId)
        {
            string RetVal = "";
            ReviewStatus m_ReviewStatus = new ReviewStatus();
            m_ReviewStatus = m_ReviewStatus.Get(ReviewStatusId);
            string culture = Thread.CurrentThread.CurrentCulture.Name;
            if (culture == LibConstants.CULTURE_VN)
            {
                RetVal = m_ReviewStatus.ReviewStatusDesc;
            }
            else
            {
                RetVal = m_ReviewStatus.ReviewStatusName;
            }
            return RetVal;
        }
        //--------------------------------------------------------------
    }
}
