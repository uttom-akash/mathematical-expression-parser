using System.Collections.Generic;

namespace lrCalculator{
    public class Lexer
    {
        private int position;
        private int length;
        public Lexer(string text)
        {
            Text = text;
            position=0;
            length=text.Length;
        }

        private string Text { get; }


        private char Peek(int offset){
            if(position+offset<length)
                return Text[position+offset];
            return '\0'; 
        }

        private char Current=>Peek(0);

        private bool Next(){
            position++;
            if(position<length)
                return true;
            else 
                return false;    
        }

        public List<SyntaxToken> Lex(){
            List<SyntaxToken> tokenList=new List<SyntaxToken>();
            while (true)
            {
                SyntaxToken token=GetToken();
                if(token.Kind==TokenKind.WhiteSpace)
                    continue;
                if(token.Kind==TokenKind.EndOfFile)
                    break;    
                tokenList.Add(token);
            }

            return tokenList;
        }

        private SyntaxToken GetToken(){
            SyntaxToken syntaxToken=new SyntaxToken(TokenKind.EndOfFile);
            
            char token=Current;
            if(token=='\0')
                return syntaxToken;
            
            if(char.IsWhiteSpace(token)){
                return GetWhiteSpaceToken();
            }

            if(char.IsDigit(token)){
                return GetNumericalToken();
            }


            switch (token)
            {
                case '+':syntaxToken=new SyntaxToken(TokenKind.Plus);break;
                case '-':syntaxToken=new SyntaxToken(TokenKind.Minus);break;
                case '*':syntaxToken=new SyntaxToken(TokenKind.Star);break;
                case '/':syntaxToken=new SyntaxToken(TokenKind.Slash);break;
                case '(':syntaxToken=new SyntaxToken(TokenKind.LeftParenthesis);break;
                case ')':syntaxToken=new SyntaxToken(TokenKind.RightParenthesis);break;
                default:break;
            }
            Next();
            return syntaxToken;
        }

        private SyntaxToken GetWhiteSpaceToken(){
            while (char.IsWhiteSpace(Current))
                Next();
            return new SyntaxToken(TokenKind.WhiteSpace);
        }

        private SyntaxToken GetNumericalToken(){
            int number=0;
            while(char.IsDigit(Current))
            {
                number*=10;
                number+= (int)(Current-'0');
                Next();                
            }
            return new SyntaxToken(TokenKind.Number,number);
        }
    
    
    }

}