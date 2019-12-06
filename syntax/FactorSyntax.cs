namespace lrCalculator
{
    public class FactorSyntax:Syntax
    {
       public FactorSyntax(Syntax unit):this(null,null,unit){

        }
        public FactorSyntax(Syntax unit,Syntax division,Syntax factor):base(TokenKind.Factor)
        {
            Unit = unit;
            Division = division;
            Factor = factor;
        }

        public Syntax Unit { get; }
        public Syntax Division { get; }
        public Syntax Factor { get; }
    }
}