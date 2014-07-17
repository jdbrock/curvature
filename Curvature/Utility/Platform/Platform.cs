using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curvature
{
    public static class Platform
    {
        public static String Code { get { return IntPtr.Size == 4 ? "x86" : "x64"; } }
    }
}
