using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagerLib
{
    public class WarehouseStatus
    {
        private DBAccess db;
        private byte _WarehouseStatusId;
        private string _WarehouseStatusName;
        private string _WarehouseStatusDesc;
        public byte WarehouseStatusId
        {
            get { return _WarehouseStatusId; }
            set { _WarehouseStatusId = value; }
        }
        public string WarehouseStatusName
        {
            get { return _WarehouseStatusName; }
            set { _WarehouseStatusName = value; }
        }
        public string WarehouseStatusDesc
        {
            get { return _WarehouseStatusDesc; }
            set { _WarehouseStatusDesc = value; }
        }

        public WarehouseStatus()
        {
            this.db = new DBAccess();
        }

        public WarehouseStatus(string constr)
        {
            this.db = ((constr.Length > 0) ? new DBAccess(constr) : new DBAccess());
        }

        ~WarehouseStatus()
        {
        }

        public virtual void Dispose()
        {
        }

        private List<WarehouseStatus> Init(SqlCommand cmd)
        {
            SqlConnection connection = this.db.GetConnection();
            cmd.Connection = connection;
            List<WarehouseStatus> result = new List<WarehouseStatus>();
            try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartDataReader = new SmartDataReader(reader);
                while (smartDataReader.Read())
                {
                    result.Add(new WarehouseStatus
                    {
                        WarehouseStatusDesc = smartDataReader.GetString("WarehouseStatusDesc"),
                        WarehouseStatusId = smartDataReader.GetByte("WarehouseStatusId"),
                        WarehouseStatusName = smartDataReader.GetString("WarehouseStatusName")
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

        public List<WarehouseStatus> GetAll()
        {
            List<WarehouseStatus> result;
            try
            {
                string cmdText = "SELECT * FROM WarehouseStatus";
                SqlCommand cmd = new SqlCommand(cmdText);
                result = this.Init(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public WarehouseStatus Get(byte id)
        {
            WarehouseStatus result;
            try
            {
                string cmdText = "SELECT * FROM WarehouseStatus WHERE (WarehouseStatusId=" + id + ")";
                List<WarehouseStatus> list = this.Init(new SqlCommand(cmdText)
                {
                    CommandType = CommandType.Text
                });
                result = list.Count == 1 ? list[0] : new WarehouseStatus();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public static WarehouseStatus Static_Get(int warehouseStatusId, List<WarehouseStatus> listWarehouseStatus)
        {
            var retVal = listWarehouseStatus.Find(x => x.WarehouseStatusId == warehouseStatusId) ?? new WarehouseStatus();
            return retVal;
        }
    }
}
