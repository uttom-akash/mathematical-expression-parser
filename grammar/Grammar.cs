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

            // stanford grammar
            // GrammarList.Add(new Production(Expression,new CompositeGrammarToken(new List<GrammarToken>{Term})));
            // GrammarList.Add(new Production(Expression,new CompositeGrammarToken(new List<GrammarToken>{Term,Plus,Expression})));
            
            // GrammarList.Add(new Production(Term,new CompositeGrammarToken(new List<GrammarToken>{Number,Star,Term})));
            // GrammarList.Add(new Production(Term,new CompositeGrammarToken(new List<GrammarToken>{Number})));
            // GrammarList.Add(new Production(Term,new CompositeGrammarToken(new List<GrammarToken>{LeftParenthesis,Expression,RightParenthesis})));
            

            GrammarList.Add(new Production(Accepted,new CompositeGrammarToken(new List<GrammarToken>{Expression})));
            // book grammar
            GrammarList.Add(new Production(Expression,new CompositeGrammarToken(new List<GrammarToken>{Term})));
            GrammarList.Add(new Production(Expression,new CompositeGrammarToken(new List<GrammarToken>{Term,Add_op,Expression})));

            GrammarList.Add(new Production(Term,new CompositeGrammarToken(new List<GrammarToken>{Factor})));
            GrammarList.Add(new Production(Term,new CompositeGrammarToken(new List<GrammarToken>{Factor,Mult_op,Term})));

            GrammarList.Add(new Production(Factor,new CompositeGrammarToken(new List<GrammarToken>{Unit})));
            GrammarList.Add(new Production(Factor,new CompositeGrammarToken(new List<GrammarToken>{Unit,Devision_op,Factor})));
            
            GrammarList.Add(new Production(Unit,new CompositeGrammarToken(new List<GrammarToken>{Number})));
            GrammarList.Add(new Production(Unit,new CompositeGrammarToken(new List<GrammarToken>{LeftParenthesis,Expression,RightParenthesis})));
            





            // my grammmar
            // GrammarList.Add(new Production(Expression,new CompositeGrammarToken(new List<GrammarToken>{Term})));
            // GrammarList.Add(new Production(Expression,new CompositeGrammarToken(new List<GrammarToken>{Expression,Add_op,Term})));
            // GrammarList.Add(new Production(Expression,new CompositeGrammarToken(new List<GrammarToken>{LeftParenthesis,Expression,RightParenthesis})));

            // GrammarList.Add(new Production(Term,new CompositeGrammarToken(new List<GrammarToken>{Factor})));

            // GrammarList.Add(new Production(Factor,new CompositeGrammarToken(new List<GrammarToken>{Number})));
            // GrammarList.Add(new Production(Factor,new CompositeGrammarToken(new List<GrammarToken>{Factor,Mult_op,Number})));
            
            // common terminal
            GrammarList.Add(new Production(Add_op,new CompositeGrammarToken(new List<GrammarToken>{Plus})));
            GrammarList.Add(new Production(Add_op,new CompositeGrammarToken(new List<GrammarToken>{Minus})));
            
            
            GrammarList.Add(new Production(Mult_op,new CompositeGrammarToken(new List<GrammarToken>{Star})));
            GrammarList.Add(new Production(Devision_op,new CompositeGrammarToken(new List<GrammarToken>{Slash})));
            
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
            
            int index=0;
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
        private GrammarToken Accepted=new GrammarToken(TokenKind.Accepted,false,"accepted");
        private GrammarToken Expression=new GrammarToken(TokenKind.Expression,false,"exp");
        private GrammarToken Term=new GrammarToken(TokenKind.Term,false,"term");
        private GrammarToken Factor=new GrammarToken(TokenKind.Factor,false,"factor");

        private GrammarToken Unit=new GrammarToken(TokenKind.Unit,false,"unit");
        private GrammarToken Add_op=new GrammarToken(TokenKind.Add_op,false,"add");
        private GrammarToken Mult_op=new GrammarToken(TokenKind.Mult_op,false,"mult");
        private GrammarToken Devision_op=new GrammarToken(TokenKind.Division_op,false,"devision");
        
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