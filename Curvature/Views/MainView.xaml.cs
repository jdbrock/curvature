using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Curvature.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Page
    {
        public MainView()
        {
            InitializeComponent();

            DataContext = new MainViewModel();
        }

        private void SystemTablesFilter(Object inSender, FilterEventArgs inArgs)
        {
            if (!(inArgs.Item is IDataTable))
            {
                inArgs.Accepted = true;
                return;
            }

            var item = (IDataTable)inArgs.Item;

            inArgs.Accepted = item.Type == DataTableType.System;
        }

        private void NonSystemTablesFilter(Object inSender, FilterEventArgs inArgs)
        {
            if (!(inArgs.Item is IDataTable))
            {
                inArgs.Accepted = true;
                return;
            }

            var item = (IDataTable)inArgs.Item;

            inArgs.Accepted = item.Type != DataTableType.System;
        }
    }
}
