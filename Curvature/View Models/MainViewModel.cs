using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Curvature
{
    [ImplementPropertyChanged]
    public class MainViewModel
    {
        public IList<IDataSourceFactory> DataSourceFactories { get; private set; }
        public IList<IDataSource> DataSources { get; private set; }

        public ICommand AddDataSourceCommand { get; private set; }

        public MainViewModel()
        {
            DataSourceFactories = new List<IDataSourceFactory>();
            DataSourceFactories.Add(new SpatiaLiteDataSourceFactory());

            DataSources = new List<IDataSource>();
            DataSources.Add(new DummyDataSource());

            AddDataSourceCommand = new StandardCommand(DoAddDataSourceCommand);
        }

        private void DoAddDataSourceCommand(Object inParameter)
        {
            // TEMP
            var x = DataSourceFactories.First().OpenInteractive();

            if (x != null)
                DataSources.Add(x);
        }
    }
}
