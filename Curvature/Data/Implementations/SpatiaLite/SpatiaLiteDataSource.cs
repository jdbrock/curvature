using PropertyChanged;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curvature
{
    [ImplementPropertyChanged]
    public class SpatiaLiteDataSource : IDataSource
    {
        public String Name { get; private set; }
        public IList<IDataTable> Tables { get; private set; }

        public SpatiaLiteDataSource(String inFileName)
        {
            Name = Path.GetFileName(inFileName);
        }
    }
}
