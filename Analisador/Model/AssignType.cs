using System.Formats.Asn1;
using Analisador.Lexer;
using sly.lexer;

namespace Analisador.Model
{
    public class AssignType : AST
    {
        public AssignType(Token<Tokens> token)
        {
            Token = token;
        }

        public Token<Tokens> Token;

        public LexerPosition Position { get; set; }
        public AsnWriter.Scope CompilerScope { get; set; }
        public string Dump(string tab)
        {
            throw new System.NotImplementedException();
        }
    }
}