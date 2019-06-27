using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp3
{
    class FormMNRCalc
    {
        public static string Evaluate(string expression, int var)
        {
            MNRProgram Prog = new MNRProgram(var);
            var lexer = new Combined1Lexer(new AntlrInputStream(expression));
            lexer.RemoveErrorListeners();
            lexer.AddErrorListener(new ThrowExceptionErrorListener());
            var tokens = new CommonTokenStream(lexer);
            var parser = new Combined1Parser(tokens);
            var tree = parser.compileUnit();
            var visitor = new Combined1Visitor(Prog, var);
            visitor.Visit(tree);
            return Prog.GetProgram();
        }
    }
}
