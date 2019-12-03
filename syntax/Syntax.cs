namespace lrCalculator{
    public class Syntax : SyntaxToken
    {
        public Syntax(TokenKind kind) : this(kind,null)
        {}
        
        public Syntax(TokenKind kind,object value):base(kind,value){
        }
    }
}