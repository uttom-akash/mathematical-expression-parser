using System;
using System.Collections.Generic;
using System.Linq;

namespace lrCalculator{
    public class ParsingTable
    {
        public  List<Dictionary<TokenKind,ParseAction>> _action;
        public  List<Dictionary<TokenKind,int>> _goto;

        public Grammar Grammar { get; }

        public ParsingTable(Grammar grammar)
        {
            _action=new List<Dictionary<TokenKind,ParseAction>>();
            _goto=new List<Dictionary<TokenKind, int>>();
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
                        var reducedLeftHandSide=Grammar.NumberedGrammar.GetValueOrDefault(state.LrItem.HashCode);
                        _action[stateNo][token.Kind]=new ParseAction('r', reducedLeftHandSide);
                    }
                    continue;
                }

                foreach (var transision in state.Transisions)
                {
                    GrammarToken token=transision.Value;
                    int dest=transision.Key;

                    if(token.IsTerminal){
                        _action[stateNo][token.Kind]=new ParseAction('s',dest);
                    }else{
                        _goto[stateNo][token.Kind]=dest;
                    }
                }
            }
        }


        private void init(List<States> states, Dictionary<GrammarToken,List<CompositeGrammarToken>> grammer){

            // initialize parsing table 
            foreach (var item in states)
            {
                var stateAction=new Dictionary<TokenKind, ParseAction>();
                var stateGoto=  new Dictionary<TokenKind, int>();
                

                foreach (var token in Grammar.TerminalTokens)
                {
                    stateAction.Add(token.Kind,new ParseAction('b',-1));
                }

                foreach (var token in Grammar.NonTerminalTokens)
                {
                    stateGoto.Add(token.Kind,-1);
                }
                _action.Add(stateAction);
                _goto.Add(stateGoto);       
            }

        }

    }

    public class ParseAction
        {
            public ParseAction(char action):this(action,0){

            }
            public ParseAction(char action,int value)
            {
                Action = action;
                Value = value;
            }

            public char Action { get; }
            public int Value { get; }
            
        }
}