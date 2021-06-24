using System.Formats.Asn1;
using sly.lexer;

namespace Analisador.Model
{
    public class FuncStatement : Statement
    {
        public LexerPosition Position { get; set; }
        public AsnWriter.Scope CompilerScope { get; set; }
        public string Dump(string tab)
        {
            throw new System.NotImplementedException();
        }
    }
}