using System.Collections.Generic;

namespace lrCalculator{
    public class Grammar
    {
        public List<KeyValuePair<GrammarToken,CompositeGrammarToken>> grammarList;
        public Dictionary<GrammarToken,List<CompositeGrammarToken>> grammarDictionary;
        public Dictionary<int, int> NumberedGrammar { get; private set; }
        public List<GrammarToken> terminalTokens;
        public List<GrammarToken> nonTerminalTokens;
        

        public Grammar(List<KeyValuePair<GrammarToken,CompositeGrammarToken>> grammarList)
        {
            this.grammarList = grammarList;
            init();
        }



        private void init(){
            grammarDictionary=GetGrammarDictionary(grammarList);
            NumberedGrammar=GetGrammarNumbering(grammarList);
            terminalTokens=new List<GrammarToken>();
            nonTerminalTokens=new List<GrammarToken>();
            ListSymbols(grammarList);

            
        }
        private Dictionary<GrammarToken,List<CompositeGrammarToken>> GetGrammarDictionary(List<KeyValuePair<GrammarToken,CompositeGrammarToken>> grammarList){
             Dictionary<GrammarToken,List<CompositeGrammarToken>> grammarDictionary=new Dictionary<GrammarToken, List<CompositeGrammarToken>>();

             foreach (var grammar in grammarList)
                { 
                    if(grammarDictionary.ContainsKey(grammar.Key))
                        grammarDictionary[grammar.Key].Add(grammar.Value);
                    else    
                        grammarDictionary.Add(grammar.Key,new List<CompositeGrammarToken>(){grammar.Value});
                }
            return grammarDictionary;
        }



        private Dictionary<int,int> GetGrammarNumbering(List<KeyValuePair<GrammarToken,CompositeGrammarToken>> grammarList){
            Dictionary<int,int> GrammarNumber=new Dictionary<int, int>();
            
            int index=1;
            foreach (var grammar in grammarList)
            {
                GrammarNumber.Add(grammar.Value.GetHashCode(),index); 
                index++;   
            }
            return GrammarNumber;
        }
        
        public void ListSymbols(List<KeyValuePair<GrammarToken, CompositeGrammarToken>> grammarList){
            
            foreach (var grammar in grammarList)
            {
                gatherSymbol(grammar.Key);
                foreach (var token in grammar.Value.TokenList)
                {
                    gatherSymbol(token);
                }
            }
        }

        private void gatherSymbol(GrammarToken token){
            if(token.IsTerminal && !terminalTokens.Contains(token))
                terminalTokens.Add(token);
            else if(!token.IsTerminal && !nonTerminalTokens.Contains(token))
                nonTerminalTokens.Add(token);
        }

    }
}