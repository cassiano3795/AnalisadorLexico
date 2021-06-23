using System.Formats.Asn1;
using sly.lexer;

namespace Analisador.Model
{
    public class WhileStatement : Statement
    {
        public WhileStatement(AST condition, AST blockStmt)
        {
            Condition = condition;
            BlockStmt = blockStmt;
        }

        public AST Condition { get; set; }
        public AST BlockStmt { get; set; }
        public LexerPosition Position { get; set; }
        public AsnWriter.Scope CompilerScope { get; set; }
        public string Dump(string tab)
        {
            throw new System.NotImplementedException();
        }
    }
}