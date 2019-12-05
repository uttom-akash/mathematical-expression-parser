namespace lrCalculator
{
    public class DivisionOpSyntax:Syntax
    {
        public DivisionOpSyntax(Syntax _operator):base(TokenKind.Division_op)
        {
            Operator = _operator;
        }
        public Syntax Operator { get; }

        public override TokenKind Kind =>TokenKind.Division_op;
    }
}