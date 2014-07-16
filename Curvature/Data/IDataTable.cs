using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Curvature
{
    public interface IDataTable
    {
        String Name { get; }
        DataTableType Type { get; }
    }
}
