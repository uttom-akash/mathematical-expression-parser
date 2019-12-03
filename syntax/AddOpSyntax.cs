namespace lrCalculator
{
    public class AddOpSyntax:Syntax
    {
        public AddOpSyntax(Syntax _operator):base(TokenKind.Add_op)
        {
            Operator = _operator;
        }
        public Syntax Operator { get; }

    }
}