using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tech.QScript.Syntax
{
    public class Case : Fun
    {
        public List<CaseParam> _cases;
        public List<CaseParam> Cases => _cases;
        public string FieldName;
        public string NewFieldName;
        public string elseValue;

        public Case(Declare assignTo, string datasource, Source.SourceSpan span) : base(assignTo, FunctionType.Case, datasource, span)
        {
            _cases = new List<CaseParam>();
        }

        public IFunction AddCase( string condition, string value,string aliasValue) {
            _cases.Add(new CaseParam() { Condition=condition, Value= value, AliasValue =aliasValue});
            return this;
        }

    }

    public class CaseParam {        
        public string Condition;
        public string Value;
        public string AliasValue;
        public CaseParam() {            
            this.Condition = "";
            this.Value = "";
            this.AliasValue = "";
        }

    }
}
