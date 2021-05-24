﻿using System.Formats.Asn1;
using sly.lexer;

namespace Analisador.Model
{
    public class IdentifierStatement : Statement
    {
        public IdentifierStatement(string name)
        {
            VariableName = name;
        }

        public string VariableName { get; set; }

        public Expression Value { get; set; }

        public LexerPosition Position { get; set; }
        public AsnWriter.Scope CompilerScope { get; set; }
        public string Dump(string tab)
        {
            throw new System.NotImplementedException();
        }
    }
}