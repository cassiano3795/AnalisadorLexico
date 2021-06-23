using System.Formats.Asn1;
using sly.lexer;
using sly.parser.generator;

namespace Analisador.Model
{
    public class IncrementStatement : Statement
    {
        public IncrementStatement(IdentifierStatement identifier)
        {
            AffixIncrement = Affix.PreFix;
            Identifier = identifier;
        }

        public Affix AffixIncrement { get; }
        public IdentifierStatement Identifier { get; set; }

        public LexerPosition Position { get; set; }
        public AsnWriter.Scope CompilerScope { get; set; }
        public string Dump(string tab)
        {
            throw new System.NotImplementedException();
        }
    }
}