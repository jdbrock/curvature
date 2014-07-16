using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Curvature
{
    public interface IDataSource
    {
        String Name { get; }
        ObservableCollection<IDataTable> Tables { get; }
    }
}
