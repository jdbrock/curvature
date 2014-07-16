using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curvature
{
    public class SpatiaLiteDataSourceFactory : IDataSourceFactory
    {
        public String Name { get { return "SpatiaLite 4.1.1"; } }

        public IDataSource OpenInteractive()
        {
            var dialog = new OpenFileDialog()
            {
                Title = "Select Database",
                Filter = "SpatiaLite Databases (*.sqlite, *.db)|*.sqlite;*.db|All Files (*.*)|*.*"
            };

            if (!dialog.ShowDialog().GetValueOrDefault())
                return null;

            return new SpatiaLiteDataSource(dialog.FileName);
        }
    }
}
