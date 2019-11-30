using System;
using System.Collections.Generic;
using System.Linq;

namespace lrCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            // List<KeyValuePair<char,string>> grammarList=new List< KeyValuePair<char,string> >();
            
            
            
            // grammarList.Add(new KeyValuePair<char, string>('S',"AA"));
            // grammarList.Add(new KeyValuePair<char, string>('A',"aA"));
            // grammarList.Add(new KeyValuePair<char, string>('A',"b"));
            // Grammar grammar=new Grammar(grammarList);

            List<KeyValuePair<GrammarToken,CompositeGrammarToken>> grammarList=new List< KeyValuePair<GrammarToken,CompositeGrammarToken>>();
            
            GrammarToken Expression=new GrammarToken(TokenKind.Expression,false,"exp");
            GrammarToken Term=new GrammarToken(TokenKind.Term,false,"term");
            GrammarToken Factor=new GrammarToken(TokenKind.Factor,false,"factor");
            GrammarToken Add_op=new GrammarToken(TokenKind.Add_op,false,"add");
            GrammarToken Mult_op=new GrammarToken(TokenKind.Mult_op,false,"mult");
            
            GrammarToken Id=new GrammarToken(TokenKind.Id,true,"id");
            GrammarToken Number=new GrammarToken(TokenKind.Number,true,"number");
            GrammarToken LeftParenthesis=new GrammarToken(TokenKind.LeftParenthesis,true,"(");
            GrammarToken RightParenthesis=new GrammarToken(TokenKind.RightParenthesis,true,")");

            GrammarToken Plus=new GrammarToken(TokenKind.Plus,true,"+");
            GrammarToken Minus=new GrammarToken(TokenKind.Minus,true,"-");
            GrammarToken Star=new GrammarToken(TokenKind.Star,true,"*");
            GrammarToken Slash=new GrammarToken(TokenKind.Slash,true,"\\");
            
            
            
            grammarList.Add(new KeyValuePair<GrammarToken,CompositeGrammarToken >(Expression,new CompositeGrammarToken(new List<GrammarToken>{Term})));
            grammarList.Add(new KeyValuePair<GrammarToken,CompositeGrammarToken >(Expression,new CompositeGrammarToken(new List<GrammarToken>{Expression,Add_op,Term})));

            grammarList.Add(new KeyValuePair<GrammarToken,CompositeGrammarToken >(Term,new CompositeGrammarToken(new List<GrammarToken>{Factor})));
            grammarList.Add(new KeyValuePair<GrammarToken,CompositeGrammarToken >(Term,new CompositeGrammarToken(new List<GrammarToken>{Term,Mult_op,Factor})));


            grammarList.Add(new KeyValuePair<GrammarToken,CompositeGrammarToken >(Factor,new CompositeGrammarToken(new List<GrammarToken>{Id})));
            grammarList.Add(new KeyValuePair<GrammarToken,CompositeGrammarToken >(Factor,new CompositeGrammarToken(new List<GrammarToken>{Number})));
            grammarList.Add(new KeyValuePair<GrammarToken,CompositeGrammarToken >(Factor,new CompositeGrammarToken(new List<GrammarToken>{LeftParenthesis,Expression,RightParenthesis})));

            grammarList.Add(new KeyValuePair<GrammarToken,CompositeGrammarToken >(Add_op,new CompositeGrammarToken(new List<GrammarToken>{Plus})));
            grammarList.Add(new KeyValuePair<GrammarToken,CompositeGrammarToken >(Add_op,new CompositeGrammarToken(new List<GrammarToken>{Minus})));
            
            grammarList.Add(new KeyValuePair<GrammarToken,CompositeGrammarToken >(Mult_op,new CompositeGrammarToken(new List<GrammarToken>{Star})));
            grammarList.Add(new KeyValuePair<GrammarToken,CompositeGrammarToken >(Mult_op,new CompositeGrammarToken(new List<GrammarToken>{Slash})));
            
           
           
            Grammar grammar=new Grammar(grammarList);
           
            var states = States.GetStates(Expression,grammar.grammarDictionary);
            Console.WriteLine(States.gl);
            ParsingTable parsingTable=new ParsingTable(grammar);
            parsingTable.createParsingTable(states);
            
            var _action=parsingTable._action;
            var _goto=parsingTable._goto;

            printStates(states);
            printParsingTable(_action,_goto);
              

              
        }
        

        static void printParsingTable(List<Dictionary<GrammarToken,ParseAction>> _action,List<Dictionary<GrammarToken,int>> _goto){
            
            int length=_action.Count;
            
            for (int index = 0; index < length; index++)
            {
                var stateAction=_action.ElementAt(index);
                var stateGoto=_goto.ElementAt(index);
                Console.WriteLine("state action");
                foreach (var key in stateAction.Keys)
                  {
                     Console.WriteLine($"{key.Value} -> {stateAction[key].Action} -{stateAction[key].Value}");
                  }
                Console.WriteLine("state goto");
                foreach (var key in stateGoto.Keys)
                  {
                      Console.WriteLine($"{key.Value} -> {stateGoto[key]}");
                  }


            }
            // foreach (var item in _action)
            //   {
            //       Console.WriteLine("state action");
            //       Console.WriteLine();
            //       foreach (var key in item.Keys)
            //       {
            //           Console.WriteLine($"{key.Value} -> {item[key].Action} -{item[key].Value}");
            //       }
            //   }

            //   foreach (var item in _goto)
            //   {
            //       Console.WriteLine("state goto");
            //       Console.WriteLine();
            //       foreach (var key in item.Keys)
            //       {
            //           Console.WriteLine($"{key.Value} -> {item[key]}");
            //       }
            //   }
        }


        static void printStates(List<States> states){
            foreach (var state in states)
              {
                  Console.WriteLine($"State: {state.StateNo} item: {state.LrItem.toString()} dot: {state.DotPointer} isLeaf: {state.Leaf}");
                  Console.WriteLine();
                  Console.WriteLine("------------ closure ----------");
                  foreach (var closure in state.CanonicalCollections)
                  {
                      Console.WriteLine(closure.Key.toString());
                  }
                  Console.WriteLine();
                  Console.WriteLine("------------ transision ----------");
                  foreach (var transision in state.Transisions)
                  {
                        Console.WriteLine($"goto - {transision.Value.Value} -> {transision.Key}");                      
                  }
                  Console.WriteLine();
              }
        }
    }
}
