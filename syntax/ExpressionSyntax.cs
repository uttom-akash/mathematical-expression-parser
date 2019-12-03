namespace lrCalculator
{
    public class ExpressionSyntax:Syntax
    {

        public ExpressionSyntax(TermSyntax term):this(term,null,null)
        {

        }

        public ExpressionSyntax(TermSyntax term,AddOpSyntax addOp,ExpressionSyntax expression):base(TokenKind.Expression){
            Term = term;
            AddOp = addOp;
            Expression = expression;
        }
        public TermSyntax Term { get; }
        public AddOpSyntax AddOp { get; }
        public ExpressionSyntax Expression { get; }
    }
}