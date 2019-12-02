namespace lrCalculator
{
    public class Production
    {
        public Production(GrammarToken leftHandSide,CompositeGrammarToken rightHandSide)
        {
            LeftHandSide = leftHandSide;
            RightHandSide = rightHandSide;
        }

        public GrammarToken LeftHandSide { get; }
        public CompositeGrammarToken RightHandSide { get; }
    }

}