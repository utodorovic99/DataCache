using DistributedDB_Project.Connection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedDB_Project.DAO.Impl
{
    internal enum ETableType : int
    {
        Consumption,
        Audit,
        EES,
        Geography,
    }

    internal class CommonImpl
    {


        internal static bool ContainsPK(string key, ETableType tableType)
        {
            string query = "";
            switch (tableType)
            {
                case ETableType.Consumption: 
                    {
                        query = "SELECT CID FROM CONSUMPTION";
                        break; 
                    }
                case ETableType.Audit: 
                    {
                        query = "SELECT AID FROM CONSUMPTION_AUDIT";
                        break; 
                    }
                case ETableType.EES: 
                    {
                        query = "SELECT RECID FROM EES";
                        break; 
                    }
                case ETableType.Geography:
                    {
                        query = "SELECT GID FROM GEOGRAPHY_SUBSYSTEM";
                        break;
                    }
            }

            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    command.Prepare();
                    return command.ExecuteScalar().ToString() != "";
                }
            }
        }
 
    }
}
