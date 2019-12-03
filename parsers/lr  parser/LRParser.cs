using System;
using System.Collections.Generic;
using System.Linq;

namespace lrCalculator{
    public class LRParser{
        public static void Run()
        {

            Grammar grammar=new Grammar();
            ParsingTable parsingTable=new ParsingTable(grammar);
            
            var states = States.GetStates(grammar.Start,grammar.GrammarDictionary);

            Console.WriteLine(States.gl);
            
            parsingTable.createParsingTable(states);
            
            printStates(states);
            printParsingTable(parsingTable._action,parsingTable._goto);
            
            List<SyntaxToken> tokens=new List<SyntaxToken>{new SyntaxToken(TokenKind.Number,10),new SyntaxToken(TokenKind.Plus,'+'),new SyntaxToken(TokenKind.Number,10),new SyntaxToken(TokenKind.Star,'*'),new SyntaxToken(TokenKind.Number,20),new SyntaxToken(TokenKind.Dollar,'$')};
            TreeGenerator treeGenerator=new TreeGenerator(parsingTable,tokens,grammar);
           treeGenerator.generateTree();   
        }
        


        static void printParsingTable(List<Dictionary<TokenKind,ParseAction>> _action,List<Dictionary<TokenKind,int>> _goto){
            
            int length=_action.Count;
            
            Console.Write("   ");
            foreach (var actionHeader in _action.ElementAt(0))
            {
                Console.Write($"| {actionHeader.Key.ToString()} ");
            }
            Console.Write("  ||  ");

            foreach (var gotoHeader in _goto.ElementAt(0))
            {
                Console.Write($"| {gotoHeader.Key.ToString()} ");
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