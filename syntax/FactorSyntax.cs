namespace lrCalculator
{
    public class FactorSyntax:Syntax
    {
        public FactorSyntax(Token number):base(TokenKind.Factor){
            Number = number;
        }

        public FactorSyntax(Token leftParenthesis,ExpressionSyntax expression,Token rightParenthesis):base(TokenKind.Factor){
            LeftParenthesis = leftParenthesis;
            Expression = expression;
            RightParenthesis = rightParenthesis;
        }

        public Token Number { get; }
        public Token LeftParenthesis { get; }
        public ExpressionSyntax Expression { get; }
        public Token RightParenthesis { get; }
    }
}