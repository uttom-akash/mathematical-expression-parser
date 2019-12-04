using System;
using System.Collections.Generic;
using System.Linq;

namespace lrCalculator{
    class TreeGenerator
    {
        int position;
        int length=0;
         Stack<Syntax> rememberSyntax=new Stack<Syntax>();
        Stack<int> rememberState=new Stack<int>();

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


        private SyntaxToken Peek(int offset){
            if(position+offset<length)
                return Tokens.ElementAt(position+offset);
            return new SyntaxToken(TokenKind.Invalid,null);
        }

        private SyntaxToken CurrentToken=>Peek(0);
        private void Next()=>position++;
        
        public Syntax generateTree(){
            var actionTable=ParsingTable._action;
            var gotoTable=ParsingTable._goto;

            rememberState.Push(0);

            while (position<length)
            {

                var token=CurrentToken;    
                var currentState=rememberState.Peek();
                var action=actionTable[currentState][token.Kind];
    
                if(ShowStats.showParsingLive){
                    Print.PrintStack($"position :{position} token:{token.Kind} state:{currentState}",rememberSyntax,rememberState);
                }
                switch (action.Action)
                {
                    case 's': ShiftAction(token,action.Value);break;
                    case 'r': ReduceAction(token,action.Value,gotoTable);break;
                    case 'a': AcceptedAction();return rememberSyntax.Pop();
                    default: Error() ;return rememberSyntax.Pop();
                }
            }
            return rememberSyntax.Pop();

        }

        

        public void ShiftAction(SyntaxToken token,int state){
            if(ShowStats.showParsingLive){
                Print.PrintHeader("shift:");
            }

            rememberSyntax.Push(new Syntax(token.Kind,token.Value));
            rememberState.Push(state);
            Next();
        }
    

        private void ReduceAction(SyntaxToken token, int production, List<Dictionary<TokenKind, int>> gotoTable)
        {
            if(ShowStats.showParsingLive){
                Print.PrintHeader("reduce:");
            }

            if(production<0)
            {
                Error();
                return;
            }

            var grammar=Grammar.GrammarList.ElementAt(production);
            var rightHandSide=grammar.RightHandSide.Length;
            var leftHandSide=grammar.LeftHandSide;

            if(leftHandSide.Kind==TokenKind.Accepted)
                {
                    AcceptedAction();
                    return ;
                }
            

            List<Syntax> reducableList=new List<Syntax>();
            while (rightHandSide>0)
            {
                rememberState.Pop();
                var reducable=rememberSyntax.Pop();
                reducableList.Add(reducable);
                
                rightHandSide--;
            }
            if(ShowStats.showParsingLive){
                Print.PrintLine($"{leftHandSide.Kind} ==> {grammar.RightHandSide.toString()}");
            }
            rememberSyntax.Push(Reduce(leftHandSide,reducableList));    
            var previuosState=rememberState.Peek();
            rememberState.Push(gotoTable[previuosState][leftHandSide.Kind]);
        }

        public Syntax Reduce(SyntaxToken token,List<Syntax> reducableList){
            reducableList.Reverse();
            var reducerIterator=reducableList.AsEnumerable().GetEnumerator();    
            
            reducerIterator.MoveNext();
            switch (token.Kind)
            {
                case TokenKind.Add_op:return new AddOpSyntax(reducerIterator.Current);
                case TokenKind.Mult_op :return new MultiOpSyntax(reducerIterator.Current);
                case TokenKind.Factor:{ 
                    var l=reducerIterator.Current;reducerIterator.MoveNext();
                    var m=reducerIterator.Current;reducerIterator.MoveNext();
                    var r=reducerIterator.Current;
                    if(m==null)
                        return new FactorSyntax(l);     
                    return new FactorSyntax(l,m,r);
                };
                case TokenKind.Term: {
                    var l=reducerIterator.Current;reducerIterator.MoveNext();
                    var m=reducerIterator.Current;reducerIterator.MoveNext();
                    var r=reducerIterator.Current;
                   
                    return new TermSyntax(l,m,r);
                };
                case TokenKind.Expression:{
                    var l=reducerIterator.Current;reducerIterator.MoveNext();
                    var m=reducerIterator.Current;reducerIterator.MoveNext();
                    var r=reducerIterator.Current;
                    return new ExpressionSyntax(l,m,r);
                };
                default: {
                    Error();
                    return new Syntax(TokenKind.Invalid);
                }
            }
        }

        public void AcceptedAction(){
            Print.PrintAcceptedHeader();
            Next();
        }

        public void Error(){
            Console.WriteLine(" ________________");
            Console.WriteLine("|                │");
            Console.WriteLine("|     Error      │");
            Console.WriteLine("|________________|");
        }


        

        
        
    }
}