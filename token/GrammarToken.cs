namespace lrCalculator{
    public class GrammarToken:SyntaxToken
    {
        
        public GrammarToken(TokenKind kind,bool isTerminal,string value):base(kind,value)
        {
            IsTerminal = isTerminal;
        }

        public bool IsTerminal { get;set;}        
    }
}