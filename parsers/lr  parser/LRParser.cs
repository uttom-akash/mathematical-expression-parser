using System;
using System.Collections.Generic;
using System.Linq;

namespace lrCalculator{
    public class LRParser{
        
        static Grammar grammar;
        static ParsingTable parseTable;
        static List<States> statesTransision;

        public static void Run(string text)
        {
            Init();

            Lexer lexer=new Lexer(text);
            List<SyntaxToken> tokens=lexer.Lex();
            tokens.Add(new SyntaxToken(TokenKind.Dollar,'$'));

            TreeGenerator treeGenerator=new TreeGenerator(parseTable,tokens,grammar);
            var tree=treeGenerator.generateTree();
            
            Print.PrintHeader("Result");
            Console.WriteLine(Evaluator.Evaluate(tree));

            if(ShowStats.showParseTree)
                Print.PrintTree(tree);
            
            

            Console.WriteLine("\n         End           \n\n");
        }



        public static void Init(){
            SetGrammar();
            SetStates();
            CreateParseTable();            
            
            if(ShowStats.showStateTransision)
                Print.PrintStates(statesTransision);
            if(ShowStats.showParseTable)
                Print.PrintParsingTable(parseTable._action,parseTable._goto);
        }


        
        public static void SetGrammar(){
            if(grammar==null)
                grammar=new Grammar();
        }
        
        public static void SetStates(){
            if(statesTransision==null)
                statesTransision=new List<States>();
            else 
                return ;
            statesTransision=States.GetStates(grammar.Start,grammar.GrammarDictionary);
        }

        public static void CreateParseTable(){
            if(parseTable==null)
                parseTable=new ParsingTable(grammar);
            else 
                return;
            parseTable.createParsingTable(statesTransision);
        }
    }
}