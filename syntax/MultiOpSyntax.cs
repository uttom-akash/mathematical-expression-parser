namespace lrCalculator
{
    public class MultiOpSyntax:Syntax
    {
        public MultiOpSyntax(Syntax _operator):base(TokenKind.Mult_op)
        {
            Operator = _operator;
        }
        public Syntax Operator { get; }

        public override TokenKind Kind =>TokenKind.Mult_op;
    }
}