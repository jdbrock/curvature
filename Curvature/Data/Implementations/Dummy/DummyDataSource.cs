using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curvature
{
    [ImplementPropertyChanged]
    public class DummyDataSource : IDataSource
    {
        public String Name { get; private set; }
        public IList<IDataTable> Tables { get; private set; }

        public DummyDataSource()
        {
            Name = "Network.sqlite";

            Tables = new List<IDataTable>
            {
                new DummyDataTable("CableLV", DataTableType.Line),
                new DummyDataTable("CableHV", DataTableType.Line),
                new DummyDataTable("CableEHV", DataTableType.Line),
                new DummyDataTable("Switch", DataTableType.Point),
                new DummyDataTable("Zone", DataTableType.Polygon)
            };
        }
    }
}
