using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tech.Data.Query;

namespace Tech.DataAccess.Tables
{
    public class Schools
    {
        public const string Table = "School";

        public static readonly DBColumn ID = DBColumn.Column("SchoolID", System.Data.DbType.Int64);
        public static readonly DBColumn Name = DBColumn.Column("Name", System.Data.DbType.String, 255, Data.DBColumnFlags.Nullable);
        public static readonly DBColumn Address = DBColumn.Column("Address", System.Data.DbType.String, 1255, Data.DBColumnFlags.Nullable);
        public static readonly DBColumn OwnerName = DBColumn.Column("OwnerName", System.Data.DbType.String, 255, Data.DBColumnFlags.Nullable);
        public static readonly DBColumn DateCreated = DBColumn.Column("LastUPD", System.Data.DbType.DateTime2,Data.DBColumnFlags.Nullable );
                
    }
}
