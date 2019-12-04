namespace lrCalculator{
    public abstract class Token
    {    
        public abstract TokenKind Kind { get;}
        public abstract object Value {get;}
    }
}