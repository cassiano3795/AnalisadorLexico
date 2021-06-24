using System.Formats.Asn1;
using sly.lexer;

namespace Analisador.Model
{
    public class ForStatement : Statement
    {
        public ForStatement(AssignStatement assignStatement, AST condition, AST increment, AST statements)
        {
            AssignStatement = assignStatement;
            Condition = condition;
            Increment = increment;
            Statements = statements;
        }

        public AssignStatement AssignStatement { get; set; }
        public AST Condition { get; set; }
        public AST Increment { get; set; }
        public AST Statements { get; set; }
        public LexerPosition Position { get; set; }
        public AsnWriter.Scope CompilerScope { get; set; }
        public string Dump(string tab)
        {
            throw new System.NotImplementedException();
        }
    }
}