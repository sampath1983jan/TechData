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
    public class DataParser: DB
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
            bool isAdded = false;
             foreach (Syntax.Query.Table t in sQuery.tables) {
            foreach (Syntax.Query.TableField  tf in t.Fields ) {
                    if (t.TableAlias != "")
                    {
                        select.Field(t.TableAlias.Trim(), tf.FieldName.Trim()).As(tf.FieldName.Trim());
                    }
                    else {
                        select.Field(t.TableName.Trim(), tf.FieldName.Trim()).As(tf.FieldName.Trim());
                    }
                    
                }
               
             }
            foreach (Syntax.Query.Table t in sQuery.tables)
            {
                if (t.Relations.Count == 0)
                {
                    if (isAdded == false)
                    {
                        select.From(t.TableName.Trim()).As(t.TableAlias.Trim());
                        isAdded = true;
                    }
                }
                isAdded = false;
                int indx = 0;
                foreach (Syntax.Query.Relation r in t.Relations)
                {
                    if (indx == 0)
                    {
                        select.InnerJoin(t.TableName.Trim()).As(t.TableAlias.Trim()).On(r.lefttable.Trim(), r.LeftField.Trim(), GetCompare(r.Operator), r.RightTable.Trim(), r.RightField.Trim());
                    }
                    else if (t.Relations.Count > 1)
                    {
                        select.And(r.lefttable.Trim(), r.LeftField.Trim(), GetCompare(r.Operator), r.RightTable.Trim(), r.RightField.Trim());
                    }
                    indx = indx + 1;
                }               
            }
            //foreach (Syntax.Query.Table t in sQuery.from.Tables )
            //{
            //    select.From(t.TableName ).As(t.TableName );
            //}
            int index = 0;
            foreach (Syntax.Query.Where where in sQuery.Conditions)
            {
                if (where.Type == Syntax.Query.ConditionType.filter)
                {
                    if (where.JoinType == Syntax.Query.JoinType.And)
                    {                        
                        if (index == 0)
                        {
                            select.WhereField(where.lefttable.Trim(), where.LeftField.Trim(), GetCompare(where.Operator), DBConst.String(getValue(where.Operator, where.Value.Trim())));
                        }
                        else
                        {
                            select.AndWhere(where.lefttable.Trim(), where.LeftField.Trim(), GetCompare(where.Operator), DBConst.String(getValue(where.Operator, where.Value.Trim())));
                        }
                    }
                    else if (where.JoinType == Syntax.Query.JoinType.or)
                    {
                        DBField lff = DBField.Field(where.lefttable, where.LeftField.Trim());
                        DBField rff = DBField.Field(where.RightTable, where.RightField.Trim());
                        DBComparison d = DBComparison.Equal(lff, DBConst.String(where.Value.Trim()));

                        select.OrWhere(d);
                    }
                    else if (where.JoinType == Syntax.Query.JoinType.None)
                    {
                        DBField lff = DBField.Field(where.lefttable, where.LeftField.Trim());
                        DBField rff = DBField.Field(where.RightTable, where.RightField.Trim());
                        DBComparison d = DBComparison.Equal(lff, DBConst.String(where.Value.Trim()));
                        select.OrWhereNone(d);
                    }
                    index = index + 1;
                }
            }                          
            var data= Database.GetDatatable(select);
            query = select.ToSQLString(Database); 
            dataTable
                = (System.Data.DataTable)data;
        }
        private Data.Compare GetCompare(Syntax.Query.WhereOperators opr) {
            if (opr == Syntax.Query.WhereOperators.Equal)
            {
                return Compare.Equals;
            }
            switch (opr){
                case Syntax.Query.WhereOperators.Equal:
                    return Compare.Equals;
                case Syntax.Query.WhereOperators.notEqual:
                    return Compare.NotEqual;
                case Syntax.Query.WhereOperators.In:
                    return Compare.In;
                case Syntax.Query.WhereOperators.NotIn:
                    return Compare.NotIn;
                case Syntax.Query.WhereOperators.start:
                    return Compare.Like;
                case Syntax.Query.WhereOperators.end:
                    return Compare.Like;
                case Syntax.Query.WhereOperators.greater:
                    return Compare.GreaterThan;
                case Syntax.Query.WhereOperators.greaterthan:
                    return Compare.GreaterThanEqual;
                case Syntax.Query.WhereOperators.lesser:
                    return Compare.LessThan;
                case Syntax.Query.WhereOperators.lesserthan:
                    return Compare.LessThanEqual;
                case Syntax.Query.WhereOperators.Like:
                    return Compare.Like;
                default:
                    return Compare.Equals;                                       
                }
        }
        private string getValue(Syntax.Query.WhereOperators d, string val) {
            if (d == Syntax.Query.WhereOperators.Like)
            {
                return "%" + val + "%";
            }
            else if (d == Syntax.Query.WhereOperators.end)
            {
                return  "%" + val;
            }
            else if (d == Syntax.Query.WhereOperators.start) {
                return val + "%";
            }
            else
            {
                return val;
            }
        }

        public void SelectInto(Tech.QScript.Syntax.SQuery sQuery) {

            //sQuery.dtResult.DefaultView.ToTable(true,{ });
            //DBSelectQuery select = DBQuery.Select("");
            //DBInsertQuery ins = DBQuery.InsertInto("Categories")
            //                         .Fields("CategoryName", "Description", "Picture")
            //                         .Select("");


        }

        public void putData(Tech.QScript.Syntax.SQuery sQuery) {

        }



    }

      public class DB
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

        public override bool BuildStatement(DBStatementBuilder builder, bool isInorNot = false)
        {
            throw new NotImplementedException();
        }
    }
}






