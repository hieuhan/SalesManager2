using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagerLib
{
    public class BillStatus
    {
        private byte _StatusId;

        private string _StatusName;

        private string _StatusDesc;

        private DBAccess db;

        public byte StatusId
        {
            get
            {
                return this._StatusId;
            }
            set
            {
                this._StatusId = value;
            }
        }

        public string StatusName
        {
            get
            {
                return this._StatusName;
            }
            set
            {
                this._StatusName = value;
            }
        }

        public string StatusDesc
        {
            get
            {
                return this._StatusDesc;
            }
            set
            {
                this._StatusDesc = value;
            }
        }

        public BillStatus()
        {
            this.db = new DBAccess();
        }

        public BillStatus(string constr)
        {
            this.db = ((constr.Length > 0) ? new DBAccess(constr) : new DBAccess());
        }

        ~BillStatus()
        {
        }

        public virtual void Dispose()
        {
        }

        private List<BillStatus> Init(SqlCommand cmd)
        {
            SqlConnection connection = this.db.GetConnection();
            cmd.Connection = connection;
            List<BillStatus> result = new List<BillStatus>();
            try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartDataReader = new SmartDataReader(reader);
                while (smartDataReader.Read())
                {
                    result.Add(new BillStatus
                    {
                        StatusDesc = smartDataReader.GetString("StatusDesc"),
                        StatusId = smartDataReader.GetByte("StatusId"),
                        StatusName = smartDataReader.GetString("StatusName")
                    });
                }
                smartDataReader.disposeReader(reader);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                this.db.closeConnection(connection);
            }
            return result;
        }

        public List<BillStatus> GetAll()
        {
            List<BillStatus> result;
            try
            {
                string cmdText = "SELECT * FROM BillStatus";
                SqlCommand cmd = new SqlCommand(cmdText);
                result = this.Init(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public BillStatus Get(byte id)
        {
            BillStatus result;
            try
            {
                string cmdText = "SELECT * FROM BillStatus WHERE (StatusId=" + id + ")";
                List<BillStatus> list = this.Init(new SqlCommand(cmdText)
                {
                    CommandType = CommandType.Text
                });
                result = list.Count == 1 ? list[0] : new BillStatus();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public static BillStatus Static_Get(byte stausId, List<BillStatus> listStatus)
        {
            var retVal = listStatus.Find(x => x.StatusId == stausId) ?? new BillStatus();
            return retVal;
        }
    }
}
