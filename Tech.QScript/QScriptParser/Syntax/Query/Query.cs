using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Tech.QScript.Syntax.Query;

namespace Tech.QScript.Syntax
{
    public enum QueryType {
        select,
        put,
        push,
        selectinto,
        Cases,
    }
    //  get(asdfsd:[userid:"aaaa", username:sd, F_200005:"sdd"] : aaa join b:[positionid,
    //positionname] with[b:userid equalto c:userid] join c:[status] with[c:userid equalto d:userid, d:positionid equalto a:positionid])
    public class QueryDefination {
        //https://stackoverflow.com/questions/133601/can-regular-expressions-be-used-to-match-nested-patterns
        public static string getdata= @"(?<=\().*(?=\))"; // getdata(get[tb1:UserID,tb1.UserName],from(tb1),and[],or) - end to end bracket
        public static string get = @"get(?<=\().*(?=\))";  // @"get(\[(?:\[[^\]]*\]|[^\[\]]*)*\])";
        public static string from = @"from(\[(?:\[[^\]]*\]|[^\[\]]*)*\])";
        public static string andcondition = @"and(\[(?:\[[^\]]*\]|[^\[\]]*)*\])";
        public static string andorconditionExtract = @"\((?:(?:p(1))|(?:[^()]))*\)";
        public static string orcondition= @"or(\[(?:\[[^\]]*\]|[^\[\]]*)*\])";
        public static string getFields = @"\w*?:\w*";
        public static string getinto = @"(?<=\().*(?=\))";
        public static string push = @"(?<=\().*(?=\))";
        public static string Case = @"(?<=\().*(?=\))";

        public static string getTableWithField = @"\w*[[:space:]]?:[[:space:]]?\[([^]]*)\]([[:space:]]?:[[:space:]]?[a-z]*)?";
        public static string getTableRelation = @"with[[:space:]]?\[([^]]*)\]";
        public static string splitTableName = @"(?![^)(]*\([^)(]*?\)\)):(?![^\[]*\])"; // split with outer :

    }

    public class SQuery:SyntaxNode  ,Element
    {
        public string Name;
        public QueryType Type;        
        private From _from;
        private List<Where> _conditions;
        private List<Table> _tables;
        private Declare _assignTo;
        public Declare AssignTo { get => _assignTo; }
        public List<Table> tables { get{return _tables;} }
        public From from { get { return _from; } }        
        public List<Where> Conditions { get { return _conditions; } }        
        public override SyntaxCatagory Catagory => SyntaxCatagory.Query;
        public override TokenKind Kind =>  TokenKind.Get;

        public dynamic EvalutionResult => throw new NotImplementedException();

        public System.Data.DataTable dtResult;
        public string dtVariableName;
        public SQuery(Declare assignTo, QueryType type,QScript.Source.SourceSpan span,string name=""):base(span) {        
            Type = type;
            Name = name;            
            _tables = new List<Table>();
            _from = new From();
            _conditions = new List<Where>();
            _assignTo = assignTo;
        }        
        public void addGetInto(System.Data.DataTable dt, string name) {
            this.Name = name;
            dtResult = dt;
        }
        public void addCase(string dtVName, string name) {
            this.Name = name;
            dtVariableName = dtVName;
        }
        public void addGetInto(string dtVName, string name)
        {
            this.Name = name;
            dtVariableName= dtVName;
        }       
        public SQuery From(params string[] tables) {
            for (int i = 0; i < tables.Length; i++) {
                this._from.AddTable(tables[i]);
            }
            return this;
        }
        public SQuery AddTableField(string tbName, 
            string fieldName,string fieldValue ="",string tbAlias ="")
        {
            if (tbAlias == "") {
                tbAlias = tbName;
            }
            var tbl = this.tables.Where(x => x.TableName == tbName || x.TableAlias == tbAlias).ToList();
            if (this.tables.Where(x => x.TableName == tbName || x.TableAlias == tbName).ToList().Count > 0)
            {
                tbl[0].AddField(new TableField() { FieldName = fieldName,FieldValue=fieldValue });
            }
            else
            {
                this.tables.Add(new Table() { TableName = tbName, TableAlias = tbAlias }
                .AddField(new TableField() { FieldName = fieldName, FieldValue = fieldValue }));
            }
            return this;
        }
        public SQuery AddRelation(string basetable, string lefttb, string leftfld, string righttb, string rightfld, WhereOperators operators) {
            var tbl = this.tables.Where(x => x.TableName == basetable || x.TableAlias == basetable).ToList();
            if (tbl.Count > 0) {
                tbl[0].AddRelation(lefttb, leftfld, righttb, rightfld, operators);
            }
            return this;
        }
        public SQuery And(string lefttb,
            string leftField,
            string righttb, 
            string rightField,
            WhereOperators opt) {
            Conditions.Add(new Query.Where() { LeftField = leftField, lefttable = lefttb,
                RightTable = righttb, RightField = rightField, Operator = opt, Value = "", JoinType =  JoinType.And,
                Type = ConditionType.relation
            });
            return this;
        }

        public SQuery And(string lefttb,
            string leftField, 
            string value,
            WhereOperators opt,string defaultValue="") {
            Conditions.Add(new Query.Where() { LeftField = leftField, lefttable = lefttb,
                RightTable = "", RightField = "", Operator = opt, Value = value, JoinType = JoinType.And, DefaultValue=defaultValue });
            return this;
        }

        

        public SQuery Or(string lefttb, 
            string leftField, 
            string righttb,
            string rightField, 
            WhereOperators opt)
        {
            Conditions.Add(new Query.Where() { LeftField = leftField, lefttable = lefttb, RightTable = righttb,
                RightField = rightField, Operator = opt, Value = "", JoinType = JoinType.or,
                Type = ConditionType.relation
            });
            return this;
        }

        public SQuery Or(string lefttb,
            string leftField,
            string value, 
            WhereOperators opt, string defaultValue = "")
        {
            Conditions.Add(new Query.Where() { LeftField = leftField, lefttable = lefttb,
                RightTable = "", RightField = "", Operator = opt, Value = value, JoinType = JoinType.or, DefaultValue= defaultValue
            });
            return this;
        }

        public SQuery None(string lefttb,
            string leftField,
            string righttb, 
            string rightField,
            WhereOperators opt,
            JoinType joinType= JoinType.None) {
            Conditions.Add(new Query.Where() { LeftField = leftField, lefttable = lefttb,
                RightTable = righttb, RightField = rightField, Operator = opt, Value = "", JoinType = JoinType.None, Type= ConditionType.relation });
            return this;
        }
        public SQuery None(string lefttb,
            string leftField, 
            string value,
            WhereOperators opt,
            JoinType joinType = JoinType.None)
        {
            Conditions.Add(new Query.Where() { LeftField = leftField, lefttable = lefttb, RightTable = "", RightField = "", Operator = opt, Value = value, JoinType = JoinType.None  });
            return this;
        }

        

        public void setResult(string name, object Right)
        {
            throw new NotImplementedException();
        }

        public void Accept(IVisitor visitor, EvaluationParam evaluation)
        {
            visitor.Visit(this,evaluation);
        }
    }


}
