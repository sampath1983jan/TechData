using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Tech.QScript.Syntax.Query;
using Tech.QScript.Source;

namespace Tech.QScript.Syntax
{
    public enum FunctionType {
        Filter,
        Merge,
        Count,
        UCount,
        Avg,
        Min,
        Sum,
        Max,
        Mean,
        Mode,
        Range,
        Medium,
        GetValue,
        GetValues,
        toString,
        Expr,
        Pivot,
        Join,
    }
    public enum AggregateType {
        none,
        count,
        ucount,
        min,
        max,
        avg,
        mode,
        sum,
        mean,
        medium,
    }
    public class FunDefination {
        public static string fun = @"(?<=\().*(?=\))"; // extract from first bracket;
        public static string splitbyComma = @"(?![^)(]*\([^)(]*?\)\)),(?![^\[]*\])";
        public static string row = @"(row|Row):[[\w,]*]";
        public static string column = @"(column|Column):[[\w,]*]";
        public static string agg = @"(aggr|Aggr):[[\w:,]*]";
    }
    public class Fun : SyntaxNode, IFunction,Element
    {
        public string Name;
        public string Datasource;
        public string ActionField;
        public FunctionType Type;
        private Object Data;

        private List<ParamFields> _arguments;
        private TokenKind tokenKind;
        private Declare _assignTo;
        public Declare AssignTo  {get => _assignTo; }
        public List<ParamFields> Arguments { get => _arguments; }
        public override SyntaxCatagory Catagory =>  SyntaxCatagory.function;
        public override TokenKind Kind =>  tokenKind;
        public dynamic EvalutionResult => throw new NotImplementedException();
        public Fun( Declare assignTo, FunctionType type,string datasource, SourceSpan span, string name =""):base(span) {
            Type = type;
            Datasource = datasource;
            Name = name;
            _arguments = new List<ParamFields>();
            _assignTo= assignTo;
            setType();
        }
        public void SetData(Object data) {
            Data = data;
        }
        public Object GetData() {
            return Data;
        }
        private void setType() {
            if (Type == FunctionType.Count)
            {
                tokenKind= TokenKind.Count;
            }
            else if (Type == FunctionType.UCount) {
                tokenKind = TokenKind.UCount;
            }
            else if (Type == FunctionType.Pivot)
            {
                tokenKind = TokenKind.Pivot ;
            }
            else if (Type == FunctionType.Min )
            {
                tokenKind = TokenKind.Min;
            }
            else if (Type == FunctionType.Max)
            {
                tokenKind = TokenKind.Max;
            }
            else if (Type == FunctionType.Mode)
            {
                tokenKind = TokenKind.Mode;
            }
            else if (Type == FunctionType.Mean)
            {
                tokenKind = TokenKind.Mean;
            }
            else if (Type == FunctionType.Range)
            {
                tokenKind = TokenKind.Range;
            }
            else if (Type == FunctionType.Sum)
            {
                tokenKind = TokenKind.Sum;
            }
            else if (Type == FunctionType.Avg)
            {
                tokenKind = TokenKind.Avg;
            }
            else if (Type == FunctionType.Filter)
            {
                tokenKind = TokenKind.Filter;
            }
        }
        
        public IFunction AddFields(string fName, FieldType fType, string fieldValue)
        {
            this._arguments.Add(new ParamFields() { FieldName = fName, Fieldtype = fType, FieldValue = fieldValue });
               return this;  
        }

        public IFunction AddSource(string dataSourceName)
        {
            this.AddSource(dataSourceName);
            return this;
        }

        public object GetResult(DataTable dt, params ParamFields[] arguments)
        {
            throw new NotImplementedException();
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        public void setResult(string name, object Right)
        {
            throw new NotImplementedException();
        }

        public void AddSource(object data)
        {
            this.Data = data;
        }

        public Object GetSource()
        {
            return this.Data;
        }

        

       
    }

    public class ParamFields {
        public string FieldName;
        public FieldType Fieldtype;
        public string FieldValue;
        public AggregateType Aggregate;
        public string DataSource;
        public string RelatedSource;
        public string RelatedFields;
        public FieldType RelatedFieldType;
        public string RelatedFieldValues;
        public WhereOperators relationship;
    }
    public interface IFunction{

         IFunction AddFields(string fName, FieldType fType, string fieldValue) ;
        IFunction AddSource(string dataSourceName) ;
        void AddSource(Object Data);
        Object GetSource();
        Object GetResult(DataTable dt, params ParamFields[] arguments);
    }
}
