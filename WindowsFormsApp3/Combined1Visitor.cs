using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp3
{
    class Combined1Visitor: Combined1BaseVisitor<Value>
    {
        MNRBuilder MNR ;

        public Combined1Visitor(MNRProgram Prog, int var)
        {
            MNR = new MNRBuilder(Prog, var);
        }
        public override Value VisitCompileUnit(Combined1Parser.CompileUnitContext context)
        {
            return Visit(context.expression());
        }
        public override Value VisitNumberExpr(Combined1Parser.NumberExprContext context)
        {
            var result = int.Parse(context.GetText());
            return MNR.Value(result);
        }

        //IdentifierExpr
        public override Value VisitIdentifierExpr(Combined1Parser.IdentifierExprContext context)
        {
            var result = context.GetText();
            //видобути значення змінної з таблиці
            return MNR.GetRegIndex(result.ToString());

        }
        public override Value VisitParenthesizedExpr(Combined1Parser.ParenthesizedExprContext context)
        {
            return Visit(context.expression());
        }

        public override Value VisitUnarnaExpr( Combined1Parser.UnarnaExprContext context)
        {
            var number = WalkLeft(context);
            if (context.operatorToken.Type == Combined1Lexer.INCREM)
            {
                //Debug.WriteLine("inc( {0} )", number);
                return MNR.Inc(number);
            }
            else if (context.operatorToken.Type == Combined1Lexer.DECREM)
            {
                //Debug.WriteLine("dec( {0} )", number);
                return MNR.Dec(number);
            }
            else if (context.operatorToken.Type == Combined1Lexer.SIG)
            {
                //Debug.WriteLine("sig( {0} )", number);
                return MNR.Sig(number);
            }
            else //NSIG
            {
                //Debug.WriteLine("nsig( {0} )", number);
                return MNR.Nsig(number);
            }

        }
        public override Value VisitExtrExpr( Combined1Parser.ExtrExprContext context)
        {
            var left = WalkLeft(context);
            var right = WalkRight(context);
            if (context.operatorToken.Type == Combined1Lexer.MAX)
            {
                return MNR.Max(left, right);
            }
            else
            {
                return MNR.Min(left, right);
            }
        }

        public override Value VisitExponentialExpr(Combined1Parser.ExponentialExprContext context)
        {
            var left = WalkLeft(context);
            var right = WalkRight(context);
            //Debug.WriteLine("{0} ^ {1}", left, right);
            return MNR.Pow(left, right);
        }
        public override Value VisitAdditiveExpr(Combined1Parser.AdditiveExprContext context)
        {
            var left = WalkLeft(context);
            var right = WalkRight(context);
            if (context.operatorToken.Type == Combined1Lexer.ADD)
            {
                //Debug.WriteLine("{0} + {1}", left, right);
                return MNR.Sum(left, right);
            }
            else if (context.operatorToken.Type == Combined1Lexer.SUBTRACT)
            {
                //Debug.WriteLine("{0} - {1}", left, right);
                return MNR.Sub(left, right);
            }
            else //Semisub
            {
                //Debug.WriteLine("{0} _ {1}", left, right);
                return MNR.SemiSub(left, right);
            }
        }
        public override Value VisitMultiplicativeExpr(Combined1Parser.MultiplicativeExprContext context)
        {
            var left = WalkLeft(context);
            var right = WalkRight(context);
            if (context.operatorToken.Type == Combined1Lexer.MULTIPLY)
            {
                //Debug.WriteLine("{0} * {1}", left, right);
                return MNR.Mult(left, right);

            }
            else //MNRLexer.DIVIDE
            {
                //Debug.WriteLine("{0} / {1}", left, right);
                return MNR.Div(left, right);
            }
        }



        private Value WalkLeft(Combined1Parser.ExpressionContext context)
        {
            return Visit(context.GetRuleContext<Combined1Parser.ExpressionContext>(0));
        }
        private Value WalkRight(Combined1Parser.ExpressionContext context)
        {
            return Visit(context.GetRuleContext<Combined1Parser.ExpressionContext>(1));
        }
    }
}

