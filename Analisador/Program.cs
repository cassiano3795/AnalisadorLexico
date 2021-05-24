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
            var result = parser.Parse("int _8; int a;");

            var tree = result.SyntaxTree;
            var graphviz = new GraphVizEBNFSyntaxTreeVisitor<Tokens>();
            var root = graphviz.VisitTree(tree);
            string graph = graphviz.Graph.Compile();
            File.Delete("c:\\temp\\tree.dot");
            File.AppendAllText("c:\\temp\\tree.dot", graph);
        }
    }
}
