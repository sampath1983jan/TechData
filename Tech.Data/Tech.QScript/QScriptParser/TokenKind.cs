using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tech.QScript
{
    public enum TokenKind
    {
        EndOfFile,
        EndOfLine,
        WhiteSpace,
        Error,
        #region Query
        Get,
        From,
        Case,
        Put,
        GetInto,
        Push,
        And,
        Or,
        #endregion

        #region function
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
        #endregion

        #region Statment
        If,
        For,
        #endregion

        #region Declaration
        d,
        p,
        #endregion

        #region Punctuation
        Dot,
        Comma,
        Semicolon,
        Colon,
        #endregion

literal,

    }

    public static class TokenExpression {
        public static string Get = @"getdata\((.*?)\)";  //reg expre nexted ()-  \((?:(?:p(N-1))|(?:[^()]))*\)
        public static string from = @"from(\[(?:\[[^\]]*\]|[^\[\]]*)*\])";
        public static string And = @"and(\[(?:\[[^\]]*\]|[^\[\]]*)*\])";
        public static string Or = @"or(\[(?:\[[^\]]*\]|[^\[\]]*)*\])";
        public static string Case = @"Case\((.*?)\)";
        public static string Put = @"Put\((.*?)\)";
        public static string Push = @"Push\((.*?)\)";
        public static string GetInto= @"GetInto\((.*?)\)";
        public static string d = @"d:[a-z]*(:[a-z]*)?";
        public static string p = @":p[a-z]*(:[a-z]*)?";

        public static string Count = @"Count\((.*?)\)";
        public static string UCount = @"Count\((.*?)\)";
        public static string Filter = @"Filter\((.*?)\)";
        public static string Merge = @"Merge\((.*?)\)";
        public static string Avg = @"Avg\((.*?)\)";
        public static string Min = @"Min\((.*?)\)";
        public static string Max = @"Max\((.*?)\)";
        public static string Mean = @"Medium\((.*?)\)";
        public static string Medium = @"Medium\((.*?)\)";
        public static string Mode = @"Mode\((.*?)\)";
        public static string Range = @"Range\((.*?)\)";
        public static string Join = @"Join\((.*?)\)";
        public static string GetValue = @"GetValue\((.*?)\)";
        public static string GetValues = @"GetValues\((.*?)\)";
        public static string Tostring = @"Tostring\((.*?)\)";
        public static string Expr = @"Expr\((.*?)\)";
        public static string ifstate = @"if\((.*?)\)";
        public static string literal = "(.*?)";
    }
}


