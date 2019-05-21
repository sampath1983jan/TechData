using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tech.QueryParser.Language.Parser
{
    public sealed class QueryScriptParserOptions
    {
        public static readonly QueryScriptParserOptions Default = new QueryScriptParserOptions();
        public static readonly QueryScriptParserOptions OptionalSemicolons = new QueryScriptParserOptions { EnforceSemicolons = false };

        public bool AllowRootStatements { get; set; }

        public bool EnforceSemicolons { get; set; }

        public QueryScriptParserOptions()
        {
            EnforceSemicolons = true;
            AllowRootStatements = true;
        }
    }
}
