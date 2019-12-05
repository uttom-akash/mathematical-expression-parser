using System;
using System.Collections.Generic;

namespace lrCalculator{
    public class Evaluator
    {

        public static object Evaluate(Syntax current){
                if(current.Kind==TokenKind.Term){
                        TermSyntax term=current as TermSyntax;
                        
                        if(term.Multiply!=null){
                            MultiOpSyntax multi=term.Multiply as MultiOpSyntax;

                            if(multi.Operator.Kind==TokenKind.Star)
                                return (int)Evaluate(term.Factor)*(int)Evaluate(term.Term);
                            else
                                {
                                    int a=(int)Evaluate(term.Term);
                                    a=a==0 ? a++:a;
                                    return (int)Evaluate(term.Factor)/a;
                                }
                        }
                        else if(term.Factor!=null){
                            return Evaluate(term.Factor);
                        }
                }else if(current.Kind==TokenKind.Factor){
                        FactorSyntax factor=current as FactorSyntax;
                        
                        if(factor.Number!=null){
                            return Evaluate(factor.Number);
                        }else if(factor.LeftParenthesis!=null){
                            return Evaluate(factor.Expression);
                        }
                }else if(current.Kind==TokenKind.Expression){
                        ExpressionSyntax expression=current as ExpressionSyntax;

                        if(expression.AddOp!=null){
                            AddOpSyntax add=expression.AddOp as AddOpSyntax;
                            if(add.Operator.Kind==TokenKind.Plus)
                                {
                                    return (int)Evaluate(expression.Term)+(int)Evaluate(expression.Expression);
                                }
                            else
                                return (int)Evaluate(expression.Term)-(int)Evaluate(expression.Expression);

                        }else if(expression.Term!=null)
                                return Evaluate(expression.Term);

                }else if(current.Kind==TokenKind.Number){
                    return current.Value;
                }
                
                return 0;
        }

        



    }
}