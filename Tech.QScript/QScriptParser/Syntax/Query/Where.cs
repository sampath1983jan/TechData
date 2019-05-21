using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tech.QScript.Syntax.Query
{
    public enum WhereOperators {
        Equal,
        notEqual,
        In,
        NotIn,
        Like,
        start,
        end,
        none,
        greater,
        lesser,
        greaterthan,
        lesserthan,
        defaults,
    }
    public enum JoinType {
        None,
        And,
        or
    }
    public enum ConditionType {
        relation,
        filter,
    }
    public class Relation: Where {
        //private List<Where> _where;
        //public List<Where> Condition;
        public Relation(string lefttb, string leftfld, string righttb, string rightfld, WhereOperators operators) {
            //_where = new List<Where>();
            this.lefttable = lefttb;
            this.LeftField = leftfld;
            this.RightTable = righttb;
            this.RightField = rightfld;
            this.Operator = operators;
            this.Type = ConditionType.relation;
        }
    }

    public class Where
    {
        public string lefttable;
        public string RightTable;
        public string RightField;
        public string LeftField;
        public string Value;
        public WhereOperators Operator;
        public JoinType JoinType;
        public string DefaultValue;
        public ConditionType Type;
        public Where() {
            Type = ConditionType.filter;
            lefttable = "";
            LeftField = "";
            RightField = "";
            RightTable = "";
            Operator = WhereOperators.Equal;
            Value = "";
            JoinType = JoinType.None;
            DefaultValue = "";
        }
    }
}
