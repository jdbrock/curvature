using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Curvature
{
    public interface IDataSource
    {
        String Name { get; }
        IList<IDataTable> Tables { get; }
    }
}
