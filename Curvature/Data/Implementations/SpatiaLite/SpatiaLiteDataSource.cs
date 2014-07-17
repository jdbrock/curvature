using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Curvature
{
    [ImplementPropertyChanged]
    public class SpatiaLiteDataSource : BaseDataSource
    {
        // ===========================================================================
        // = Private Field
        // ===========================================================================
        
        private String _filePath;
        private SQLiteConnection _connection;

        // ===========================================================================
        // = Construction
        // ===========================================================================
        
        public SpatiaLiteDataSource(String inFilePath)
        {
            Name = Path.GetFileName(inFilePath);

            _filePath = inFilePath;

            Connect();
            ReadTables();
        }

        private void Connect()
        {
            // Set current working directory to the platform code (x86, x64, etc).
            var currentDirectory = Environment.CurrentDirectory;
            var exePath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            Environment.CurrentDirectory = Path.Combine(exePath, Platform.Code);

            // Open connection.
            _connection = new SQLiteConnection(String.Format("Data Source=\"{0}\"", _filePath));
            _connection.Open();

            // Load SpatiaLite extension.
            _connection.LoadExtension("libspatialite-4.dll");

            // Restore working directory.
            Environment.CurrentDirectory = currentDirectory;
        }

        private void ReadTables()
        {
            Tables.Clear();

            var tables = new List<SpatiaLiteDataTableInternal>();

            using (var reader = _connection.Query("SELECT type, name, tbl_name, rootpage, sql FROM sqlite_master WHERE type='table'"))
                while (reader.Read())
                    tables.Add(new SpatiaLiteDataTableInternal(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetInt64(3), reader.GetString(4)));

            foreach (var table in tables)
                Tables.Add(new SpatiaLiteDataTable(table.Name, GetTableType(table)));
        }

        private DataTableType GetTableType(SpatiaLiteDataTableInternal inInternalTable)
        {
            if (inInternalTable.Type != "table" || inInternalTable.Name == "sqlite_sequence" || inInternalTable.Sql.Trim().StartsWith("INSERT INDEX", StringComparison.OrdinalIgnoreCase) ||
                inInternalTable.Name == "geometry_columns" || inInternalTable.Name.StartsWith("idx_") || inInternalTable.Name == "spatial_ref_sys" || inInternalTable.Name == "geometry_columns_auth" ||
                inInternalTable.Name == "virts_geometry_columns" || inInternalTable.Name == "views_geometry_columns")
                return DataTableType.System;

            if (inInternalTable.Sql.Contains("POLYGON"))
                return DataTableType.Polygon;

            if (inInternalTable.Sql.Contains("POINT"))
                return DataTableType.Point;

            if (inInternalTable.Sql.Contains("LINESTRING"))
                return DataTableType.Line;

            return DataTableType.Basic;
        }
    }
}
