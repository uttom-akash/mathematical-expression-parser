using System.Collections.Generic;

namespace lrCalculator
{
    public class Grammar{
        
        public Grammar()
        {
            init();
        }

        public  GrammarToken  Start {get=>Expression;}
        public List<Production> GrammarList {get;private set;}
        public Dictionary<GrammarToken,List<CompositeGrammarToken>> GrammarDictionary {get;private set;}
        public Dictionary<int, int> NumberedGrammar { get; private set; }
        
        public List<GrammarToken> TerminalTokens { get; private set; }
        public List<GrammarToken> NonTerminalTokens { get; private set; }

         

        private void init(){
            GrammarList=GetGrammarList();
            GrammarDictionary=GetGrammarDictionary();
            NumberedGrammar=GetGrammarNumbering();
            TerminalTokens=new List<GrammarToken>();
            NonTerminalTokens=new List<GrammarToken>();
            ListSymbols();

            // $$
            TerminalTokens.Add(new GrammarToken(TokenKind.Dollar,true,"$"));

            
        }
 
        public List<Production> GetGrammarList(){
            if(GrammarList==null)
                GrammarList=new List<Production>();
            else 
                return GrammarList;

            
            GrammarList.Add(new Production(Expression,new CompositeGrammarToken(new List<GrammarToken>{Term})));
            GrammarList.Add(new Production(Expression,new CompositeGrammarToken(new List<GrammarToken>{Expression,Add_op,Term})));
            GrammarList.Add(new Production(Expression,new CompositeGrammarToken(new List<GrammarToken>{LeftParenthesis,Expression,RightParenthesis})));

            GrammarList.Add(new Production(Term,new CompositeGrammarToken(new List<GrammarToken>{Number})));
            
            GrammarList.Add(new Production(Add_op,new CompositeGrammarToken(new List<GrammarToken>{Plus})));
            GrammarList.Add(new Production(Add_op,new CompositeGrammarToken(new List<GrammarToken>{Minus})));
            
            
            return GrammarList;   
        }

        
        private Dictionary<GrammarToken,List<CompositeGrammarToken>> GetGrammarDictionary(){
             Dictionary<GrammarToken,List<CompositeGrammarToken>> grammarDictionary=new Dictionary<GrammarToken, List<CompositeGrammarToken>>();

             foreach (var grammar in GrammarList)
                { 
                    if(grammarDictionary.ContainsKey(grammar.LeftHandSide))
                        grammarDictionary[grammar.LeftHandSide].Add(grammar.RightHandSide);
                    else    
                        grammarDictionary.Add(grammar.LeftHandSide,new List<CompositeGrammarToken>(){grammar.RightHandSide});
                }
            return grammarDictionary;
        }



        private Dictionary<int,int> GetGrammarNumbering(){
            Dictionary<int,int> GrammarNumber=new Dictionary<int, int>();
            
            int index=1;
            foreach (var grammar in GrammarList)
            {
                GrammarNumber.TryAdd(grammar.RightHandSide.HashCode,index); 
                index++;   
            }
            return GrammarNumber;
        }
        
        public void ListSymbols(){
            
            foreach (var grammar in GrammarList)
            {
                gatherSymbol(grammar.LeftHandSide);
                foreach (var token in grammar.RightHandSide.TokenList)
                {
                    gatherSymbol(token);
                }
            }
        }

        private void gatherSymbol(GrammarToken token){
            if(token.IsTerminal && !TerminalTokens.Contains(token))
                TerminalTokens.Add(token);
            else if(!token.IsTerminal && !NonTerminalTokens.Contains(token))
                NonTerminalTokens.Add(token);
        }



        // Grammar token
        private GrammarToken Expression=new GrammarToken(TokenKind.Expression,false,"exp");
        private GrammarToken Term=new GrammarToken(TokenKind.Term,false,"term");
        private GrammarToken Factor=new GrammarToken(TokenKind.Factor,false,"factor");
        private GrammarToken Add_op=new GrammarToken(TokenKind.Add_op,false,"add");
        private GrammarToken Mult_op=new GrammarToken(TokenKind.Mult_op,false,"mult");
        
        private GrammarToken Id=new GrammarToken(TokenKind.Id,true,"id");
        private GrammarToken Number=new GrammarToken(TokenKind.Number,true,"number");
        private GrammarToken LeftParenthesis=new GrammarToken(TokenKind.LeftParenthesis,true,"(");
        private GrammarToken RightParenthesis=new GrammarToken(TokenKind.RightParenthesis,true,")");

        private GrammarToken Plus=new GrammarToken(TokenKind.Plus,true,"+");
        private GrammarToken Minus=new GrammarToken(TokenKind.Minus,true,"-");
        private GrammarToken Star=new GrammarToken(TokenKind.Star,true,"*");
        private GrammarToken Slash=new GrammarToken(TokenKind.Slash,true,"\\");
    }

}