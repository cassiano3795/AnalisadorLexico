using System;
using System.Formats.Asn1;
using sly.lexer;

namespace Analisador.Model
{
    public interface AST
    {
        LexerPosition Position { get; set; }

        AsnWriter.Scope CompilerScope { get; set; }
        string Dump(string tab);
    }
}