namespace lrCalculator{
    public class GrammarToken
    {
        public GrammarToken(TokenKind kind,bool isTerminal,string value)
        {
            Kind = kind;
            IsTerminal = isTerminal;
            Value = value;
        }

        public TokenKind Kind { get; }
        public bool IsTerminal { get; }
        public string Value { get; }
    }
}