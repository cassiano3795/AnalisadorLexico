using System.Collections.Generic;
using System.Linq;
using Analisador.Lexer;
using Analisador.Model;
using sly.lexer;
using sly.parser;
using sly.parser.generator;

namespace Analisador.CParser
{
    public class CParser
    {
        //[Production("program: ")]
        //public void ProgramEmpty()
        //{

        //}

        [Production("program: block")]
        public AST Program(AST block)
        {
            return block;
        }

        [Production("block: statements")]
        public AST Block(AST statements)
        {
            return statements;
        }

        [Production("statements: statement statements*")]
        [Production("statements: statement")]
        public AST Statements(AST statement, List<AST> statements)
        {
            var seq = new SequenceStatement(statement);

            seq.AddRange(statements);

            return seq;
        }

        [Production("statements: statement")]
        public AST Statements(AST statement)
        {
            return statement;
        }

        [Production("statement: intdeclaration")]
        public AST Statement(AST intDeclaration)
        {
            return intDeclaration;
        }

        [Production("intdeclaration: INT ids SEMI")]
        public AST IntDeclaration(Token<Tokens> intToken, AST identifierStatement, Token<Tokens> semiToken)
        {
            return new IntDeclarationStatement(identifierStatement);
        }

        [Production("ids: ID")]
        public AST IdDeclaration(Token<Tokens> idToken)
        {
            return new IdentifierStatement(idToken.StringWithoutQuotes);
        }

        public static Parser<Tokens, AST> GetParser()
        {
            var parserInstance = new CParser();
            var builder = new ParserBuilder<Tokens, AST>();
            var buildResult = builder.BuildParser(parserInstance, ParserType.EBNF_LL_RECURSIVE_DESCENT, "program");

            if (buildResult.IsOk)
                return buildResult.Result;

            return null;
        }
    }
}