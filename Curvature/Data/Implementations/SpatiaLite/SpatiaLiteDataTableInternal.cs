using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curvature
{
    public class SpatiaLiteDataTableInternal
    {
        // ===========================================================================
        // = Public Properties
        // ===========================================================================
        
        public String Type { get; private set; }
        public String Name { get; private set; }
        public String TableName { get; private set; }
        public Int64 RootPage { get; private set; }
        public String Sql { get; private set; }

        // ===========================================================================
        // = Construction
        // ===========================================================================
        
        public SpatiaLiteDataTableInternal(String inType, String inName, String inTableName, Int64 inRootPage, String inSql)
        {
            Type = inType;
            Name = inName;
            TableName = inTableName;
            RootPage = inRootPage;
            Sql = inSql;
        }
    }
}
