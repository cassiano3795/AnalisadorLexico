using System.Collections.Generic;
using System.Linq;
using Analisador.Lexer;
using Analisador.Model;
using sly.lexer;
using sly.parser;
using sly.parser.generator;
using sly.parser.parser;

namespace Analisador.CParser
{
    public class CParser
    {
        [Production("program: statements")]
        public AST Program(AST block)
        {
            return block;
        }

        [Production("statements: statement statements*")]
        public AST Statements(AST statement, List<AST> statements)
        {
            var seq = new SequenceStatement(statement);

            foreach (var ast in statements)
            {
                if (ast is SequenceStatement sequenceStatement)
                    seq.AddRange(sequenceStatement.Statements);
                else
                    seq.Add(ast);
            }

            return seq;
        }

        [Production("statements: statement")]
        public AST Statements(AST statement)
        {
            return statement;
        }

        [Production("statement: intdeclaration")]
        [Production("statement: chardeclaration")]
        [Production("statement: floatdeclaration")]
        [Production("statement: ifstatement")]
        [Production("statement: assignstatement")]
        //[Production("statement: switchstatement")]
        public AST Statement(AST declaration)
        {
            return declaration;
        }

        [Production("intdeclaration: INT ids SEMI")]
        public AST IntDeclaration(Token<Tokens> intToken, AST statement, Token<Tokens> semiToken)
        {
            IntDeclarationStatement intDeclaration;

            if (statement is SequenceStatement sequenceStatement)
                intDeclaration = new IntDeclarationStatement(sequenceStatement.Statements)
                {
                    Position = intToken.Position
                };
            else
                intDeclaration = new IntDeclarationStatement(statement)
                {
                    Position = intToken.Position
                };

            return intDeclaration;
        }

        [Production("chardeclaration: CHAR ids SEMI")]
        public AST CharDeclaration(Token<Tokens> charToken, AST statement, Token<Tokens> semiToken)
        {
            CharDeclarationStatement charDeclaration;

            if (statement is SequenceStatement sequenceStatement)
                charDeclaration = new CharDeclarationStatement(sequenceStatement.Statements)
                {
                    Position = charToken.Position
                };
            else
                charDeclaration = new CharDeclarationStatement(statement)
                {
                    Position = charToken.Position
                };

            return charDeclaration;
        }

        [Production("floatdeclaration: FLOAT ids SEMI")]
        public AST FloatDeclaration(Token<Tokens> floatToken, AST statement, Token<Tokens> semiToken)
        {
            FloatDeclarationStatement floatDeclaration;

            if (statement is SequenceStatement sequenceStatement)
                floatDeclaration = new FloatDeclarationStatement(sequenceStatement.Statements)
                {
                    Position = floatToken.Position
                };
            else
                floatDeclaration = new FloatDeclarationStatement(statement)
                {
                    Position = floatToken.Position
                };

            return floatDeclaration;
        }

        [Production("ifstatement: IF LPAREN CParser_expressions RPAREN LBRACKET statements RBRACKET elsestatement?")]
        public AST IfStatement(Token<Tokens> ifToken, Token<Tokens> lparenToken, Expression condition, Token<Tokens> rparenToken, Token<Tokens> lbracketToken, AST thenStatement, Token<Tokens> rbracketToken, ValueOption<AST> elseStatement)
        {
            IfStatement ifStatement;

            var elseStm = elseStatement.Match(ast => ast, () => null);

            ifStatement = new IfStatement(condition, thenStatement, elseStm);

            return ifStatement;
        }

        [Production("elsestatement: ELSE ifstatement")]
        public AST ElseStatement(Token<Tokens> elseToken, AST ifStatement)
        {
            return ifStatement;
        }

        [Production("elsestatement: ELSE LBRACKET statements RBRACKET")]
        public AST ElseStatement(Token<Tokens> elseToken, Token<Tokens> lbracketToken, AST statements, Token<Tokens> rbracketToken)
        {
            return statements;
        }

        [Production("ids: ID")]
        public AST IdDeclaration(Token<Tokens> idToken)
        {
            return new IdentifierStatement(idToken.StringWithoutQuotes)
            {
                Position = idToken.Position
            };
        }

        [Production("ids: ID COMMA ids*")]
        public AST IdDeclaration(Token<Tokens> idToken, Token<Tokens> commaToken, List<AST> ids)
        {
            var identifier = new IdentifierStatement(idToken.StringWithoutQuotes)
            {
                Position = idToken.Position
            };

            var sequence = new SequenceStatement(identifier);

            sequence.AddRange(ids);

            return sequence;
        }

        [Production("assignstatement: ID ASSIGN expression SEMI")]
        public AST AssignStatement(Token<Tokens> idToken, Token<Tokens> assignToken, Expression operand, Token<Tokens> semiToken)
        {
            return new AssignStatement(idToken.StringWithoutQuotes, operand)
            {
                Position = idToken.Position
            };
        }

        [Production("expression: operand")]
        public AST Expression(Expression operand)
        {
            return operand;
        }

        [Operand]
        [Production("operand: literal")]
        public AST Operand(AST literal)
        {
            return literal;
        }


        [Production("literal: INTEGER")]
        public AST LiteralInteger(Token<Tokens> integerToken)
        {
            return new IntegerConstant(integerToken.IntValue)
            {
                Position = integerToken.Position
            };
        }

        [Production("literal: STRING")]
        public AST LiteralString(Token<Tokens> stringToken)
        {
            return new StringConstant(stringToken.StringWithoutQuotes)
            {
                Position = stringToken.Position
            };
        }

        [Production("literal: FLOATNUMBER")]
        public AST LiteralFloat(Token<Tokens> floatToken)
        {
            return new FloatConstant(double.Parse(floatToken.StringWithoutQuotes.Replace(",", "").Replace(".", ",")))
            {
                Position = floatToken.Position
            };
        }


        [Operation((int)Tokens.LESSER, Affix.InFix, Associativity.Right, 50)]
        [Operation((int)Tokens.LESSEROREQUAL, Affix.InFix, Associativity.Right, 50)]
        [Operation((int)Tokens.GREATER, Affix.InFix, Associativity.Right, 50)]
        [Operation((int)Tokens.GREATEROREQUAL, Affix.InFix, Associativity.Right, 50)]
        [Operation((int)Tokens.EQUALS, Affix.InFix, Associativity.Right, 50)]
        [Operation((int)Tokens.DIFFERENT, Affix.InFix, Associativity.Right, 50)]
        public AST BinaryComparisonExpression(AST left, Token<Tokens> operatorToken,
            AST right)
        {
            var oper = operatorToken.TokenID switch
            {
                Tokens.LESSER => BinaryOperator.LESSER,
                Tokens.LESSEROREQUAL => BinaryOperator.LESSEROREQUAL,
                Tokens.GREATER => BinaryOperator.GREATER,
                Tokens.GREATEROREQUAL => BinaryOperator.GREATEROREQUAL,
                Tokens.EQUALS => BinaryOperator.EQUALS,
                Tokens.DIFFERENT => BinaryOperator.DIFFERENT,
                _ => BinaryOperator.ADD
            };

            var operation = new BinaryOperation(left as Expression, oper, right as Expression);
            return operation;
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