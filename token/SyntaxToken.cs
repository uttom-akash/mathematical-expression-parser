namespace lrCalculator
{
    public class SyntaxToken :Token
    {
        public SyntaxToken(TokenKind kind)
        {
            Kind=kind;
        }

        public SyntaxToken(TokenKind kind,object value){
            Kind = kind;
            Value = value;
        }

        public override TokenKind Kind {get;}
        public object Value { get;set;}
    }
}