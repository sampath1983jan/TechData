﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tech.QueryParser.Language.Syntax
{
    public abstract class Statement : SyntaxNode
    {
        public override SyntaxCatagory Catagory => SyntaxCatagory.Statement;

        protected Statement(SourceSpan span) : base(span)
        {
        }
    }
}
