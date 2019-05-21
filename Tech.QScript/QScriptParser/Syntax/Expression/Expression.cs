using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tech.QScript.Syntax
{
    public class Expression:SyntaxNode, Element
    {
        public static string  ExpressionReg= @"(?<=\().*(?=\))";
        private string _formula;
        private Declare _assignTo;
        private dynamic Result;
        /// <summary>
        /// 
        /// </summary>
        public string formula => _formula;
        /// <summary>
        /// 
        /// </summary>
        public Declare AssignTo { get => _assignTo; }
        /// <summary>
        /// 
        /// </summary>        /// 
        public override SyntaxCatagory Catagory =>  SyntaxCatagory.Expression;
        /// <summary>
        /// 
        /// </summary>
        public override TokenKind Kind =>  TokenKind.Expr;
        /// <summary>
        /// 
        /// </summary>
        public dynamic EvalutionResult => Result;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="span"></param>
        public Expression(Declare assignTo, string expression,Source.SourceSpan span) : base(span) {
            _formula = expression;
            _assignTo = assignTo;
        }
        public void ReplaceWith(string name,string Val) {
            _formula =_formula.Replace("[" +name + "]",  Val);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="visitor"></param>
        public void Accept(IVisitor visitor, EvaluationParam evaluationParam)
        {
            visitor.Visit(this, evaluationParam);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="Right"></param>
        public void setResult(string name, object Right)
        {
            throw new NotImplementedException();
        }
    }
}
