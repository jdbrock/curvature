using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curvature
{
    public static class ObjectExtensions
    {
        public static Boolean IsNull(this Object inThis)
        {
            return inThis == null || inThis == DBNull.Value;
        }
    }
}
