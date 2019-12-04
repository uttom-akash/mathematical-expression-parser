using System;

namespace lrCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
                Print.PrintIntro();

                while (true)
                {
                    Console.ForegroundColor=ConsoleColor.DarkCyan;
                    Console.Write(" ► ");
                    Console.ResetColor();

                    string text=Console.ReadLine();
                    if(text.StartsWith("#"))
                    {
                        char cmd='a';
                        if(text.Length>=2)
                            cmd=text[1];

                        switch (cmd)
                        {
                            case 's':ShowStats.showStateTransision=!ShowStats.showStateTransision;break;
                            case 'p':ShowStats.showParseTable=!ShowStats.showParseTable;break;
                            case 'l':ShowStats.showParsingLive=!ShowStats.showParsingLive;break;
                            case 't':ShowStats.showParseTree=!ShowStats.showParseTree;break;
                            default: Console.WriteLine($"invalid input {text}");break;
                        }
                    }else if(text.Equals("exit")){
                        break;
                    }
                    else
                    {
                        LRParser.Run(text);
                    }
                }              
        }

    }
}
