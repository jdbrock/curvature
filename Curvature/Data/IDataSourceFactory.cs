using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curvature
{
    public interface IDataSourceFactory
    {
        String Name { get; }

        void Initialise();
        IDataSource OpenInteractive();
    }
}
