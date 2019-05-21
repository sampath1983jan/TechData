using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tech.Data;
using Tech.Data.Query;
using Tech.QScript.Syntax;
using System.Data;
namespace Tech.QScript.Domain.QData
{
    public class DataParser: DataBase
    {
        private string query;
        public string Query => query;
        private DataTable dataTable;
        public System.Data.DataTable Datatable => dataTable;
        public DataParser(string conn) {
            base.InitDbs(conn);
        }

        public void GetDatatable(Tech.QScript.Syntax.SQuery sQuery) {
            DBSelectQuery select = DBQuery.Select();
            foreach (Syntax.Query.Table t in sQuery.tables) {
                foreach (Syntax.Query.TableField tf in t.Fields) {
                      select.Field(t.TableName, tf.FieldName).As(tf.FieldName);
                }
                foreach (Syntax.Query.Where where in sQuery.Conditions) {
                    if (where.Type == Syntax.Query.ConditionType.relation)
                    {
                        if (where.JoinType == Syntax.Query.JoinType.And)
                        {
                            select.And(where.lefttable, where.LeftField, Data.Compare.Equals, where.RightTable, where.RightField);
                        }
                        else if (where.JoinType == Syntax.Query.JoinType.or)
                        {
                            DBTable lft = DBTable.Table(where.lefttable);
                            DBTable rgt = DBTable.Table(where.RightTable);
                            DBField lff = DBField.Field(where.lefttable, where.LeftField);
                            DBField rff = DBField.Field(where.RightTable, where.RightField);
                            select.OrWhere(lff, Data.Compare.Equals, rff);

                            // DBComparison joined = DBComparison.Compare(DBClause);
                            //select.InnerJoin(where.lefttable).As (where.lefttable)
                            //     .On(where.lefttable,where.LeftField,Data.Compare.Equals,where.RightTable,where.RightField)

                            //select.OrWhere(where.lefttable,where.LeftField,Data.Compare.Equals, )
                            //select.OrWhere (where.lefttable, where.LeftField, Data.Compare.Equals, where.RightTable, where.RightField);
                        }
                        else if (where.JoinType == Syntax.Query.JoinType.None)
                        {
                            DBField lff = DBField.Field(where.lefttable, where.LeftField);
                            DBField rff = DBField.Field(where.RightTable, where.RightField);
                            DBComparison d = DBComparison.Equal(lff, rff);
                            select.WhereNone(d);
                        }
                    }
                    else if (where.Type == Syntax.Query.ConditionType.filter) {
                        if (where.JoinType == Syntax.Query.JoinType.And)
                        {
                            select.WhereField(where.lefttable, where.LeftField, Data.Compare.Equals, DBConst.String(where.Value));
                        }
                        else if (where.JoinType == Syntax.Query.JoinType.or)
                        {
                            DBField lff = DBField.Field(where.lefttable, where.LeftField);
                            DBField rff = DBField.Field(where.RightTable, where.RightField);
                            DBComparison d = DBComparison.Equal(lff, DBConst.String(where.Value));

                            select.OrWhere(d);
                        }
                        else if (where.JoinType == Syntax.Query.JoinType.None) {
                            DBField lff = DBField.Field(where.lefttable, where.LeftField);
                            DBField rff = DBField.Field(where.RightTable, where.RightField);
                            DBComparison d = DBComparison.Equal(lff, DBConst.String(where.Value));
                            select.OrWhereNone(d);
                        }                        
                    }
                }                
            }
           var data= Database.GetDatatable(select);
            query = select.ToSQLString(Database); 
            dataTable
                = (System.Data.DataTable)data;
        }

        public void SelectInto(Tech.QScript.Syntax.SQuery sQuery) {

            //sQuery.dtResult.DefaultView.ToTable(true,{ });
            //DBSelectQuery select = DBQuery.Select("");
            //DBInsertQuery ins = DBQuery.InsertInto("Categories")
            //                         .Fields("CategoryName", "Description", "Picture")
            //                         .Select("");


        }



    }
}

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

public class Cause : DBClause
{
    protected override string XmlElementName => throw new NotImplementedException();

    public override bool BuildStatement(DBStatementBuilder builder)
    {
        throw new NotImplementedException();
    }
}

