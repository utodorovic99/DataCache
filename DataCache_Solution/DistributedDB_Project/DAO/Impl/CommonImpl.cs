using DistributedDB_Project.Connection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedDB_Project.DAO.Impl
{
    internal class CommonImpl
    {
        //Container class for all common methods
        internal static  bool ContainsPK(string query)
        {
            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    command.Prepare();
                    return command.ExecuteScalar() != null;

                }
            }
        }
    }
}
