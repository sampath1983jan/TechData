using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tech.Lexer
{
    public class Lexer
    {
        // Lexer

        // String:"sampath","kumar"
        //Symbols:"FUN","CALC" (Nane of the variable)
        //Number:12,12.4
        // Operators:{+,-}
        //punctuation:";,,}{()"


        // Parser - Define the structure of the statement
        // X=3 + 4;
        //Assignment:
        //    Symbol:
        //    Value:
        //        Operation:
        //            Type:"+";
        //            Arguments:
        //                3
        //                4
        //Print(x +2);
        //Function Call:
        //    Function: "Print"  (symbol)
        //    Arguments:
        //        Operation:
        //            Type:"+";
        //            Arguments:
        //                x (symbol)
        //                2 (number)


    }


}
