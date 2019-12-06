using System;
using System.Collections.Generic;

namespace lrCalculator{
    public class Evaluator
    {

        public static object Evaluate(Syntax current,bool fromDivsion){
                if(current.Kind==TokenKind.Expression){
                        ExpressionSyntax expression=current as ExpressionSyntax;

                        if(expression.AddOp!=null){
                            AddOpSyntax add=expression.AddOp as AddOpSyntax;
                            if(add.Operator.Kind==TokenKind.Plus)
                                {
                                    return (int)Evaluate(expression.Term,false)+(int)Evaluate(expression.Expression,false);
                                }
                            else
                                return (int)Evaluate(expression.Term,false)-(int)Evaluate(expression.Expression,false);

                        }else if(expression.Term!=null)
                                return Evaluate(expression.Term,false);

                }else if(current.Kind==TokenKind.Term){
                        TermSyntax term=current as TermSyntax;
                        
                        if(term.Multiply!=null){
                            MultiOpSyntax multi=term.Multiply as MultiOpSyntax;
                            return (int)Evaluate(term.Factor,false)*(int)Evaluate(term.Term,false);
                        }
                        else if(term.Factor!=null){
                            return Evaluate(term.Factor,false);
                        }
                }else if(current.Kind==TokenKind.Factor){
                        FactorSyntax factor=current as FactorSyntax;
                        
                        if(factor.Division!=null){
                            MultiOpSyntax divide=factor.Division as MultiOpSyntax;
                            int a=(int)Evaluate(factor.Unit,false);
                            int b=(int)Evaluate(factor.Factor,true);
                            if(fromDivsion)
                                return a*b;
                            return a/b;
                                
                        }
                        else if(factor.Unit!=null){
                            return Evaluate(factor.Unit,false);
                        }
                }else if(current.Kind==TokenKind.Unit){
                        UnitSyntax unit=current as UnitSyntax;
                        
                        if(unit.Number!=null){
                            return Evaluate(unit.Number,false);
                        }else if(unit.LeftParenthesis!=null){
                            return Evaluate(unit.Expression,false);
                        }
                }else if(current.Kind==TokenKind.Number){
                    return current.Value;
                }
                
                return 0;
        }

        



    }
}