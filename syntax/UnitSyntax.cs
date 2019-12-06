namespace lrCalculator
{
    public class UnitSyntax:Syntax
    {
        public UnitSyntax(Syntax number):base(TokenKind.Unit){
            Number = number;
        }

        public UnitSyntax(Syntax leftParenthesis,Syntax expression,Syntax rightParenthesis):base(TokenKind.Unit){
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