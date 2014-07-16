using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curvature
{
    [ImplementPropertyChanged]
    public class DummyDataSource : IDataSource
    {
        public String Name { get; private set; }
        public ObservableCollection<IDataTable> Tables { get; private set; }

        public DummyDataSource()
        {
            Name = "Network.sqlite";

            Tables = new ObservableCollection<IDataTable>
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
