﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tech.QScript.Source;

namespace Tech.QScript
{
   
    public class Token: IEquatable<Token>
    {
        public TokenCatagory Catagory;

        public string Value { get; }

        public SourceSpan Span { get; }

        public TokenKind Kind { get; }

        public Token(TokenKind kind, string contents, SourceLocation start, SourceLocation end)
        {
            Kind = kind;
            Value = contents;
            Span = new SourceSpan(start, end);
            Catagory = GetCategory();
        }

        public override bool Equals(object obj)
        {
            if (obj is Token)
            {
                return Equals((Token)obj);
            }
            return base.Equals(obj);
        }

        public bool Equals(Token other)
        {
            throw new NotImplementedException();
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode() ^ Span.GetHashCode() ^ Kind.GetHashCode();
        }

        public bool IsTrivia()
        {
            return Catagory == TokenCatagory.WhiteSpace;
        }

        private TokenCatagory GetCategory() {
            switch (Kind)
            {
                case TokenKind.WhiteSpace:
                    return TokenCatagory.WhiteSpace;
                case TokenKind.Get:
                case TokenKind.From:
                case TokenKind.And:
                case TokenKind.Or:
                case TokenKind.Case:
                case TokenKind.Push:
                case TokenKind.Put:
                case TokenKind.GetInto:
                    {
                        return TokenCatagory.Query;
                    }
                case TokenKind.If:
                case TokenKind.For:
                    {
                        return TokenCatagory.Statement;
                    }
                case TokenKind.Sum:
                case TokenKind.Avg:
                case TokenKind.Min:
                case TokenKind.Max:
                case TokenKind.Mean:
                case TokenKind.Range:
                case TokenKind.Medium:
                case TokenKind.Merge:
                case TokenKind.Join:
                case TokenKind.Pivot:
                case TokenKind.toString:
                case TokenKind.GetValue:
                case TokenKind.GetValues:
                case TokenKind.Mode:
                case TokenKind.Expr:
                    {
                        return TokenCatagory.Expression;
                    }
                case TokenKind.Count:
                case TokenKind.UCount:
                case TokenKind.Filter:
                    {
                        return TokenCatagory.Function;
                    }
                case TokenKind.literal:
                    return TokenCatagory.Literal;
                case TokenKind.d:
                case TokenKind.p:
                    {
                        return TokenCatagory.Declaration;
                    }
                default:
                        {
                        return TokenCatagory.Invalid;
                    }                   
            }            
        }
    }
}
