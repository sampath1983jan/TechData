using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tech.Data;

namespace Tech.DataAccess
{
    public class DataBase
    {
        private DBDatabase _database;

        public DBDatabase Database
        {
            get { return _database; }
        }
        public const string Schema = "talentozdev";
        public const string DbProvider = "MySql.Data.MySqlClient";
        public const string DbConnection = "Server=dell6;Initial Catalog=talentozdev;Uid=root;Pwd=admin312";
        public void InitDbs(string conn)
        {
            //Modify the connections to suit
            //Comment any databases that should not be executed against
            DBDatabase mysql = DBDatabase.Create("MySQL"
                                                    , conn
                                                    , DbProvider);
            this._database = mysql;
            this._database.HandleException += new DBExceptionHandler(database_HandleException);
        }

        void database_HandleException(object sender, DBExceptionEventArgs args)
        {
            System.Console.WriteLine("Database encountered an error : {0}", args.Message);
            //Not nescessary - but hey, validates it's writable.
            args.Handled = false;
        }
    }
}
