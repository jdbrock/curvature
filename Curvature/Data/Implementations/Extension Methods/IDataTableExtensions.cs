using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curvature
{
    public static class IDataTableExtensions
    {
        public static DataTable QueryDataTable(this IDataSource inThis, String inSql, params Object[] inParams)
        {
            using (var reader = inThis.Query(inSql, inParams))
            {
                var schema = reader.GetSchemaTable();
                var data = new DataTable();

                foreach (var schemaRow in schema.Rows.Cast<DataRow>())
                    data.Columns.Add((String)schemaRow["ColumnName"]);

                while (reader.Read())
                {
                    var values = new Object[reader.FieldCount];
                    reader.GetValues(values);

                    data.Rows.Add(values);
                }

                return data;
            }
        }
    }
}
