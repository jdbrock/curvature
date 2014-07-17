using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curvature
{
    [ImplementPropertyChanged]
    public class SpatiaLiteDataTable : IDataTable
    {
        // ===========================================================================
        // = Public Properties
        // ===========================================================================

        public IDataSource Source { get; private set; }
        public String Name { get; private set; }
        public DataTableType Type { get; private set; }

        public Boolean IsSelected { get; set; }

        // ===========================================================================
        // = Construction
        // ===========================================================================
        
        public SpatiaLiteDataTable(IDataSource inSource, String inName, DataTableType inType)
        {
            Source = inSource;
            Name = inName;
            Type = inType;
        }
    }
}
