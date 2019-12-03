namespace lrCalculator
{
    public class TermSyntax:Syntax
    {
        public TermSyntax(Syntax factor):this(null,null,factor){

        }
        public TermSyntax(Syntax factor,Syntax multiply,Syntax term):base(TokenKind.Term)
        {
            Term = term;
            Multiply = multiply;
            Factor = factor;
        }

        public Syntax Term { get; }
        public Syntax Multiply { get; }
        public Syntax Factor { get; }
    }
}