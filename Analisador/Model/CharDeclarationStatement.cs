using System.Collections.Generic;
using System.Formats.Asn1;
using sly.lexer;

namespace Analisador.Model
{
    public class CharDeclarationStatement : Statement
    {
        public CharDeclarationStatement()
        {
            Declarations = new List<AST>();
        }

        public CharDeclarationStatement(AST declaration)
        {
            Declarations = new List<AST>() { declaration };
        }

        public CharDeclarationStatement(List<AST> declarations)
        {
            Declarations = declarations;
        }

        private List<AST> Declarations { get; }

        public LexerPosition Position { get; set; }
        public AsnWriter.Scope CompilerScope { get; set; }
        public string Dump(string tab)
        {
            throw new System.NotImplementedException();
        }

        public AST Get(int i)
        {
            return Declarations[i];
        }

        public void Add(AST statement)
        {
            Declarations.Add(statement);
        }

        public void AddRange(List<AST> stmts)
        {
            Declarations.AddRange(stmts);
        }
    }
}