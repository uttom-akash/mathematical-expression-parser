using System;
using System.Collections.Generic;
using System.Linq;

namespace lrCalculator{
    public class Print
    {
        public static void PrintIntro(){
            Console.ForegroundColor=ConsoleColor.Blue;
            Console.WriteLine("#s  ->set/unset to show states transision");
            Console.WriteLine("#p  ->set/unset to show parse table");
            Console.WriteLine("#l  ->set/unset to show parse live");
            Console.WriteLine("#t  ->set/unset to show parse tree");
            Console.WriteLine("N.B. Default true for all");
            Console.WriteLine();
            Console.ResetColor();
        }
        public static void PrintSection(string header){
            Console.ForegroundColor=ConsoleColor.Red;
            Console.WriteLine(header);
            Console.WriteLine();
            Console.ResetColor();
        }


        public static void PrintHeader(string header){
            Console.ForegroundColor=ConsoleColor.Blue;
            Console.WriteLine(header);
            Console.ResetColor();
        }

        public static void PrintLine(string line){
            Console.ForegroundColor=ConsoleColor.Gray;
            Console.WriteLine(line);
            Console.ResetColor();
        }

        static string FixedString(string pString){
            string padding="";
            int pLength=5-pString.Length;
            while (pLength>0)
            {
                padding+="";
                pLength--;
            }
            if(pString.Length<5)
                return pString+padding;
            return pString.Substring(0,5);
        }

        internal static void PrintParsingTable(List<Dictionary<TokenKind, ParseAction>> _action, List<Dictionary<TokenKind, int>> _goto){
            Print.PrintSection("-------------> Parse Table <----------");
            
            int length=_action.Count;
            Console.Write("state|");
            foreach (var actionHeader in _action.ElementAt(0))
            {
                Console.Write($"|{FixedString(actionHeader.Key.ToString())}");
            }
            Console.Write("|");

            foreach (var gotoHeader in _goto.ElementAt(0))
            {
                Console.Write($"|{FixedString(gotoHeader.Key.ToString())}");
            }
            Console.WriteLine("|");


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
                  
                Console.Write(" |");
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

        public static void PrintStates(List<States> states){
            PrintSection("-------------> States start <----------");
            foreach (var state in states)
              {
                  PrintHeader($"State: {state.StateNo} Item: {state.LrItem.toString()} Dot_pointer: {state.DotPointer} Is_leaf: {state.Leaf}");
                //   PrintHeader("Closure :");
                //   foreach (var closure in state.CanonicalCollections)
                //   {
                //       Console.WriteLine(closure.Key.toString());
                //   }
                  PrintHeader("Transision :");
                  foreach (var transision in state.Transisions)
                  {
                        Console.WriteLine($" transision - {transision.Value.Kind}->{transision.Key}");                      
                  }
              } 
            
        }

        public static void PrintTree(Syntax root){
            Stack<TreeState> tree=new Stack<TreeState>();
            tree.Push(new TreeState(root,"",false));
            PrintSection("Parse tree:");
            while (tree.Count>0)
            {
                var currentSyntax=tree.Pop();
                
                var current=currentSyntax.Syntax;
                var indent=currentSyntax.Indent;
                
                var childIndent=currentSyntax.IsLast ? indent+"    ":indent+"│   ";
                var marker=currentSyntax.IsLast ? "└──":"├──";
                
                Console.Write($"{indent}{marker}");
                
                Console.ForegroundColor=ConsoleColor.DarkCyan;
                Console.Write(current.Kind);

                if(current.Value!=null)
                    Console.Write($" {current.Value}");
                Console.WriteLine();
                Console.ResetColor();

                switch (current.Kind)
                {
                    // using stack cause we can write to console horizontally
                    // when pushing to the stack 
                    // pushing inverse order of grammar 
                    // cause last item should be printed at last
                    case TokenKind.Expression:{
                        ExpressionSyntax expression=current as ExpressionSyntax;
                        if(expression.AddOp!=null){
                            tree.Push(new TreeState(expression.Expression,childIndent,true));
                            tree.Push(new TreeState(expression.AddOp,childIndent,false));
                            tree.Push(new TreeState(expression.Term,childIndent,false));
                            
                        }else if(expression.Term!=null)
                            tree.Push(new TreeState(expression.Term,childIndent,true));
                        

                    };break;
                    case TokenKind.Term:{
                        TermSyntax term=current as TermSyntax;
                        if(term.Multiply!=null){
                            tree.Push(new TreeState(term.Term,childIndent,true));
                            tree.Push(new TreeState(term.Multiply,childIndent,false));
                            tree.Push(new TreeState(term.Factor,childIndent,false));
                        }
                        else if(term.Factor!=null){
                            tree.Push(new TreeState(term.Factor,childIndent,true));
                        }
                    };break;
                    case TokenKind.Factor:{
                        FactorSyntax factor=current as FactorSyntax;
                        if(factor.Division!=null){
                            tree.Push(new TreeState(factor.Factor,childIndent,true));
                            tree.Push(new TreeState(factor.Division,childIndent,false));
                            tree.Push(new TreeState(factor.Unit,childIndent,false));
                        }
                        else if(factor.Unit!=null){
                            tree.Push(new TreeState(factor.Unit,childIndent,true));
                        }
                    };break;
                    case TokenKind.Unit:{
                        UnitSyntax unit=current as UnitSyntax;
                        
                        if(unit.Number!=null){
                            tree.Push(new TreeState(unit.Number,childIndent,true));
                        }else if(unit.LeftParenthesis!=null){
                            tree.Push(new TreeState(unit.RightParenthesis,childIndent,true));
                            tree.Push(new TreeState(unit.Expression,childIndent,false));
                            tree.Push(new TreeState(unit.LeftParenthesis,childIndent,false));
                        }
                    };break;
                    
                    case TokenKind.Add_op:{
                        AddOpSyntax add=current as AddOpSyntax;
                        tree.Push(new TreeState(add.Operator,childIndent,true));
                        };break;

                    case TokenKind.Mult_op:{
                        MultiOpSyntax multi=current as MultiOpSyntax;
                        tree.Push(new TreeState(multi.Operator,childIndent,true));
                        };break;
                    default:break;
                    
                }
            }

        }

        public static void PrintStack(string header,Stack<Syntax> rememberSyntax, Stack<int> rememberState){

            PrintSection(header);
            PrintHeader("state stack:");
            var states=rememberState.ToList();
            var syntaxes=rememberSyntax.ToList();
            
            states.Reverse();
            syntaxes.Reverse();

            foreach (var item in states)
            {
                Console.Write($"|{item}");           
            }
            Console.WriteLine();
            PrintHeader("syntax stack:");
            foreach (var item in syntaxes)
            {
                Console.Write($"|{item.Kind}");           
            }
            Console.WriteLine();
        }

        public static void PrintAcceptedHeader(){
            Console.ForegroundColor=ConsoleColor.DarkCyan;
            Console.WriteLine("==================");
            Console.WriteLine("=                =");
            Console.WriteLine("=   Accepted     =");
            Console.WriteLine("=                =");
            Console.WriteLine("==================");
            Console.ResetColor();
        }
    }

    class TreeState
        {
            public TreeState(Syntax syntax,string indent,bool isLast)
            {
                Syntax = syntax;
                Indent = indent;
                IsLast = isLast;
            }

            public Syntax Syntax { get; }
            public string Indent { get; }
            public bool IsLast { get; }
        }
}