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
    public abstract class BaseDataSource : IDataSource
    {
        // ===========================================================================
        // = Public Properties
        // ===========================================================================
        
        public String Name { get; protected set; }
        public ObservableCollection<IDataTable> Tables { get; protected set; }

        // ===========================================================================
        // = Public Properties - Views
        // ===========================================================================
        
        public CollectionViewSource<IDataTable> ViewSystemTables { get; private set; }
        public CollectionViewSource<IDataTable> ViewNonSystemTables { get; private set; }

        // ===========================================================================
        // = Construction
        // ===========================================================================
        
        public BaseDataSource()
        {
            // Create collections.
            Tables              = new ObservableCollection<IDataTable>();

            // Create views.
            ViewSystemTables    = new CollectionViewSource<IDataTable>(Tables, new [] { new SortDescription<IDataTable>(X => X.Name) }, X => X.Type == DataTableType.System);
            ViewNonSystemTables = new CollectionViewSource<IDataTable>(Tables, new [] { new SortDescription<IDataTable>(X => X.Name) }, X => X.Type != DataTableType.System);
        }
    }
}
