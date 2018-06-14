using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagerLib
{
    public class PaymentTypes
    {
        private byte _PaymentTypeId;

        private string _PaymentTypeName;

        private string _PaymentTypeDesc;

        private DBAccess db;

        public byte PaymentTypeId
        {
            get
            {
                return this._PaymentTypeId;
            }
            set
            {
                this._PaymentTypeId = value;
            }
        }

        public string PaymentTypeName
        {
            get
            {
                return this._PaymentTypeName;
            }
            set
            {
                this._PaymentTypeName = value;
            }
        }

        public string PaymentTypeDesc
        {
            get
            {
                return this._PaymentTypeDesc;
            }
            set
            {
                this._PaymentTypeDesc = value;
            }
        }

        public PaymentTypes()
        {
            this.db = new DBAccess();
        }

        public PaymentTypes(string constr)
        {
            this.db = ((constr.Length > 0) ? new DBAccess(constr) : new DBAccess());
        }

        ~PaymentTypes()
        {
        }

        public virtual void Dispose()
        {
        }

        private List<PaymentTypes> Init(SqlCommand cmd)
        {
            SqlConnection connection = this.db.GetConnection();
            cmd.Connection = connection;
            List<PaymentTypes> result = new List<PaymentTypes>();
            try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartDataReader = new SmartDataReader(reader);
                while (smartDataReader.Read())
                {
                    result.Add(new PaymentTypes
                    {
                        PaymentTypeDesc = smartDataReader.GetString("PaymentTypeDesc"),
                        PaymentTypeId = smartDataReader.GetByte("PaymentTypeId"),
                        PaymentTypeName = smartDataReader.GetString("PaymentTypeName")
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

        public List<PaymentTypes> GetAll()
        {
            List<PaymentTypes> result;
            try
            {
                string cmdText = "SELECT * FROM PaymentTypes";
                SqlCommand cmd = new SqlCommand(cmdText);
                result = this.Init(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public PaymentTypes Get(byte id)
        {
            PaymentTypes result;
            try
            {
                string cmdText = "SELECT * FROM PaymentTypes WHERE (PaymentTypeId=" + id + ")";
                List<PaymentTypes> list = this.Init(new SqlCommand(cmdText)
                {
                    CommandType = CommandType.Text
                });
                result = list.Count == 1 ? list[0] : new PaymentTypes();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public static PaymentTypes Static_Get(byte paymentTypeId, List<PaymentTypes> listPaymentTypes)
        {
            var retVal = listPaymentTypes.Find(x => x.PaymentTypeId == paymentTypeId) ?? new PaymentTypes();
            return retVal;
        }
    }
}