//if (where.Type == Syntax.Query.ConditionType.relation)
//{
//    DBTable lft = DBTable.Table(where.lefttable);
//    DBTable rgt = DBTable.Table(where.RightTable);
//    DBField lff = DBField.Field(where.lefttable, where.LeftField);
//    DBField rff = DBField.Field(where.RightTable, where.RightField);
//    if (index == 0)
//    {
//        select.From(lft.Name).InnerJoin(rgt.Name).On(lft.Name, lff.Name, Data.Compare.Equals, rgt.Name, rff.Name);
//    }
//    else {
//        select.InnerJoin(rgt.Name).On(lft.Name, lff.Name, Data.Compare.Equals, rgt.Name, rff.Name);
//    }                    
//    //if (where.JoinType == Syntax.Query.JoinType.And)
//    //{
//    //    DBTable lft = DBTable.Table(where.lefttable);
//    //    DBTable rgt = DBTable.Table(where.RightTable);
//    //    DBField lff = DBField.Field(where.lefttable, where.LeftField);
//    //    DBField rff = DBField.Field(where.RightTable, where.RightField);
//    //    if (index == 0)
//    //    {
//    //        select.Where(lff, Data.Compare.Equals, rff);
//    //    }
//    //    else
//    //    {
//    //        select.AndWhere(lff, Data.Compare.Equals, rff);
//    //    }
//    //    index = index + 1;

//    //}
//    //else if (where.JoinType == Syntax.Query.JoinType.or)
//    //{
//    //    DBTable lft = DBTable.Table(where.lefttable);
//    //    DBTable rgt = DBTable.Table(where.RightTable);
//    //    DBField lff = DBField.Field(where.lefttable, where.LeftField);
//    //    DBField rff = DBField.Field(where.RightTable, where.RightField);
//    //    select.OrWhere(lff, Data.Compare.Equals, rff);

//    //    // DBComparison joined = DBComparison.Compare(DBClause);
//    //    //select.InnerJoin(where.lefttable).As (where.lefttable)
//    //    //     .On(where.lefttable,where.LeftField,Data.Compare.Equals,where.RightTable,where.RightField)

//    //    //select.OrWhere(where.lefttable,where.LeftField,Data.Compare.Equals, )
//    //    //select.OrWhere (where.lefttable, where.LeftField, Data.Compare.Equals, where.RightTable, where.RightField);
//    //}
//    //else if (where.JoinType == Syntax.Query.JoinType.None)
//    //{
//    //    DBField lff = DBField.Field(where.lefttable, where.LeftField);
//    //    DBField rff = DBField.Field(where.RightTable, where.RightField);
//    //    DBComparison d = DBComparison.Equal(lff, rff);
//    //    select.WhereNone(d);
//    //}
//}
//else if (where.Type == Syntax.Query.ConditionType.filter)
//{
//    if (where.JoinType == Syntax.Query.JoinType.And)
//    {
//        if (index == 0)
//        {
//            select.WhereField(where.lefttable, where.LeftField, Data.Compare.Equals, DBConst.String(where.Value));
//        }
//        else
//        {
//            select.WhereField(where.lefttable, where.LeftField, Data.Compare.Equals, DBConst.String(where.Value));
//        }
//    }
//    else if (where.JoinType == Syntax.Query.JoinType.or)
//    {
//        DBField lff = DBField.Field(where.lefttable, where.LeftField);
//        DBField rff = DBField.Field(where.RightTable, where.RightField);
//        DBComparison d = DBComparison.Equal(lff, DBConst.String(where.Value));

//        select.OrWhere(d);
//    }
//    else if (where.JoinType == Syntax.Query.JoinType.None)
//    {
//        DBField lff = DBField.Field(where.lefttable, where.LeftField);
//        DBField rff = DBField.Field(where.RightTable, where.RightField);
//        DBComparison d = DBComparison.Equal(lff, DBConst.String(where.Value));
//        select.OrWhereNone(d);
//    }
//    index = index + 1;
//}
//}                