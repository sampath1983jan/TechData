using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tech.QScript.Syntax
{
    public class Merge:Fun
    {
        private List<string> _datasources;
        public List<string> DataSources { get => _datasources; }
        public Merge(Declare assignTo, string dataSource, Source.SourceSpan span) : base(assignTo,FunctionType.Pivot, dataSource, span,"")
        {
            _datasources = new List<string>();

        }
        public new IFunction AddSource(string dataSourceName)
        {
            _datasources.Add(dataSourceName);
            return this;
        }

    }
}
