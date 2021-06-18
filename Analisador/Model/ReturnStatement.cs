using System.Formats.Asn1;
using sly.lexer;

namespace Analisador.Model
{
    public class ReturnStatement : Statement
    {
        public AST? Operand { get; set; }

        public ReturnStatement(AST? operand = null)
        {
            if (operand != null)
                Operand = operand;
        }

        public LexerPosition Position { get; set; }
        public AsnWriter.Scope CompilerScope { get; set; }
        public string Dump(string tab)
        {
            throw new System.NotImplementedException();
        }
    }
}