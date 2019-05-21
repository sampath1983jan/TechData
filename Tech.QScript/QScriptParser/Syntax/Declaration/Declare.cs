using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tech.QScript.Syntax;

namespace Tech.QScript.Syntax
{
    public enum DeclartionType{
        Variable,
        Param
    }
    public enum FieldType {
        String,
        Number,
        Currency,
        Decimal,
        Date,
        DateTime,
        Text, // multiline
        Bool,
        Object,
        List,
    }
    public class DeclarationDefination {
        public static string DeclareWithFieldType = @"^d:[\w]*:[\w]*";
        public static string DeclareWithoutFieldType = @"^d:[\w]*";
        public static string ParamWithFieldType = @"^p:[\w]*:[\w]*";
        public static string ParamWithoutFieldType = @"^p:[\w]*";
    }
    public class Declare: SyntaxNode, Tech.QScript.Syntax.Element

    {        
        public string Name;
        private dynamic result; 
        public DeclartionType Type;
        public FieldType FieldType;        
        public override SyntaxCatagory Catagory => SyntaxCatagory.Declaration;
        public override TokenKind Kind => TokenKind.d;
        public dynamic EvalutionResult => result;
        public object Value;
        public Declare(Source.SourceSpan span):base(span) {
            Name = "";
            Type = DeclartionType.Variable;
            FieldType = FieldType.Object; // Default is object
        }

       
        public dynamic GetResult(string name) {
            if (result != null)
            {
                return result.get(name);
            }
            else {
                return "";
            }            
        }
        //public void Accept(IVisitor visitor)
        //{
        //    visitor.Visit(this);
        //}
        public void setResult(string name, object Right)
        {
            result = new Result();
            result.AddProperty(name, Right);
            //result.Value = Right;
        }

        public void Accept(IVisitor visitor, EvaluationParam evaluation)
        {
            visitor.Visit(this,evaluation);
        }
    }
}

