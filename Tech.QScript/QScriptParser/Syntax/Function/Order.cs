using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tech.QScript.Syntax;

namespace Tech.QScript.Syntax
{
    public class Order: Fun
    {
        public List<ParamFields> Fields;
        public Order(Declare assignTo, string datasource, Source.SourceSpan span) 
            : base(assignTo, FunctionType.Order, datasource, span)
        {
            Fields = new List<ParamFields>();           
        }
        public void AddField(string fieldName, string OrderType) {
            Fields.Add(new ParamFields() { FieldName = fieldName, Order = OrderType });
        }
    }
}
