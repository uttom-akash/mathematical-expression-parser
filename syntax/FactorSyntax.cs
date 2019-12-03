namespace lrCalculator
{
    public class FactorSyntax:Syntax
    {
        public FactorSyntax(Syntax number):base(TokenKind.Factor){
            Number = number;
        }

        public FactorSyntax(Syntax leftParenthesis,Syntax expression,Syntax rightParenthesis):base(TokenKind.Factor){
            LeftParenthesis = leftParenthesis;
            Expression = expression;
            RightParenthesis = rightParenthesis;
        }

        public Syntax Number { get; }
        public Syntax LeftParenthesis { get; }
        public Syntax Expression { get; }
        public Syntax RightParenthesis { get; }
    }
}