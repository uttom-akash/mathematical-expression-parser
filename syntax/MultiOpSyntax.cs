namespace lrCalculator
{
    public class MultiOpSyntax:Syntax
    {
        public MultiOpSyntax(Token _operator):base(TokenKind.Mult_op)
        {
            Operator = _operator;
        }
        public Token Operator { get; }

        public override TokenKind Kind =>TokenKind.Mult_op;
    }
}