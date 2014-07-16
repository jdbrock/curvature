using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curvature
{
    public static class IDbConnectionExtensions
    {
        public static IDataReader Query(this IDbConnection inConnection, String inText, params Object[] inParams)
        {
            using (var command = inConnection.CreateCommand())
            {
                command.CommandText = String.Format(inText, inParams);
                return command.ExecuteReader();
            }
        }
    }
}
