using System;
using System.Collections.Generic;

namespace lrCalculator{
    public class ParsingTable
    {
        public  List<Dictionary<GrammarToken,ParseAction>> _action;
        public  List<Dictionary<GrammarToken,int>> _goto;

        public Grammar Grammar { get; }

        public ParsingTable(Grammar grammar)
        {
            _action=new List<Dictionary<GrammarToken,ParseAction>>();
            _goto=new List<Dictionary<GrammarToken, int>>();
            Grammar = grammar;
        }


        public  void createParsingTable(List<States> states){
            init(states,Grammar.GrammarDictionary);

            foreach (var state in states)
            {
                int stateNo=state.StateNo;
                if(state.Leaf){
                    foreach (var token in Grammar.TerminalTokens)
                    {
                        _action[stateNo][token]=new ParseAction('r', Grammar.NumberedGrammar.GetValueOrDefault(state.LrItem.HashCode));
                    }
                    continue;
                }

                foreach (var transision in state.Transisions)
                {
                    GrammarToken token=transision.Value;
                    int dest=transision.Key;

                    if(token.IsTerminal){
                        _action[stateNo][token]=new ParseAction('s',dest);
                    }else{
                        _goto[stateNo][token]=dest;
                    }
                }
            }
        }


        private void init(List<States> states, Dictionary<GrammarToken,List<CompositeGrammarToken>> grammer){

            // initialize parsing table 
            foreach (var item in states)
            {
                var stateAction=new Dictionary<GrammarToken, ParseAction>();
                var stateGoto=  new Dictionary<GrammarToken, int>();
                

                foreach (var token in Grammar.TerminalTokens)
                {
                    stateAction.Add(token,new ParseAction('b',-1));
                }

                foreach (var token in Grammar.NonTerminalTokens)
                {
                    stateGoto.Add(token,-1);
                }
                _action.Add(stateAction);
                _goto.Add(stateGoto);       
            }

        }

    }

    public class ParseAction
        {
            public ParseAction(char action,int value)
            {
                Action = action;
                Value = value;
            }

            public char Action { get; }
            public int Value { get; }
            
        }
}