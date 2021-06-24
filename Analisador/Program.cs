using System;
using System.ComponentModel;
using System.IO;
using Analisador.Lexer;
using sly.parser.generator.visitor;

namespace Analisador
{
    class Program
    {
        static void Main(string[] args)
        {
            var parser = CParser.CParser.GetParser();
            
            var result = parser.Parse("void Print(){ while(a < 4){ if (a == 3){ a++; } } }");

            var currentDirectory = Directory.GetCurrentDirectory();

            if (result.IsOk)
            {
                var tree = result.SyntaxTree;
                var graphviz = new GraphVizEBNFSyntaxTreeVisitor<Tokens>();
                var root = graphviz.VisitTree(tree);
                string graph = graphviz.Graph.Compile();

                var path = Path.Combine(currentDirectory, "tree.dot");

                File.Delete(path);
                File.AppendAllText(path, graph);
            }
            else
            {
                var path = Path.Combine(currentDirectory, "error.txt");

                File.Delete(path);
                File.AppendAllText(path, result.Errors.ToString());
            }

        }
    }
}
