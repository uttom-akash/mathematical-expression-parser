namespace lrCalculator
{
    public class SyntaxToken :Token
    {
        public SyntaxToken(TokenKind kind):this(kind,null)
        {}

        public SyntaxToken(TokenKind kind,object value){
            Kind = kind;
            Value = value;
        }

        public override TokenKind Kind {get;}
        public override object Value {get;}
    }
}