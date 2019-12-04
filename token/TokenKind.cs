namespace lrCalculator
{
    public enum TokenKind
    {
        
        S,
        A,
        a,
        b,


        // calculator
        Expression,
        Term,
        Factor,
        Add_op,
        Mult_op,
        
        // terminal
        Id,
        Number,
        LeftParenthesis,
        RightParenthesis,
        Plus,
        Minus,
        Star,
        Slash,
        Dollar,
        Accepted,
        Invalid,
        EndOfFile,
        WhiteSpace
    }
}