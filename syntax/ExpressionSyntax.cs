namespace lrCalculator
{
    public class ExpressionSyntax:Syntax
    {

        public ExpressionSyntax(Syntax term):this(term,null,null)
        {
        }

        public ExpressionSyntax(Syntax term,Syntax addOp,Syntax expression):base(TokenKind.Expression){
            Term = term;
            AddOp = addOp;
            Expression = expression;
        }
        public Syntax Term { get; }
        public Syntax AddOp { get; }
        public Syntax Expression { get; }
    }
}