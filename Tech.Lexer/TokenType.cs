using System.Collections.Generic;
namespace Tech.Lexer
{
    public class TokenType
    {
        public static int Identifier = 0;  // variable
        public static int Keyword = 1; // syntex
        public static int Sparator = 2; // sparator
        public static int Operator = 3; // operator
        public static string BlockOpen = ""; // (
        public static string BlockClose = ""; // )
        public static string Text = ""; // general text
    }

    public abstract class ITokenElements
    {
        public string Key = "";
        public string Value = "";
    }

    public class LexerRule {
        public List<ITokenElements> Rules { get; }
        private int CurrentIndex;
        public LexerRule() {
            Rules = new List<ITokenElements>();
            CurrentIndex = 0;
        }
        public void Add(ITokenElements rule) {
            this.Rules.Add(rule);
            CurrentIndex = this.Rules.Count - 1;
        }
        public ITokenElements GetNext() {
            CurrentIndex = CurrentIndex + 1;
            if ((CurrentIndex - 1) > this.Rules.Count - 1)
            {
                return null;
            }
            else {
                return this.Rules[CurrentIndex - 1];
            }            
        }
    }     
    
    public class KeyWord:ITokenElements
    { // data type and its value                
        public KeyWord(string key,string value) {
            Key = key;
            Value = value;
        }
    }    
    public class Identifier: ITokenElements // variables
    {        
        public Identifier(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }

    public class Sparator: ITokenElements
    { // spearator for spliting char        
        public Sparator(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }

    public class Operator: ITokenElements  // operation type
    {        
        public Operator(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
    public class Text: ITokenElements // value or text or constant
    {        
        public Text(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }

}
