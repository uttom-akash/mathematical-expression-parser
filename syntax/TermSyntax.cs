namespace lrCalculator
{
    public class TermSyntax:Syntax
    {
        public TermSyntax(FactorSyntax factor):this(null,null,factor){

        }
        public TermSyntax(TermSyntax term,MultiOpSyntax multiply,FactorSyntax factor):base(TokenKind.Term)
        {
            Term = term;
            Multiply = multiply;
            Factor = factor;
        }

        public TermSyntax Term { get; }
        public MultiOpSyntax Multiply { get; }
        public FactorSyntax Factor { get; }
    }
}