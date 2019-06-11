using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Tech.QScript.Syntax;
namespace Tech.QScript.Parser
{
    public sealed partial class QScriptParser
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tokens"></param>
        /// <returns></returns>
        private Syntax.Declare DeclarationParser(Token tokens)
        {
            var dec = new Tech.QScript.Syntax.Declare( tokens.Span);
            dec.Name = "";                       
            if (isParam(tokens.Value))
            {
                dec.Type = DeclartionType.Param;
            }
            else if (isVariable(tokens.Value))
            {

                dec.Type = DeclartionType.Variable;
            }
            else {
                AddError(Source.Severity.Error, "invalid declaration" + tokens.Value, tokens.Span);
            }           
            setValue(ref dec, tokens.Value);
            return dec;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        private bool isAlreadyDeclared(string txt) {
            if (Scan(Syntax.DeclarationDefination.DeclareWithFieldType, txt) || Scan(Syntax.DeclarationDefination.DeclareWithoutFieldType, txt) || Scan(Syntax.DeclarationDefination.ParamWithFieldType, txt) || Scan(Syntax.DeclarationDefination.ParamWithoutFieldType, txt)) {
                if ((_tokens.Where(x => x.Kind == TokenKind.p && x.Value == txt).ToList().Count > 0) || _tokens.Where(x => x.Kind == TokenKind.d && x.Value == txt).ToList().Count > 0) {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dec"></param>
        /// <param name="val"></param>
        private void setValue(ref Syntax.Declare dec,string val) {
            val = val.Replace("d:", "");
            val = val.Replace("p:", "");

            var content = Regex.Split(val,":");
            if (content.Length == 1)
            {
                dec.Name = content[0];
                if (dec.Name == "")
                {
                    //throw exception syntax
                }
            }
            else if (content.Length == 2)
            {
                dec.Name = content[0];
                if (dec.Name == "")
                {
                    //throw exception syntax
                }
                var a = "string";
                switch (content[1].ToLower())
                {
                    case "1":
                        //throw error
                    case "string":
                        dec.FieldType = FieldType.String;
                        break;
                    case "number":
                        dec.FieldType = FieldType.Number;
                        break;
                    case "currency":
                        dec.FieldType = FieldType.Currency;
                        break;
                    case "decimal":
                        dec.FieldType = FieldType.Decimal;
                        break;
                    case "date":
                        dec.FieldType = FieldType.Date;
                        break;
                    case "datetime":
                        dec.FieldType = FieldType.DateTime ;
                        break;
                    case "bool":
                        dec.FieldType = FieldType.Bool;
                        break;
                    case "text":
                        dec.FieldType = FieldType.Text;
                        break;
                    case "list":
                        dec.FieldType = FieldType.List;
                        break;
                    default:
                        AddError(Source.Severity.Error, "Invalid Field Type");
                        break;
                }
                
            }
            else {
                //throw syntax exception
            }                     
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        private bool isVariable(string txt)
        {
            if (Scan(Syntax.DeclarationDefination.DeclareWithFieldType, txt) || Scan(Syntax.DeclarationDefination.DeclareWithoutFieldType, txt))
            {
                return true;
            }
            else {
                if (_tokens.Where(x => x.Kind == TokenKind.d && x.Value == txt).ToList().Count > 0)
                {
                    return true;
                }
                else {
                    return false;
                }
            }           
             
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        private bool isParam(string txt) {
            if (Scan(Syntax.DeclarationDefination.ParamWithFieldType, txt) || Scan(Syntax.DeclarationDefination.ParamWithoutFieldType, txt))
            {
                return true;
            }
            else
                if (_tokens.Where(x => x.Kind == TokenKind.p && x.Value == txt).ToList().Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

          




    }
}
