using System;
using System.Collections.Generic;

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
            
            GrammarToken S=new GrammarToken(TokenKind.S,false,"S");
            GrammarToken A=new GrammarToken(TokenKind.A,false,"A");
            GrammarToken a=new GrammarToken(TokenKind.a,true,"a");
            GrammarToken b=new GrammarToken(TokenKind.b,true,"b");
            
            grammarList.Add(new KeyValuePair<GrammarToken,CompositeGrammarToken >(S,new CompositeGrammarToken(new List<GrammarToken>{A,A})));
            grammarList.Add(new KeyValuePair<GrammarToken,CompositeGrammarToken >(A,new CompositeGrammarToken(new List<GrammarToken>{a,A})));
            grammarList.Add(new KeyValuePair<GrammarToken,CompositeGrammarToken >(A,new CompositeGrammarToken(new List<GrammarToken>{b})));
           
           
            Grammar grammar=new Grammar(grammarList);

            var states = States.spreadGrammer(S,grammar.grammarDictionary);
            ParsingTable parsingTable=new ParsingTable(grammar);
            parsingTable.createParsingTable(states);
            
            var _action=parsingTable._action;
            var _goto=parsingTable._goto;

            printStates(states);
            printParsingTable(_action,_goto);
              

              
        }

        static void printParsingTable(List<Dictionary<GrammarToken,ParseAction>> _action,List<Dictionary<GrammarToken,int>> _goto){
            foreach (var item in _action)
              {
                  Console.WriteLine("state action");
                  Console.WriteLine();
                  foreach (var key in item.Keys)
                  {
                      Console.WriteLine($"{key.Value} -> {item[key].Action} -{item[key].Value}");
                  }
              }

              foreach (var item in _goto)
              {
                  Console.WriteLine("state goto");
                  Console.WriteLine();
                  foreach (var key in item.Keys)
                  {
                      Console.WriteLine($"{key.Value} -> {item[key]}");
                  }
              }
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
