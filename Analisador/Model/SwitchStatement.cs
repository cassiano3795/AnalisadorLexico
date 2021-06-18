using System;
using System.Collections;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Text;
using sly.lexer;

namespace Analisador.Model
{
    public class SwitchStatement : Statement
    {
        public SwitchStatement(IdentifierStatement id, IList<AST> cases)
        {
            Id = id;
            Cases = cases;
        }

        public IdentifierStatement Id { get; set; }

        public IList<AST> Cases { get; set; }

        public AsnWriter.Scope CompilerScope { get; set; }

        public LexerPosition Position { get; set; }

        public string Dump(string tab)
        {
            throw new NotImplementedException();
        }
    }

    public class CaseStatement : Statement
    {
        public CaseStatement(AST literal, AST statements)
        {
            Literal = literal;
            Statements = statements;
        }

        public AST Literal { get; set; }

        public AST Statements { get; set; }

        public LexerPosition Position { get; set; }
        public AsnWriter.Scope CompilerScope { get; set; }
        public string Dump(string tab)
        {
            throw new NotImplementedException();
        }
    }
}