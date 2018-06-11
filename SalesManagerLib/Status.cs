using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagerLib
{
    public class Status
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

        public Status()
        {
            this.db = new DBAccess();
        }

        public Status(string constr)
        {
            this.db = ((constr.Length > 0) ? new DBAccess(constr) : new DBAccess());
        }

        ~Status()
        {
        }

        public virtual void Dispose()
        {
        }

        private List<Status> Init(SqlCommand cmd)
        {
            SqlConnection connection = this.db.GetConnection();
            cmd.Connection = connection;
            List<Status> result = new List<Status>();
            try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartDataReader = new SmartDataReader(reader);
                while (smartDataReader.Read())
                {
                    result.Add(new Status
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

        public List<Status> GetAll()
        {
            List<Status> result;
            try
            {
                string cmdText = "SELECT * FROM Status";
                SqlCommand cmd = new SqlCommand(cmdText);
                result = this.Init(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public Status Get(byte id)
        {
            Status result;
            try
            {
                string cmdText = "SELECT * FROM Status WHERE (StatusId=" + id + ")";
                List<Status> list = this.Init(new SqlCommand(cmdText)
                {
                    CommandType = CommandType.Text
                });
                result = list.Count == 1 ? list[0] : new Status();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public static Status Static_Get(byte stausId, List<Status> listStatus)
        {
            var retVal = listStatus.Find(x => x.StatusId == stausId) ?? new Status();
            return retVal;
        }
    }
}
