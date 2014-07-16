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
    public class SpatiaLiteDataSource : IDataSource
    {
        // ===========================================================================
        // = Public Properties
        // ===========================================================================
        
        public String Name { get; private set; }
        public ObservableCollection<IDataTable> Tables { get; private set; }

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
            Tables = new ObservableCollection<IDataTable>();

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

            var names = new List<String>();

            using (var reader = _connection.Query("SELECT name FROM sqlite_master WHERE type='table'"))
                while (reader.Read())
                    names.Add(reader.GetString(0));

            foreach (var name in names)
                Tables.Add(new SpatiaLiteDataTable(name, DataTableType.Basic));
        }     
    }
}
