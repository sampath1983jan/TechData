using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tech.QScript.Syntax
{
    public class Pivot:Fun
    {
        public List<ParamFields> RowArguments { get; }
        public List<ParamFields> ColumnArguments { get; }
        public List<ParamFields> AggrArguments { get; }

        public Pivot(Declare assignTo,string datasource, Source.SourceSpan span) : base(assignTo, FunctionType.Pivot, datasource,span) {
            RowArguments = new List<ParamFields>();
            ColumnArguments = new List<ParamFields>();
            AggrArguments = new List<ParamFields>();
        }
        public IFunction AddAggregate(string fName, AggregateType aggregate)
        {
            AggrArguments.Add(new ParamFields() { FieldName = fName, Aggregate = aggregate });
            return this;
        }
        public IFunction AddColumnFields(string fName)
        {
            ColumnArguments.Add(new ParamFields() { FieldName = fName });
            return this;
        }
        public IFunction AddRowFields(string fName)
        {
            RowArguments.Add(new ParamFields() { FieldName = fName });
            return this;
        }

        public new IFunction AddFields(string fName, FieldType fType, string fieldValue)
        {
            return base.AddFields(fName, fType, fieldValue);
        }

        public new object GetResult(DataTable dt, params ParamFields[] arguments)
        {
           return base.GetResult(dt, arguments);
        }
    }
}
