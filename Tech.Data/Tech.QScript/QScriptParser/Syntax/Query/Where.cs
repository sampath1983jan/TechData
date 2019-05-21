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
