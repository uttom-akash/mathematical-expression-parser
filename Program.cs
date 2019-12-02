using System;
using System.Collections.Generic;
using System.Linq;

namespace lrCalculator
{
    class Program
    {
        static void Main(string[] args)
        {

            Grammar grammar=new Grammar();
            ParsingTable parsingTable=new ParsingTable(grammar);
            
            var states = States.GetStates(grammar.Start,grammar.GrammarDictionary);
            
            Console.WriteLine(States.gl);
            
            parsingTable.createParsingTable(states);
            
            printStates(states);
            printParsingTable(parsingTable._action,parsingTable._goto);
              
        }
        


        static void printParsingTable(List<Dictionary<GrammarToken,ParseAction>> _action,List<Dictionary<GrammarToken,int>> _goto){
            
            int length=_action.Count;
            
            Console.Write("   ");
            foreach (var actionHeader in _action.ElementAt(0))
            {
                Console.Write($"| {actionHeader.Key.Value} ");
            }
            Console.Write("  ||  ");

            foreach (var gotoHeader in _goto.ElementAt(0))
            {
                Console.Write($"| {gotoHeader.Key.Value} ");
            }
            Console.WriteLine("| ");


            for (int index = 0; index < length; index++)
            {
                var stateAction=_action.ElementAt(index);
                var stateGoto=_goto.ElementAt(index);
                var space=index<10 ? " ":"";
                Console.Write($" {space}{index} ");
                foreach (var key in stateAction.Keys)
                  {
                     int value=stateAction[key].Value; 
                     string padding=" ";
                     if(value<0 || value>9)padding="";

                     Console.Write($"| {stateAction[key].Action}{padding}{value} ");
                  }
                  
                Console.Write("  |");
                foreach (var key in stateGoto.Keys)
                  {
                    int value=stateGoto[key]; 
                     string padding=" ";
                     if(value<0 || value>9)padding="";

                      Console.Write($"| {padding}{value} ");
                  }
                Console.WriteLine("|");


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
