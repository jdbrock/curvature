using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;

namespace Curvature
{
    public interface IDataSource
    {
        String Name { get; }
        ObservableCollection<IDataTable> Tables { get; }

        IDataReader Query(String inSql, params Object[] inParams);
    }
}
