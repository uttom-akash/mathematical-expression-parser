namespace lrCalculator
{
    public class AddOpSyntax:Syntax
    {
        public AddOpSyntax(Token _operator):base(TokenKind.Add_op)
        {
            Operator = _operator;
        }
        public Token Operator { get; }

    }
}