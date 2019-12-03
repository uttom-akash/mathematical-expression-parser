namespace lrCalculator{
    public class GrammarToken:Token
    {
        
        public GrammarToken(TokenKind kind,bool isTerminal,string value)
        {
            Kind = kind;
            IsTerminal = isTerminal;
            Value = value;
        }


        public override TokenKind Kind {get;}
        public bool IsTerminal { get;set;}
        public object Value { get;set;}        
    }
}