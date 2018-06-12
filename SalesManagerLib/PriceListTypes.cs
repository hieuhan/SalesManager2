using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagerLib
{
    public class PriceListTypes
    {
        private byte _PriceListTypeId;

        private string _PriceListTypeName;

        private string _PriceListTypeDesc;

        private DBAccess db;

        public byte PriceListTypeId
        {
            get
            {
                return this._PriceListTypeId;
            }
            set
            {
                this._PriceListTypeId = value;
            }
        }

        public string PriceListTypeName
        {
            get
            {
                return this._PriceListTypeName;
            }
            set
            {
                this._PriceListTypeName = value;
            }
        }

        public string PriceListTypeDesc
        {
            get
            {
                return this._PriceListTypeDesc;
            }
            set
            {
                this._PriceListTypeDesc = value;
            }
        }

        public PriceListTypes()
        {
            this.db = new DBAccess();
        }

        public PriceListTypes(string constr)
        {
            this.db = ((constr.Length > 0) ? new DBAccess(constr) : new DBAccess());
        }

        ~PriceListTypes()
        {
        }

        public virtual void Dispose()
        {
        }

        private List<PriceListTypes> Init(SqlCommand cmd)
        {
            SqlConnection connection = this.db.GetConnection();
            cmd.Connection = connection;
            List<PriceListTypes> result = new List<PriceListTypes>();
            try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartDataReader = new SmartDataReader(reader);
                while (smartDataReader.Read())
                {
                    result.Add(new PriceListTypes
                    {
                        PriceListTypeDesc = smartDataReader.GetString("PriceListTypeDesc"),
                        PriceListTypeId = smartDataReader.GetByte("PriceListTypeId"),
                        PriceListTypeName = smartDataReader.GetString("PriceListTypeName")
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

        public List<PriceListTypes> GetAll()
        {
            List<PriceListTypes> result;
            try
            {
                string cmdText = "SELECT * FROM PriceListTypes";
                SqlCommand cmd = new SqlCommand(cmdText);
                result = this.Init(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public PriceListTypes Get(byte id)
        {
            PriceListTypes result;
            try
            {
                string cmdText = "SELECT * FROM PriceListTypes WHERE (PriceListTypeId=" + id + ")";
                List<PriceListTypes> list = this.Init(new SqlCommand(cmdText)
                {
                    CommandType = CommandType.Text
                });
                result = list.Count == 1 ? list[0] : new PriceListTypes();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public static PriceListTypes Static_Get(byte priceListTypeId, List<PriceListTypes> listStatus)
        {
            var retVal = listStatus.Find(x => x.PriceListTypeId == priceListTypeId) ?? new PriceListTypes();
            return retVal;
        }
    }
}
