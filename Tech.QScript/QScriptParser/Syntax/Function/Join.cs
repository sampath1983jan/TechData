using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tech.QScript.Syntax.Query;
namespace Tech.QScript.Syntax
{
    public class Join:Fun
    {
        private List<ParamFields> _arguments;              
        public Join(Declare assignTo, string dataSource, Source.SourceSpan span)
            : base(assignTo,FunctionType.Join, dataSource,span) {
            _arguments = new List<ParamFields>();
        }
        public IFunction AddRelation(string ds,string  fieldname, string relatedData,string relatedField, WhereOperators relationship) {
            this._arguments.Add(new ParamFields() { DataSource = ds, FieldName = fieldname, RelatedSource = relatedData, RelatedFields = relatedField, relationship = relationship });
            return this;
        }
       
       
    }
}
