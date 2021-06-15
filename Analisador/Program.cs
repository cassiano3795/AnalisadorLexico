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

            var result = parser.Parse("switch (a) { case 1: int a; return a; case 2: int b; break; default: char c; break; }");

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
