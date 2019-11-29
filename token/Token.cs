namespace lrCalculator
{
    class Token
    {
        public Token(TokenKind kind,int value)
        {
            Kind = kind;
            Value = value;
        }

        public TokenKind Kind { get; }
        public int Value { get; }
    }
}