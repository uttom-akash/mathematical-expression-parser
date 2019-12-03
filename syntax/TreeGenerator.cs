using System;
using System.Collections.Generic;
using System.Linq;

namespace lrCalculator{
    class TreeGenerator
    {
        int position;
        int length=0;
        public TreeGenerator(ParsingTable parsingTable,List<SyntaxToken> tokens,Grammar grammar)
        {
            ParsingTable = parsingTable;
            Tokens = tokens;
            Grammar = grammar;
            
            position =0;
            length=Tokens.Count;

        }

        private ParsingTable ParsingTable { get; }
        private List<SyntaxToken> Tokens { get; }
        public Grammar Grammar { get; }

        Stack<SyntaxToken> rememberSyntax=new Stack<SyntaxToken>();
        Stack<int> rememberState=new Stack<int>();


        private SyntaxToken Peek(int offset){
            if(position+offset<length)
                return Tokens.ElementAt(position+offset);
            return new SyntaxToken(TokenKind.Invalid);
        }
        private SyntaxToken CurrentToken=>Peek(0);
        private void Next()=>position++;
        public void generateTree(){
            var actionTable=ParsingTable._action;
            var gotoTable=ParsingTable._goto;

            rememberState.Push(0);

            while (position<length)
            {

                var token=CurrentToken;    
                var currentState=rememberState.Peek();
                var action=actionTable[currentState][token.Kind];
                Console.WriteLine("--------------------------------------------------------------------------");
                Console.WriteLine($"position :{position} token:{token.Kind} state:{currentState} ");
                printStack();



                switch (action.Action)
                {
                    case 's': ShiftAction(token,action.Value);break;
                    case 'r': ReduceAction(token,action.Value,gotoTable);break;
                    case 'a': AcceptedAction();break;
                    default: Error() ;break;
                }
            }

        }

        

        public void ShiftAction(SyntaxToken token,int state){
            Console.WriteLine("Shift");

            rememberSyntax.Push(token);
            rememberState.Push(state);
            Next();
        }

        private void ReduceAction(SyntaxToken token, int production, List<Dictionary<TokenKind, int>> gotoTable)
        {
            Console.WriteLine("Reduce");
            
            if(production<0)
            {
                Error();
                return;
            }

            var grammar=Grammar.GrammarList.ElementAt(production);
            var rightHandSide=grammar.RightHandSide.Length;
            var leftHandSide=grammar.LeftHandSide;

            while (rightHandSide>0)
            {
                rememberState.Pop();
                rememberSyntax.Pop();
                rightHandSide--;
            }
            Console.WriteLine($"{leftHandSide.Kind} ==> {grammar.RightHandSide.toString()}");
            rememberSyntax.Push(new Syntax(leftHandSide.Kind));
            if(leftHandSide.Kind==TokenKind.Accepted)
                AcceptedAction();
            var previuosState=rememberState.Peek();
            rememberState.Push(gotoTable[previuosState][leftHandSide.Kind]);
        }

        public void AcceptedAction(){
            Console.WriteLine("===================");
            Console.WriteLine("||                ||");
            Console.WriteLine("||  Accepted      ||");
            Console.WriteLine("||                ||");
            Console.WriteLine("===================");
            Next();
        }

        public void Error(){
            Console.WriteLine("===================");
            Console.WriteLine("||                ||");
            Console.WriteLine("||  Error      ||");
            Console.WriteLine("||                ||");
            Console.WriteLine("===================");
        }


        public void printStack(){

            Console.WriteLine("========");
            foreach (var item in rememberState.ToList())
            {
                Console.WriteLine($"|{item}|");           
            }

            Console.WriteLine("========");
            foreach (var item in rememberSyntax.ToList())
            {
                Console.WriteLine($"|{item.Kind}|");           
            }
            Console.WriteLine("========");
        }
        
    }
}