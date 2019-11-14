/*** Compiler Front-End Test automatically generated by the BNF Converter ***/
/*                                                                          */
/* This test will parse a file, print the abstract syntax tree, and then    */
/* pretty-print the result.                                                 */
/*                                                                          */
/****************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OCL.Absyn;

namespace OCL
{
    public class Test
    {
        public static ArrayList ScanFile(string file)
        {
            Stream stream = File.OpenRead(file);
            Scanner scanner = Scanner.CreateScanner(stream);
            Parser parser = new Parser(scanner);
            try
            {
                OCLfile parse_tree = parser.ParseOCLfile();
                return AspectPrinter.Print(parse_tree);
            }
            catch (Exception e)
            {
                Console.Out.WriteLine("Parse NOT Successful:");
                Console.Out.WriteLine(e.Message);
                Console.Out.WriteLine("");
                Console.Out.WriteLine("Stack Trace:");
                Console.Out.WriteLine(e.StackTrace);
                return null;
            }
        }
        public static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                Stream stream = File.OpenRead(args[0]);
                /* The default entry point is used. For other options see class Parser */
                Scanner scanner = Scanner.CreateScanner(stream);
                Parser parser = new Parser(scanner);
                // Uncomment to enable trace information:
                // parser.Trace shows what the parser is doing
                // parser.Trace = true;
                // scanner.Trace prints the tokens as they are parsed, one token per line
                // scanner.Trace = true;
                // parser.scanner = scanner;

                try
                {
                    OCLfile parse_tree = parser.ParseOCLfile();
                    if (parse_tree != null)
                    {
                        Console.Out.WriteLine("Parse Successful!");
                        Console.Out.WriteLine("");
                        Console.Out.WriteLine("[Abstract Syntax]");
                        Console.Out.WriteLine("{0}", PrettyPrinter.Show(parse_tree));
                        Console.Out.WriteLine("");
                        Console.Out.WriteLine("[Linearized Tree]");
                        Console.Out.WriteLine("{0}", PrettyPrinter.Print(parse_tree));
                        Console.Out.WriteLine("");
                        Console.Out.WriteLine("[OCL Objects List]");
                        ArrayList array = AspectPrinter.Print(parse_tree);
                        foreach (Aspect a in array)
                            a.Print();
                        List<int> l = new List<int>() { 1, 2, 3, 4, 5, 6 };
                        l.TrueForAll(x => x + 1 > 2);
                    }
                    else
                    {
                        Console.Out.WriteLine("Parse NOT Successful!");
                    }
                }
                catch (Exception e)
                {
                    Console.Out.WriteLine("Parse NOT Successful:");
                    Console.Out.WriteLine(e.Message);
                    Console.Out.WriteLine("");
                    Console.Out.WriteLine("Stack Trace:");
                    Console.Out.WriteLine(e.StackTrace);
                }
            }
            else
            {
                Console.Out.WriteLine("You must specify a filename!");
            }
        }
    }
}
