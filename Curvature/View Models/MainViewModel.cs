using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Curvature
{
    [ImplementPropertyChanged]
    public class MainViewModel
    {
        public ObservableCollection<IDataSourceFactory> DataSourceFactories { get; private set; }
        public ObservableCollection<IDataSource> DataSources { get; private set; }

        public IDataTable SelectedTable { get; private set; }

        public ICommand AddDataSourceCommand { get; private set; }
        public ICommand SelectTableCommand { get; private set; }

        public DataView PreviewData { get; private set; }

        public MainViewModel()
        {
            DataSourceFactories = new ObservableCollection<IDataSourceFactory>();

            RegisterDataSourceFactories();

            DataSources = new ObservableCollection<IDataSource>();
            //DataSources.Add(new DummyDataSource());

            AddDataSourceCommand = new StandardCommand(DoAddDataSourceCommand);
            SelectTableCommand = new StandardCommand<IDataTable>(DoSelectTableCommand);
        }

        private void RegisterDataSourceFactories()
        {
            // Register SpatiaLite.
            var spatialite = new SpatiaLiteDataSourceFactory();
            DataSourceFactories.Add(spatialite);

            // Initialise.
            spatialite.Initialise();
        }

        private void DoAddDataSourceCommand(Object inParameter)
        {
            // TEMP
            var x = DataSourceFactories.First().OpenInteractive();

            if (x != null)
                DataSources.Add(x);
        }

        private void DoSelectTableCommand(IDataTable inParameter)
        {
            if (SelectedTable != null)
                SelectedTable.IsSelected = false;

            inParameter.IsSelected = true;
            SelectedTable = inParameter;

            RefreshDataPreview();
        }

        private void RefreshDataPreview()
        {
            PreviewData = SelectedTable.Source.QueryDataTable("SELECT * FROM {0} LIMIT 500", SelectedTable.Name).AsDataView();
        }
    }
}
