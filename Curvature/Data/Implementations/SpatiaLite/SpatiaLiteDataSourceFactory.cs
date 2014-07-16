using Microsoft.Win32;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curvature
{
    [ImplementPropertyChanged]
    public class SpatiaLiteDataSourceFactory : IDataSourceFactory
    {
        // ===========================================================================
        // = Public Properties
        // ===========================================================================
        
        public String Name { get { return "SpatiaLite 4.1.1"; } }

        // ===========================================================================
        // = Public Methods
        // ===========================================================================
        
        public void Initialise()
        {

        }

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
